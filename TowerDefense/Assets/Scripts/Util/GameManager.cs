using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : SingletoneBehaviour<GameManager>
{
    [SerializeField] private TextMeshProUGUI _goldText;

    #region �÷��̾����

    public float PlayerSpeed;
    private PlayerDB playerData;
    public int PlayerGold;

    #endregion



    #region ��ž����
    public int TowerDamage;
    public float TowerAttackArea;
    public float TowerAttackSpeed;

    public int TowerHP;
    public int TowerLevel;

    public int TowerPrice;


    private TowerDB toweData;

    #endregion





    #region ������
    public float EnemySpeed;
    public int EnemyHP;
    public float EnemyAttackSpeed;

    public int EnemyExp;
    public int EnemyGold;
    public float EnemyAttackArea;

    public int EnemySpawnCount;
    public int EnemyDeathCount;

    private EnemyDB enemyData;

    #endregion


    #region ���̺����

    private WaveDB waveData;

    #endregion

    private void Awake()
    {
        //waveData = DataManager.Instance.GetWaveData(1);
        //enemyData = DataManager.Instance.GetEnemyData(1);
    }

    private void Start()
    {
        //Debug.Log(waveData.ID);
        //Debug.Log(enemyData.Move_Speed);

        PlayerGold = 3000;
        GameOverAvaliable = false;
    }

    public bool GameOverAvaliable;

    private void Update()
    {
        _goldText.text = $"Gold : {PlayerGold}";


        if (EnemySpawnCount == EnemyDeathCount)
        {
            Debug.Log("������");
            GameOverAvaliable = true;
        }
    }
}
