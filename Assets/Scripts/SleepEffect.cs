using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SleepEffect : MonoBehaviour
{
    [SerializeField] private PostProcessVolume ppVolume;

    private float timer;
    private GameObject camObj;
    private Vector3 camRotatorInitPos;

    private Vignette vignette;
    private DepthOfField depthOfField;
    private State state;
    
    private void Start()
    {
        timer = 0f;
        state = State.Find();
        vignette = ppVolume.profile.GetSetting<Vignette>();
        depthOfField = ppVolume.profile.GetSetting<DepthOfField>();

        camObj = Camera.main.gameObject;
        camRotatorInitPos = camObj.transform.position;
    }

    private void Update()
    {
        if (state.energyLevel < 0)
        {
            vignette.intensity.value = Mathf.Lerp(
                vignette.intensity.value,
                -state.energyLevel * 1.5f,
                Time.time * 0.01f
            );
            
            
            depthOfField.focalLength.value = Mathf.Lerp(
                depthOfField.focalLength.value,
                -state.energyLevel * 60f,
                Time.time * 0.01f
            );
            
            
            Rotatat();
        }
        else
        {
            vignette.intensity.value = 0;
        }
    }

    private void Rotatat()
    {
        timer += Time.deltaTime;
        
        var multiplier = 1 - Mathf.InverseLerp(state.minEnergyLevel, 0, state.energyLevel);
        
        var pos = camObj.transform.position;
        pos.y = camRotatorInitPos.y + Mathf.Sin(timer * 1.1f) * 0.1f * multiplier;
        pos.x = camRotatorInitPos.x + Mathf.Cos(timer * 0.9f) * 0.04f * multiplier;
        pos.z = camRotatorInitPos.z + Mathf.Sin(timer * 1.3f) * 0.01f * multiplier;
        camObj.transform.position = pos;
    }
}