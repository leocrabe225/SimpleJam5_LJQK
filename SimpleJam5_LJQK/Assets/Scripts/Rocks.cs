using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rocks : Entity
{
    [SerializeField]
    List<Sprite> RockSprites;
    // Start is called before the first frame update
    void Awake()
    {
        int random = Random.Range(0, 3);
        gameObject.GetComponent<SpriteRenderer>().sprite = RockSprites[random];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
