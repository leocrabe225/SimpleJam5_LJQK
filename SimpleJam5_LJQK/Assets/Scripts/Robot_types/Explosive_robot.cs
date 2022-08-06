using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_robot : Robot
{
    private bool exploded = false;

    void OnCollisionStay2D(Collision2D col)
    {
        if (is_at_war) {
            Entity entity_hit = col.gameObject.GetComponent<Entity>();
            if (entity_hit && entity_hit.is_ally != is_ally && !exploded) {
                RaycastHit2D[] results = new RaycastHit2D[50];
                ContactFilter2D filter_test = new ContactFilter2D();
                filter_test.SetLayerMask(layers[is_ally ? 0 : 1]);
                int amount = Physics2D.CircleCast(transform.position, 1.5f, Vector2.zero, filter_test, results, 0);
                for (var i = 0; i < amount; i++)
                {
                    results[i].transform.GetComponent<Entity>().removeHealth(damage);
                }
                exploded = true;
                if (transform.parent.GetComponent<Outpost>()) {
                    transform.parent.GetComponent<Outpost>().FetchChildNbr();
                }
                Destroy(gameObject);
            }
        }
    }

    protected override void attack_target() {
        Vector2 to_move = (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
        transform.Translate(to_move);
    }
}
