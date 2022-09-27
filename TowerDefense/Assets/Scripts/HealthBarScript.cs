using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public GameObject ObjectHP;

    //private Slider healthSlider;

    private void Awake()
    {
        // 슬라이더에서 체력정보를 받아와 적용할 방법을 찾아보자
        //healthSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ObjectHP.transform.position + new Vector3(0f, 4f, 0f);
    }
}
