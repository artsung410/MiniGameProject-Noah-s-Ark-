using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [Header("Turret")]
    [SerializeField] protected Transform BulletSpawnPoint;
    [SerializeField] protected GameObject RotatedTurret;
    [SerializeField] protected GameObject Gun;
    [SerializeField] protected GameObject BulletPrefabs;

    [Header("TurretOption")]
    [SerializeField] protected float RotateSpeed;
    [SerializeField] protected float Health;

    public bool onFire = false;
    public bool isDetactive = false;

    public abstract void TakeDamage(int damage);

    protected abstract IEnumerator Fire();
}
