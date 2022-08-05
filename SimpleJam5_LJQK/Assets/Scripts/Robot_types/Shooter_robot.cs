using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter_robot : Robot
{
    private float attack_range = 4;
    [SerializeField]
    private GameObject bullet_prefab;
    private float bullet_cooldown = 0.2f;
    private float bullet_timer = 0;

    protected override void attack_target() {
        bullet_timer += Time.deltaTime;
        if ((target.transform.position - transform.position).magnitude > attack_range) {
            Vector2 to_move = (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
            transform.Translate(to_move);
        }
        else if (bullet_timer > bullet_cooldown){
            GameObject bullet = Instantiate(bullet_prefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().target = target;
            bullet.GetComponent<Bullet>().damage = damage;
            bullet_timer = 0;
        }
    }
}
