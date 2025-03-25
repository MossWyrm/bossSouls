using System;
using UnityEngine;

public class PlayerTransformationManager : MonoBehaviour
{
    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats ??= GetComponent<PlayerStats>();
    }
}