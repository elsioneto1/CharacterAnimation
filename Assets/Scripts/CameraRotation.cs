using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

    public Transform cameraPivot;
    public Transform cameraLookAt;
    public Transform parentTransform;
    public Transform RefObjLocation;

    public float axisX;

    public Transform cameraTransform;
    public float cameraDistance = 5;
    public float pivotDistance = 5;
    public float angle;
    

	void Start () {
        cameraTransform = Camera.main.transform;
        parentTransform = transform.parent;
        //angle = 1.5f;
	}
	

	void Update () {

        // get the controller input
        angle += Input.GetAxis("AxisController") * 0.1f;
        // make a trigonometric vector with the cos and the sin (why the z has to be reversed its an enigma)
        Vector3 angleVector = new Vector3(Mathf.Cos(angle),0,-Mathf.Sin(angle));
        // gather the values individually
        float cosInput = Mathf.Sin(angle);
        float sinInput = Mathf.Cos(angle);

        Vector3 pivotPosition = new Vector3(cosInput,0,sinInput); 
        // set the point position in relation to the character
        cameraPivot.position = -pivotPosition * 5 + new Vector3(parentTransform.position.x, 15, parentTransform.position.z);

        // set the point to be looked at in front of the point in position in relation to the character and the camera behind this point
        cameraLookAt.position = cameraPivot.position + (angleVector) * cameraDistance;
        Vector3 cameraPos = cameraPivot.position + (angleVector) * -cameraDistance;
        cameraTransform.position -=(cameraTransform.position-  cameraPos) * 0.2f;

        // make the camera look to the point
        Vector3 lookAtVector = cameraLookAt.position - cameraTransform.position; 
        cameraTransform.forward -= (cameraTransform.forward - lookAtVector.normalized) * 0.5f;
        


    }


}
