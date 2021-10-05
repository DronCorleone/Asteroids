using UnityEngine;

public class PlayerController : BaseController, IExecute
{
    private PlayerView _player;

    private float _moveSpeed;
    private float _rotateSpeed;
    private float _moveInput;
    private float _rotateInput;

    private int _laserMagazine;
    private float _laserCooldown;

    public PlayerController(MainController main) : base(main)
    {
        _moveSpeed = main.Config.PlayerMoveSpeed;
        _rotateSpeed = main.Config.PlayerRotateSpeed;
        _laserMagazine = main.Config.LaserMagazineSize;
    }


    public override void Initialize()
    {
        base.Initialize();

        _player = Object.Instantiate(Resources.Load<GameObject>("Player/Player"), Main.PlayerSpawnPoint.position, Quaternion.identity).GetComponent<PlayerView>();
        _main.SetPlayer(_player);

        InputEvents.Current.OnMoveInput += SetMoveInput;
        InputEvents.Current.OnRotateInput += SetRotateInput;
        InputEvents.Current.OnBulletAttackInput += BulletAttack;
        InputEvents.Current.OnLaserAttackInput += LaserAttack;
    }

    public void Execute()
    {
        //Moving
        _player.Transform.Translate(Vector3.up * _moveInput * _moveSpeed * Time.deltaTime);
        _player.Transform.Rotate(_player.Transform.forward, _rotateInput * _rotateSpeed * Time.deltaTime);
        GameEvents.Current.PlayerSpeed(_moveInput * _moveSpeed);
        GameEvents.Current.PlayerPosition(_player.Transform.position);

        //Check bounds
        if (_player.Transform.position.x > _main.Config.MaxX)
        {
            _player.Transform.position = new Vector3(_main.Config.MinX, _player.Transform.position.y, _player.Transform.position.z);
        }
        if (_player.Transform.position.x < _main.Config.MinX)
        {
            _player.Transform.position = new Vector3(_main.Config.MaxX, _player.Transform.position.y, _player.Transform.position.z);
        }
        if (_player.Transform.position.y > _main.Config.MaxY)
        {
            _player.Transform.position = new Vector3(_player.Transform.position.x, _main.Config.MinY, _player.Transform.position.z);
        }
        if (_player.Transform.position.y < _main.Config.MinY)
        {
            _player.Transform.position = new Vector3(_player.Transform.position.x, _main.Config.MaxY, _player.Transform.position.z);
        }

        //Weapon
        if (_laserMagazine == _main.Config.LaserMagazineSize)
        {
            _laserCooldown = 0.0f;
        }
        else
        {
            _laserCooldown += Time.deltaTime;

            if (_laserCooldown >= _main.Config.LaserCooldown)
            {
                _laserCooldown = 0.0f;
                _laserMagazine++;
            }
        }

        if (_laserCooldown == 0.0f)
        {
            GameEvents.Current.LaserCooldown(_laserCooldown);
        }
        else
        {
            GameEvents.Current.LaserCooldown(_main.Config.LaserCooldown - _laserCooldown);
        }
    }


    private void SetMoveInput(float value)
    {
        _moveInput = value;
    }
    private void SetRotateInput(float value)
    {
        _rotateInput = value;
    }

    private void BulletAttack()
    {
        GameEvents.Current.BulletSpawn(_player.ProjectileSpawnPoint);
    }
    private void LaserAttack()
    {
        if (_laserMagazine > 0)
        {
            GameEvents.Current.LaserSpawn(_player.ProjectileSpawnPoint);
            _laserMagazine--;
        }
    }
}