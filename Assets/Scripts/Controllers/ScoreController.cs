public class ScoreController : BaseController
{
    private int _currentScore;
    private int _scoreForBigAsteroid;
    private int _scoreForSmallAsteroid;
    private int _scoreForUFO;

    public ScoreController(MainController main) : base(main)
    {
        _currentScore = 0;
        _scoreForBigAsteroid = main.Config.BigAsteroidReward;
        _scoreForSmallAsteroid = main.Config.SmallAsteroidReward;
        _scoreForUFO = main.Config.UFOReward;
    }


    public override void Initialize()
    {
        base.Initialize();

        GameEvents.Current.OnBigAsteroidReward += BigAsteroidReward;
        GameEvents.Current.OnSmallASteroidReward += SmallAsteroidReward;
        GameEvents.Current.OnUFOReward += UFOReward;
    }


    private void BigAsteroidReward()
    {
        _currentScore += _scoreForBigAsteroid;
        GameEvents.Current.CurrentScore(_currentScore);
    }
    private void SmallAsteroidReward()
    {
        _currentScore += _scoreForSmallAsteroid;
        GameEvents.Current.CurrentScore(_currentScore);
    }
    private void UFOReward()
    {
        _currentScore += _scoreForUFO;
        GameEvents.Current.CurrentScore(_currentScore);
    }
}