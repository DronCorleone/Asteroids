using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : BaseController
{
    private List<BaseMenuView> _menues;
    private UIView _uiView;
    private InGameUIView _inGameUI;
    private EndGameMenuView _endGameMenu;
    private int _score;

    public UIController(MainController main) : base(main)
    {
        _menues = new List<BaseMenuView>();
        _score = 0;
    }


    public override void Initialize()
    {
        base.Initialize();

        _uiView = Object.Instantiate(Resources.Load<GameObject>("UI/UI")).GetComponent<UIView>();

        for (int i = 0; i < _uiView.Menues.Length; i++)
        {
            AddView(_uiView.Menues[i]);
        }

        UIEvents.Current.OnButtonStartGame += StartGame;
        UIEvents.Current.OnButtonPauseGame += PauseGame;
        UIEvents.Current.OnButtonResumeGame += StartGame;
        UIEvents.Current.OnButtonRestartGame += RestartGame;

        GameEvents.Current.OnCurrentScore += UpdateScoreText;
        GameEvents.Current.OnPlayerPosition += UpdateGPSText;
        GameEvents.Current.OnPlayerRotation += UpdateAngleText;
        GameEvents.Current.OnPlayerSpeed += UpdatePlayerSpeedText;
        GameEvents.Current.OnLaserMagazine += UpdateLaserMagazineText;
        GameEvents.Current.OnLaserCooldown += UpdateLaserTimerText;
        GameEvents.Current.OnGameOver += GameOver;

        Time.timeScale = 0.0f;
        SwitchUI(UIState.MainMenu);
    }


    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PauseGame()
    {
        SwitchUI(UIState.Pause);
        Time.timeScale = 0.0f;
    }

    private void StartGame()
    {
        SwitchUI(UIState.InGame);
        Time.timeScale = 1.0f;
    }

    private void GameOver()
    {
        SwitchUI(UIState.EndGame);
        _endGameMenu.SetScore(_score);
        Time.timeScale = 0.0f;
    }

    private void AddView(BaseMenuView view)
    {
        _menues.Add(view);

        if (view.GetType() == typeof(InGameUIView))
        {
            _inGameUI = view as InGameUIView;
        }
        else if (view.GetType() == typeof(EndGameMenuView))
        {
            _endGameMenu = view as EndGameMenuView;
        }
    }

    #region UpdateInGameText
    private void UpdateScoreText(int score)
    {
        _score = score;
        _inGameUI.UpdateText($"SCORE: {score}", TextInfoType.Score);
    }
    private void UpdateGPSText(Vector2 gps)
    {
        _inGameUI.UpdateText($"X:{Mathf.Round(gps.x)} Y:{Mathf.Round(gps.y)}", TextInfoType.GPS);
    }
    private void UpdateAngleText(Vector3 rotation)
    {
        _inGameUI.UpdateText($"ANGLE:{Mathf.Round(rotation.z)}", TextInfoType.PlayerAngle);
    }
    private void UpdatePlayerSpeedText(float playerSpeed)
    {
        _inGameUI.UpdateText($"SPEED:{Mathf.Round(playerSpeed)}", TextInfoType.PlayerSpeed);
    }
    private void UpdateLaserMagazineText(int capacity)
    {
        _inGameUI.UpdateText($"LASERS:{capacity}", TextInfoType.LaserMagazine);
    }
    private void UpdateLaserTimerText(float value)
    {
        _inGameUI.UpdateText($"{Mathf.Round(value)}", TextInfoType.LaserTimer);
    }
    #endregion

    #region Switch
    private void SwitchUI(UIState state)
    {
        if (_menues.Count == 0)
        {
            CustomDebug.LogWarning("There is no menues to switch.");
        }
        switch (state)
        {
            case UIState.MainMenu:
                SwitchMenu(typeof(MainMenuView));
                break;
            case UIState.InGame:
                SwitchMenu(typeof(InGameUIView));
                break;
            case UIState.Pause:
                SwitchMenu(typeof(PauseMenuView));
                break;
            case UIState.EndGame:
                SwitchMenu(typeof(EndGameMenuView));
                break;
        }
    }
    private void SwitchMenu(System.Type type)
    {
        bool isFound = false;

        for (int i = 0; i < _menues.Count; i++)
        {
            if (_menues[i].GetType() == type)
            {
                _menues[i].Show();
                isFound = true;
            }
            else
            {
                _menues[i].Hide();
            }

            if (i == _menues.Count - 1f && !isFound)
            {
                CustomDebug.LogWarning($"Oops! Menu {type} not found");
            }
        }
    }
    #endregion
}