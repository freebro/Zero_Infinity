using UnityEngine;
using System.Collections;

public class PController : MonoBehaviour {
	int speed = 8000;
	State state= new State();
	public float fTemp=1;
	public Transform colloooor;
	public GameObject quikTimeEvent;
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
			fTemp -= Time.deltaTime;
			quikTimeEvent.SetActive (true);


			if(fTemp >= 10 )
			{
				state.myState = MyState.warm;
			}
			colloooor.renderer.material.color = Color.blue;
			break;
		case MyState.warm:
			quikTimeEvent.SetActive (false);
			fTemp -= Time.deltaTime*2;
			move ();
			if(fTemp<10 )
			{
				state.myState = MyState.frozen;
			}
			if(fTemp >= 50 )
			{
				state.myState = MyState.hot;
			}
			colloooor.renderer.material.color = Color.green;
			break;
		case MyState.hot:
			fTemp -= Time.deltaTime*3;
			move ();
			if(fTemp < 50 )
			{
				state.myState = MyState.warm;
			}
			if(fTemp >= 100)
			{
				state.myState = MyState.epiclyHot;
			}
			colloooor.renderer.material.color = Color.yellow;
			break;
		case MyState.epiclyHot:
			fTemp -= Time.deltaTime*5;
			move ();
			if(fTemp < 100 )
			{
				state.myState = MyState.hot;
			}
			colloooor.renderer.material.color = Color.red;
			break;
		}
		temp ();
	}

	void temp()
	{
		if(fTemp <1)
			fTemp =1;

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
