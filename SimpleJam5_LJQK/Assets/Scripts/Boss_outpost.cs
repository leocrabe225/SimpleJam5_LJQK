using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_outpost : Outpost
{
    [SerializeField]
    private GameObject[] outpost_array;

    void Start()
    {
        gameManager = transform.parent.gameObject;
        //gameManager.GetComponent<Game_manager>().spawn_entities_in_circle(robotToInstantiate, spawnAmount, transform.position, safeZone, totalRadius, transform, false);*/
        army_left = 0;
        generate_rings(10, safeZone + 5);
    }

    void Update() {

    }

    private void generate_rings(float spacing, float start) {
        float rad_distance;
        int ring_amount = 2;

        for (var y=0; y < ring_amount; y++) {
            float circumference = Mathf.PI * (start + spacing * y) * 2;
            int spots = Mathf.FloorToInt(circumference / spacing);
            army_left += spots;

            rad_distance = 0;
            for (var i = 0; i < spots; i++){
                rad_distance = 2 * Mathf.PI * ((float)i / (float)spots);
                float sin = Mathf.Sin(rad_distance);
                float cos = Mathf.Cos(rad_distance);
                Instantiate(outpost_array[Random.Range(0, outpost_array.Length)], transform.position + (new Vector3(cos * (start + spacing * y),sin * (start + spacing * y))), Quaternion.identity, transform);
            }
        }
    }

    public override void FetchChildNbr()
    {
        army_left--;
        Debug.Log("Outposts left to win : " + army_left);
        if (army_left == 0)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allyOutpostSprite;
            gameManager.GetComponent<MainMenu>().Win();
        }
    }

}
