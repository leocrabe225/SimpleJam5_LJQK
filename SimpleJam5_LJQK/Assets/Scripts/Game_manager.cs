using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private bool attack_mode = false;
    [SerializeField]
    private Button craft_button;
    [SerializeField]
    private Button attack_button;

    public void spawn_entities_in_circle(GameObject prefab, int amount, Vector2 start, float safezone, float radius, Transform parent, bool is_ally) {
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
            GameObject temp_object = Instantiate(prefab, temp, new Quaternion(0,0,0,1), parent);
            temp_object.GetComponent<Entity>().is_ally = is_ally;
        }
    }

    void spawnZone1() {
        //spawn fighter robots
        spawn_entities_in_circle(fighter_robot_prefab, ZONE_1_AMOUNT, Vector2.zero, ZONE_1_SAFEZONE, ZONE_1_RADIUS, transform, false);
        //spawn dead machines
        spawn_entities_in_circle(dead_robot_prefab, ZONE_1_AMOUNT, Vector2.zero, 2f, ZONE_1_RADIUS, transform, true);
    }

    public void add_scraps(int amount) {
        scraps += amount;
        update_scraps_text();
        Debug.Log(scraps);
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
        spawnZone1();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (remove_scraps(5)) {
                spawn_entities_in_circle(fighter_robot_prefab, 1, player.transform.position, 1, 3, player.transform, true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            attack_mode = !attack_mode;
            if (attack_mode) {
                attack_button.GetComponent<Image>().color = Color.green;
                attack_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Fight mode on  (Space)";
            }
            else {
                attack_button.GetComponent<Image>().color = Color.red;
                attack_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Fight mode off (Space)";
            }
        }
        if (attack_mode) {
            player.GetComponent<Player>().time_no_fight = 0;
        }
    }
}