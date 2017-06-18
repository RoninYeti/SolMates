﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAction : MonoBehaviour {
    //is soul clean?
    public bool clean = false;
    //reference to friendlysoul
    public FriendlySoul friendsoulref;
    //time to look at based on size
    private float waitTimebySize = 0;
    //simple counter to keep count
    private float counter = 0;
    //reference to animator
    private Animator anim;
    // are you being looked at by player
    private bool lookedAt = false;
    // gameboject to create when dead or small
    public GameObject cleanSoulobj;
    // scale of object;
    private Vector3 soulScale;
    // size of sample
    public float samplesize = .2f;
    //reference to the player
    private Transform player;
    void Awake()
    {
        friendsoulref = GetComponent<FriendlySoul>();
        anim = GetComponent<Animator>();
        soulScale = transform.localScale;
        player = friendsoulref.player;
    }

    void Start()
    {
        
        waitTimebySize = friendsoulref.size;
    }

    void Update()
    {
 
    }

    public void Cleaning()
    {
        if (!clean)
        {
            lookedAt = true;
            anim.SetBool("looked", true);
            StartCoroutine(cleanUp());
        }


    }

    IEnumerator cleanUp()
    {
      //  print(waitTimebySize);
        while (lookedAt && counter < waitTimebySize)
        {
            yield return new WaitForSeconds(.1f);
            counter += .1f;
           
         //   print(counter);
        }
        if (counter > waitTimebySize)
        {
            anim.SetBool("looked", false);
            clean = true;
            PlayerStats.cleansouls++;
            float finalsize;

            if ((soulScale.x > soulScale.y) && (soulScale.x > soulScale.z))
            {
                finalsize = samplesize * soulScale.x;
                while (finalsize < soulScale.x)
                {
                    soulScale.x -= .1f;
                    soulScale.y -= .1f;
                    soulScale.z -= .1f;
                    transform.localScale = soulScale;
                    yield return new WaitForSeconds(.02f);
                }

            }
            else if ((soulScale.y > soulScale.x) && (soulScale.y > soulScale.z))
            {
                finalsize = samplesize * soulScale.y;
           
                while (finalsize < soulScale.y)
                {
                    soulScale.x -= .1f;
                    soulScale.y -= .1f;
                    soulScale.z -= .1f;
                    transform.localScale = soulScale;
                    yield return new WaitForSeconds(.02f);
                }
            }
            else if ((soulScale.z > soulScale.x) && (soulScale.z > soulScale.y))
            {
                finalsize = samplesize * soulScale.z;
                while (finalsize < soulScale.z)
                {
                    soulScale.x -= .1f;
                    soulScale.y -= .1f;
                    soulScale.z -= .1f;
                    transform.localScale = soulScale;
                    yield return new WaitForSeconds(.02f);
                }
            }
            else
            {
                finalsize = samplesize * soulScale.z;
                while (finalsize < soulScale.z)
                {
                    soulScale.x -= .1f;
                    soulScale.y -= .1f;
                    soulScale.z -= .1f;
                    transform.localScale = soulScale;
                    yield return new WaitForSeconds(.02f);
                }
            }
        }
        yield return new WaitForSeconds(1);
        GameObject cleanSoul = Instantiate(cleanSoulobj, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
    }

    public void Unlook()
    {
        if(counter < waitTimebySize)
        {
            counter = 0;
            anim.SetBool("looked", false);
            lookedAt = false;
          //  StopCoroutine(cleanUp());
        }

    }
}
