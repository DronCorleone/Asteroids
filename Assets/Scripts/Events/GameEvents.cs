using System;
using UnityEngine;


public class GameEvents : MonoBehaviour
{
    public static GameEvents Current;


    private void Awake()
    {
        Current = this;
    }


    #region Player
    public event Action<float> OnPlayerSpeed;
    public void PlayerSpeed(float value)
    {
        OnPlayerSpeed?.Invoke(value);
    }

    public event Action<Vector2> OnPlayerPosition;
    public void PlayerPosition(Vector2 position)
    {
        OnPlayerPosition?.Invoke(position);
    }

    public event Action<Vector3> OnPlayerRotation;
    public void PlayerRotation(Vector3 rotation)
    {
        OnPlayerRotation?.Invoke(rotation);
    }
    #endregion

    #region Weapon
    public event Action<Transform> OnBulletSpawn;
    public void BulletSpawn(Transform point)
    {
        OnBulletSpawn?.Invoke(point);
    }

    public event Action<Transform> OnLaserSpawn;
    public void LaserSpawn(Transform point)
    {
        OnLaserSpawn?.Invoke(point);
    }

    public event Action<Transform> OnBulletDestroy;
    public void BulletDestroy(Transform bullet)
    {
        OnBulletDestroy?.Invoke(bullet);
    }

    public event Action<Transform> OnLaserDestroy;
    public void LaserDestroy(Transform laser)
    {
        OnLaserDestroy?.Invoke(laser);
    }

    public event Action<float> OnLaserCooldown;
    public void LaserCooldown(float value)
    {
        OnLaserCooldown?.Invoke(value);
    }

    public event Action<int> OnLaserMagazine;
    public void LaserMagazine(int value)
    {
        OnLaserMagazine?.Invoke(value);
    }
    #endregion

    #region Enemies
    public event Action<Transform> OnBigAsteroidHit;
    public void BigAsteroidHit(Transform asteroid)
    {
        OnBigAsteroidHit?.Invoke(asteroid);
    }

    public event Action<Transform> OnSmallAsteroidHit;
    public void SmallAsteroidHit(Transform asteroid)
    {
        OnSmallAsteroidHit?.Invoke(asteroid);
    }

    public event Action<Transform> OnUFOHit;
    public void UFOHit(Transform ufo)
    {
        OnUFOHit?.Invoke(ufo);
    }

    public event Action OnBigAsteroidReward;
    public void BigAsteroidReward()
    {
        OnBigAsteroidReward?.Invoke();
    }

    public event Action OnSmallASteroidReward;
    public void SmallAsteroidReward()
    {
        OnSmallASteroidReward?.Invoke();
    }

    public event Action OnUFOReward;
    public void UFOReward()
    {
        OnUFOReward?.Invoke();
    }
    #endregion

    public event Action<int> OnCurrentScore;
    public void CurrentScore(int score)
    {
        OnCurrentScore?.Invoke(score);
    }

    public event Action OnGameOver;
    public void GameOver()
    {
        OnGameOver?.Invoke();
    }
}