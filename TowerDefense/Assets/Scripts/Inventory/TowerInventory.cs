using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerInventory : MonoBehaviour
{
    public static TowerInventory Instance;

    public bool isGripTower = false;

    private void Awake()
    {
        Instance = this;
    }
}
