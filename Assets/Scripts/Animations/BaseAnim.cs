using UnityEngine;
using System.Collections;

public abstract class BaseAnim : MonoBehaviour {

    public FloatData inputX;
    public FloatData inputY;

    // to be paired in the dictionary in ManageAnimations
    public string AnimationName;

    public bool isActive;

    // animation states
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
    protected Animator anim;


	// Use this for initialization
	public virtual void Start () {
	    anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
