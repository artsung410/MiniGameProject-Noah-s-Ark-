using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Dictionary<string, object>> waveData = CSVReader.Read("Csv/WaveDB");

        Debug.Log(waveData[0]["Wave"].ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
