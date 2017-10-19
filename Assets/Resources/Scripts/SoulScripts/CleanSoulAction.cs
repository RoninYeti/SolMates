using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace solmates {
    public class CleanSoulAction : MonoBehaviour {

        public Transform player;
        public float WaitBeforeFollow;
        public bool follow = false;
        private Transform chosenFollow;
        public float followSpeed = 3;
        private bool closeByFollow = false;
        public float followdisMin = 20f;

        void OnEnable() {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            StartCoroutine(becomeFollower());
        }

        IEnumerator becomeFollower() {
            PlayerStats stats = player.GetComponent<PlayerStats>();
            stats.cleanSoulsList.Add(this.gameObject);

            yield return new WaitForSeconds(WaitBeforeFollow);
                               
            int i = Random.Range(0, stats.followTrans.Count);
            chosenFollow = stats.followTrans[i];
            follow = true;
        }

        void Update() {
            if (follow) {
                transform.position = Vector3.MoveTowards(transform.position, chosenFollow.position, followSpeed);
                float dis = Vector3.Distance(transform.position, chosenFollow.position);
                if (dis < followdisMin && !closeByFollow) {
                    closeByFollow = true;
                    SoulPurityAction.Instance.cleansouls++;
                }
            }
        }
    }
}