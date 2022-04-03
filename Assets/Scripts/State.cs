using UnityEngine;

public class State : MonoBehaviour
{
    public enum HandObjects
    {
        PENCIL,
        CUP,
        PHONE,
        LAMP,
        NONE
    }
    
    
    public float typingSpeed;
    public float coffeePower;
    public float workCost;
    public float energyLevel;
    public bool isLampEnabled;

    public float minEnergyLevel;
    public float maxEnergyLevel;

    public float minMouseSens;
    public float maxMouseSens;

    public HandObjects inHand;
    
    public float LookSensitivity => Mathf.Lerp(minMouseSens,maxMouseSens,Mathf.InverseLerp(minEnergyLevel, maxEnergyLevel, energyLevel));

    public static State Find()
    {
        var go = GameObject.FindWithTag("Logic");
        return go.GetComponent<State>();
    }
}