using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_manager : MonoBehaviour
{
    //constants
    int ZONE_1_AMOUNT = 50;
    float ZONE_1_SAFEZONE = 10;
    float ZONE_1_RADIUS = 50;

    [SerializeField]
    private GameObject player_prefab;
    [SerializeField]
    private GameObject fighter_robot_prefab;
    [SerializeField]
    private GameObject dead_robot_prefab;
    private GameObject player;
    [System.NonSerialized]
    public int scraps;
    [SerializeField]
    private TextMeshProUGUI scraps_text;

    public void spawn_entities_in_circle(GameObject prefab, int amount, Vector2 start, float safezone, float radius, Transform parent) {
        for (var i=0;i < amount;i++) {
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
            temp = temp.normalized * Random.Range(safezone, radius) + start;
            Instantiate(prefab, temp, new Quaternion(0,0,0,1), parent);
        }
    }

    void spawnZone1() {
        //spawn fighter robots
        spawn_entities_in_circle(fighter_robot_prefab, ZONE_1_AMOUNT, Vector2.zero, ZONE_1_SAFEZONE, ZONE_1_RADIUS, transform);
        //spawn dead machines
        spawn_entities_in_circle(dead_robot_prefab, ZONE_1_AMOUNT, Vector2.zero, 2f, ZONE_1_RADIUS, transform);
    }

    public void add_scraps(int amount) {
        scraps += amount;
        scraps_text.text = "Scraps : " + scraps.ToString();
        Debug.Log(scraps);
    }

    void Start()
    {
        scraps = 0;
        player = Instantiate(player_prefab);
        spawnZone1();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            if (scraps >= 5) {
                scraps -= 5;
            }
        }
    }
}