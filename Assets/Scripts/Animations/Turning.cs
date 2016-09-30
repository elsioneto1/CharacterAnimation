using UnityEngine;
using System.Collections;

public class Turning : BaseAnim {

    public bool turning;
    public bool setBackToNormal;
    ManageAnimations animManager;
    Quaternion initRotation;
    Vector3 destRotation = new Vector3 (0,180,0);
    bool setFinalLerp;

    Vector3 lastTurnVector;

    void Start()
    {
        base.Start();
        animManager = GetComponent<ManageAnimations>();
        initRotation = anim.rootRotation;
    }

    public override void Enter()
    {

        setFinalLerp = false;
        setBackToNormal = false;
        anim.SetTrigger("turn");
        lastTurnVector = new Vector2(animManager.transformedInputX.Value, animManager.transformedInputY.Value);
        // set both animations to the same value
       
        anim.SetFloat("turningX", lastTurnVector.x);
        anim.SetFloat("turningY", lastTurnVector.y);
      //  Debug.Log(lastTurnVector.normalized);

    }

    public override void Execute()
    {
        

    }

    public override void Exit()
    {

        anim.SetFloat("axisX", -lastTurnVector.normalized.x * 0.1f);
        anim.SetFloat("axisY", -lastTurnVector.normalized.y * 0.1f);
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
        if (!setFinalLerp)
        {
            setFinalLerp = true;
            StartCoroutine(FinalLerp());
        }

    }

    public IEnumerator FinalLerp()
    {
        bool execute = true;
        while (execute)
        {
            yield return new WaitForSeconds(0.016f);
            anim.rootRotation = Quaternion.Euler(anim.rootRotation.eulerAngles + anim.rootRotation.eulerAngles.normalized);

            if (Mathf.Abs(destRotation.y - anim.rootRotation.eulerAngles.y) < 1.5f)
            {
                execute = false;

                anim.rootRotation = initRotation;
                animManager.changeAnimation("Walking");
                
            }
        }
    }

}
