using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    public void ActWith()
    {
        var initScale = transform.localScale;
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale( initScale * 2f, 2f).SetEase(Ease.OutElastic));
        seq.Append(transform.DOScale(initScale, 2f).SetEase(Ease.OutBack));
    }
}