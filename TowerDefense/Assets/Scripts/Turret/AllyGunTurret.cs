using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyGunTurret : Turret
{
    private void Start()
    {
        StartCoroutine(Fire());
    }

    private void Update()
    {
        if (false == onFire)
        {
            RotatedTurret.transform.Rotate(0, -1f * Time.deltaTime * RotateSpeed, 0);
        }

        // 범위내에 적이 존재하는지 확인해줄것
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            onFire = true;
            isDetactive = true;
            Vector3 dir = other.gameObject.transform.position - BulletSpawnPoint.position;
            Gun.transform.rotation = Quaternion.LookRotation(dir);
        }
        
    }

    

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        onFire = false;
    //    }
    //}

    public override void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (onFire)
            {
                Bullet bullet = BulletPool.GetObject();
                bullet.gameObject.transform.position = BulletSpawnPoint.position;
                bullet.gameObject.transform.rotation = BulletSpawnPoint.rotation;
            }
        }
    }
}
