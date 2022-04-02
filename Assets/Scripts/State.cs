using System;
using UnityEngine;

public class State : MonoBehaviour
{
    public float typingSpeed;
    public float coffeePower;
    public float workCost;
    public float energyLevel;

    public static State Find()
    {
        var go = GameObject.FindWithTag("Logic");
        return go.GetComponent<State>();
    }
}