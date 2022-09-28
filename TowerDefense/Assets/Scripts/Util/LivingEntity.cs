using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour
{
    [SerializeField] protected int Health;
    [SerializeField] protected int Damage;
    [SerializeField] protected float MoveSpeed;
    [SerializeField] protected float AttackSpeed;
    [SerializeField] protected float RotateSpeed;

    public abstract void TakeDamage(int damage);
}

