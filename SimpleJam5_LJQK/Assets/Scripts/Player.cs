using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Start is called before the first frame update
    private float speed = 5;


    [SerializeField]
    private float rotationSpeed;



    void Start()
    {
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }




        Debug.Log("boite en fer (c'est mieux)");
        /*if (transform.position.x > 10.7f) {
            Vector2 temp = new Vector2(-10.7f, transform.position.y);
            transform.position = temp;
        }
        if (transform.position.x < -10.7f) {
            Vector2 temp = new Vector2(10.7f, transform.position.y);
            transform.position = temp;
        }
        if (transform.position.y > 5f) {
            Vector2 temp = new Vector2(transform.position.x, -5f);
            transform.position = temp;
        }
        if (transform.position.y < -5f) {
            Vector2 temp = new Vector2(transform.position.x, 5f);
            transform.position = temp;
        }*/
    }
}
