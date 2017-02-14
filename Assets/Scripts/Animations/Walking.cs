using UnityEngine;
using System.Collections;

public class Walking : BaseAnim {


    ManageAnimations animManager;

    public float oldInputX;
    public float oldInputY;
    public float lastX;
    public float lastY;

    public float _inputX;
    public float _inputY;


    // public Animator anim;

    Transform cameraTransform;
    public Transform hips;
    Quaternion initRotation;

    public float dotY;
    public float dotX;

    Vector2 input;

    public ManageAnimations animationManager;

    bool idleSet;
    bool turning;
    bool turningRight;
    bool turningLeft;
    bool firstFrame;
	// Use this for initialization
	void Start () {
        base.Start();
        animManager = GetComponent<ManageAnimations>();

        cameraTransform = Camera.main.transform;
        animationManager = GetComponent<ManageAnimations>();

	}


	// Update is called once per frame
    void Update()
    {


      

	}


    void checkTurningRight()
    {

        if (turningRight)
        {

            animationManager.changeAnimation("rightTurn");
        }
    }
    void checkTurningLeft()
    {

        if (turningRight  || Input.GetKeyDown(KeyCode.E))
        {

            animationManager.changeAnimation("leftTurn");
        }
    }


    void checkTurning()
    {
        if (turning)
        {
            turning = false;
            animationManager.changeAnimation("Turning");
          
        }
    }


    void delegateInputs()
    {


        if (input == Vector2.zero)
            return;

        if (input.magnitude > 0.2f)
        {
            EvaluateTurning();
            EvaluateTurningRight();
            EvaluateTurningLeft();
            if (turning)
                return;
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


    public void EvaluateTurning()
    {
        // verifying how we set our turn detection
        if (Vector2.Dot(new Vector2(lastX, lastY).normalized, input.normalized) < -0.8f)
        {
            turning = true;
            lastX = -input.normalized.x;
            lastY = -input.normalized.y;
            animManager.transformedInputX.Value = lastX;
            animManager.transformedInputY.Value = lastY;
            
        }
    }

    public void EvaluateTurningRight()
    {
        Vector3 v1 = new Vector3(hips.transform.forward.z,0, hips.transform.forward.x);
        Vector3 v2 = new Vector3(animationManager.InputX.Value,0, animationManager.InputY.Value);
        Vector3 futurePosition = new Vector3(animationManager.InputX.Value, 0, animationManager.InputY.Value);
        futurePosition.Normalize();

        MathOperations.RotateVector(ref v2, animationManager.CamRotation.angle);
        float rad = (anim.rootRotation.eulerAngles.y  / 180) * Mathf.PI;
        MathOperations.RotateVector(ref futurePosition, rad);
        



       // Debug.Log(futurePosition);

      //  MathOperations.RotateVector(ref futurePosition, animationManager.CamRotation.angle);


        Vector3 cross = Vector3.Cross(v1.normalized,v2.normalized);
        GetComponent<TurningRight>().LastInputOnPreviousAnimation = new Vector2(animationManager.InputX.Value, animationManager.InputY.Value);
        if (cross.y < -0.85f)
        {
            Debug.Log(futurePosition);

            animationManager.changeAnimation("rightTurn");
            turningLeft = true;
        }

    }


    public void EvaluateTurningLeft()
    {
        Vector3 v1 = new Vector3(hips.transform.forward.z, 0, hips.transform.forward.x);
        Vector3 v2 = new Vector3(animationManager.InputX.Value, 0, animationManager.InputY.Value);


        MathOperations.RotateVector(ref v2, animationManager.CamRotation.angle);
        GetComponent<TurningLeft>().LastInputOnPreviousAnimation = new Vector2(lastX, lastY);

       // Debug.Log("v1 : " + v1);
       // Debug.Log("v2 : " + v2);
        Vector3 cross = Vector3.Cross(v1.normalized, v2.normalized);

        if (cross.y > 0.85f)
        {
            animationManager.changeAnimation("leftTurn");
            turningRight = true;
        }

    }


    public void setInputs()
    {
        anim.SetFloat("axisX", lastX);
        anim.SetFloat("axisY", lastY);

    }

    public override void Enter()
    {
       // input = new Vector2(anim.GetFloat("axisX"), anim.GetFloat("axisY"));
        firstFrame = true;
        turning = false;
    }

    public override void Execute()
    {



        _inputX = animationManager.transformedInputX.Value;
        _inputY = animationManager.transformedInputY.Value;

        
        input = new Vector2( animationManager.transformedInputX.Value,  animationManager.transformedInputY.Value);
        //_inputX = inputX.Value;
        //_inputY = inputY.Value;
       

       // transformInputs();
       
        
            checkTurningRight();
            checkTurningLeft();
            delegateInputs();
        if (!firstFrame)
        {
            if (!turningRight && !turningLeft)
            {
                setInputs();
                checkTurning();
            }
        }
        turningRight = false;
        turningLeft = false;
        firstFrame = false;
    }

    public override void Exit()
    {


    }
}
