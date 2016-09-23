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
    Vector2 input;
    Transform cameraTransform;
    public float dotY;
    public float dotX;

    public BaseAnim[] anims;
    public string currentAnim;
    public Dictionary<string, BaseAnim> animationPairer = new Dictionary<string,BaseAnim>();

    // bool to give one frame of relief to change the animations
    bool changeAnimationFlag;
    string nextAnimation;

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

	}
	
    void lockUpdate()
    {

    }

    void unlockUpdate()
    {

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

        // IF THE INPUT IS TOO SMALL, DONT VALUE IT
        InputX.Value = -Input.GetAxis("Horizontal");
        InputY.Value = Input.GetAxis("Vertical");
        
    }


    void transformInputs()
    {
        //transitate inputs first
        Vector2 oldInputs = new Vector2(oldInputX , oldInputY);
        Vector2 newInputs = new Vector2(InputX.Value, InputY.Value);

        float tempInputX = oldInputX;
        float tempInputY = oldInputY;
        tempInputX -= (oldInputs.x - newInputs.x) * 0.2f;
        tempInputY -= (oldInputs.y - newInputs.y) * 0.2f;


        oldInputX = tempInputX;
        oldInputY = tempInputY;

        transformedInputX.Value = tempInputX;
        transformedInputY.Value = tempInputY;


        input = new Vector2(transformedInputX.Value, transformedInputY.Value);
        Vector2 forward = new Vector2(cameraTransform.forward.x, cameraTransform.forward.z);
        Vector2 right = new Vector2(cameraTransform.right.x, cameraTransform.right.z);
        dotY = Vector2.Dot(input, forward);
        dotX = Vector2.Dot(input, right);

        transformedInputX.Value = -dotX;
        transformedInputY.Value = dotY;



    }

}
