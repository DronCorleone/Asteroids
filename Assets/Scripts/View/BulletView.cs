using UnityEngine;

public class BulletView : BaseObjectView
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyView enemy))
        {
            switch (enemy.Type)
            {
                case EnemyType.BigAsteroid:
                    GameEvents.Current.BigAsteroidHit(enemy.Transform);
                    GameEvents.Current.BigAsteroidReward();
                    break;
                case EnemyType.SmallAsteroid:
                    GameEvents.Current.SmallAsteroidHit(enemy.Transform);
                    GameEvents.Current.SmallAsteroidReward();
                    break;
                case EnemyType.UFO:
                    GameEvents.Current.UFOHit(enemy.Transform);
                    GameEvents.Current.UFOReward();
                    break;
            }

            GameEvents.Current.BulletDestroy(transform);
        }
    }   
}