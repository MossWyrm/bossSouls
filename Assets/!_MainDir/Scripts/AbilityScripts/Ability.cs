using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Ability : ScriptableObject
{
    public AbilityType abilityType;
    public int cooldownTime;
    public float cooldownTimer;
    protected GameObject parentObject;
    

    public virtual void UseAbility()
    {
        throw new System.NotImplementedException();
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