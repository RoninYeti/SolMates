using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace solmates {
    public class FriendlySoul : MonoBehaviour {

        [SerializeField]
        Color[] colors;

        public  Color finalcolor;
        public Transform player;
        public float size;

        public float minSize=.5f;
        public float maxSize=3f;
        private void Awake() {
   
  
            int colorchosen = Random.Range(0, colors.Length);
            finalcolor = colors[colorchosen];
            size = Random.Range(minSize, maxSize);
            foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
                sr.color = finalcolor;
            }
        }

        void Start() {
           // transform.localScale *= size;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            transform.LookAt(player, transform.up);
        }
    }
}