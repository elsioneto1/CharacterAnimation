using UnityEngine;
using System.Collections;

public class TurningLeft : BaseAnim {

    ManageAnimations animManager;
    float OriginalY;
    bool adjusting = false;
    public Vector2 LastInputOnPreviousAnimation;

    // Use this for initialization
    void Start()
    {
        animManager = GetComponent<ManageAnimations>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Enter()
    {
        OriginalY = anim.rootPosition.y;

        anim.SetFloat("turningX", anim.GetFloat("axisX"));
        anim.SetFloat("turningY", anim.GetFloat("axisY"));
        anim.SetTrigger("turnLeft");
    }
    public override void Execute()
    {
        if (adjusting)
        {

        }
    }
    public override void Exit()
    {
        anim.rootPosition = new Vector3(anim.rootPosition.x, OriginalY, anim.rootPosition.z);

        anim.SetFloat("turningX", 0);
        anim.SetFloat("turningY", 0);
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
