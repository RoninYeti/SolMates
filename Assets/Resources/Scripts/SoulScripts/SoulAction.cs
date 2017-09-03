using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace solmates {
    public class SoulAction : MonoBehaviour {

        //is soul clean?
        public bool clean = false;

        //reference to friendlysoul
        public FriendlySoul friendsoulref;

        //time to look at based on size
        private float waitTimebySize = 0;

        //simple counter to keep count
        private float counter = 0;

        //reference to animator
        private Animator anim;

        //are you being looked at by player
        private bool lookedAt = false;

        //gameboject to create when dead or small
        public GameObject cleanSoulobj;

        //scale of object;
        private Vector3 soulScale;

        //size of sample
        public float samplesize = .2f;

        //reference to the player
        private Transform player;
        public float CounterSizeTime = .1f;
        public int SoulWorth = 0;

        //sound effects
        public AudioSource aSource;
        public AudioClip cleansingSoul;
        public AudioClip cleansedSoul;

        //ref;
      private  SoulPurityAction PurityActionRef;

        //scale down size
        public float ScaleDownAmount = 3f;

        public List<int> sizeone = new List<int>();
        public List<int> sizetwo = new List<int>();
        public List<int> sizethree = new List<int>();

        public Spawner SpawnRef;


        void ThreeSizes(int tempInt)
        {
            switch (tempInt)
            {
                case 1:
                    SoulWorth = Random.Range(sizeone[0], sizeone[1]);
                    break;
                case 2:
                    SoulWorth = Random.Range(sizetwo[0], sizetwo[1]);
                    break;
                case 3:
                    SoulWorth = Random.Range(sizethree[0], sizethree[1] + 1);
                    break;
            }
                       
            soulScale = new Vector3(tempInt,tempInt,tempInt);
            transform.localScale = soulScale;
        }

        void ManySizes( int tempsize)
        {
            SoulWorth = Mathf.FloorToInt( tempsize / ScaleDownAmount);
            soulScale = new Vector3(SoulWorth, SoulWorth, SoulWorth);
            transform.localScale = soulScale;
        }

        void Awake() {

            friendsoulref = GetComponentInParent<FriendlySoul>();
            int quicksize = Mathf.FloorToInt(friendsoulref.SizeCategory);
            if (friendsoulref.threeSizeChoses)
            {
                ThreeSizes(quicksize);
            }
            else
            {
                // make function work with multiple sizes
                ManySizes(quicksize);
            }
            waitTimebySize = friendsoulref.SizeCategory;
            aSource = GetComponent<AudioSource>();
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            PurityActionRef = player.GetComponent<SoulPurityAction>();
        }

        void Start() {

        }
        
        public void Cleaning() {

            if (!clean && !SoulPurityAction.PureSoulMaking) {
           
                lookedAt = true;
                aSource.PlayOneShot(cleansingSoul);                              
              
                anim.SetBool("looked", true);
                StartCoroutine(cleanUp());
            }
        }

        IEnumerator cleanUp() {
            while (lookedAt && (counter < waitTimebySize)) {
                yield return new WaitForSeconds(CounterSizeTime/3f);
                counter += CounterSizeTime;
            }

            if ((counter > waitTimebySize)) {
                anim.SetBool("looked", false);
                clean = true;
              
                float finalsize;

                if ((soulScale.x > soulScale.y) && (soulScale.x > soulScale.z)) {
                    finalsize = samplesize * soulScale.x;

                    while (finalsize < soulScale.x) {
                        soulScale.x -= .2f;
                        soulScale.y -= .2f;
                        soulScale.z -= .2f;
                        transform.localScale = soulScale;
                        yield return new WaitForSeconds(.02f);
                    }
                }

                else if ((soulScale.y > soulScale.x) && (soulScale.y > soulScale.z))
                {
                    finalsize = samplesize * soulScale.y;

                    while (finalsize < soulScale.y) {
                        soulScale.x -= .2f;
                        soulScale.y -= .2f;
                        soulScale.z -= .2f;
                        transform.localScale = soulScale;
                        yield return new WaitForSeconds(.02f);
                    }
                }

                else if ((soulScale.z > soulScale.x) && (soulScale.z > soulScale.y)) {
                    finalsize = samplesize * soulScale.z;

                    while (finalsize < soulScale.z) {
                        soulScale.x -= .2f;
                        soulScale.y -= .2f;
                        soulScale.z -= .2f;
                        transform.localScale = soulScale;
                        yield return new WaitForSeconds(.02f);
                    }
                }

                else {
                    finalsize = samplesize * soulScale.z;

                    while (finalsize < soulScale.z) {
                        soulScale.x -= .2f;
                        soulScale.y -= .2f;
                        soulScale.z -= .2f;
                        transform.localScale = soulScale;
                        yield return new WaitForSeconds(.02f);
                    }
                }

                aSource.PlayOneShot(cleansedSoul);
                // place wait if it doesn't look right
                GameObject cleanSoul = Instantiate(cleanSoulobj, transform.position, transform.rotation) as GameObject;
                 
              //  yield return new WaitForSeconds(.3f);
                //particle system for the new created soul and changing it color
                ParticleSystem pars = cleanSoul.GetComponent<ParticleSystem>();
                var parmain = pars.main;
                parmain.startColor = friendsoulref.finalcolor;
                PurityActionRef.SoulWorthCount += SoulWorth;
                SpawnRef.SoulCheck();
                yield return new WaitForSeconds(.01f);
                Destroy(gameObject);
          
            }
        }

        public void Unlook() {
            if (counter < waitTimebySize) {
                counter = 0;
                anim.SetBool("looked", false);
                lookedAt = false;
                //anim.enabled = false;
                StopCoroutine(cleanUp());
            }
        }
    }
}