using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Entity
{
    protected float damage;
    protected float attack_cooldown;
    protected float cooldown_time;
    void Start()
    {
        damage = 5;
        cooldown_time = 1;
        attack_cooldown = cooldown_time;
    }

    void Update()
    {
        attack_cooldown -= Time.deltaTime;
    }

    protected void Attack(Entity target)
    {
        target.removeHealth(damage);
    }
}
