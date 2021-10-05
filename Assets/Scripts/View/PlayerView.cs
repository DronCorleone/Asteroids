using UnityEngine;

public class PlayerView : BaseObjectView
{
    [SerializeField] private Transform _projectileSpawnPoint;

    public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
}