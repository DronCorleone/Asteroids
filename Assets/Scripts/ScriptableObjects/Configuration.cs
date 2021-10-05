using UnityEngine;


[CreateAssetMenu]
public class Configuration : ScriptableObject
{
    [Header("Player")]
    public float PlayerMoveSpeed;
    public float PlayerRotateSpeed;
    public float PlayerAcceleration;

    [Header("Game field")]
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float EnemyMinX;
    public float EnemyMaxX;
    public float EnemyMinY;
    public float EnemyMaxY;
}