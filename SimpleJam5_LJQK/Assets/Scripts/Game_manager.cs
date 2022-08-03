using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_manager : MonoBehaviour
{
    //constants
    float ZONE_1_AMOUNT = 50;
    float ZONE_1_SAFEZONE = 10;
    float ZONE_1_RADIUS = 50;

    [SerializeField]
    private GameObject player_prefab;
    [SerializeField]
    private GameObject fighter_robot_prefab;
    private GameObject player;

    void spawnZone1() {
        for (var i=0;i < ZONE_1_AMOUNT;i++) {
            var x = Random.Range(0f, 1f);
            if (Random.Range(0f, 2f) > 1) {
                x *= -1;
            }
            var y = Random.Range(0f, 1f);
            Debug.Log(Random.Range(0f, 2f));
            if (Random.Range(0f, 2f) > 1) {
                y *= -1;
            }
            Vector2 temp = new Vector2(x,y);
            temp = temp.normalized * Random.Range(ZONE_1_SAFEZONE, ZONE_1_RADIUS);
            Instantiate(fighter_robot_prefab, temp, new Quaternion(0,0,0,1));
        }
    }

    void Start()
    {
        player = Instantiate(player_prefab);
        spawnZone1();
    }

    void Update()
    {
        
    }
}