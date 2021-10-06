using UnityEngine;
using UnityEngine.UI;

public class EndGameMenuView : BaseMenuView
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Elements")]
    [SerializeField] private Text _textScore;
    [SerializeField] private Button _buttonRestart;

    private void Awake()
    {
        _buttonRestart.onClick.AddListener(UIEvents.Current.ButtonRestartGame);
    }


    public void SetScore(int score)
    {
        _textScore.text = $"YOUR SCORE:{score}";
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
        _buttonRestart.onClick.RemoveAllListeners();
    }
}