namespace DefconZ.Entity
{
    public interface IDestructible
    {
        void DestroySelf();

        void TakeDamage(float damage);
    }
}