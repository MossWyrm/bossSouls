using UnityEngine;


public enum AbilityType
{
    Attack,
    Dodge,
    Defend
}

[System.Serializable]
public class AbilityStat
{
    public AbilityType abilityType;
    public int cooldown;
    public int value;
}