using UnityEngine;
using System.Collections;

public abstract class BaseData : MonoBehaviour {

    public string DataName;
    public bool isActive;
   
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetActive()
    {
        isActive = true;
    }

    public void SetUnactive()
    {
        isActive = false;
    }



}
