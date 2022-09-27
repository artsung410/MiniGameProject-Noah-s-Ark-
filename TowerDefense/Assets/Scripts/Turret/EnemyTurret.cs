using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Turret
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            onFire = true;
            isDetactive = true;
            Vector3 dir = other.gameObject.transform.position - BulletSpawnPoint.position;
            Gun.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            onFire = false;
        }
    }

    public override void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (onFire)
            {
                Bullet bullet = BulletPool.GetObject();
                bullet.gameObject.transform.position = BulletSpawnPoint.position;
                bullet.gameObject.transform.rotation = BulletSpawnPoint.rotation;
            }
        }
    }
}
