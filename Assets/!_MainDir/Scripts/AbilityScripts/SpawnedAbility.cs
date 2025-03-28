using UnityEngine;
using UnityEngine.Serialization;

public abstract class SpawnedAbility : MonoBehaviour
{
    public abstract void Initialize(float lifeTime);
    public abstract void Activate();
    public abstract float LifeTime { get; set; }
}