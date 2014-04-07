using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float mSpeed = 0.3f;
	public int ObsHit = 0;
	bool justStarted = true;

	IEnumerator WaitForStart() {
		yield return new WaitForSeconds(1);
		justStarted = false;
	}

	void Start () {

	}

	void Update () {
		if (justStarted == true) {
			StartCoroutine (WaitForStart());
		} 
		else {
			SphereCollider myCollider = transform.GetComponent<SphereCollider> ();

			//Forward speed	
			transform.position += new Vector3 (0, 0, mSpeed);

			//Move left	
			if (Input.GetKeyDown (KeyCode.A) && transform.position.x > -2.0f) {
					transform.position += new Vector3 (-3.0f, 0, 0);
			}

			//Move right
			if (Input.GetKeyDown (KeyCode.D) && transform.position.x < 2.0f) {
					transform.position += new Vector3 (3.0f, 0, 0);
			}

			//jump	
			if (Input.GetKeyDown (KeyCode.W) && transform.position.y < 0.8) {
				rigidbody.AddForce(0, 8, 0, ForceMode.Impulse);
			}

			//crouch	
			if (Input.GetKey (KeyCode.S)) {
					transform.localScale = new Vector3 (1, 0.5f, 1); //as long as s is pressed scale to this size 
					myCollider.radius = 0.25f;
			} 
			//normal size
			else {
					transform.localScale = new Vector3 (1, 1, 1); //else be this size
					myCollider.radius = 0.5f;
			}

		}
	}

	void OnTriggerEnter(Collider other)
	{	
		if (other.gameObject.tag == "Enemy") { 
			ObsHit = ObsHit + 1;
			Debug.Log (ObsHit);
		}

		if (other.gameObject.tag == "Finish") {
			Application.LoadLevel(0);
		}
	}
}
