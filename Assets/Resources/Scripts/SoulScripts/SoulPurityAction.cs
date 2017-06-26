﻿using System.Collections;
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
        public float soulSpeed = 3f;
        public bool sendSoulAway = false;
        public float hitdistance = 20f;

        public AudioSource aSource;
        public AudioClip pureShot;
        public AudioClip planetHit;

        private void Awake() {
            statsRef = GetComponent<PlayerStats>();
        }

        public void CreatePureSoul() {
            StartCoroutine(WaitThenDestory());
        }

        IEnumerator WaitThenDestory() {
            yield return new WaitForSeconds(5f);
            soul = Instantiate(pureSoulObj, spawnTransform.position, spawnTransform.rotation) as GameObject;
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
                        //aSource.PlayOneShot(pureShot);                                                                                          Fix This Sound!!!
                        StartCoroutine(quickAnim());
                    }
                }
            }

            if (sendSoulAway) {
                soul.transform.position = Vector3.MoveTowards(soul.transform.position, planet.transform.position, soulSpeed * Time.deltaTime);
                float dis = Vector3.Distance(planet.transform.position, soul.transform.position);

                if (dis < hitdistance) {
                    planet = null;
                    //aSource.PlayOneShot(planetHit);                                                                                              Fix This Sound!!!
                    Destroy(soul.gameObject);
                    purSoulmade = false;
                    soulmaking = false;
                    sendSoulAway = false;
                }
            }
        }
    }
}
