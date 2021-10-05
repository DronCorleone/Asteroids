using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Transform[] _enemySpawnPoints;

    private List<BaseController> _controllers = new List<BaseController>();
    private Configuration _config;

    private InputController _inputController;
    private PlayerController _playerController;
    private WeaponController _weaponController;

    public Configuration Config => _config;
    public Transform PlayerSpawnPoint => _playerSpawnPoint;
    public Transform[] EnemySpawnPoints => _enemySpawnPoints;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _config = Resources.Load<Configuration>("ScriptableObjects/Config");

        //Controllers
        _inputController = new InputController(this);
        _playerController = new PlayerController(this);
        _weaponController = new WeaponController(this);
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