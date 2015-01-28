using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {

	GameObject World;
	Vector3 Distance;
	float gravity=9.81f;
	Vector3 rota;
	float disx,disy,disz;
	void Start () 
	{
		World = GameObject.FindGameObjectWithTag ("World");
	}

	void Update () 
	{
		fGravity ();
		fForce ();
		fPosition ();
	}

	void fPosition()
	{
		transform.up = -Distance;
	}



	void fGravity()
	{
		Distance = World.transform.position - transform.position;
	}

	void fForce()
	{
		rigidbody.AddForce (Distance*gravity);
	}
}