namespace DefconZ
{
    public interface IDestructible
    {
        void DestroySelf();

        void TakeDamage(float damage);
    }
}