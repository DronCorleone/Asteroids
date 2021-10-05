using UnityEngine;

public class EnemyView : BaseObjectView
{
    [SerializeField] private EnemyType _type;

    private Vector3 _direction;

    public EnemyType Type => _type;
    public Vector3 Direction => _direction;


    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerView player))
        {
            GameEvents.Current.GameOver();
        }
    }
}