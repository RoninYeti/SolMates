using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace solmates {
    public class Rotation : MonoBehaviour {

        public float speed = 10f;

        void Update() {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
    }
}
