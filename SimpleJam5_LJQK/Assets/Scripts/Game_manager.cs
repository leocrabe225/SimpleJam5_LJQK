using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_manager : MonoBehaviour
{
    //constants
    int ZONE_1_SCRAPS = 50;
    int ZONE_1_ROCKS = 50;
    int ZONE_1_LVL1_ROBOTS = 50;

    int ZONE_2_SCRAPS = 50;
    int ZONE_2_ROCKS = 30;
    int ZONE_2_LVL1_ROBOTS = 50;
    int ZONE_2_LVL1_OUTPOSTS = 20;

    int ZONE_3_SCRAPS = 50;
    int ZONE_3_ROCKS = 100;
    int ZONE_3_LVL1_ROBOTS = 150;
    int ZONE_3_LVL2_ROBOTS = 300;
    //int ZONE_3_LVL1_OUTPOSTS = 20;
    int ZONE_3_LVL2_OUTPOSTS = 20;

    int ZONE_4_SCRAPS = 50;
    int ZONE_4_ROCKS = 200;
    int ZONE_4_LVL1_ROBOTS = 300;
    int ZONE_4_LVL2_ROBOTS = 600;
    int ZONE_4_LVL3_ROBOTS = 1200;
    //int ZONE_4_LVL1_OUTPOSTS = 20;
    //int ZONE_4_LVL2_OUTPOSTS = 20;
    int ZONE_4_LVL3_OUTPOSTS = 20;



    float ZONE_1_SAFEZONE = 10;
    float ZONE_1_RADIUS = 50;
    float ZONE_2_RADIUS = 100;
    float ZONE_3_RADIUS = 200;
    float ZONE_4_RADIUS = 350;
    float ZONE_5_RADIUS = 500;
    [SerializeField]
    private GameObject player_prefab;
    [SerializeField]
    private GameObject fighter_robot_prefab;
    [SerializeField]
    private GameObject shooter_robot_prefab;
    [SerializeField]
    private GameObject explosive_robot_prefab;
    [SerializeField]
    private GameObject snowpiercer_robot_prefab;
    [SerializeField]
    private GameObject dead_robot_prefab;
    [SerializeField]
    private GameObject outpost_lvl1_prefab;
    [SerializeField]
    private GameObject outpost_lvl2_prefab;
    [SerializeField]
    private GameObject outpost_lvl3_prefab;
    [SerializeField]
    private GameObject outpost_lvl4_prefab;
    [SerializeField]
    private GameObject outpost_final_prefab;
    [SerializeField]
    private GameObject rocks_prefab;
    public GameObject player;
    //[System.NonSerialized]
    public int scraps;
    [SerializeField]
    private TextMeshProUGUI scraps_text;
    public bool attack_mode = false;
    [SerializeField]
    private Button craft_button;
    [SerializeField]
    private Button attack_button;

    public GameObject spawn_entities_in_circle(GameObject prefab, int amount, Vector2 start, float safezone, float radius, Transform parent, bool is_ally) {
        GameObject temp_object = null;
        for (var i=0;i < amount;i++) {
            var x = Random.Range(0f, 1f);
            if (Random.Range(0f, 2f) > 1) {
                x *= -1;
            }
            var y = Random.Range(0f, 1f);
            if (Random.Range(0f, 2f) > 1) {
                y *= -1;
            }
            Vector2 temp = new Vector2(x,y);
            temp = temp.normalized * Random.Range(safezone, radius) + start;
            temp_object = Instantiate(prefab, temp, new Quaternion(0,0,0,1), parent);

            temp_object.GetComponent<Entity>().is_ally = is_ally;
            
        }
        return (temp_object);
    }

    //Where the player spawns, no outposts
    void spawnZone1() {
        //spawn fighter robots
        spawn_entities_in_circle(fighter_robot_prefab, ZONE_1_LVL1_ROBOTS, Vector2.zero, ZONE_1_SAFEZONE, ZONE_1_RADIUS, transform, false);
        //spawn dead machines
        spawn_entities_in_circle(dead_robot_prefab, ZONE_1_SCRAPS, Vector2.zero, 2f, ZONE_1_RADIUS, transform, true);
        //spawn rocks
        spawn_entities_in_circle(rocks_prefab, ZONE_1_ROCKS, Vector2.zero, 4f, ZONE_1_RADIUS, transform, false);
    }

    //Outpots lvl 1
    void spawnZone2() {
        //spawn fighter robots
        spawn_entities_in_circle(fighter_robot_prefab, ZONE_2_LVL1_ROBOTS, Vector2.zero, ZONE_1_RADIUS, ZONE_2_RADIUS, transform, false);
        //spawn dead machines
        spawn_entities_in_circle(dead_robot_prefab, ZONE_2_SCRAPS, Vector2.zero, ZONE_1_RADIUS, ZONE_2_RADIUS, transform, true);
        //spawn outposts
        spawn_entities_in_circle(outpost_lvl1_prefab, ZONE_2_LVL1_OUTPOSTS , Vector2.zero, ZONE_1_RADIUS, ZONE_2_RADIUS, transform, false);
        //spawn rocks
        spawn_entities_in_circle(rocks_prefab, ZONE_2_ROCKS, Vector2.zero, ZONE_1_RADIUS, ZONE_2_RADIUS, transform, false);
    }

    //Outpots lvl 2
    void spawnZone3() {
        spawn_entities_in_circle(fighter_robot_prefab, ZONE_3_LVL1_ROBOTS, Vector2.zero, ZONE_2_RADIUS, ZONE_3_RADIUS, transform, false);
        spawn_entities_in_circle(shooter_robot_prefab, ZONE_3_LVL2_ROBOTS, Vector2.zero, ZONE_2_RADIUS, ZONE_3_RADIUS, transform, false);
        //spawn dead machines
        spawn_entities_in_circle(dead_robot_prefab, ZONE_3_SCRAPS, Vector2.zero, ZONE_2_RADIUS, ZONE_3_RADIUS, transform, true);
        //spawn outposts
        spawn_entities_in_circle(outpost_lvl2_prefab, ZONE_3_LVL2_OUTPOSTS , Vector2.zero, ZONE_2_RADIUS, ZONE_3_RADIUS, transform, false);
        //spawn rocks
        spawn_entities_in_circle(rocks_prefab, ZONE_3_ROCKS, Vector2.zero, ZONE_2_RADIUS, ZONE_3_RADIUS, transform, false);
    }

    //Outpots lvl 3
    void spawnZone4() {
        spawn_entities_in_circle(fighter_robot_prefab, ZONE_4_LVL1_ROBOTS, Vector2.zero, ZONE_3_RADIUS, ZONE_4_RADIUS, transform, false);
        spawn_entities_in_circle(shooter_robot_prefab, ZONE_4_LVL2_ROBOTS, Vector2.zero, ZONE_3_RADIUS, ZONE_4_RADIUS, transform, false);
        spawn_entities_in_circle(explosive_robot_prefab, ZONE_4_LVL3_ROBOTS, Vector2.zero, ZONE_3_RADIUS, ZONE_4_RADIUS, transform, false);
        //spawn dead machines
        spawn_entities_in_circle(dead_robot_prefab, ZONE_4_SCRAPS, Vector2.zero, ZONE_3_RADIUS, ZONE_4_RADIUS, transform, true);
        //spawn outposts
        spawn_entities_in_circle(outpost_lvl3_prefab, ZONE_4_LVL3_OUTPOSTS , Vector2.zero, ZONE_3_RADIUS, ZONE_4_RADIUS, transform, false);
        //spawn rocks
        spawn_entities_in_circle(rocks_prefab, ZONE_4_ROCKS, Vector2.zero, ZONE_3_RADIUS, ZONE_4_RADIUS, transform, false);
    }

    /*Outpots lvl 4
    void spawnZone5() {
        spawn_entities_in_circle(fighter_robot_prefab, ??????????, Vector2.zero, ZONE_4_RADIUS, ZONE_5_RADIUS, transform, false);
        //spawn dead machines
        spawn_entities_in_circle(dead_robot_prefab, ZONE_5_SCRAPS, Vector2.zero, ZONE_4_RADIUS, ZONE_5_RADIUS, transform, true);
        //spawn outposts
        spawn_entities_in_circle(outpost_lvl4_prefab, ZONE_5_LVL4_OUTPOSTS , Vector2.zero, ZONE_4_RADIUS, ZONE_5_RADIUS, transform, false);
        //spawn rocks
        spawn_entities_in_circle(rocks_prefab, ZONE_5_ROCKS, Vector2.zero, ZONE_4_RADIUS, ZONE_5_RADIUS, transform, false);
    }*/

    /*Boss zone
    void spawnZoneFinal() {
        //spawn fighter robots
        spawn_entities_in_circle(fighter_robot_prefab, ??????????, Vector2.zero, ZONE_1_RADIUS, ZONE_2_RADIUS, transform, false);
        //spawn dead machines
        spawn_entities_in_circle(dead_robot_prefab, ??????????, Vector2.zero, ZONE_1_RADIUS, ZONE_2_RADIUS, transform, true);
        //spawn outposts
        spawn_entities_in_circle(outpost_final_prefab, 20 , Vector2.zero, ZONE_1_RADIUS, ZONE_2_RADIUS, transform, false);
        //spawn rocks
        spawn_entities_in_circle(rocks_prefab, 30, Vector2.zero, ZONE_1_RADIUS, ZONE_2_RADIUS, transform, false);
    }*/

    public void add_scraps(int amount) {
        scraps += amount;
        update_scraps_text();
    }

    public bool remove_scraps(int amount) {
        if (scraps >= amount) {
            scraps -= amount;
            update_scraps_text();
            return true;
        }
        else {
            return false;
        }
    }

    void update_scraps_text() {
        scraps_text.text = "Scraps : " + scraps.ToString();
        if (scraps >= 5) {
            craft_button.interactable = true;
        }
        else {
            craft_button.interactable = false;
        }
    }

    void Start()
    {
        scraps = 0;
        player = Instantiate(player_prefab);
        player.GetComponent<Player>().game_Manager = gameObject;
        spawnZone1();
        spawnZone2();
        spawnZone3();
        spawnZone4();
        //spawnZone5();
        //spawnZoneFinal();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (remove_scraps(5)) {
                GameObject robot = player.GetComponent<Player>().spawn_new_robot(fighter_robot_prefab);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            attack_mode = !attack_mode;
            if (attack_mode) {
                if (player.GetComponent<Player>().order_attack()) {
                    attack_button.GetComponent<Image>().color = Color.green;
                    attack_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Fight mode on  (Space)";
                    gameObject.GetComponent<MainMenu>().AttackMode();
                }
                else {
                    attack_mode = false;
                }
            }
            else {
                player.GetComponent<Player>().order_defense();
                attack_button.GetComponent<Image>().color = Color.red;
                attack_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Fight mode off (Space)";
                gameObject.GetComponent<MainMenu>().DefenseMode();
            }
        }
        if (attack_mode) {
            player.GetComponent<Player>().time_no_fight = 0;
        }
    }
}