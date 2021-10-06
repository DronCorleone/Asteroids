using System;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
    public static InputEvents Current;


    private void Awake()
    {
        Current = this;
    }


    public event Action<float> OnMoveInput;
    public void MoveInput(float value)
    {
        OnMoveInput?.Invoke(value);
    }

    public event Action<float> OnRotateInput;
    public void RotateInput(float value)
    {
        OnRotateInput?.Invoke(value);
    }

    public event Action OnBulletAttackInput;
    public void BulletAttackInput()
    {
        OnBulletAttackInput?.Invoke();
    }

    public event Action OnLaserAttackInput;
    public void LaserAttackInput()
    {
        OnLaserAttackInput?.Invoke();
    }
}