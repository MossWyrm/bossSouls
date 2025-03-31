using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [ReadOnly] public Character currentCharacter;
    [ReadOnly] public List<Ability> abilities = new List<Ability>(); 
    private bool _initializing = true;
    public void UseAbility(int index)
    {
        if (abilities.Count < index + 1 || abilities[index] == null) return;
        abilities[index].UseAbility();
    }
    
    private void Update()
    {
        foreach (var t in abilities.Where(t => t).Where(t => t.cooldownTimer > 0))
        {
            t.cooldownTimer -= Time.deltaTime;
        }
    }

    public Ability GetAbility(int index)
    {
        if (abilities.Count < index + 1 || abilities[index] == null) return null;
        return abilities[index];
    }

    public void ChangeCharacter(Character character)
    {
        CleanUp();
        currentCharacter = character;
        GetNewAbilities();
        PrepareObjects();
        _initializing = false;
    }

    private void CleanUp()
    {
        if (_initializing) return;
        foreach (var t in abilities)
        {
            t?.CleanUp();
        }
    }

    private void GetNewAbilities()
    {
        abilities.Clear();
        foreach (Ability ability in currentCharacter.abilities)
        {
            abilities.Add(Instantiate(ability));
        }
    }

    private void PrepareObjects()
    {
        foreach (var t in abilities.Where(t => t?.abilityType == AbilityType.Projectile))
        {
            t?.Prepare(gameObject);
        }
    }
}