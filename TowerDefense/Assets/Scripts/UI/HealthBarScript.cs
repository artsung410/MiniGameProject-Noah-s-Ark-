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
        // �����̴����� ü�������� �޾ƿ� ������ ����� ã�ƺ���
        //healthSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ObjectHP.transform.position + new Vector3(0f, 4f, 0f);
    }
}
