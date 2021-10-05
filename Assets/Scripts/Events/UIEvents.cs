using System;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Current;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Current = this;
    }



}