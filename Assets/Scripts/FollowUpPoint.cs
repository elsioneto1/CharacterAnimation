using UnityEngine;
using System.Collections;

public class FollowUpPoint : MonoBehaviour {

    public Transform pointToBeFollowed;
    public Transform follower;

    [Range(0.01f, 1)]
    public float followSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if ( pointToBeFollowed != null && follower != null)
            follower.position -= ( follower.position - pointToBeFollowed.position) * followSpeed;


      //  follower.position = pointToBeFollowed.position;
	}
}
