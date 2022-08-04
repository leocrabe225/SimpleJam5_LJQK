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
    private float regen_speed;
    public LayerMask masktest;

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
                if (!regen_on) {
                    regen_speed = (max_health - health) / 10;
                }
                regen_on = true;
            }
            else {
                regen_on = false;
            }
        }
        else {
            regen_on = false;
        }
        if (regen_on) {
            health += regen_speed * Time.deltaTime;
        }
    }

    public bool order_attack() {
        var i = 0;
        float farest_robot = 0;
        foreach(Transform child in transform)
        {
            if (child.GetComponent<Robot>()) {
                if (child.localPosition.magnitude > farest_robot) {
                    farest_robot = child.localPosition.magnitude;
                }
                i++;
            }
        }
        //Debug.Log(i.ToString() + "robots");
        //Debug.Log("farest : " + farest_robot.ToString());
        RaycastHit2D[] results = new RaycastHit2D[200];
        ContactFilter2D filter_test = new ContactFilter2D();
        filter_test.SetLayerMask(masktest);
        int amount = Physics2D.CircleCast(transform.position, farest_robot + 3, Vector2.zero, filter_test, results, 0);
        Debug.Log("hits : " + amount.ToString());
        //Debug.Log("hits : " + Physics2D.CircleCast(transform.position, farest_robot + 3, Vector2.zero, new ContactFilter2D(), results, 0).ToString());
        if (amount > 0) {
            foreach(Transform child in transform)
            {
                if (child.GetComponent<Robot>()) {
                    child.GetComponent<Robot>().target = results[Random.Range(0, amount)].transform.gameObject;
                    child.GetComponent<Robot>().is_at_war = true;
                }
            }
        }
        return (amount > 0); 
    }

    public void order_defense() {
        foreach(Transform child in transform)
        {
            if (child.GetComponent<Robot>()) {
                child.GetComponent<Robot>().is_at_war = false;
            }
        }
    }
}
