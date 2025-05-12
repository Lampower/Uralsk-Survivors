public abstract class LivingEntity : AbstractEntity, IDamagable
{
    public int Health = 10;

    public int ScoreForKilling = 1;
    public abstract void TakeDamage(int damage, LivingEntity takenBy = null);
}