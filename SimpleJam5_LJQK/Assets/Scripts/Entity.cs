using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int health;
    void Start()
    {
        health = 10;
    }
    
    void Update()
    {
        if (health < 0)
            print("OH NO");
    }
    
    
}
