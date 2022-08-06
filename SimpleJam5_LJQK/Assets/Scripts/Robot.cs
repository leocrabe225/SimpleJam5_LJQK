using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Robot : Entity
{
    private float REGEN_TIME = 5;
    [SerializeField]
    private GameObject dead_robot_prefab;
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float speed;
    protected float attack_cooldown;
    protected float cooldown_time;
    private bool regenerating = false;
    private float regen_speed;
    
    public GameObject target = null;
    public bool is_at_war = false;
    public Vector3 defense_target;
    public int[] layers;

    public GameObject sprite;
    [SerializeField]
    protected float vision_range;
    [SerializeField]
    List<Sprite> robotSprites;


    void Start()
    {
        gameObject.layer = layers[is_ally ? 1 : 0];
        layers[0] = (int)Mathf.Pow(2, layers[0]);
        layers[1] = (int)Mathf.Pow(2, layers[1]);
        defense_target = transform.localPosition;
        health = max_health;
        cooldown_time = 1;
        attack_cooldown = cooldown_time;
        if (!is_ally) {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = robotSprites[1];
            speed -= 1;
            is_at_war = true;
        }
        else {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = robotSprites[0];
        }
    }

    void Update()
    {
        attack_cooldown -= Time.deltaTime;
        if (is_ally)
        {
            if (!regenerating && transform.parent.GetComponent<Player>().regen_on) {
                regenerating = true;
                regen_speed = (max_health - health) / REGEN_TIME;
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
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
                sprite.transform.rotation = Quaternion.RotateTowards(sprite.transform.rotation, toRotation, 360 * Time.deltaTime);
                //sprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
                attack_target();
            }
            else {
                RaycastHit2D[] results = new RaycastHit2D[10];
                ContactFilter2D filter_test = new ContactFilter2D();
                filter_test.SetLayerMask(layers[is_ally ? 0 : 1] );//layers[is_ally ? 0 : 1]);
                int amount = Physics2D.CircleCast(transform.position, vision_range, Vector2.zero, filter_test, results, 0);
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
                    if (is_ally) {
                        transform.parent.GetComponent<Player>().new_target = target;
                    }
                }
                else {
                    if (is_ally) {
                        target = transform.parent.GetComponent<Player>().new_target;
                    }
                    if (!target) {
                        Vector2 to_move = (defense_target - transform.localPosition).normalized * speed * Time.deltaTime;
                        if ((defense_target - transform.localPosition).magnitude > speed * Time.deltaTime) {
                            transform.Translate(to_move);
                        }
                        else {
                            transform.localPosition = defense_target;
                        }
                        if (is_ally) {
                            sprite.transform.rotation = Quaternion.RotateTowards(sprite.transform.rotation, transform.parent.GetComponent<Player>().sprite.transform.rotation, 360 * Time.deltaTime);
                        }
                    }
                }
            }
        }
        else {
            Vector2 to_move = (defense_target - transform.localPosition).normalized * speed * Time.deltaTime;
            if ((defense_target - transform.localPosition).magnitude > speed * Time.deltaTime) {
                transform.Translate(to_move);
            }
            else {
                transform.localPosition = defense_target;
            }
            if (is_ally){
                //sprite.transform.rotation = transform.parent.GetComponent<Player>().sprite.transform.rotation;
                sprite.transform.rotation = Quaternion.RotateTowards(sprite.transform.rotation, transform.parent.GetComponent<Player>().sprite.transform.rotation, 360 * Time.deltaTime);
            }
        }
    }

    protected abstract void attack_target();

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
        if (gameObject.scene.isLoaded) {
            Instantiate(dead_robot_prefab, transform.position, new Quaternion(0,0,0,1));
        }
    }
}