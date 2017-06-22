using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolMates {

    [RequireComponent(typeof(Rigidbody))]

    public class Planet : MonoBehaviour {

        Rigidbody rb;

        [SerializeField]
        float force = 10;

        [SerializeField]
        Vector3 initialVelocity;

        [SerializeField]
        Transform sunCenterOfMass;

        void Start() {
            rb = GetComponent<Rigidbody>();
            rb.velocity = initialVelocity;
        }

        void Update() {
            rb.AddForce((sunCenterOfMass.position - transform.position).normalized * force);
        }
    }
}