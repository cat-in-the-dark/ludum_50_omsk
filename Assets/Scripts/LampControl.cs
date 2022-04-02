using UnityEngine;

public class LampControl : MonoBehaviour
{
    [SerializeField] private GameObject lightObj;
    [SerializeField] private float disableAfterSec;
    [SerializeField] private float timeAliveSec;

    private State state;

    private void Start()
    {
        state = State.Find();
        lightObj.SetActive(true);
        state.isLampEnabled = true;
    }

    public void TurnOn()
    {
        timeAliveSec = 0;
    }

    private void Update()
    {
        if (timeAliveSec >= disableAfterSec)
        {
            state.isLampEnabled = false;
            lightObj.SetActive(false);
        }
        else
        {
            timeAliveSec += Time.deltaTime;
            state.isLampEnabled = true;
            lightObj.SetActive(true);
        }
    }
}