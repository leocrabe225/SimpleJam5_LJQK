using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health;
    //[System.NonSerialized]
    public bool is_ally;
    public bool is_immortal;

    public void removeHealth(float amount)
    {
        health -= amount;
        Debug.Log("Taken " + amount.ToString() + " damage, " + health.ToString() + "hp left");
        if (health <= 0 && !is_immortal) {
            Destroy(gameObject);
        }
    }
}
