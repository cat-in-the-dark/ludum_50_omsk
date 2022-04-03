using UnityEngine;

public class PaperClicker : MonoBehaviour
{
    private Camera cam;
    private int layerMask;
    
    [SerializeField] private Paper paper;
    [SerializeField] private Coffee coffee;
    [SerializeField] private LampControl lamp;
    [SerializeField] private Phone phone;
    
    private State state;

    private bool canInteract;

    private void Start()
    {
        state = State.Find();
        cam = Camera.main;
        layerMask = 1 << LayerMask.NameToLayer("Clickable");
    }

    private void OnPaperClick()
    {
        paper.AppendText();
    }

    private void OnCoffeeClick()
    {
        coffee.Drink();
    }

    private void OnLampClick()
    {
        lamp.TurnOn();
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
                    OnPaperClick();
                }
                
                if (hit.collider.CompareTag("Coffee"))
                {
                    OnCoffeeClick();
                }
                
                if (hit.collider.CompareTag("Lamp"))
                {
                    OnLampClick();
                }
                
                if (hit.collider.CompareTag("Phone"))
                {
                    phone.TouchPhone();
                }
            }
        }
    }
    
    private void Update()
    {
        HandleClick();
    }
}
