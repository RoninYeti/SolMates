using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace solmates {
    public class FriendlySoul : MonoBehaviour {

        [SerializeField]
        Color[] colors;

        public  Color finalcolor;
        public Transform player;
        public float SizeCategory;
        public bool threeSizeChoses = true;
        public float minSize=.5f;
        public float maxSize=3f;
        public float spriteAlpha = .5f;
        private void Awake() {

            if (threeSizeChoses)
            {// only 3 sizes
                SizeCategory = Random.Range(1, 4);
            }
            else
            {// more than 3 sizes
                SizeCategory = Random.Range(minSize, maxSize);
            }


            int colorchosen = Random.Range(0, colors.Length);
            finalcolor = colors[colorchosen];
          
            foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
                finalcolor.a = spriteAlpha;
                sr.color = finalcolor;
            }
        }



        void Start() {
        
            player = GameObject.FindGameObjectWithTag("Player").transform;
            transform.LookAt(player, transform.up);
        }
    }
}