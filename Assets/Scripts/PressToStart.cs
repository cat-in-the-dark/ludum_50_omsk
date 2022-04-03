using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressToStart : MonoBehaviour
{
    private bool canSkip = false;
    
    private IEnumerator Start()
    {
        canSkip = false;
        yield return new WaitForSeconds(0.5f);
        canSkip = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && canSkip)
        {
            SceneManager.LoadScene(0);
        }    
    }
}
