using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulPurityAction : MonoBehaviour {

    public GameObject pureSoulObj;
    public Transform spawnTransform;
    private PlayerStats statsRef;
    private bool soulmaking = false;
    public int maxSoulAmount = 3;
    public bool purSoulmade = false;

    public AudioClip pureSoul;
    private AudioSource source;

    private void Awake() {
        statsRef = GetComponent<PlayerStats>();
    }

    public void CreatePureSoul() {
        StartCoroutine(WaitThenDestory());
    }

    IEnumerator WaitThenDestory() {
        yield return new WaitForSeconds(5f);
        GameObject soul = Instantiate(pureSoulObj, spawnTransform.position, spawnTransform.rotation) as GameObject;
        //source.PlayOneShot(absorbSoul);                                                                               Fix this sound (also needs to loop)!!
        soul.transform.parent = transform;
        PlayerStats.cleansouls -= maxSoulAmount;
        //print(PlayerStats.cleansouls + " clean soul static variable");

        //print(statsRef.cleanSoulsList.Count + " clean soul count");
        for (int i = 0; i < maxSoulAmount; i++) {
            Destroy(statsRef.cleanSoulsList[0]);
            statsRef.cleanSoulsList.RemoveAt(0);
        }
    }
	
	void Update () {
        if (PlayerStats.cleansouls >= maxSoulAmount && !soulmaking) {
            soulmaking = true;
            CreatePureSoul();
        }

        if (purSoulmade) {
            //input to launch it
            //set parent of soul to be empty
            //set pursoulmade to be false
            //set soulmaking to false
        }
	}
}
