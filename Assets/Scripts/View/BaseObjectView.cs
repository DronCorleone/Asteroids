using UnityEngine;


public class BaseObjectView : MonoBehaviour
{
    public Transform Transform => transform;

    protected bool _isActive = false;

    public bool IsActive => _isActive;

    public void Activate()
    {
        _isActive = true;
    }
    public void Deactivate()
    {
        _isActive = false;
    }
}