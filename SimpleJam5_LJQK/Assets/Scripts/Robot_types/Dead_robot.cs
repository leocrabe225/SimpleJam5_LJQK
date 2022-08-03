using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_robot : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collectible hovered by " + col.gameObject.name);
        Entity entity_hit =  col.gameObject.gameObject.GetComponent<Entity>();
        if (entity_hit.is_ally) {
            transform.parent.GetComponent<Game_manager>().add_scraps(1);
            Destroy(gameObject);
        }
    }
}
