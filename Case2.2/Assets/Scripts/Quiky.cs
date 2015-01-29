using UnityEngine;
using System.Collections;

public class Quiky : MonoBehaviour {
	float move = 0.2f;
	PController pcontroller;
	float temp;


	void Update () 
	{
		Debug.DrawRay (transform.position,new Vector3(0,0,1));
		Debug.Log (temp);
		if(Input.GetMouseButtonDown (0))
		{
			if(Physics.Raycast (transform.position,new Vector3(0,0,1),10f,1<<LayerMask.NameToLayer ("Cube")))
			{
				temp +=2;
				transform.renderer.material.color=Color.blue;
			}
			else
			{
				transform.renderer.material.color=Color.black;
			}
		}
		//Debug.Log (temp);
		temp -= Time.deltaTime;
		transform.position -= new Vector3(0,move,0);

		if(transform.position.y <= -5)
			move = -move;
		if(transform.position.y >=5)
			move = -move;
	}
}