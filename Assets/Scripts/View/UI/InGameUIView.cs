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


    public void UpdateText(string score, string gps, string angle, string speed, string laserMagazine, string laserTimer)
    {
        _textScore.text = score;
        _textPlayerGPS.text = gps;
        _textPlayerAngle.text = angle;
        _textPlayerSpeed.text = speed;
        _textLaserMagazine.text = laserMagazine;
        _textLaserTimer.text = laserTimer;
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