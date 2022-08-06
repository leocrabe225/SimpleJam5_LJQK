using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_robot : Entity
{
    public int scrap_amount;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject game_manager = GameObject.Find("Game Manager");
        Entity entity_hit =  col.gameObject.GetComponent<Entity>();
        if (entity_hit.is_ally) {
            game_manager.GetComponent<MainMenu>().Win();
            game_manager.GetComponent<Game_manager>().add_scraps(scrap_amount);
            scrap_amount = 0;
            Destroy(gameObject);
        }
    }
}
