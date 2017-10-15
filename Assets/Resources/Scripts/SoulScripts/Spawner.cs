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
        public int soulCount;
        public GameObject EndGameUI;

        void Start()
        {
            soulCount = soulsToSpawn;
            for (int i = 1; i < soulsToSpawn; i++)
            {

                int choice = Random.Range(0, Souls.Count);
                soulPrefab = Souls[choice].gameObject;

                GameObject newSoul = Instantiate(soulPrefab);
                newSoul.transform.position = Random.onUnitSphere * ((maxDistance - minDistance) * Random.value + minDistance);
                newSoul.GetComponentInChildren<SoulAction>().SpawnRef = this;
            }
        }

        public void SoulCheck()
        {
            soulCount--;
            if (soulCount == 0)
            {
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
