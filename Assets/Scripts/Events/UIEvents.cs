using System;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Current;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Current = this;
    }


    public event Action OnButtonStartGame;
    public void ButtonStartGame()
    {
        OnButtonStartGame?.Invoke();
    }

    public event Action OnButtonPauseGame;
    public void ButtonPauseGame()
    {
        OnButtonPauseGame?.Invoke();
    }

    public event Action OnButtonResumeGame;
    public void ButtonResumeGame()
    {
        OnButtonResumeGame?.Invoke();
    }

    public event Action OnButtonRestartGame;
    public void ButtonRestartGame()
    {
        OnButtonRestartGame?.Invoke();
    }
}