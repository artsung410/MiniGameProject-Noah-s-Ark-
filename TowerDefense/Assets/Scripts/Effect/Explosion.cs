using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    void OnEnable()
    {
        StartCoroutine(Deactivation());
    }

    IEnumerator Deactivation()
    {
        yield return new WaitForSeconds(lifeTime);
        ExplosionPool.ReturnObject(this);
    }
}
