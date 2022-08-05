using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [System.NonSerialized]
    public GameObject target;
    [System.NonSerialized]
    public float damage;
    private float speed = 30;
    
    void Update()
    {
        if (target) {
            if ((target.transform.position - transform.position).magnitude > speed * Time.deltaTime) {
                Vector2 to_move = (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
                transform.Translate(to_move);
            }
            else {
                target.GetComponent<Entity>().removeHealth(damage);
                Destroy(gameObject);
            }
        }
        else {
            Destroy(gameObject);
        }
    }
}
