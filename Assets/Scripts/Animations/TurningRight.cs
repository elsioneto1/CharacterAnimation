using UnityEngine;
using System.Collections;

public class TurningRight : BaseAnim {

    ManageAnimations animManager;
    float OriginalY;
    bool adjusting = false;
    public Vector2 LastInputOnPreviousAnimation;

	// Use this for initialization
	void Start () {
        animManager = GetComponent<ManageAnimations>();
        anim = GetComponent<Animator>();
        Debug.Log(anim.rootRotation.eulerAngles.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Enter()
    {
        OriginalY = anim.rootPosition.y;

        anim.SetFloat("turningX", anim.GetFloat("axisX"));
        anim.SetFloat("turningY", anim.GetFloat("axisY"));
        anim.SetTrigger("turnRight");
    }

    public override void Execute()
    {
        if (adjusting)
        {
          //  Debug.Log(LastInputOnPreviousAnimation);

            //LastInputOnPreviousAnimation = LastInputOnPreviousAnimation.normalized;

            //Vector3 bodyForward = Camera.main.transform.forward;
            //Vector3 bodyRight   = Camera.main.transform.right;
            //float rad = (anim.rootRotation.eulerAngles.y / 180) * Mathf.PI;
            //rad = -rad;
            //MathOperations.RotateVector(ref bodyForward, rad + 1.57f);
            //MathOperations.RotateVector(ref bodyRight, rad + 1.57f);

            //Vector2 forward = new Vector2(bodyForward.x, bodyForward.z);
            //Vector2 right = new Vector2(bodyRight.x, bodyRight.z);

            //float dotY = Vector2.Dot(LastInputOnPreviousAnimation, bodyForward);
            //float dotX = Vector2.Dot(LastInputOnPreviousAnimation, bodyRight);
            //Debug.Log("input : " +LastInputOnPreviousAnimation);
            //if (Vector2.zero != new Vector2(dotY, dotX))
            //    Debug.Log(new Vector2(-dotX,dotY));
            //anim.SetFloat("axisX", -dotX);
            //anim.SetFloat("axisY", dotY);

        }
    }

    public override void Exit()
    {
        anim.rootPosition = new Vector3(anim.rootPosition.x,OriginalY,anim.rootPosition.z);

        Debug.Log(anim.rootRotation.eulerAngles.y);

        anim.SetFloat("turningX", 0);
        anim.SetFloat("turningY", 0);
        adjusting = false;
    }

    public void ReturnToPreviousState()
    {
        
        if (animManager.currentAnim == AnimationName)
            animManager.changeAnimation("Walking");
    }

    public void SetAdjustmentFlags()
    {
        adjusting = true;
    }

}
