using UnityEngine;
using System.Collections;

public class Walking : BaseAnim {

   

    public float oldInputX;
    public float oldInputY;
    public float lastX;
    public float lastY;

    public float _inputX;
    public float _inputY;

   // public Animator anim;

    Transform cameraTransform;
    Quaternion initRotation;

    public float dotY;
    public float dotX;

    Vector2 input;

    public ManageAnimations animationManager;


	// Use this for initialization
	void Start () {
        base.Start();
        //AnimatorClipInfo[] infos = anim.getcu
        AnimationClip clip;


        cameraTransform = Camera.main.transform;
        animationManager = GetComponent<ManageAnimations>();

	}

    Vector3 bla;
	// Update is called once per frame
    void Update()
    {

	}

   

    void checkTurning()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animationManager.changeAnimation("Turning");
          
        }
    }


    bool idleSet;
    void delegateInputs()
    {


        if (input == Vector2.zero)
            return;
        if (input.magnitude > 0.2f)
        {
            lastX = input.x;
            lastY = input.y;
            idleSet = false;
        }
        else
        {
            // he's on idle
            // set idle properly

            if (!idleSet)
            {
                idleSet = true;
               

                input = new Vector2(lastX, lastY);
                lastX = input.normalized.x * 0.1f;
                lastY = input.normalized.y * 0.1f;
            }
        }
    }

    public void setInputs()
    {
        anim.SetFloat("axisX", lastX);
        anim.SetFloat("axisY", lastY);

    }

    public override void Enter()
    {


        input = new Vector2(anim.GetFloat("axisX"), anim.GetFloat("axisY"));
        Debug.Log("bla");
    }

    public override void Execute()
    {
        


        _inputX = animationManager.transformedInputX.Value;
        _inputY = animationManager.transformedInputY.Value;

      
        input = new Vector2( animationManager.transformedInputX.Value,  animationManager.transformedInputY.Value);
        //_inputX = inputX.Value;
        //_inputY = inputY.Value;
       

       // transformInputs();
        delegateInputs();
        setInputs();
        checkTurning();

      
    }

    public override void Exit()
    {
       
    }
}
