using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : BaseController, IExecute
{
    private List<BulletView> _bulletPool;
    private LaserView[] _laserPool;
    private int _bulletPoolSize = 10;


    public WeaponController(MainController main) : base(main)
    {
    }


    public override void Initialize()
    {
        base.Initialize();
    }

    public void Execute()
    {

    }

}