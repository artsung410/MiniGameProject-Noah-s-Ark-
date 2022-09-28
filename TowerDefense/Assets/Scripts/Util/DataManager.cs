using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDB
{
    public int ID;
    public int Gold;
    public int Level;
    public int Max_Exp;
    public float Move_Speed;

    public int Attack;
    public float Attack_Speed;
    public float Attack_Range;

    public int Hp;
    public float Build_Range;


    #region 플레이어관련 생성자
    public PlayerDB(int iD, int gold, int level, int max_Exp, float move_Speed, int attack, float attack_Speed, float attack_Range, int hp, float build_Range)
    {
        ID = iD;
        Gold = gold;
        Level = level;
        Max_Exp = max_Exp;
        Move_Speed = move_Speed;
        Attack = attack;
        Attack_Speed = attack_Speed;
        Attack_Range = attack_Range;
        Hp = hp;
        Build_Range = build_Range;
    }


    #endregion



}
public class EnemyDB
{
    public int ID;
    public string Name;
    public int HP;
    public float Move_Speed;
    public int Attack;
    public float Attack_speed;
    public float Attack_Range;
    public int Exp;


    #region 적관련 생성자
    public EnemyDB(int iD, string name, int hP, float move_Speed, int attack, float attack_speed, float attack_Range, int exp)
    {
        ID = iD;
        Name = name;
        HP = hP;
        Move_Speed = move_Speed;
        Attack = attack;
        Attack_speed = attack_speed;
        Attack_Range = attack_Range;
        Exp = exp;
    }


    #endregion
}
public class TowerDB
{
    public int ID;
    public string Name;
    public int Gold;
    public int Level;
    public int Upgrade;
    public int Next_Upgrade;
    
    public int Attack;
    public float Attack_range;
    public float Attack_Speed;


    #region 타워관련 생성자
    public TowerDB(int iD, string name, int gold, int level, int upgrade, int next_Upgrade, int attack, float attack_range, float attack_Speed)
    {
        ID = iD;
        Name = name;
        Gold = gold;
        Level = level;
        Upgrade = upgrade;
        Next_Upgrade = next_Upgrade;
        Attack = attack;
        Attack_range = attack_range;
        Attack_Speed = attack_Speed;
    }


    #endregion
}
public class WaveDB
{
    public int ID;

    public int Enemy_ID;
    public int Enemy_Value;

    public int Enemy2_ID;
    public int Enemy2_Value;

    public int Enemy3_ID;
    public int Enemy3_Value;

    public int Spawn_Time;



    #region 웨이브관련 생성자
    public WaveDB(int iD, int enemy_ID, int enemy_Value, int enemy2_ID, int enemy2_Value, int enemy3_ID, int enemy3_Value, int spawn_Time)
    {
        ID = iD;
        Enemy_ID = enemy_ID;
        Enemy_Value = enemy_Value;
        Enemy2_ID = enemy2_ID;
        Enemy2_Value = enemy2_Value;
        Enemy3_ID = enemy3_ID;
        Enemy3_Value = enemy3_Value;
        Spawn_Time = spawn_Time;
    }



    #endregion

}


public class DataManager : MonoBehaviour
{
    // 내부에서만 값수정(set)가능, 외부에선 get 가능
    // public int Index { get; private set; }


    static GameObject container;
    static GameObject Container { get => container; }

    static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                container = new GameObject();
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;

                // 함수가 처음 만들어질때 wave 불러옴 
                instance.SetWaveDataFromCsv();
                instance.SetEnemyDataFromCsv();
                instance.SetPlayerDataFromCsv();
                instance.SetTowerDataFromCsv();



