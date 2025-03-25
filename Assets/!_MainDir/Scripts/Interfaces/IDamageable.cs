public interface IDamageable
{
    public void CalculateDamage(ref int damage);
    public void ApplyDamage(int damage);
    public void CheckState();
}