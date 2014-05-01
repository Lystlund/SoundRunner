using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class Movement : MonoBehaviour {

	public float mSpeed = 0.3f;
	public int ObsHit = 0;
	bool justStarted = true;
	public int levelholder;
	string fileName;
	public AudioClip hurt;
	public AudioSource dodge;
	public Vector3 heroPos;
	public float ControlHolder;
	public Tutorial tut;
	Vector3 midVec;
	Vector3 rightVec;
	Vector3 leftVec;
	bool midPos;
	bool leftPos;
	bool rightPos;
	public bool playDodgeSound = false;
	bool hitIsPlaying = false;


	IEnumerator WaitForStart() {
		yield return new WaitForSeconds(1);
		justStarted = false;
	}

	void Start () {

		midPos = true;
		leftPos = false;
		rightPos = false;

		if (Application.loadedLevel == 1){
			fileName = "SoundRunner_Visual_" + System.DateTime.Now.ToString("dd-MM-yy_hh-mm-ss") + ".txt"; 
			StreamWriter sw1 = new StreamWriter(fileName, true);
			sw1.WriteLine("Visual.");
			sw1.WriteLine("hit, tag, zPos");
			sw1.Close();
		}
		if (Application.loadedLevel == 10){
			fileName = "SoundRunner_Auditory_" + System.DateTime.Now.ToString("dd-MM-yy_hh-mm-ss") + ".txt"; 
			StreamWriter sw1 = new StreamWriter(fileName, true);
			sw1.WriteLine("Auditory.");
			sw1.WriteLine("hit, tag, zPos");
			sw1.Close();
		}
	}

	void Update () {
		
		ControlHolder = transform.position.x;

		heroPos.x = transform.position.x;
		heroPos.y = transform.position.y;
		heroPos.z = transform.position.z;

		midVec = new Vector3 (0,heroPos.y,heroPos.z);
		rightVec = new Vector3 (3,heroPos.y,heroPos.z);
		leftVec = new Vector3 (-3,heroPos.y,heroPos.z);

		//Debug.Log(heroPos.x);
		//Debug.Log("Left: " + leftPos + "     Mid: " + midPos + "     Right: " + rightPos);  

		int level = Application.loadedLevel;
		levelholder = level;
		
		/*
		if((Input.GetKeyDown(KeyCode.A)))	 {
			StreamWriter sw1 = new StreamWriter(fileName, true);
			long rightNow = System.DateTime.Now.Second;
			long diffRight = rightNow - clockInit;
			string stringNow = diffRight.ToString();
			sw1.WriteLine(stringNow + ",0");
			sw1.Close();
		}
		if((Input.GetKeyDown(KeyCode.D)))	 {
			StreamWriter sw1 = new StreamWriter(fileName, true);
			long rightNow = System.DateTime.Now.Second;
			long diffRight = rightNow - clockInit;
			string stringNow = diffRight.ToString();
			sw1.WriteLine("0," + stringNow);
			sw1.Close();
		}
		*/


		if (Application.loadedLevel == 9 && transform.position.z > 199) {
			Destroy(this.gameObject);
		}

		
		if (justStarted == true) {
			StartCoroutine (WaitForStart());
		} 

		else {
			//Forward speed	
			transform.position += new Vector3 (0, 0, mSpeed);

			SphereCollider myCollider = transform.GetComponent<SphereCollider> ();

			//Move left	
			if (Input.GetKeyDown (KeyCode.A) && midPos == true)  {
				transform.position = leftVec;
				leftPos = true;
				midPos = false;
			}

			if (Input.GetKeyDown (KeyCode.A) && rightPos == true) {
				transform.position = midVec;
				midPos = true;
				rightPos = false;
			}
			
			//Move right
			if (Input.GetKeyDown (KeyCode.D) && midPos == true) {
				transform.position = rightVec;
				rightPos = true;
				midPos = false;
			}

			if (Input.GetKeyDown (KeyCode.D) && leftPos == true) {
				transform.position = midVec;
				midPos = true;
				leftPos = false;
			}

			//jump	
			if (Input.GetKeyDown (KeyCode.W) && transform.position.y < 0.8f) {
				rigidbody.AddForce(0, 25, 0 , ForceMode.Impulse);
			}
			
			//if target gets to high, makes them fall down faster
			if (transform.position.y > 2.5f){
				rigidbody.AddForce(0, -2.5f, 0 , ForceMode.Impulse);
			}
			
			//crouch	
			if (Input.GetKey (KeyCode.S)) {
				transform.localScale = new Vector3 (1, 0.5f, 1); //as long as s is pressed scale to this size 
				myCollider.radius = 0.25f;
				rigidbody.AddForce(0, -2, 0 , ForceMode.Impulse);
			}
			
			//normal size
			else {
				transform.localScale = new Vector3 (1, 1, 1); //else be this size
				myCollider.radius = 0.5f;
			}

			if (Input.GetKey (KeyCode.Escape)) {
				Application.LoadLevel(0);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "LowOBS" || other.gameObject.tag == "MidROBS" || other.gameObject.tag == "MidLOBS" || other.gameObject.tag == "HighOBS") { 
			AudioSource.PlayClipAtPoint(hurt,heroPos, 0.5f);
			hitIsPlaying = true;
	
			if (Application.loadedLevel == 1 || Application.loadedLevel == 10){
			ObsHit = ObsHit + 1;
			StreamWriter sw1 = new StreamWriter(fileName, true);
			sw1.WriteLine(ObsHit + ", " + other.gameObject.tag + ", " + transform.position.z);
			sw1.Close();
			}

			else{
				Application.LoadLevel(levelholder);
			}
		}

		if (other.gameObject.tag == "Finish") {
			if (Application.loadedLevel == 1){
				Application.LoadLevel(0);
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

			if (Application.loadedLevel == 8){
				Application.LoadLevel(8);
			}

			if (Application.loadedLevel == 9){
				Application.LoadLevel(11);
			}
			if (Application.loadedLevel == 10){
				Application.LoadLevel(0);
			}

		}
		
	}

	public void PlayDodgeSound(){
		if (!audio.isPlaying)
			if (hitIsPlaying) {
			hitIsPlaying = false;
			} else {
			dodge.Play ();
			}
			
	}

}
