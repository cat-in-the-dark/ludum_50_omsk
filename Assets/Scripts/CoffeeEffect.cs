using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class CoffeeEffect : MonoBehaviour
{
    [SerializeField] private PostProcessVolume ppVolume;
    
    private ChromaticAberration chromatic;
    private Bloom bloom;
    private State state;
    
    private void Start()
    {
        chromatic = ppVolume.profile.GetSetting<ChromaticAberration>();
        bloom = ppVolume.profile.GetSetting<Bloom>();
        state = State.Find();
    }
    
    private void Update()
    {
        if (state.energyLevel > 0)
        {
            var et = Mathf.InverseLerp(0, state.maxEnergyLevel, state.energyLevel);
            chromatic.intensity.value = Mathf.Lerp(0, 5, et);
            bloom.intensity.value = Mathf.Lerp(0, 7, et);
        }
    }
}