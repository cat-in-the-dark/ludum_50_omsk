using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class CoffeeEffect : MonoBehaviour
{
    [SerializeField] private PostProcessVolume ppVolume;
    
    private ChromaticAberration chromatic;
    private State state;
    
    private void Start()
    {
        chromatic = ppVolume.profile.GetSetting<ChromaticAberration>();
        state = State.Find();
    }
    
    private void Update()
    {
        if (state.energyLevel > 0)
        {
            chromatic.intensity.value = state.energyLevel;
        }
    }
}