using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Start is called before the first frame update
    private float speed = 5;
    public bool regen_on = false;
    public GameObject sprite;
    public float time_no_fight;


    [SerializeField]
    private float rotationSpeed;



    void Start()
    {
        health = max_health;
        is_ally = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 to_move = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) {
            to_move += Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S)) {
            to_move += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A)) {
            to_move += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D)) {
            to_move += Vector2.right;
        }
        to_move = to_move.normalized * speed * Time.deltaTime;
        transform.Translate(to_move);

        if(to_move != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, to_move);
            sprite.transform.rotation = Quaternion.RotateTowards(sprite.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        //Uncomment when aggro system is done
        time_no_fight += Time.deltaTime;
        if (time_no_fight > 10) {
            if (time_no_fight < 20.5f) {
                regen_on = true;
            }
            else {
                regen_on = false;
            }
        }
        else {
            regen_on = false;
        }
    }
}
