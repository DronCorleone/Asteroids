using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : BaseController, IExecute
{
    private Keyboard _keyboard;
    private float _moveSpeed;
    private float _rotateSpeedLeft = 1.0f;
    private float _rotateSpeedRight = -1.0f;
    private float _maxMoveSpeed = 1.0f;
    private float _minMoveSpeed = 0.0f;
    private float _acceleration;

    public InputController(MainController main) : base(main)
    {
        _keyboard = Keyboard.current;
        _moveSpeed = 0.0f;
        _acceleration = main.Config.PlayerAcceleration;
    }


    public override void Initialize()
    {
        base.Initialize();
    }

    public void Execute()
    {
        //Move
        if (_keyboard.upArrowKey.isPressed)
        {
            _moveSpeed += Time.deltaTime * _acceleration;

            if (_moveSpeed >= _maxMoveSpeed)
            {
                _moveSpeed = _maxMoveSpeed;
            }
        }
        else
        {
            _moveSpeed -= Time.deltaTime * _acceleration;

            if (_moveSpeed <= _minMoveSpeed)
            {
                _moveSpeed = _minMoveSpeed;
            }
        }
        InputEvents.Current.MoveInput(_moveSpeed);

        //Rotate
        if (_keyboard.rightArrowKey.isPressed)
        {
            InputEvents.Current.RotateInput(_rotateSpeedRight);
        }
        else if (_keyboard.leftArrowKey.isPressed)
        {
            InputEvents.Current.RotateInput(_rotateSpeedLeft);
        }
        else
        {
            InputEvents.Current.RotateInput(0.0f);
        }
    }
}