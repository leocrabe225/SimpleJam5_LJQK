using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{ 
    public GameObject player;
    public GameObject gameManager;

    [SerializeField]
    float speed = 10;
    //[SerializeField]
    //int spawnAmount = 1;
    [SerializeField]
    public GameObject robotToInstantiate;
    [SerializeField]
    int safeZone;
    [SerializeField]
    int totalRadius;
    public bool is_at_war;

    void Start()
    {
        //player = ;
        gameManager = transform.parent.parent.gameObject;
        player = gameManager.GetComponent<Game_manager>().player;
    }

    void Update()
    {
        //print(player.transform.position);
        is_at_war = gameManager.GetComponent<Game_manager>().attack_mode;
        float step = speed * Time.deltaTime;
        if (is_at_war) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position - (player.transform.position - transform.position).normalized * 10, step);
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Player>().spawn_new_robot(robotToInstantiate);
            Destroy(gameObject);
        }
    }
}