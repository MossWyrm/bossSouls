using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Ability : ScriptableObject
{
    public AbilityType abilityType;
    public int cooldownTime;
    public float cooldownTimer;
    public bool blocksMovement;
    public bool needsTrigger = false;
    public bool canBreak;
    public bool canUse => cooldownTimer <= 0;
    protected GameObject parentObject;

    private bool triggerCreated;

    private Action animationTrigger;


    public virtual void ActivateAbility()
    {
        CancelUse();
        cooldownTimer = cooldownTime;
    }

    public void UseAbility()
    {
        if (cooldownTimer > 0) return;
        if (needsTrigger)
        {
            triggerCreated = true;
            animationTrigger += ActivateAbility;
        }
        else
        {
            ActivateAbility();
        }
    }

    public void AnimationTrigger()
    {
        if(!needsTrigger) return;
        if(!canBreak) return;
        animationTrigger.Invoke();
    }

    public void CancelUse()
    {
        if (triggerCreated)
        {
            triggerCreated = false;
            animationTrigger -= ActivateAbility;
        }
    }

    public virtual void CleanUp()
    {
        return;
    }

    public virtual void Prepare(GameObject toParent)
    {
        parentObject = toParent;
    }
}