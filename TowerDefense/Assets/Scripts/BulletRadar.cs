using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRadar : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    private void OnTriggerStay(Collider other)
    {
        //if (other.tag == "Enemy")
        //{
        //    if (AllyHowitzerTurret._target == null)
        //    {
        //        return;
        //    }

        //    Vector3 dir = AllyHowitzerTurret._target.position - transform.position;
        //    bullet.transform.rotation = Quaternion.LookRotation(dir.normalized);
        //    bullet.transform.Translate(bullet.transform.forward);
        //}
    }
}
