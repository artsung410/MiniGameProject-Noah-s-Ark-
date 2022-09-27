using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : LivingEntity
{
    public bool IsDead;
    protected float Speed;

    public override void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        IsDead = false;
        Speed = MoveSpeed;
    }
}
