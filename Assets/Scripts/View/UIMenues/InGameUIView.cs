using UnityEngine;
using UnityEngine.UI;

public class InGameUIView : BaseMenuView
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Elements")]
    [SerializeField] private Button _buttonPause;
    [SerializeField] private Text _textScore;
    [SerializeField] private Text _textPlayerGPS;
    [SerializeField] private Text _textPlayerAngle;
    [SerializeField] private Text _textPlayerSpeed;
    [SerializeField] private Text _textLaserMagazine;
    [SerializeField] private Text _textLaserTimer;


    private void Awake()
    {
        _buttonPause.onClick.AddListener(UIEvents.Current.ButtonPauseGame);
    }


    
    public void UpdateText(string text, TextInfoType type)
    {
        switch (type)
        {
            case TextInfoType.Score:
                _textScore.text = text;
                break;
            case TextInfoType.GPS:
                _textPlayerGPS.text = text;
                break;
            case TextInfoType.PlayerAngle:
                _textPlayerAngle.text = text;
                break;
            case TextInfoType.PlayerSpeed:
                _textPlayerSpeed.text = text;
                break;
            case TextInfoType.LaserMagazine:
                _textLaserMagazine.text = text;
                break;
            case TextInfoType.LaserTimer:
                _textLaserTimer.text = text;
                break;
        }
    }

    public override void Hide()
    {
        if (!IsShow) return;
        _panel.gameObject.SetActive(false);
        IsShow = false;
    }

    public override void Show()
    {
        if (IsShow) return;
        _panel.gameObject.SetActive(true);
        IsShow = true;
    }

    private void OnDestroy()
    {
        _buttonPause.onClick.RemoveAllListeners();
    }
}