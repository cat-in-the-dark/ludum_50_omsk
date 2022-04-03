using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    [SerializeField] private Transform faceAnchor;
    public event Action OnFinishDrinking;
    private bool isDrinking;

    private State state;

    private void Start()
    {
        state = State.Find();
    }

    public void Drink()
    {
        if (state.energyLevel < state.maxEnergyLevel && !isDrinking)
        {
            StartDrink();
        }
    }

    private void StartDrink()
    {
        state.inHand = State.HandObjects.CUP;
        isDrinking = true;
        
        var initPos = transform.position;
        var initRot = transform.rotation;
        
        var seq = DOTween.Sequence();
        seq.Append(transform.DOMove(faceAnchor.position, 0.5f));
        seq.Append(transform.DORotate(faceAnchor.rotation.eulerAngles, 0.5f));
        seq.Append(transform.DOMove(initPos, 0.5f));
        seq.Join(transform.DORotate(initRot.eulerAngles, 0.25f));
        seq.OnComplete(OnDrink);

        StartCoroutine(ApplyEnergy());
    }

    private void OnDrink()
    {
        OnFinishDrinking?.Invoke();
        isDrinking = false;
    }

    private IEnumerator ApplyEnergy()
    {
        float applied = 0;
        float delta = state.coffeePower * 0.01f;
        while (applied < state.coffeePower)
        {
            state.energyLevel = Mathf.Clamp(state.energyLevel + delta, state.minEnergyLevel, state.maxEnergyLevel);
            applied += delta; 
            yield return new WaitForFixedUpdate();
        }
    }
}