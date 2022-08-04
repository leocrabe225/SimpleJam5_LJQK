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
    void Start()
    {
        health = max_health;
        damage = 5;
        cooldown_time = 1;
        attack_cooldown = cooldown_time;
    }

    void Update()
    {
        attack_cooldown -= Time.deltaTime;
        if (transform.parent.name == "Main Character(Clone)")
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
