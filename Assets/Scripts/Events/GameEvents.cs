using System;
using UnityEngine;


public class GameEvents : MonoBehaviour
{
    public static GameEvents Current;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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

    public event Action<BulletView> OnBulletDestroy;
    public void BulletDestroy(BulletView bullet)
    {
        OnBulletDestroy?.Invoke(bullet);
    }

    public event Action<LaserView> OnLaserDestroy;
    public void LaserDestroy(LaserView laser)
    {
        OnLaserDestroy?.Invoke(laser);
    }

    public event Action<float> OnLaserCooldown;
    public void LaserCooldown(float value)
    {
        OnLaserCooldown?.Invoke(value);
    }
    #endregion
}