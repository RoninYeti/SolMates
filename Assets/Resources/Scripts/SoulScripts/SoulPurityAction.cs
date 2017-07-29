using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace solmates {
    public class SoulPurityAction : MonoBehaviour {

        public Transform spawnTransform;
        private PlayerStats statsRef;
        private bool soulmaking = false;
        public int maxSoulAmount = 3;
        public static bool purSoulmade = false;
        public LayerMask lmask;
        public GameObject pureSoulObj;
        public GameObject soul;
        public GameObject planet;
        public GameObject planetParn;
        public GameObject planet1;
        public GameObject planet2;
        public GameObject planet3;
        public GameObject planet4;
        public float soulSpeed = 3f;
        public bool sendSoulAway = false;
        public float hitdistance = 20f;
        public float waitTimePureSoulCreation =5f;
        public AudioSource aSource;
        public AudioClip pureShot;
        public AudioClip planetHit;

        private void Awake() {
            statsRef = GetComponent<PlayerStats>();
            aSource = GetComponent<AudioSource>();
        }

        public void CreatePureSoul() {
            StartCoroutine(WaitThenDestory());
        }

        IEnumerator WaitThenDestory() {
            yield return new WaitForSeconds(waitTimePureSoulCreation);
            soul = Instantiate(pureSoulObj, spawnTransform.position, spawnTransform.rotation) as GameObject;
            planetParn.GetComponent<Light>().enabled = true;
            planet1.GetComponent<Light>().enabled = true;
            planet2.GetComponent<Light>().enabled = true;
            planet3.GetComponent<Light>().enabled = true;
            planet4.GetComponent<Light>().enabled = true;
            purSoulmade = true;
            soul.transform.parent = transform;
            PlayerStats.cleansouls -= maxSoulAmount;
            //print(PlayerStats.cleansouls + " clean soul static variable");
            //print(statsRef.cleanSoulsList.Count + " clean soul count");

            for (int i = 0; i < maxSoulAmount; i++) {
                if (statsRef.cleanSoulsList.Count != 0) {
                    Destroy(statsRef.cleanSoulsList[0]);
                    statsRef.cleanSoulsList.RemoveAt(0);
                }
            }
        }

        IEnumerator quickAnim() {
            purSoulmade = false;
            yield return new WaitForSeconds(2f);
            soul.GetComponent<Animator>().enabled = false;
            sendSoulAway = true;
        }

        void Update() {
            RaycastHit rhit;

            if (PlayerStats.cleansouls >= maxSoulAmount && !soulmaking) {
                soulmaking = true;
                CreatePureSoul();
            }

            if (purSoulmade) {

                if (Input.GetMouseButtonDown(0)) {

                    if (Physics.Raycast(transform.position, Camera.main.transform.forward, out rhit, Mathf.Infinity, lmask)) {
                        planet = rhit.transform.gameObject;
                        soul.transform.parent = null;
                        soul.GetComponent<Animator>().enabled = true;
                        aSource.PlayOneShot(pureShot);                                                                                          
                        StartCoroutine(quickAnim());
                    }
                }
            }

            if (sendSoulAway) {
                soul.transform.position = Vector3.MoveTowards(soul.transform.position, planet.transform.position, soulSpeed * Time.deltaTime);
                float dis = Vector3.Distance(planet.transform.position, soul.transform.position);

                if (dis < hitdistance) {
                    planet = null;
                    aSource.PlayOneShot(planetHit);                                                                                           
                    Destroy(soul.gameObject);
                    planetParn.GetComponent<Light>().enabled = false;
                    planet1.GetComponent<Light>().enabled = false;
                    planet2.GetComponent<Light>().enabled = false;
                    planet3.GetComponent<Light>().enabled = false;
                    planet4.GetComponent<Light>().enabled = false;
                    soulmaking = false;
                    sendSoulAway = false;
                }
            }
        }
    }
}
