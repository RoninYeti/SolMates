using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace solmates {
    public class SoulPurityAction : MonoBehaviour {

        public Transform spawnTransform;
        private PlayerStats statsRef;
        private bool soulmaking = false;
        public static int maxSoulAmount = 3;
        public static bool purSoulmade = false;
        public LayerMask lmask;
        public GameObject pureSoulObj;
        public GameObject soul;
        public GameObject planet;
        public GameObject sunLight;
        public GameObject planetParn;
        public GameObject planet1;
        public GameObject planet2;
        public GameObject planet3;
        public GameObject planet4;
        public float soulSpeed = 2f;
        public float ToReadySpeed = 3f;
        public bool sendSoulAway = false;
        public float hitdistance = 20f;
        public float waitTimePureSoulCreation =5f;
        public AudioSource aSource;
        public AudioClip pureShot;
        public AudioClip planetHit;
        private float intensity;
        public float lightTransitionSpeed = .05f;
        private void Awake() {
            statsRef = GetComponent<PlayerStats>();
            aSource = GetComponent<AudioSource>();
        }

        public void CreatePureSoul() {
            StartCoroutine(WaitThenDestory());
        }

        IEnumerator WaitThenDestory() {

            for (int i = 0; i < maxSoulAmount; i++)
            {
                if (statsRef.cleanSoulsList.Count != 0)
                {
                    Destroy(statsRef.cleanSoulsList[0]);
                    statsRef.cleanSoulsList.RemoveAt(0);
                }
            }

            yield return new WaitForSeconds(waitTimePureSoulCreation);
            purSoulmade = true;
            soul = Instantiate(pureSoulObj, spawnTransform.position, spawnTransform.rotation) as GameObject;
            float tempintensity;
            tempintensity = sunLight.GetComponent<Light>().intensity;
            intensity = sunLight.GetComponent<Light>().intensity;
            while (tempintensity > .01f)
            {
                tempintensity -= .01f;
                yield return new WaitForSeconds(lightTransitionSpeed);
                sunLight.GetComponent<Light>().intensity = tempintensity;
            }
            planetParn.GetComponent<Light>().enabled = true;
            planet1.GetComponent<Light>().enabled = true;
            planet2.GetComponent<Light>().enabled = true;
            planet3.GetComponent<Light>().enabled = true;
            planet4.GetComponent<Light>().enabled = true;
  
            soul.transform.parent = transform;
            PlayerStats.cleansouls -= maxSoulAmount;
        }

        IEnumerator ReadyToFly(Ray line) {
            purSoulmade = false;
            Vector3 vec = line.GetPoint(2f);
            float dis = Vector3.Distance(soul.transform.position, vec);
            while (dis > .2f)
            {
                soul.transform.position = Vector3.MoveTowards(soul.transform.position, vec,Time.deltaTime * ToReadySpeed);
                yield return new WaitForSeconds(.01f);
                dis = Vector3.Distance(soul.transform.position, vec);
            }

            yield return new WaitForSeconds(1.5f);
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
                        Ray quickRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward *5f);
                        planet = rhit.transform.gameObject;
                        soul.transform.parent = null;
                      
                        aSource.PlayOneShot(pureShot);                                                                                          
                        StartCoroutine(ReadyToFly(quickRay));
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
                    sunLight.GetComponent<Light>().enabled = true;
                    planetParn.GetComponent<Light>().enabled = false;
                    planet1.GetComponent<Light>().enabled = false;
                    planet2.GetComponent<Light>().enabled = false;
                    planet3.GetComponent<Light>().enabled = false;
                    planet4.GetComponent<Light>().enabled = false;
                    soulmaking = false;
                    StartCoroutine(bringBackUp());
                    sendSoulAway = false;
                }
            }
        }

        IEnumerator bringBackUp()
        {
            float tempintensity;
            tempintensity = sunLight.GetComponent<Light>().intensity;
            while (tempintensity<=intensity)
            {
                tempintensity += .01f;
                sunLight.GetComponent<Light>().intensity = tempintensity;
                yield return new WaitForSeconds(lightTransitionSpeed);
            }
        }
    }
}
