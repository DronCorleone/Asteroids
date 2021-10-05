using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : BaseController, IExecute
{
    private List<BulletView> _bulletPool;
    private List<LaserView> _laserPool;
    private int _bulletPoolSize = 10;
    private Vector3 _poolPosition = new Vector3(100.0f, 100.0f, 0.0f);
    private float _bulletSpeed;
    private float _laserLifetime;


    public WeaponController(MainController main) : base(main)
    {
        _bulletSpeed = main.Config.BulletSpeed;
        _laserLifetime = main.Config.LaserLifetime;

        _bulletPool = new List<BulletView>();
        _laserPool = new List<LaserView>();

        for (int i = 0; i < _bulletPoolSize; i++)
        {
            var bullet = Object.Instantiate(Resources.Load<GameObject>("Weapon/Bullet"), _poolPosition, Quaternion.identity).GetComponent<BulletView>();
            _bulletPool.Add(bullet);
            BackToPool(bullet);
        }

        for (int i = 0; i < main.Config.LaserMagazineSize; i++)
        {
            var laser = Object.Instantiate(Resources.Load<GameObject>("Weapon/Laser"), _poolPosition, Quaternion.identity).GetComponent<LaserView>();
            _laserPool.Add(laser);
            BackToPool(_laserPool[i]);
        }
    }


    public override void Initialize()
    {
        base.Initialize();

        GameEvents.Current.OnBulletSpawn += SpawnBullet;
    }

    public void Execute()
    {
        //Bullet move
        for (int i = 0; i < _bulletPool.Count; i++)
        {
            if (_bulletPool[i].IsActive == true)
            {
                _bulletPool[i].Transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);

                if (_bulletPool[i].Transform.position.x > _main.Config.MaxX ||
                _bulletPool[i].Transform.position.x < _main.Config.MinX ||
                _bulletPool[i].Transform.position.y > _main.Config.MaxY ||
                _bulletPool[i].Transform.position.y < _main.Config.MinY)
                {
                    BackToPool(_bulletPool[i]);
                }
            }
        }
    }


    private void SpawnBullet(Transform point)
    {
        var bullet = BulletFromPool();
        bullet.Transform.position = point.position;
        bullet.Transform.rotation = point.rotation;
        bullet.Activate();
        bullet.gameObject.SetActive(true);
    }

    #region Pool
    private void BackToPool(BulletView bullet)
    {
        bullet.Deactivate();
        bullet.Transform.position = _poolPosition;
        bullet.gameObject.SetActive(false);
    }
    private void BackToPool(LaserView laser)
    {
        laser.Deactivate();
        laser.Transform.position = _poolPosition;
        laser.gameObject.SetActive(false);
    }
    private BulletView BulletFromPool()
    {
        BulletView bullet = null;

        for (int i = 0; i < _bulletPool.Count; i++)
        {
            if (_bulletPool[i].IsActive == false)
            {
                bullet = _bulletPool[i];
                return bullet;
            }
        }

        if (bullet == null)
        {
            bullet = Object.Instantiate(Resources.Load<GameObject>("Weapon/Bullet"),_poolPosition, Quaternion.identity).GetComponent<BulletView>();
            _bulletPool.Add(bullet);
            BackToPool(bullet);
        }

        return bullet;
    }
    private LaserView LaserFromPool()
    {
        LaserView laser = null;

        for (int i = 0; i < _laserPool.Count; i++)
        {
            if (_laserPool[i].IsActive == false)
            {
                laser = _laserPool[i];
                return laser;
            }
        }

        if (laser == null)
        {
            laser = Object.Instantiate(Resources.Load<GameObject>("Weapon/Laser"), _poolPosition, Quaternion.identity).GetComponent<LaserView>();
            _laserPool.Add(laser);
            BackToPool(laser);
        }

        return laser;
    }
    #endregion
}