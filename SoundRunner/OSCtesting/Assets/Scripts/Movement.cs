using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float mSpeed = 0.3f;
	public int ObsHit = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		SphereCollider myCollider = transform.GetComponent<SphereCollider>();

		//Forward speed	
		transform.position += new Vector3(0,0,mSpeed);
		
		//Move left	
		if(Input.GetKeyDown(KeyCode.A))
		{
			transform.position += new Vector3(-3.0f,0,0);
		}
		
		//Move right
		if(Input.GetKeyDown(KeyCode.D))
		{
			transform.position += new Vector3(3.0f,0,0);
		}
		
		//jump	
		if(Input.GetKeyDown(KeyCode.W) && transform.position.y < 1)
		{
			rigidbody.AddForce(new Vector3(0,300.0f,0));	
		}
		
		//crouch	
		if(Input.GetKey(KeyCode.S))
		{
			transform.localScale = new Vector3(1,0.5f,1); //as long as s is pressed scale to this size 
			myCollider.radius = 0.25f;
		}
		else
		{
			transform.localScale = new Vector3(1, 1,1); //else be this size
		}

	}

	void OnTriggerEnter(Collider other)
	{	
		if(other.gameObject.tag == "Enemy") //if you hit an enemy(obstacle) you loose one health
		{
			ObsHit = ObsHit + 1;
			Debug.Log(ObsHit);
		}
	}
}
