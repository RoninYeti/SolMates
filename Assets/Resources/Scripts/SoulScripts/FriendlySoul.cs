using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace soulmates
{

    public class FriendlySoul : MonoBehaviour
    {

        [SerializeField]
        float minimumScale = 0.5f;

        [SerializeField]
        float maximumScale = 3f;

        [SerializeField]
        Color[] colors;
        Color finalcolor;
        public float size;
        public Transform player;

        private void Awake()
        {
            size = Random.Range(minimumScale, maximumScale);
            int colorchosen = Random.Range(0, colors.Length);
            finalcolor = colors[colorchosen];
            foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = finalcolor;
            }
        }

        void Start()
        {
            transform.localScale *= size;
            //color = colors[Random.Range(0, colors.Length - 1)];

            player = GameObject.FindGameObjectWithTag("Player").transform;

            transform.LookAt(player, transform.up);
        }

        void Update()
        {
        }
    }
}