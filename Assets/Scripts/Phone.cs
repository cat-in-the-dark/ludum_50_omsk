using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Phone : MonoBehaviour
{
    private VideoPlayer player;
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material videoMaterial;
    [SerializeField] private MeshRenderer mr;

    private State state;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        mr.material = blackMaterial;
        state = State.Find();
    }

    public void TouchPhone()
    {
        if (player.isPlaying) return;
        state.inHand = State.HandObjects.PHONE;
        player.Play();
        mr.material = videoMaterial;
        state.videoIsOn = true;
        StartCoroutine(StopPlayer());
    }

    private IEnumerator StopPlayer()
    {
        yield return new WaitForSeconds(state.turnVideoOffAfter);
        player.Pause();
        mr.material = blackMaterial;
        state.videoIsOn = false;
    }
}
