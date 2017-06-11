using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlySoul : MonoBehaviour {
    [SerializeField]
    float minimumScale = 0.5f;
    [SerializeField]
    float maximumScale = 2f;
    [SerializeField]
    Color[] colors;
    Color color;
	// Use this for initialization
	void Start () {
        transform.localScale *= Random.Range(minimumScale, maximumScale);
        color = colors[Random.Range(0, colors.Length - 1)];
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = color;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform,transform.up);
	}
}
