using UnityEngine;

public class LampControl : MonoBehaviour
{
    [SerializeField] private GameObject lightObj;
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
        state.inHand = State.HandObjects.LAMP;
        timeAliveSec = 0;
    }

    private void Update()
    {
        if (timeAliveSec >= state.disableLightAfter)
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