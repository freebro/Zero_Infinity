using UnityEngine;
using System.Collections;

public class PController : MonoBehaviour {
	int speed = 8000;
	State state= new State();
	public float fTemp=1;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(state.myState)
		{
		case MyState.frozen:
			if(Input.GetMouseButtonDown (0))
			{
				fTemp +=1.5f;
			}

			if(fTemp >= 10 )
			{
				state.myState = MyState.warm;
			}
			transform.renderer.material.color = Color.blue;
			break;
		case MyState.warm:
			move ();
			if(fTemp<10 )
			{
				state.myState = MyState.frozen;
			}
			if(fTemp >= 50 )
			{
				state.myState = MyState.hot;
			}
			transform.renderer.material.color = Color.green;
			break;
		case MyState.hot:
			move ();
			if(fTemp < 50 )
			{
				state.myState = MyState.warm;
			}
			if(fTemp >= 100)
			{
				state.myState = MyState.epiclyHot;
			}
			transform.renderer.material.color = Color.magenta;
			break;
		case MyState.epiclyHot:
			move ();
			if(fTemp < 100 )
			{
				state.myState = MyState.hot;
			}
			transform.renderer.material.color = Color.red;
			break;
		}
		temp ();
	}

	void temp()
	{
		Debug.Log ("changing tempratureoverhere");
		if(state.myState == MyState.frozen)
		{
			fTemp -=Time.deltaTime*2;
		}
		else
		{
			fTemp -=Time.deltaTime;
		}


		if(fTemp <0)
			fTemp =0;

	}

	void move()
	{
		if(Input.GetAxis ("Horizontal") !=0)
		{
			rigidbody.AddRelativeForce (new Vector3(Input.GetAxis ("Horizontal")*Time.deltaTime*speed,0,0));
		}
		if(Input.GetAxis ("Vertical")!=0)
		{
			rigidbody.AddRelativeForce (new Vector3(0,0,Input.GetAxis ("Vertical")*Time.deltaTime*speed));
			fTemp+=0.05f;
		}
	}
}
