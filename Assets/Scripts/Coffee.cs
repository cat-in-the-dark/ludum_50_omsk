using System;
using System.Collections;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    public event Action OnFinishDrinking;
    private bool isDrinking;
    private Animator animator;
    
    private State state;

    private void Start()
    {
        state = State.Find();
        animator = GetComponent<Animator>();
    }

    public void Drink()
    {
        if (state.energyLevel <= state.maxEnergyLevel)
        {
            StartCoroutine(StartDrink());
        }
    }

    private IEnumerator StartDrink()
    {
        animator.SetTrigger("Drink");
        yield return new WaitForFixedUpdate();
        isDrinking = true;
        StartCoroutine(ApplyEnergy());
        
        var animState = animator.GetCurrentAnimatorStateInfo(0);
        if (isDrinking && !animState.IsName("drink"))
        {
            isDrinking = false;
            OnDrink();
            OnFinishDrinking?.Invoke();
        }
    }

    private void OnDrink()
    {
    }

    private IEnumerator ApplyEnergy()
    {
        float applied = 0;
        float delta = state.coffeePower * 0.01f;
        while (applied < state.coffeePower)
        {
            state.energyLevel += delta;
            applied += delta; 
            yield return new WaitForFixedUpdate();
        }
    }
}