using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace solmates {
    public class Spawner : MonoBehaviour {

        [SerializeField]
        List<GameObject> Souls = new List<GameObject>();

        [SerializeField]
        float minDistance = 200f;

        [SerializeField]
        float maxDistance = 250f;

        [SerializeField]
        int soulsToSpawn;

        private GameObject soulPrefab;

        void Start() {
            for (int i = 1; i < soulsToSpawn; i++) {

                int choice = Random.Range(0, Souls.Count);
                soulPrefab = Souls[choice].gameObject;

                GameObject newSoul = Instantiate(soulPrefab);
                newSoul.transform.position = Random.onUnitSphere * ((maxDistance - minDistance) * Random.value + minDistance);
            }
        }

        void Update() {
        }
    }
}
