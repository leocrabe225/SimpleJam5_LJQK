using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{ 
    public GameObject player;
    public GameObject gameManager;

    [SerializeField]
    float speed = 10;
    [SerializeField]
    int spawnAmount = 1;
    [SerializeField]
    GameObject robotToInstantiate;
    [SerializeField]
    int safeZone;
    [SerializeField]
    int totalRadius;
    // Start is called before the first frame update
    void Start()
    {
        //player = ;
        gameManager = transform.parent.parent.gameObject;
        player = gameManager.GetComponent<Game_manager>().player;
    }

    // Update is called once per frame
    void Update()
    {
        //print(player.transform.position);
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("works");
            gameManager.GetComponent<Game_manager>().spawn_entities_in_circle(robotToInstantiate, spawnAmount, transform.position, safeZone, totalRadius, transform, true);
            Destroy(gameObject);
        }
    }


}
