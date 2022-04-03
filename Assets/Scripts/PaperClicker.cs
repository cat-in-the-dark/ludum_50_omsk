using UnityEngine;

public class PaperClicker : MonoBehaviour
{
    private Camera cam;
    private int layerMask;

    [SerializeField] private PaperManager paperManager;
    [SerializeField] private Coffee coffee;
    [SerializeField] private LampControl lamp;
    [SerializeField] private Phone phone;
    [SerializeField] private Pencil pencil;
    [SerializeField] private Rotator logo;

    private bool canInteract;

    private void Start()
    {
        cam = Camera.main;
        layerMask = 1 << LayerMask.NameToLayer("Clickable");
    }

    private void HandleClick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.CompareTag("Paper"))
                {
                    paperManager.AppendText();
                }
                
                if (hit.collider.CompareTag("Coffee"))
                {
                    coffee.Drink();
                }
                
                if (hit.collider.CompareTag("Lamp"))
                {
                    lamp.TurnOn();
                }
                
                if (hit.collider.CompareTag("Phone"))
                {
                    phone.TouchPhone();
                }
                
                if (hit.collider.CompareTag("Pencil"))
                {
                    pencil.ActWith();
                }

                if (hit.collider.CompareTag("Logo"))
                {
                    logo.ActWith();
                }
            }
        }
    }
    
    private void Update()
    {
        HandleClick();
    }
}
