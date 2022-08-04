using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Start is called before the first frame update
    private float TIME_TO_START_REGEN = 10;
    private float REGEN_TIME = 10;

    bool isAlive = true;
    private float speed = 5;
    public bool regen_on = false;
    public GameObject sprite;
    public float time_no_fight;
    private float regen_speed;
    public LayerMask masktest;

    [SerializeField]
    private float rotationSpeed;
    private bool[][] rings_status;
    private Vector2[][] rings_positions;

    public GameObject temp_ring;
    public GameObject game_Manager;


    void Start()
    {
        health = max_health;
        is_ally = true;
        generate_rings(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Vector2 to_move = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                to_move += Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                to_move += Vector2.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                to_move += Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                to_move += Vector2.right;
            }
            to_move = to_move.normalized * speed * Time.deltaTime;
            transform.Translate(to_move);

            if (to_move != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, to_move);
                sprite.transform.rotation = Quaternion.RotateTowards(sprite.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            //Uncomment when aggro system is done
            time_no_fight += Time.deltaTime;
            if (time_no_fight > TIME_TO_START_REGEN)
            {
                if (time_no_fight < TIME_TO_START_REGEN + REGEN_TIME + 0.5f)
                {
                    if (!regen_on)
                    {
                        regen_speed = (max_health - health) / REGEN_TIME;
                    }
                    regen_on = true;
                }
                else
                {
                    regen_on = false;
                }
            }
            else
            {
                regen_on = false;
            }
            if (regen_on)
            {
                health += regen_speed * Time.deltaTime;
            }
        }
    }

    private void generate_rings(float spacing, float start) {
        int ring_amount = 5;
        float rad_distance;
        rings_positions = new Vector2[ring_amount][];
        rings_status = new bool[ring_amount][];
        for (var y=0; y < ring_amount; y++) {
            float circumference = Mathf.PI * (start + spacing * y) * 2;
            int spots = Mathf.FloorToInt(circumference);
            rings_positions[y] = new Vector2[spots];
            rings_status[y] = new bool[spots];

            rad_distance = 0;
            for (var i = 0; i < spots; i++){
                rad_distance = 2 * Mathf.PI * ((float)i / (float)spots);
                float sin = Mathf.Sin(rad_distance);
                float cos = Mathf.Cos(rad_distance);
                rings_positions[y][i] = new Vector2(cos * (start + spacing * y),sin * (start + spacing * y));
                Instantiate(temp_ring, rings_positions[y][i], new Quaternion(0,0,0,1));
            }
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
        List<Robot> robot_list = new List<Robot>();
        foreach(Transform child in transform)
        {
            if (child.GetComponent<Robot>()) {
                Robot robot = child.GetComponent<Robot>();
                robot.is_at_war = false;
                robot_list.Add(robot);
            }
        }
        Debug.Log("List size : " + robot_list.Count.ToString());
        List<Robot>.Enumerator em = robot_list.GetEnumerator();
        em.MoveNext();
        for (var y=0; y < rings_status.Length; y ++) {
            for (var i=0; i < rings_status[y].Length; i++) {
                if (em.Current){ 
                    Debug.Log("em.Current : " + em.Current);
                    em.Current.defense_target = rings_positions[y][i];
                    rings_status[y][i] = true;
                    em.MoveNext();
                }
                else {
                    Debug.Log("em.Current : " + em.Current);
                    rings_status[y][i] = false;
                }

            }
        }
    }

    public override void removeHealth(float amount)
    {
        health -= amount;
        Debug.Log("Taken " + amount.ToString() + " damage, " + health.ToString() + "hp left");
        if (health <= 0 && !is_immortal)
        {
            isAlive = false;
            game_Manager.GetComponent<MainMenu>().Death();
            
        }
    }
}
