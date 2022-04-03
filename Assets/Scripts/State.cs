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

    public float currentProgress;

    public float baseTypingSpeed;
    public float coffeePower;
    public float workCost;
    public float energyLevel;
    public bool isLampEnabled;
    public bool videoIsOn;

    public float minEnergyLevel;
    public float maxEnergyLevel;

    public float minMouseSens;
    public float maxMouseSens;

    public float turnVideoOffAfter;
    public float disableLightAfter;

    public HandObjects inHand;

    public float LookSensitivity => Mathf.Lerp(minMouseSens,maxMouseSens,Mathf.InverseLerp(minEnergyLevel, maxEnergyLevel, energyLevel));

    public float typingSpeed {
        get
        {
            float mul = 1;
            if (videoIsOn)
            {
                mul *= 2.0f;
            }
            
            if (energyLevel > 0)
            {
                mul *= Mathf.Pow(1.5f, energyLevel);    
            }
            if (energyLevel < 0f)
            {
                mul *= Mathf.Pow(8.0f, energyLevel);
            }
            return baseTypingSpeed * mul;    
        }
    }
    
    public static State Find()
    {
        var go = GameObject.FindWithTag("Logic");
        return go.GetComponent<State>();
    }
}