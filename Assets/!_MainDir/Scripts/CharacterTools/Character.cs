using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
public class Character : ScriptableObject
{
    public Stats stats = new Stats();
    public List<Ability> abilities = new List<Ability>();
    public GameObject characterPrefab;
}