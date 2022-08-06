using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outpost : Entity
{

    GameObject player;


    [SerializeField]
    protected GameObject gameManager;
    [SerializeField]
    GameObject robotToInstantiate;
    [SerializeField]
    GameObject drone;
    [SerializeField]
    int spawnAmount;
    [SerializeField]
    protected int safeZone;
    [SerializeField]
    int totalRadius;
    [SerializeField]
    float deliveryCoolDown;
    [SerializeField]
    protected Sprite allyOutpostSprite;
    [SerializeField]
    private int game_progression;

    protected  int army_left;
    float time;
    public bool boss_reached = false;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.GetComponent<Boss_outpost>()) {
            gameManager = transform.parent.GetComponent<Boss_outpost>().gameManager;
        }
        else {
            gameManager = transform.parent.gameObject;
        }
        gameManager.GetComponent<Game_manager>().spawn_entities_in_circle(robotToInstantiate, spawnAmount, transform.position, safeZone, totalRadius, transform, false);
        army_left = spawnAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_ally) {
            time += Time.deltaTime;
            if (time >= deliveryCoolDown)
            {
                time = 0.0f;
                if (!boss_reached) {
                    Deliver();
                }
            }
        }
    }

    public virtual void FetchChildNbr()
    {
        army_left--;

        if (army_left == 0)
        {   
            if (transform.parent.GetComponent<Boss_outpost>()) {
                Destroy(gameObject);
            }
            else {
                transform.parent.GetComponent<Game_manager>().acknowledge_outpost_death(game_progression);
                is_ally = true;
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allyOutpostSprite;
            }
        }
    }

    void Deliver()
    {
        GameObject temp = Instantiate(drone, transform);
        temp.GetComponent<Drone>().robotToInstantiate = robotToInstantiate;
        temp.GetComponent<Drone>().is_at_war = transform.parent.GetComponent<Game_manager>().attack_mode;
    }

    void OnDestroy() {
        if (gameObject.scene.isLoaded && transform.parent.GetComponent<Boss_outpost>()) {
            transform.parent.GetComponent<Boss_outpost>().FetchChildNbr();
        }
    }
}
