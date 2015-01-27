using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {

	GameObject World;
	Vector3 Distance;
	float gravity=9.81f;

	void Start () 
	{
		World = GameObject.FindGameObjectWithTag ("World");
	}

	void Update () 
	{
		fGravity ();
		fForce ();

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