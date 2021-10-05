using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Transform[] _enemySpawnPoints;

    private List<BaseController> _controllers = new List<BaseController>();
    private Configuration _config;
    private PlayerView _player;

    private InputController _inputController;
    private PlayerController _playerController;
    private WeaponController _weaponController;
    private EnemyController _enemyController;
    private ScoreController _scoreController;

    public Configuration Config => _config;
    public Transform PlayerSpawnPoint => _playerSpawnPoint;
    public Transform[] EnemySpawnPoints => _enemySpawnPoints;
    public PlayerView Player => _player;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _config = Resources.Load<Configuration>("ScriptableObjects/Config");

        //Controllers
        _inputController = new InputController(this);
        _playerController = new PlayerController(this);
        _weaponController = new WeaponController(this);
        _enemyController = new EnemyController(this);
        _scoreController = new ScoreController(this);
    }

    private void Start()
    {
        for (int i = 0; i < _controllers.Count; i++)
        {
            if (_controllers[i] is IInitialize)
            {
                _controllers[i].Initialize();
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < _controllers.Count; i++)
        {
            if (_controllers[i] is IExecute)
            {
                (_controllers[i] as IExecute).Execute();
            }
        }
    }

    public void SetPlayer(PlayerView player)
    {
        _player = player;
    }

    public void AddController(BaseController controller)
    {
        if (!_controllers.Contains(controller))
        {
            _controllers.Add(controller);
        }
    }

    public void RemoveController(BaseController controller)
    {
        if (_controllers.Contains(controller))
        {
            _controllers.Remove(controller);
        }
    }

    public T GetController<T>() where T : BaseController
    {
        foreach (BaseController obj in _controllers)
        {
            if (obj.GetType() == typeof(T))
            {
                return (T)obj;
            }
        }
        return null;
    }
}