using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour

{

    public bool Active;

    public GameObject Item;
    public GameObject Bomb;
    public GameObject Hat;

    [HideInInspector]
    public bool HatEvent;

    public float HatEventSpawnRate;
    public float SpawnRate;

    [HideInInspector]
    public bool Broken;

    public float RegularBombPercent;
    public float BrokenBombPercent;
    public float HatPercent;

    public float Height;
    public float Width;

    public float DoubleSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CrankUpSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (Active && Random.value <= (HatEvent ? HatEventSpawnRate : SpawnRate))
        {
            // regular chicken spawn
            if(!HatEvent && (Random.value < (Broken ? BrokenBombPercent : RegularBombPercent)))
            {
                Spawn(Bomb);
            }
            // bad chicken spawn
            else if(!HatEvent && Random.value < HatPercent)
            {
                Spawn(Hat);
            }
            else
            {
                Spawn(Item);
            }
        }
    }

    IEnumerator CrankUpSpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(DoubleSpawnTime);
            SpawnRate *= 1.5f;
            RegularBombPercent *= 1.25f;
        }
    }

    void Spawn(GameObject item)
    {
        GameObject obj = Instantiate(item);
        obj.transform.position = new Vector2(Width * Random.Range(-1f, 1f), Height);
    }
}
