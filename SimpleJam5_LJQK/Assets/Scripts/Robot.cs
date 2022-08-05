using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Entity
{
    [SerializeField]
    private GameObject dead_robot_prefab;
    protected float damage;
    protected float attack_cooldown;
    protected float cooldown_time;
    private bool game_on = true;
    private bool regenerating = false;
    private float regen_speed;
    private float speed = 5;
    public GameObject target = null;
    public bool is_at_war = false;
    public Vector3 defense_target;
    public LayerMask masktest;
    public GameObject sprite;
    [SerializeField]
    List<Sprite> robotSprites;


    void Start()
    {
        if (!is_ally) {
            gameObject.layer = 10;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = robotSprites[1];
        }
        else {
            defense_target = transform.localPosition;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = robotSprites[0];
        }
        health = max_health;
        damage = 5;
        cooldown_time = 1;
        attack_cooldown = cooldown_time;
    }

    void Update()
    {
        attack_cooldown -= Time.deltaTime;
        if (is_ally)
        {
            if (!regenerating && transform.parent.GetComponent<Player>().regen_on) {
                regenerating = true;
                regen_speed = (max_health - health) / 10;
            }
            else if (regenerating) {
                if (!transform.parent.GetComponent<Player>().regen_on) {
                    regenerating = false;
                }
                else {
                    health += regen_speed * Time.deltaTime;
                    if (health > max_health) {
                        health = max_health;
                    }
                }
            }
        }
        if (is_at_war) {
            if (!(target == null)) {
                Vector2 to_move = (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
                transform.Translate(to_move);
            }
            else {
                RaycastHit2D[] results = new RaycastHit2D[10];
                ContactFilter2D filter_test = new ContactFilter2D();
                filter_test.SetLayerMask(masktest);
                int amount = Physics2D.CircleCast(transform.position, 3, Vector2.zero, filter_test, results, 0);
                float closest = Mathf.Infinity;
                if (amount > 0) {
                    for (var i = 0; i < amount; i++)
                    {
                        float temp = (results[i].transform.position - transform.position).magnitude;
                        if (temp < closest) {
                            target = results[i].transform.gameObject;
                            closest = temp;
                        }
                    }
                }
            }
        }
        else if (is_ally) {
            Vector2 to_move = (defense_target - transform.localPosition).normalized * speed * Time.deltaTime;
            if ((defense_target - transform.localPosition).magnitude > speed * Time.deltaTime) {
                transform.Translate(to_move);
            }
            else {
                transform.localPosition = defense_target;
            }
            sprite.transform.rotation = transform.parent.GetComponent<Player>().sprite.transform.rotation;
        }
    }

    protected void Attack(Entity target)
    {
        target.removeHealth(damage);
    }

    public override void removeHealth(float amount)
    {
        if (transform.parent.name == "Main Character(Clone)") {
            transform.parent.GetComponent<Player>().time_no_fight = 0;
        }
        health -= amount;
        if (health <= 0) {
            if (transform.parent.GetComponent<Outpost>())
            {
                transform.parent.GetComponent<Outpost>().FetchChildNbr();
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        if (game_on) {
            Instantiate(dead_robot_prefab, transform.position, new Quaternion(0,0,0,1));
        }
    }

    void OnApplicationQuit()
    {
        game_on = false;
    }

}
