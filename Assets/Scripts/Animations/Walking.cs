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

	
	// Update is called once per frame
    void Update()
    {
        
       
	}

   

    void checkTurning()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animationManager.changeAnimation("Turning");
          //  anim.SetTrigger("turn");

            // turning = true;
            //TODO insert turning logic
            
        }
    }

    

    void delegateInputs()
    {


        if (input == Vector2.zero)
            return;
        if (input.magnitude > 0.2f)
        {
            lastX = _inputX;
            lastY = _inputY; 
        }
        else
        {
            // he's on idle
            // set idle properly
            if (Mathf.Abs(lastX) > 1.3f)
            {
                lastX = 0;
            }
            if (Mathf.Abs(lastY) > 1.3f)
            {
                lastY = 0;
            }

            input = new Vector2(lastX, lastY);
            lastX = input.normalized.x * 0.1f;
            lastY = input.normalized.y * 0.1f;
        }
    }

    public void setInputs()
    {
        anim.SetFloat("axisX", lastX);
        anim.SetFloat("axisY", lastY);

    }

    public override void Enter()
    {
        anim.rootRotation = initRotation;
        time = 0;
    }
    float time =0;
    public override void Execute()
    {
        time += Time.deltaTime;
        // Lerp the rotation to match the initial one
        if (time < 1)
        {
            anim.rootRotation = Quaternion.Lerp(anim.rootRotation, initRotation, time);
        }


        _inputX = animationManager.transformedInputX.Value;
        _inputY = animationManager.transformedInputY.Value;

        lastX = animationManager.transformedInputX.Value;
        lastY = animationManager.transformedInputY.Value;

        input = new Vector2(lastX, lastY);
        //_inputX = inputX.Value;
       // _inputY = inputY.Value;
       

        checkTurning();
       // transformInputs();
        delegateInputs();
        setInputs();
      
      
    }

    public override void Exit()
    {

    }
}
