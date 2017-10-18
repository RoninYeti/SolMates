using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace solmates {
    public class Spawner : MonoBehaviour
    {

        [SerializeField]
        List<GameObject> Souls = new List<GameObject>();

        [SerializeField]
        float minDistance = 200f;

        [SerializeField]
        float maxDistance = 250f;

        [SerializeField]
        int soulsToSpawn;

        private GameObject soulPrefab;
        public GameObject EndGameUI;
		List<SoulAction> spawnedSouls = new List<SoulAction>();

        void Start()
        {
            for (int i = 1; i < soulsToSpawn; i++)
            {

                int choice = Random.Range(0, Souls.Count);
                soulPrefab = Souls[choice].gameObject;

                GameObject newSoul = Instantiate(soulPrefab);
                newSoul.transform.position = Random.onUnitSphere * ((maxDistance - minDistance) * Random.value + minDistance);
                SoulAction soulComp = newSoul.GetComponentInChildren<SoulAction>();
                soulComp.SpawnRef = this;
				spawnedSouls.Add (soulComp);
            }
        }

        public void RemoveSoul(SoulAction soul)
        {
            spawnedSouls.Remove(soul);
            Destroy(soul.gameObject);
        }

        public void SoulCheck(SoulPurityAction purity)
        {
            int remaining = 0;
            for (int i = 0; i < spawnedSouls.Count; i++)
            {
                remaining += spawnedSouls[i].SoulWorth;
            }

            if (remaining < purity.SoulWorthMaxAmount)
            {
                for (int i = 0; i < spawnedSouls.Count; i++)
                {
                    Destroy(spawnedSouls[i].gameObject);
                }
                spawnedSouls.Clear();
                EndGameUI.SetActive(true);
            }
        }

        void Update()
        {
            if(EndGameUI.activeSelf)
            {
                Ray quickRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward * 100f);
                EndGameUI.transform.position = Vector3.Lerp(EndGameUI.transform.position, quickRay.GetPoint(100f), Time.deltaTime);
                
                EndGameUI.transform.rotation = Quaternion.LookRotation(EndGameUI.transform.position - Camera.main.transform.position);
            }
        }
    }
}
