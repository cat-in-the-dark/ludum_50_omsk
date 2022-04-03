using DG.Tweening;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    private int layerMask;
    private Camera cam;
    private State state;

    private Vector2 centre;

    private Vector3 initPos;
    private Vector3 initRot;

    private bool isOnTable;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Table");
        cam = Camera.main;
        state = State.Find();

        centre = new Vector2(cam.pixelWidth, cam.pixelHeight) * 0.5f;

        initPos = transform.position;
        initRot = transform.rotation.eulerAngles;
        isOnTable = true;
    }

    private void Drop()
    {
        if (isOnTable) return;
        gameObject.layer = LayerMask.NameToLayer("Clickable");
        transform.DOMove(initPos, 1f);
        transform.DORotate(initRot, 0.8f);

        isOnTable = true;
    }

    public void ActWith()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        state.inHand = State.HandObjects.PENCIL;
        isOnTable = false;
    }

    private void Update()
    {
        if (state.inHand != State.HandObjects.PENCIL)
        {
            Drop();
            return;
        }

        if (state.energyLevel <= state.minEnergyLevel)
        {
            Drop();
            return;
        }
        
        var ray = cam.ScreenPointToRay(centre);
        if (Physics.Raycast(ray, out var hit, 100, layerMask))
        {
            var dir = (hit.point - cam.transform.position).normalized;
            var tr = transform;
            tr.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(-10, -30, 0);
            tr.position = hit.point;
        }
    }
}