using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController, IExecute
{
    private PlayerView _player;
    private Transform _playerSpawnPoint;

    private float _moveSpeed;
    private float _rotateSpeed;
    private float _moveInput;
    private float _rotateInput;

    public PlayerController(MainController main) : base(main)
    {
        _moveSpeed = main.Config.PlayerMoveSpeed;
        _rotateSpeed = main.Config.PlayerRotateSpeed;
    }


    public override void Initialize()
    {
        base.Initialize();

        _player = Object.Instantiate(Resources.Load<GameObject>("Player/Player"), Main.PlayerSpawnPoint.position, Quaternion.identity).GetComponent<PlayerView>();

        InputEvents.Current.OnMoveInput += SetMoveInput;
        InputEvents.Current.OnRotateInput += SetRotateInput;
    }

    public void Execute()
    {
        //Moving
        _player.Transform.Translate(Vector3.up * _moveInput * _moveSpeed * Time.deltaTime);
        _player.Transform.Rotate(_player.Transform.forward, _rotateInput * _rotateSpeed * Time.deltaTime);

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
    }


    private void SetMoveInput(float value)
    {
        _moveInput = value;
    }
    private void SetRotateInput(float value)
    {
        _rotateInput = value;
    }
}