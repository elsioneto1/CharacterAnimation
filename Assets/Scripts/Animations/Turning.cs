using UnityEngine;
using System.Collections;

public class Turning : BaseAnim {

    public bool turning;
    public bool setBackToNormal;
    ManageAnimations animManager;
    Quaternion initRotation;

    Vector2 lastTurnVector;

    void Start()
    {
        base.Start();
        animManager = GetComponent<ManageAnimations>();
        initRotation = anim.rootRotation;
    }

    public override void Enter()
    {


        setBackToNormal = false;
        anim.SetTrigger("turn");
        lastTurnVector = new Vector2(animManager.transformedInputX.Value, animManager.transformedInputY.Value);
        // set both animations to the same value
       
        anim.SetFloat("turningX", lastTurnVector.x);
        anim.SetFloat("turningY", lastTurnVector.y);
        Debug.Log(lastTurnVector.normalized);

    }

    public override void Execute()
    {
       


    }

    public override void Exit()
    {
        anim.SetFloat("axisX", -lastTurnVector.x);
        anim.SetFloat("axisY", -lastTurnVector.y);
    }

    public void inverseMovingValues()
    {
        


    }

    public void resetTurning()
    {
        //TODO turn back to normal

        turning = false;
    }

    public void ReturnToPreviousState()
    {
        Debug.Log("asdasd");
      
        animManager.changeAnimation("Walking");
    }

}
