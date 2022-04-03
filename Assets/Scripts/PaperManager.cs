using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaperManager : MonoBehaviour
{
    [SerializeField] private List<Paper> papers;
    [SerializeField] private List<Transform> anchors;
    [SerializeField] private int currentPaperIdx;
    private State state;
    [SerializeField] private Transform target;
    [SerializeField] private AudioSource wowAudio;

    private void Start()
    {
        state = State.Find();
        Invoke("TakeNewPaper", 0.2f);
    }

    public void AppendText()
    {
        if (currentPaperIdx >= papers.Count) return;
        var paper = papers[currentPaperIdx];
        paper.AppendText();
    }

    private void Update()
    {
        if (currentPaperIdx >= papers.Count) return;

        if (state.currentProgress >= 1)
        {
            RemovePaper();
            state.currentProgress = 0;
            currentPaperIdx++;
            if (currentPaperIdx < papers.Count)
            {
                TakeNewPaper();
            }
            else
            {
                GameEnd();
            }
        }
    }

    private void TakeNewPaper()
    {
        if (currentPaperIdx >= papers.Count) return;

        var paper = papers[currentPaperIdx];
        paper.transform.DOMove(target.position, 1f).SetEase(Ease.InQuart).OnComplete(() =>
        {
            paper.gameObject.layer = LayerMask.NameToLayer("Clickable");
            paper.enabled = true;
        });
        paper.transform.DORotate(target.rotation.eulerAngles, 0.2f);
    }

    private void RemovePaper()
    {
        if (currentPaperIdx >= papers.Count) return;

        var paper = papers[currentPaperIdx];
        var anchor = anchors[currentPaperIdx];
        paper.gameObject.layer = LayerMask.NameToLayer("Default");
        paper.enabled = false;
        paper.transform.DOMove(anchor.position, 1f).SetEase(Ease.InQuart);
        paper.transform.DORotate(anchor.rotation.eulerAngles, 0.2f);
        
        wowAudio.Play();
    }

    private void GameEnd()
    {
        Invoke("LoadGameOver", 2f);
    }

    private void LoadGameOver()
    {
        SceneManager.LoadScene(1);
    }
}