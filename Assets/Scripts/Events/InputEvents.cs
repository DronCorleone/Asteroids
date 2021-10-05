using System;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
    public static InputEvents Current;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
}