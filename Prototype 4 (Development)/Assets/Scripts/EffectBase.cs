using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider), typeof(MeshRenderer))]
abstract public class EffectBase : MonoBehaviour
{
    static public EffectManager effectManager;
    public bool isEffectActivated;
    [SerializeField]
    protected float endTime;
    [SerializeField]
    protected EEffects myEffects;
    protected Coroutine endCoroutine;

    public void ActiveEffect()
    {
        isEffectActivated = true;
        if (endCoroutine != null) StopCoroutine(endCoroutine);
        ActionAfterPicked();
        endCoroutine = StartCoroutine(EndEffectAfterSencond(endTime));
    }

    public void DisableEffectNow()
    {
        if (endCoroutine != null) StopCoroutine(endCoroutine);
        ActionBeforeDisable();
        isEffectActivated = false;
    }

    virtual protected void ActionBeforeDisable() { }
    virtual protected void ActionAfterPicked() { }

    private IEnumerator EndEffectAfterSencond(float time)
    {
        yield return new WaitForSeconds(time);
        ActionBeforeDisable();
        isEffectActivated = false;
    }

}
