using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SleepEffect : MonoBehaviour
{
    [SerializeField] private PostProcessVolume ppVolume;

    private float timer;
    private GameObject camObj;
    private Vector3 camRotatorInitPos;
    
    private Vignette vignette;
    private State state;
    
    private void Start()
    {
        timer = 0f;
        state = State.Find();
        vignette = ppVolume.profile.GetSetting<Vignette>();

        camObj = Camera.main.gameObject;
        camRotatorInitPos = camObj.transform.position;
    }

    private void Update()
    {
        if (state.energyLevel < 0)
        {
            vignette.intensity.value = -state.energyLevel * 1.5f;
            Rotatat();
        }
    }

    private void Rotatat()
    {
        timer += Time.deltaTime;
        var pos = camObj.transform.position;
        pos.y = camRotatorInitPos.y + Mathf.Sin(timer * 1.1f) * 0.05f;
        pos.x = camRotatorInitPos.x + Mathf.Cos(timer * 0.9f) * 0.1f;
        pos.z = camRotatorInitPos.z + Mathf.Sin(timer * 1.3f) * 0.02f;
        camObj.transform.position = pos;
    }
}