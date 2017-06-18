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
   public  float size;
    public Transform player;
	// Use this for initialization
	void Start () {
        size = Random.Range(minimumScale, maximumScale);
        transform.localScale *= size;
       // color = colors[Random.Range(0, colors.Length - 1)];
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = color;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;

      transform.LookAt(player, transform.up);
    }
	
	// Update is called once per frame
	void Update () {
  
	}
}
