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

        if (BuildingTower.name == "Ally_GunTower") // ��Ÿ��Ŭ��
        {
            if (GameManager.Instance.PlayerGold - GunTowerPrice < 0) // ���� ������������ ��Ÿ�� ��ġ��뺸�� ������
            {
                return; //����
            }
            Debug.Log("��Ÿ��");
            // ��Ÿ�� ��ġ���� -> Ŭ������
 
            TowerInventory.Instance.isGripTower = true;
            TowerButtonClickSignal(SilhouetteTower, Unable_SilhouetteTower, BuildingTower, GunTowerPrice);
        }

        else if(BuildingTower.name == "Ally_HowitzerTower(New)")
        {
            if (GameManager.Instance.PlayerGold - HowitzerTowerPrice < 0)
            {
                return;
            }

            Debug.Log("�����");

            TowerInventory.Instance.isGripTower = true;
            TowerButtonClickSignal(SilhouetteTower, Unable_SilhouetteTower, BuildingTower, HowitzerTowerPrice);
        }
    }
}
