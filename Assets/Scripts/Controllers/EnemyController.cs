using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController, IExecute
{
    private Transform[] _spawnPoints;
    private List<EnemyView> _bigAsteroidPool;
    private List<EnemyView> _smallAsteroidPool;
    private List<EnemyView> _ufoPool;
    private string[] _bigAsteroidPath;
    private string[] _smallAsteroidPath;
    private string _ufoPath;
    private float _timerAsteroids;
    private float _timerUFO;
    private int _bigAsteroidStartPoolCapacity = 10;
    private int _smallAsteroidStartPoolCapacity = 10;
    private int _smallAsteroidMaxGroupSize = 3;
    private int _ufoStartPoolCapacity = 3;
    private Vector3 _poolPosition = new Vector3(100.0f, 100.0f, 0.0f);
    private float _bigAsteroidSpeed;
    private float _smallAsteroidSpeed;
    private float _ufoSpeed;

    public EnemyController(MainController main) : base(main)
    {
        _bigAsteroidPath = new string[]
        {
            "Enemy/BigAsteroid-1",
            "Enemy/BigAsteroid-2"
        };
        _smallAsteroidPath = new string[]
        {
            "Enemy/SmallAsteroid-1",
            "Enemy/SmallAsteroid-2"
        };
        _ufoPath = "Enemy/UFO";

        _bigAsteroidPool = new List<EnemyView>();
        _smallAsteroidPool = new List<EnemyView>();
        _ufoPool = new List<EnemyView>();

        for (int i = 0; i < _bigAsteroidStartPoolCapacity; i++)
        {
            int random = Random.Range(0, _bigAsteroidPath.Length);
            EnemyView asteroid = Object.Instantiate(Resources.Load<GameObject>(_bigAsteroidPath[random]), _poolPosition, Quaternion.identity).GetComponent<EnemyView>();
            _bigAsteroidPool.Add(asteroid);
            BackToPool(asteroid.Transform);
        }
        for (int i = 0; i < _smallAsteroidStartPoolCapacity; i++)
        {
            int random = Random.Range(0, _smallAsteroidPath.Length);
            EnemyView asteroid = Object.Instantiate(Resources.Load<GameObject>(_smallAsteroidPath[random]), _poolPosition, Quaternion.identity).GetComponent<EnemyView>();
            _smallAsteroidPool.Add(asteroid);
            BackToPool(asteroid.Transform);
        }
        for (int i = 0; i < _ufoStartPoolCapacity; i++)
        {
            EnemyView ufo = Object.Instantiate(Resources.Load<GameObject>(_ufoPath), _poolPosition, Quaternion.identity).GetComponent<EnemyView>();
            _ufoPool.Add(ufo);
            BackToPool(ufo.Transform);
        }

        _spawnPoints = main.EnemySpawnPoints;
        _timerAsteroids = 0.0f;
        _timerUFO = 0.0f;
        _bigAsteroidSpeed = main.Config.BigAsteroidSpeed;
        _smallAsteroidSpeed = main.Config.SmallAsteroidSpeed;
        _ufoSpeed = main.Config.UFOSpeed;
    }


    public override void Initialize()
    {
        base.Initialize();

        GameEvents.Current.OnBigAsteroidHit += BigAsteroidHit;
        GameEvents.Current.OnSmallAsteroidHit += BackToPool;
        GameEvents.Current.OnUFOHit += BackToPool;
    }
    
    public void Execute()
    {
        //Spawn
        _timerAsteroids += Time.deltaTime;
        _timerUFO += Time.deltaTime;

        if (_timerAsteroids >= _main.Config.AsteroidSpawnTime)
        {
            SpawnBigAsteroid();
            _timerAsteroids = 0.0f;
        }
        if (_timerUFO >= _main.Config.UFOSpawnTime)
        {
            SpawnUFO();
            _timerUFO = 0.0f;
        }

        //Moving
        for (int i = 0; i < _bigAsteroidPool.Count; i++)
        {
            if (_bigAsteroidPool[i].IsActive == true)
            {
                _bigAsteroidPool[i].Transform.Translate(_bigAsteroidPool[i].Direction * _bigAsteroidSpeed * Time.deltaTime);
                
                if (CheckBounds(_bigAsteroidPool[i].Transform.position))
                {
                    BackToPool(_bigAsteroidPool[i].Transform);
                }
            }
        }
        for (int i = 0; i < _smallAsteroidPool.Count; i++)
        {
            if (_smallAsteroidPool[i].IsActive == true)
            {
                _smallAsteroidPool[i].Transform.Translate(_smallAsteroidPool[i].Direction * _smallAsteroidSpeed * Time.deltaTime);

                if (CheckBounds(_smallAsteroidPool[i].Transform.position))
                {
                    BackToPool(_smallAsteroidPool[i].Transform);
                }
            }
        }
        for (int i = 0; i < _ufoPool.Count; i++)
        {
            if (_ufoPool[i].IsActive == true)
            {
                _ufoPool[i].SetDirection((_main.Player.Transform.position - _ufoPool[i].Transform.position).normalized);
                _ufoPool[i].Transform.Translate(_ufoPool[i].Direction * _ufoSpeed * Time.deltaTime);
            }
        }
    }


    private void SpawnBigAsteroid()
    {
        EnemyView asteroid = EnemyFromPool(EnemyType.BigAsteroid);
        asteroid.Transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length - 1)].position;
        asteroid.SetDirection(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized);
        asteroid.Activate();
        asteroid.gameObject.SetActive(true);
    }
    private void SpawnLittleAsteroid(Transform point)
    {
        EnemyView asteroid = EnemyFromPool(EnemyType.SmallAsteroid);
        asteroid.transform.position = point.position;
        asteroid.transform.rotation = point.rotation;
        asteroid.SetDirection(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized);
        asteroid.Activate();
        asteroid.gameObject.SetActive(true);
    }
    private void SpawnUFO()
    {
        EnemyView ufo = EnemyFromPool(EnemyType.UFO);
        ufo.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length - 1)].position;
        ufo.Activate();
        ufo.gameObject.SetActive(true);
    }

    private void BigAsteroidHit(Transform enemy)
    {
        for (int i = 0; i < Random.Range(1, _smallAsteroidMaxGroupSize + 1); i++)
        {
            SpawnLittleAsteroid(enemy);
        }

        BackToPool(enemy);
    }

    private bool CheckBounds(Vector3 position)
    {
        if (position.x > _main.Config.EnemyMaxX ||
            position.x < _main.Config.EnemyMinX ||
            position.y > _main.Config.EnemyMaxY ||
            position.y < _main.Config.EnemyMinY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region Pool
    private void BackToPool(Transform transform)
    {
        EnemyView enemy = transform.GetComponent<EnemyView>();
        enemy.Deactivate();
        enemy.Transform.position = _poolPosition;
        enemy.gameObject.SetActive(false);
    }

    private EnemyView EnemyFromPool(EnemyType type)
    {
        EnemyView enemy = null;

        switch (type)
        {
            case EnemyType.BigAsteroid:
                for (int i = 0; i < _bigAsteroidPool.Count; i++)
                {
                    if (_bigAsteroidPool[i].IsActive == false)
                    {
                        enemy = _bigAsteroidPool[i];
                        return enemy;
                    }
                }

                if (enemy == null)
                {
                    int random = Random.Range(0, _bigAsteroidPath.Length);
                    enemy = Object.Instantiate(Resources.Load<GameObject>(_bigAsteroidPath[random]), _poolPosition, Quaternion.identity).GetComponent<EnemyView>();
                    _bigAsteroidPool.Add(enemy);
                    BackToPool(enemy.Transform);
                }
                break;

            case EnemyType.SmallAsteroid:
                for (int i = 0; i < _smallAsteroidPool.Count; i++)
                {
                    if (_smallAsteroidPool[i].IsActive == false)
                    {
                        enemy = _smallAsteroidPool[i];
                        return enemy;
                    }
                }

                if (enemy == null)
                {
                    int random = Random.Range(0, _smallAsteroidPath.Length);
                    enemy = Object.Instantiate(Resources.Load<GameObject>(_smallAsteroidPath[random]), _poolPosition, Quaternion.identity).GetComponent<EnemyView>();
                    _smallAsteroidPool.Add(enemy);
                    BackToPool(enemy.Transform);
                }
                break;

            case EnemyType.UFO:
                for (int i = 0; i < _ufoPool.Count; i++)
                {
                    if (_ufoPool[i].IsActive == false)
                    {
                        enemy = _ufoPool[i];
                        return enemy;
                    }
                }

                if (enemy == null)
                {
                    enemy = Object.Instantiate(Resources.Load<GameObject>(_ufoPath), _poolPosition, Quaternion.identity).GetComponent<EnemyView>();
                    _ufoPool.Add(enemy);
                    BackToPool(enemy.Transform);
                }
                break;
        }
        return enemy;
    }
    #endregion
}