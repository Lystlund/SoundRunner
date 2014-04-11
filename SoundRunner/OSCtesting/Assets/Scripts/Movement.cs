using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class Movement : MonoBehaviour {
	public float mSpeed = 0.3f;
	public int ObsHit = 0;
	bool justStarted = true;
	public int levelholder;
	public long clockInit = 0;

	IEnumerator WaitForStart() {
		yield return new WaitForSeconds(1);
		justStarted = false;
	}

	void Start () {
		if (Application.loadedLevel == 1){
			clockInit = System.DateTime.Now.Second;
			StreamWriter sw1 = new StreamWriter("tilCSV.txt", true);
			sw1.WriteLine("hit, time, zPos");
			sw1.Close();
		}
	}

	void Update () {
		int level = Application.loadedLevel;
		levelholder = level;


		/*
		if((Input.GetKeyDown(KeyCode.A)))	 {
			StreamWriter sw1 = new StreamWriter("tilCSV.txt", true);
			long rightNow = System.DateTime.Now.Second;
			long diffRight = rightNow - clockInit;
			string stringNow = diffRight.ToString();
			sw1.WriteLine(stringNow + ",0");
			sw1.Close();
		}
		if((Input.GetKeyDown(KeyCode.D)))	 {
			StreamWriter sw1 = new StreamWriter("tilCSV.txt", true);
			long rightNow = System.DateTime.Now.Second;
			long diffRight = rightNow - clockInit;
			string stringNow = diffRight.ToString();
			sw1.WriteLine("0," + stringNow);
			sw1.Close();
		}
		*/
		
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
				rigidbody.AddForce(0, 20, 0 , ForceMode.Impulse);
			}
			//if target gets to high, makes them fall down faster
			if (transform.position.y > 2){
				rigidbody.AddForce(0, -4, 0 , ForceMode.Impulse);
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
		if (other.gameObject.tag == "LowOBS" || other.gameObject.tag == "MidOBS" || other.gameObject.tag == "HighOBS") { 
			if (Application.loadedLevel == 1){
			ObsHit = ObsHit + 1;
			long rightNow = System.DateTime.Now.Second;
			StreamWriter sw1 = new StreamWriter("tilCSV.txt", true);
			string stringNow = System.DateTime.Now.Ticks.ToString();
			sw1.WriteLine(ObsHit + ", " + (rightNow-clockInit) + ", " + transform.position.z);
			sw1.Close();
			}

			else{
				Application.LoadLevel(levelholder);
			}
		}

		if (other.gameObject.tag == "Finish") {
			if (Application.loadedLevel == 1){
				ObsHit = ObsHit + 1;
				StreamWriter sw1 = new StreamWriter("tilCSV.txt", true);
				sw1.WriteLine("-------------------------- GAME ENDED --------------------------");
				sw1.Close();
			}
			if (Application.loadedLevel == 3){
				Application.LoadLevel(3);
			}

			if (Application.loadedLevel == 4){
				Application.LoadLevel(4);
			}

			if (Application.loadedLevel == 5){
				Application.LoadLevel(5);
			}

			if (Application.loadedLevel == 6){
				Application.LoadLevel(6);
			}

			if (Application.loadedLevel == 7){
				Application.LoadLevel(7);
			}
		}
	}
}
