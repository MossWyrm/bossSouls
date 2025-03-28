using UnityEngine;

public class PlayerStats : MonoBehaviour, IAttack
{
    private PlayerShapeshiftManager _shapeShiftMgr;
    private AbilityManager _abilityManager;

    public Character CurrentCharacter => _shapeShiftMgr.CurrentCharacter;
    public Stats Stats => _shapeShiftMgr.CurrentCharacter.stats;

    private void Start()
    {
        _shapeShiftMgr = GetComponent<PlayerShapeshiftManager>();
        _abilityManager = GetComponent<AbilityManager>();
    }
    
    public void UseAbility(int abilityIndex)
    {
        if(_abilityManager == null)
        {
            print("no ability Manager found");
            return;
        }
        _abilityManager.UseAbility(abilityIndex);
    }
}
