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

    public float Height;
    public float Width;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Active && Random.value <= SpawnRate)
        {
            // regular chicken spawn
            if(Random.value >= BombPercent)
            {
                Spawn(Item);
            }
            // bad chicken spawn
            else
            {
                Spawn(Bomb);
            }
        }
    }

    void Spawn(GameObject item)
    {
        GameObject obj = Instantiate(item);
        obj.transform.position = new Vector2(Width * Random.Range(-1f, 1f), Height);
    }
}
