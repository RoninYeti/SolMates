using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    GameObject soulPrefab;
    [SerializeField]
    float timeBetweenSpawns = 5;
    float timer;
    [SerializeField]
    float minDistance = 200f;
    [SerializeField]
    float maxDistance = 250f;
    [SerializeField]
    int soulsToSpawn;
	// Use this for initialization
	void Start () {
        for (int i = 1; i < soulsToSpawn; i++) {
            GameObject newSoul = Instantiate(soulPrefab);
            newSoul.transform.position = Random.onUnitSphere * ((maxDistance - minDistance) * Random.value + minDistance);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
