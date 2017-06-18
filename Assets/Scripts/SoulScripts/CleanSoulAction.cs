using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanSoulAction : MonoBehaviour {

    public Transform player;
    public float WaitBeforeFollow;
    public bool follow = false;
    private Transform chosenFollow;
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(becomeFollower());
    }

    IEnumerator becomeFollower()
    {
        yield return new WaitForSeconds(WaitBeforeFollow);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        int i = Random.Range(0, stats.followTrans.Count);
        chosenFollow = stats.followTrans[i];
        follow = true;
    }

    void Update()
    {
        if(follow)
        {
            transform.position = Vector3.MoveTowards(transform.position,chosenFollow.position,0.5f);
        }
    }

}
