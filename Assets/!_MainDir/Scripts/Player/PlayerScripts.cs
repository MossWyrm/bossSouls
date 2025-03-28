using UnityEngine;
using UnityEngine.Serialization;

public class PlayerScripts: MonoBehaviour
{
    public PlayerControls playerControls;
    [FormerlySerializedAs("transformationManager")] public PlayerShapeshiftManager shapeshiftManager;
    public PlayerStats stats;
    public AbilityManager abilityManager;
}
