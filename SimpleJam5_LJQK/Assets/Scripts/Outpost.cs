using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outpost : Entity
{

    GameObject player;


    [SerializeField]
    GameObject gameManager;
    [SerializeField]
    GameObject robotToInstantiate;
    [SerializeField]
    GameObject drone;
    [SerializeField]
    int spawnAmount;
    [SerializeField]
    int safeZone;
    [SerializeField]
    int totalRadius;
    [SerializeField]
    float deliveryCoolDown;
    [SerializeField]
    Sprite allyOutpostSprite;


    int initialchildNbr;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = transform.parent.gameObject;
        gameManager.GetComponent<Game_manager>().spawn_entities_in_circle(robotToInstantiate, spawnAmount, transform.position, safeZone, totalRadius, transform, false);

        int childNbr = transform.childCount;

    }

    // Update is called once per frame
    void Update()
    {
        if (is_ally) {
            time += Time.deltaTime;
            if (time >= deliveryCoolDown)
            {
                time = 0.0f;
                Deliver();
            }
        }
    }

    public void FetchChildNbr()
    {
        int childNbr = transform.childCount-1;

        if (childNbr <=1)
        {
            is_ally = true;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allyOutpostSprite;
        }
    }

    void Deliver()
    {
        GameObject temp = Instantiate(drone, transform);
        temp.GetComponent<Drone>().robotToInstantiate = robotToInstantiate;
        temp.GetComponent<Drone>().is_at_war = transform.parent.GetComponent<Game_manager>().attack_mode;
    }

}
