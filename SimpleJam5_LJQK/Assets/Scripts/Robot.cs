using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Entity
{
    [SerializeField]
    private GameObject dead_robot_prefab;
    protected float damage;
    protected float attack_cooldown;
    protected float cooldown_time;
    private bool game_on = true;
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

    private void OnDestroy() {
        if (game_on) {
            Instantiate(dead_robot_prefab, transform.position, new Quaternion(0,0,0,1));
        }
    }

    void OnApplicationQuit()
    {
        game_on = false;
    }

}
