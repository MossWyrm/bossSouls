using UnityEngine;

public class Damageable: MonoBehaviour
{
    private IDamageable _damageable;

    internal void TakeDamage(int damage)
    {
        _damageable ??= GetComponent<IDamageable>();
        _damageable.CalculateDamage(ref damage);
        _damageable.ApplyDamage(damage);
        _damageable.CheckState();
    }
}