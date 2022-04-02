using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SleepEffect : MonoBehaviour
{
    [SerializeField] private PostProcessVolume ppVolume;

    private Vignette vignette;
    private State state;
    
    private void Start()
    {
        state = State.Find();
        vignette = ppVolume.profile.GetSetting<Vignette>();
    }

    private void Update()
    {
        if (state.energyLevel <= 0)
        {
            vignette.intensity.value = -state.energyLevel * 1.5f;
        }
    }
}