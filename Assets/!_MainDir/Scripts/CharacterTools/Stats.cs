using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Stats
{
    [Header("Combat")]
    public int maxHealth;
    public int currentHealth;
    [Range(0f, 1f)]
    public float armor;
    
    [Header("Movement")]
    public int moveSpeed;
    public int sprintMultiplier;
    public int rotationSpeed;
    public int dodgeDuration;
    public int dodgeCooldown;
}