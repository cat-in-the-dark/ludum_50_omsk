using UnityEngine;

public class PaperClicker : MonoBehaviour
{
    private Camera cam;
    private int layerMask;
    
    [SerializeField] private Paper paper;
    [SerializeField] private Coffee coffee;

    private State state;

    private bool canInteract;

    private void Start()
    {
        state = State.Find();
        cam = Camera.main;
        layerMask = 1 << LayerMask.NameToLayer("Clickable");
        coffee.OnFinishDrinking += OnDrink;
    }

    private void OnPaperClick()
    {
        paper.AppendText();
    }

    private void OnCoffeeClick()
    {
        coffee.Drink();
    }

    private void OnDrink()
    {
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
            }
        }
    }
    
    private void Update()
    {
        HandleClick();

        
    }
}
