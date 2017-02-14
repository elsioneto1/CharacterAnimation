using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManageAnimations : MonoBehaviour {

    // Input handler
    public FloatData InputX;
    public FloatData InputY;
    public FloatData transformedInputX;
    public FloatData transformedInputY;
    public float oldInputX;
    public float oldInputY;
    public float lastX;
    public float lastY;
    public Vector2 input;
    Transform cameraTransform;
    public float dotY;
    public float dotX;

    public BaseAnim[] anims;
    public string currentAnim;
    public Dictionary<string, BaseAnim> animationPairer = new Dictionary<string,BaseAnim>();

    public Vector2 InputForward;

    // bool to give one frame of relief to change the animations
    bool changeAnimationFlag;
    string nextAnimation;

    public Animator anim;
    public CameraRotation CamRotation;

	// Use this for initialization
	void Start () {
	    anims = GetComponents<BaseAnim>();
        for (int i = 0; i < anims.Length; i++)
        {
            animationPairer.Add(anims[i].AnimationName, anims[i]);
            anims[i].inputX = InputX;
            anims[i].inputY = InputY;
        }
        cameraTransform = Camera.main.transform;
        anim = GetComponent<Animator>();
	}
	

	// Update is called once per frame
	void Update () {

        if (changeAnimationFlag)
        {
            changeAnimationFlag = false;
            switchAnimations();
        }


        getInput();
        transformInputs();

        animationPairer[currentAnim].Execute();

	}

    public void changeAnimation(string key)
    {
        // Don't allow it to transitate to the current state again
        if (key == currentAnim)
            return;

        changeAnimationFlag = true;
        nextAnimation = key;

       

    }

    void switchAnimations()
    {
        animationPairer[currentAnim].isActive = false;
        animationPairer[currentAnim].Exit();
        currentAnim = nextAnimation;
        animationPairer[currentAnim].isActive = true;
        animationPairer[currentAnim].Enter();
    }

    void getInput()
    {


        InputX.Value = -Input.GetAxis("Horizontal");
        InputY.Value = Input.GetAxis("Vertical");

    }


    void transformInputs()
    {

        Vector3 bodyRotation = anim.rootRotation.eulerAngles;

        float orientationX = Mathf.Cos((bodyRotation.y / 180) * Mathf.PI);
        float orientationY = Mathf.Sin((bodyRotation.y / 180) * Mathf.PI);


        //transitate inputs first
        Vector2 oldInputs = new Vector2(oldInputX , oldInputY);
        Vector2 newInputs = new Vector2(InputX.Value, InputY.Value);

        float tempInputX = oldInputX;   
        float tempInputY = oldInputY;
        // apply easing
        tempInputX -= (oldInputs.x - newInputs.x) * 0.05f;
        tempInputY -= (oldInputs.y - newInputs.y) * 0.05f;


        oldInputX = tempInputX;
        oldInputY = tempInputY;

        transformedInputX.Value = tempInputX;
        transformedInputY.Value = tempInputY;


        input = new Vector2(transformedInputX.Value, transformedInputY.Value);
        Vector3 bodyForward = cameraTransform.forward;
        Vector3 bodyRight = cameraTransform.right;

        // fix the input in relation to the root rotation
        Matrix4x4 rotationMatrix = new Matrix4x4();
        float rad = (anim.rootRotation.eulerAngles.y/180) * Mathf.PI;
        
        // vai entender ne amiguinho. 
        // aparentemente, o eixo x ta todo cagado invertendo a porra toda e isso ta resolvendo
        rad = -rad;
      //  MathOperations.RotateVector(ref bodyForward, rad);
      //  MathOperations.RotateVector(ref bodyRight, rad);

        rotationMatrix.SetRow(0, new Vector4(Mathf.Cos(rad),0,Mathf.Sin(rad),0));
        rotationMatrix.SetRow(1, new Vector4(0,0,0,0));
        rotationMatrix.SetRow(2, new Vector4(-Mathf.Sin(rad), 0, Mathf.Cos(rad), 0));
        rotationMatrix.SetRow(3, new Vector4(0, 0, 0, 1));
        bodyForward = rotationMatrix.MultiplyVector(bodyForward);
        bodyRight = rotationMatrix.MultiplyVector(bodyRight);

        Vector2 forward = new Vector2(bodyForward.x, bodyForward.z);
        Vector2 right = new Vector2(bodyRight.x, bodyRight.z);
        dotY = Vector2.Dot(input, forward);
        dotX = Vector2.Dot(input, right);

        

 
        float _dotX = -dotX ;
        float _dotY = dotY;

     
        transformedInputX.Value = _dotX;
        transformedInputY.Value = dotY;



    }

}
