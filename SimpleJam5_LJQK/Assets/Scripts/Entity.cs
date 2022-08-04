using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health;
    public float max_health;
    //[System.NonSerialized]
    public bool is_ally;

    public virtual void removeHealth(float amount)
    {
        health -= amount;
        Debug.Log("Taken " + amount.ToString() + " damage, " + health.ToString() + "hp left");
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
