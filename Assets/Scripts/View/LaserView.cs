using UnityEngine;

public class LaserView : BaseObjectView
{
    [SerializeField] private float _distance = 50.0f;

    private float _lifetime;

    public float Lifetime => _lifetime;


    public void SetLifetime(float time)
    {
        _lifetime = time;
    }
    public void ReduceLifetime(float value)
    {
        _lifetime -= value;
    }

    public void Fire()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, _distance);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.TryGetComponent(out EnemyView enemy))
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
            }
        }
    }
}