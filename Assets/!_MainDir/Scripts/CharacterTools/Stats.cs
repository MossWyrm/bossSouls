using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats
{
    public int maxHealth;
    public int currentHealth;
    public int speed;
    public int sprintMultiplier;
    [Range(0f, 1f)]
    public float armor;
    public int dodgeTimer;
}