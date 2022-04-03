using UnityEngine;

public class PaperClicker : MonoBehaviour
{
    private Camera cam;
    private int layerMask;
    
    [SerializeField] private Paper paper;
    [SerializeField] private Coffee coffee;
    [SerializeField] private LampControl lamp;
    [SerializeField] private Phone phone;
    [SerializeField] private Pencil pencil;
    
    private State state;

    private bool canInteract;

    private void Start()
    {
        state = State.Find();
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
                    paper.AppendText();
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
            }
        }
    }
    
    private void Update()
    {
        HandleClick();
    }
}
