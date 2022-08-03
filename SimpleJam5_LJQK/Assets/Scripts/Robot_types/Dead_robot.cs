using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_robot : MonoBehaviour
{
    [SerializeField]
    private GameObject Game_manager;
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collectible hovered by " + col.gameObject.name);
        Entity entity_hit =  col.gameObject.gameObject.GetComponent<Entity>();
        if (entity_hit.is_ally) {

        }
    }
}
