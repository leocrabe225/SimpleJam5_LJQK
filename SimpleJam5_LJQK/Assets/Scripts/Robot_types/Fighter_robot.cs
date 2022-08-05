using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_robot : Robot
{
    void OnCollisionStay2D(Collision2D col)
    {
        Entity entity_hit = col.gameObject.GetComponent<Entity>();
        if (entity_hit && entity_hit.is_ally != is_ally) {
            if (attack_cooldown < 0) {
                Attack(entity_hit);
                attack_cooldown = cooldown_time;
            }
        }
    }

    protected override void attack_target() {
        Vector2 to_move = (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
        transform.Translate(to_move);
    }

}
