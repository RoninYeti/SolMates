using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace solmates {
    public class PlayerStats : MonoBehaviour {

        public static int cleansouls;
        public GameObject playercir;

        //public AudioSource source;
        //public AudioClip [Put it here];

        public List<Transform> followTrans = new List<Transform>();
        public List<AudioClip> aclips = new List<AudioClip>();
        public List<GameObject> cleanSoulsList = new List<GameObject>();

        void Start() {
            //source = GetComponent<AudioSource>();
            if (playercir != null) {
                for (int i = 0; i < playercir.transform.childCount; i++) {
                    followTrans.Add(playercir.transform.GetChild(i).transform);
                }
            }
        }

        void Update() {
        }
    }
}