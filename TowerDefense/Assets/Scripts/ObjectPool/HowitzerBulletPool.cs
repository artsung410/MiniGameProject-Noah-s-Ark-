using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowitzerBulletPool : MonoBehaviour
{
    public static HowitzerBulletPool Instance;
    public GameObject HowitzerBulletPrefab;
    public int initActivationCount;

    private Queue<HowitzerBullet> Q = new Queue<HowitzerBullet>();

    private void Awake()
    {
        Instance = this;
        Initilize(initActivationCount);
    }

    private HowitzerBullet CreateNewObject()
    {
        var newObj = Instantiate(HowitzerBulletPrefab, transform).GetComponent<HowitzerBullet>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    private void Initilize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Q.Enqueue(CreateNewObject());
        }
    }

    public static HowitzerBullet GetObject()
    {
        if (Instance.Q.Count > 0)
        {
            var obj = Instance.Q.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public static void ReturnObject(HowitzerBullet obj)
    {
        obj.gameObject.SetActive(false);
        Instance.Q.Enqueue(obj);
    }
}