                DontDestroyOnLoad(container);
            }

            return instance;
        }
    }

    public string GameDataFileName = ".json";


    #region WAVE

    [Header("Wave관련 DB")]
    [SerializeField] TextAsset waveDB;

    public Dictionary<int, WaveDB> waveDataDict { get; set; }

    private void SetWaveDataFromCsv()
    {
        waveDB = Resources.Load<TextAsset>("Csv/WaveDB");
        if (waveDB == null)
        {
            Debug.LogError("데이터 비어있음");
        }

        if (waveDataDict == null)
        {
            waveDataDict = new Dictionary<int, WaveDB>();
        }

        string[] waveCsvLines = waveDB.text.Substring(0, waveDB.text.Length).Split('\n');
        for (int i = 3; i < waveCsvLines.Length; i++)
        {
            // 한줄당 정보저장중
            // 0번째에는 설명이므로 받을 필요없다
            // 1번째부터 필요한부분이므로 i = 1시작
            string[] row = waveCsvLines[i].Split(',');
            waveDataDict.Add(int.Parse(row[0]), new WaveDB
                (
                int.Parse(row[0]),
                int.Parse(row[1]),
                int.Parse(row[2]),
                int.Parse(row[3]),
                int.Parse(row[4]),
                int.Parse(row[5]),
                int.Parse(row[6]),
                int.Parse(row[7])
                ));
        }
    }

    public WaveDB GetWaveData(int idx)
    {
        Debug.Log("Wave Data 호출");

        if (waveDataDict.ContainsKey(idx))
        {
            return waveDataDict[idx];
        }
        Debug.LogWarning("인덱스 데이터 없음");
        return null;
    }
    #endregion


    #region PLAYER

    [Header("Player관련 DB")]
    [SerializeField] TextAsset playerDB;

    public Dictionary<int, PlayerDB> playerDataDict { get; set; }

    private void SetPlayerDataFromCsv()
    {
        playerDB = Resources.Load<TextAsset>("Csv/PlayerDB");
        if (playerDB == null)
        {
            Debug.LogError("데이터 비어있음");
        }

        if (playerDataDict == null)
        {
            playerDataDict = new Dictionary<int, PlayerDB>();
        }

        string[] playerCsvLines = playerDB.text.Substring(0, playerDB.text.Length).Split('\n');
        for (int i = 3; i < playerCsvLines.Length; i++)
        {
            string[] row = playerCsvLines[i].Split(',');
            playerDataDict.Add(int.Parse(row[0]), new PlayerDB
                (
                int.Parse(row[0]),
                int.Parse(row[1]),
                int.Parse(row[2]),
                int.Parse(row[3]),
                float.Parse(row[4]),
                int.Parse(row[5]),
                float.Parse(row[6]),
                float.Parse(row[7]),
                int.Parse(row[8]),
                float.Parse(row[9])
                ));
        }
    }

    public PlayerDB GetPlayerData(int idx)
    {
        Debug.Log("Player Data 호출");

        if (playerDataDict.ContainsKey(idx))
        {
            return playerDataDict[idx];
        }
        Debug.LogWarning("인덱스 데이터 없음");
        return null;
    }

    #endregion


    #region ENEMY

    [Header("Enemy관련 DB")]
    [SerializeField] TextAsset enemyDB;

    public Dictionary<int, EnemyDB> EnemyDataDict { get; set; }

    private void SetEnemyDataFromCsv()
    {
        enemyDB = Resources.Load<TextAsset>("Csv/EnemyDB");
        if (enemyDB == null)
        {
            Debug.LogError("데이터 비어있음");
            return;
        }

        if (EnemyDataDict == null)
        {
            EnemyDataDict = new Dictionary<int, EnemyDB>();
        }

        string[] enemyCsvLines = enemyDB.text.Substring(0, enemyDB.text.Length).Split('\n');
        for (int i = 3; i < enemyCsvLines.Length; i++)
        {
            string[] row = enemyCsvLines[i].Split('\n');
            EnemyDataDict.Add(int.Parse(row[0]), new EnemyDB(
                int.Parse(row[0]),
                row[1],
                int.Parse(row[2]),
                float.Parse(row[3]),
                int.Parse(row[4]),
                float.Parse(row[5]),
                float.Parse(row[6]),
                int.Parse(row[7])
                ));
        }
    }


    public EnemyDB GetEnemyData(int idx)
    {
        Debug.Log("Enemy Data 호출");
        if (EnemyDataDict.ContainsKey(idx))
        {
            return EnemyDataDict[idx];
        }
        Debug.LogWarning("인덱스 데이터 없음");
        return null;

    }


    #endregion


    #region TOWER

    [Header("Tower관련 DB")]
    [SerializeField] TextAsset towerDB;

    public Dictionary<int, TowerDB> towerDataDict { get; set; }

    private void SetTowerDataFromCsv()
    {
        if(towerDB == null)
        {
            return;
        }

        if (towerDataDict == null)
        {
            towerDataDict = new Dictionary<int, TowerDB>();
        }

        string[] towerCsvLines = towerDB.text.Substring(0, towerDB.text.Length).Split('\n');
        for (int i = 3; i < towerCsvLines.Length; i++)
        {
            string[] row = towerCsvLines[i].Split('\n');
            towerDataDict.Add(int.Parse(row[0]), new TowerDB(
                int.Parse(row[0]),
                row[1],
                int.Parse(row[2]),
                int.Parse(row[3]),
                int.Parse(row[4]),
                int.Parse(row[5]),
                int.Parse(row[6]),
                float.Parse(row[7]),
                float.Parse(row[8])
                ));
        }
    }

    public TowerDB GetTowerData(int idx)
    {
        Debug.Log("Tower Data 호출");

        if (towerDataDict.ContainsKey(idx))
        {
            return towerDataDict[idx];
        }

        return null;

    }

    #endregion


}
