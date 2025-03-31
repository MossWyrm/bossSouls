using UnityEngine;
using UnityEngine.Serialization;

public class PlayerScripts: MonoBehaviour
{
    [FormerlySerializedAs("playerInputManager")] [FormerlySerializedAs("playerControls")] public CustomPlayerInputManager customPlayerInputManager;
    [FormerlySerializedAs("transformationManager")] public PlayerShapeshiftManager shapeshiftManager;
    public Player stats;
    public AbilityManager abilityManager;
}
