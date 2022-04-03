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
    [SerializeField] private float turnOffAfter;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        mr.material = blackMaterial;
    }

    public void TouchPhone()
    {
        player.Play();
        mr.material = videoMaterial;
        StartCoroutine(StopPlayer());
    }

    private IEnumerator StopPlayer()
    {
        yield return new WaitForSeconds(turnOffAfter);
        player.Pause();
        mr.material = blackMaterial;
    }
}
