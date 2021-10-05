using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu]
public class Configuration : ScriptableObject
{
    [Header("Player")]
    public float PlayerMoveSpeed;
    public float PlayerRotateSpeed;
    public float PlayerAcceleration;

    [Header("Game field")]
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float EnemyMinX;
    public float EnemyMaxX;
    public float EnemyMinY;
    public float EnemyMaxY;

    [Header("Controls")]
    public Key MoveForwardKey;
    public Key RotateRightKey;
    public Key RotateLeftKey;
    public Key BulletAttackKey;
    public Key LaserAttackKey;

    [Header("Weapon")]
    public int LaserMagazineSize;
    public float LaserCooldown;
    public float BulletSpeed;
    public float LaserLifetime;

    [Header("Enemies")]
    public float BigAsteroidSpeed;
    public float SmallAsteroidSpeed;
    public float UFOSpeed;
    public float AsteroidSpawnTime;
    public float UFOSpawnTime;
}