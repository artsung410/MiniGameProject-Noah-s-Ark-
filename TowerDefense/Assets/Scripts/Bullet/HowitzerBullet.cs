using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowitzerBullet : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private int Damage;
    [SerializeField] private float lifeTime;

    Rigidbody rb;

    Vector3 prevVeclocity;
    Vector3 PrevPos;
    Quaternion prevRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PrevPos = transform.position;
        prevRotation = transform.rotation;
        prevVeclocity = rb.velocity;
    }

    private void OnEnable()
    {
        StartCoroutine(Deactivation());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<LivingEntity>().TakeDamage(Damage);
            Explosion explosion = ExplosionPool.GetObject();
            explosion.gameObject.transform.position = transform.position;
            explosion.gameObject.transform.rotation = Quaternion.identity;
        }
    }

    IEnumerator Deactivation()
    {
        yield return new WaitForSeconds(lifeTime);

        Explosion explosion = ExplosionPool.GetObject();
        explosion.gameObject.transform.position = transform.position;
        explosion.gameObject.transform.rotation = Quaternion.identity;

        transform.position = PrevPos;
        transform.rotation = prevRotation;
        rb.velocity = prevVeclocity;
        HowitzerBulletPool.ReturnObject(this);
    }
}
