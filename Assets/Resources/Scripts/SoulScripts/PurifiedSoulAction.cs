using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace solmates
{
    public class PurifiedSoulAction : MonoBehaviour {

        public GameObject ReactiveHitParticle;

        private void OnTriggerEnter(Collider col)
        {

            // print("hit something "+ col.gameObject);

            if (col.CompareTag("Planet"))
            {

                Instantiate(ReactiveHitParticle, transform.position, transform.rotation);
                Destroy(transform.gameObject);
                SoulPurityAction.Instance.CheckForEnd();
            }
        }

        void Update()
        { }
    } }
