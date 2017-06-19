using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int cleansouls;
    public GameObject playercir;

    private AudioSource asource;
    public List<Transform> followTrans = new List<Transform>();
    //
    public List<AudioClip> aclips = new List<AudioClip>();
    public List<GameObject> cleanSoulsList = new List<GameObject>();
	// Use this for initialization
	void Start () {
        asource = GetComponent<AudioSource>();
        if (playercir != null)
        {
            for (int i = 0; i < playercir.transform.childCount; i++)
            {
                followTrans.Add(playercir.transform.GetChild(i).transform);
            }
        }

	}
	
	// Update is called once per frame
	void Update () {

	}
}
