using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    private void OnEnable()
    {
        StartCoroutine(Deactivation());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    IEnumerator Deactivation()
    {
        yield return new WaitForSeconds(lifeTime);
        BulletPool.ReturnObject(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<LivingEntity>().TakeDamage(Damage);
            BulletPool.ReturnObject(this);
        }
    }
}
