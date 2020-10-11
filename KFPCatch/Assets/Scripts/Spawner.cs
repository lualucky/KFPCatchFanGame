using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour

{

    public bool Active;

    public GameObject Item;
    public GameObject Bomb;

    public float SpawnRate;
    public float BombPercent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Active &&  Random.value <= SpawnRate)
        {
            // regular chicken spawn
            if(Random.value >= BombPercent)
            {
                GameObject obj = Instantiate(Item);
            }
            // bad chicken spawn
            else
            {
                GameObject obj = Instantiate(Bomb);
            }
        }
    }
}
