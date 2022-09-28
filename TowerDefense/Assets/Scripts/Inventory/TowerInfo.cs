using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class TowerInfo : MonoBehaviour
{
    public static event Action<GameObject, GameObject, GameObject, int> TowerButtonClickSignal = delegate { };
    public static TowerInfo Instance;

    public int ID;
    public GameObject SilhouetteTower;
    public GameObject Unable_SilhouetteTower;
    public GameObject BuildingTower;

    public int GunTowerPrice;
    public int HowitzerTowerPrice;

    private void Awake()
    {
        Instance = this;
    }

    public void BtnClick()
    {
        //TowerInventory.Instance.isGripTower = true;
        //TowerButtonClickSignal(SilhouetteTower, Unable_SilhouetteTower, BuildingTower);

        if (BuildingTower.name == "Ally_GunTower") // 건타워클릭
        {
            if (GameManager.Instance.PlayerGold - GunTowerPrice < 0) // 만약 내가가진돈이 건타워 설치비용보다 작으면
            {
                return; //리턴
            }
            Debug.Log("총타워");
            // 건타워 설치가능 -> 클릭가능
 
            TowerInventory.Instance.isGripTower = true;
            TowerButtonClickSignal(SilhouetteTower, Unable_SilhouetteTower, BuildingTower, GunTowerPrice);
        }

        else if(BuildingTower.name == "Ally_HowitzerTower(New)")
        {
            if (GameManager.Instance.PlayerGold - HowitzerTowerPrice < 0)
            {
                return;
            }

            Debug.Log("곡사포");

            TowerInventory.Instance.isGripTower = true;
            TowerButtonClickSignal(SilhouetteTower, Unable_SilhouetteTower, BuildingTower, HowitzerTowerPrice);
        }
    }
}
