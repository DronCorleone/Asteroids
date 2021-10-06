using UnityEngine;

public class UIView : BaseObjectView
{
    [SerializeField] private BaseMenuView[] _menues;

    public BaseMenuView[] Menues => _menues;
}