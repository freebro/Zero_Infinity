using UnityEngine;
using System.Collections;

public class PController : MonoBehaviour {
	int speed = 4000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetAxis ("Horizontal") !=0)
		{
			Debug.Log ("Hor");
			rigidbody.AddRelativeForce (new Vector3(Input.GetAxis ("Horizontal")*Time.deltaTime*speed,0,0));
		}
		if(Input.GetAxis ("Vertical")!=0)
		{
			Debug.Log("ver");
			rigidbody.AddRelativeForce (new Vector3(0,Input.GetAxis ("Vertical")*Time.deltaTime*speed,0));
		}
	}
}
