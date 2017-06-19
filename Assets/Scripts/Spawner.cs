using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    List<GameObject> Souls = new List<GameObject>();
   
    [SerializeField]
    float timeBetweenSpawns = 5;
    float timer;
    [SerializeField]
    float minDistance = 200f;
    [SerializeField]
    float maxDistance = 250f;
    [SerializeField]
    int soulsToSpawn;

   private GameObject soulPrefab;
    // Use this for initialization
    void Start () {

        int choice = Random.Range(0,Souls.Count);
        soulPrefab = Souls[choice].gameObject;
        for (int i = 1; i < soulsToSpawn; i++) {
            GameObject newSoul = Instantiate(soulPrefab);
            newSoul.transform.position = Random.onUnitSphere * ((maxDistance - minDistance) * Random.value + minDistance);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
