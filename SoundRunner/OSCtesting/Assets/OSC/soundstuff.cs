using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class soundstuff : MonoBehaviour {

	GameObject hero;
	Movement heroScript;

	private Dictionary<string, ServerLog> servers;
	private List<float> listtoPD = new List<float>();	

	public GameObject[] enemiesLow;
	public List<float> enemiesLowDists = new List<float> ();

	public GameObject[] enemiesHigh;
	public List<float> enemiesHighDists = new List<float> ();

	public GameObject[] enemiesMidR;
	public List<float> enemiesMidRDists = new List<float> ();

	public GameObject[] enemiesMidL;
	public List<float> enemiesMidLDists = new List<float> ();

	public GameObject enemyLow;
	public GameObject enemyHigh;
	public GameObject enemyMidR;
	public GameObject enemyMidL;

	public float[] testingArray;
	private float lowVar = 0;
	private float highVar = 0;
	private float midRVar = 0;
	private float midLVar = 0;

	private float zPosLow = 0;
	private float zPosHigh = 0;
	private float zPosMidR = 0;
	private float zPosMidL = 0;

	float lVolVar = 0;
	float hVolVar = 0;
	float mRVolVar = 0;
	float mLVolVar = 0;

	public float pan = 2;
	public float newPan = 0;

	private float distanceLow;
	private float distanceHigh;
	private float distanceMidR;
	private float distanceMidL;

	private float distanceLowNext;
	private float distanceHighNext;
	private float distanceMidRNext;
	private float distanceMidLNext;

	float closestLow = Mathf.Infinity;
	float closestHigh = Mathf.Infinity;
	float closestMidR = Mathf.Infinity;
	float closestMidL = Mathf.Infinity;

	int l = 0;
	int ln = 1;
	int h = 0;
	int hn = 1;
	int mr = 0;
	int mrn = 1;
	int ml = 0;
	int mln = 1;

	public float c = 34.0f;
	//λ2 = λ (1 + v/c)	
	public float v = 10.0f;	
	public float f = 440.0f;
	public float f2Low = 440.0f;
	public float f2High = 440.0f;
	public float f2MidR = 440.0f;
	public float f2MidL = 440.0f;

	bool lowObjects = false;
	bool highObjects = false;
	bool midRObjects = false;
	bool midLObjects = false;

	bool shouldPlaySoundL = false;
	bool shouldPlaySoundH = false;
	bool shouldPlaySoundMR = false;
	bool shouldPlaySoundML = false;


	List<float> listTest = new List<float>();


	// Use this for initialization
	void Start () {
		hero = GameObject.FindGameObjectWithTag ("Player");
		if(hero != null)
			heroScript = hero.GetComponent<Movement> ();

		OSCHandler.Instance.Init(); //init OSC
		OSCHandler.Instance.SendMessageToClient ("pdThing", "/127.0.0.1", listtoPD);
		//OSCHandler.Instance.SendMessage ("hey");

		servers = new Dictionary<string, ServerLog>();

		listtoPD.Add (lowVar);
		listtoPD.Add (zPosLow);
		listtoPD.Add (lVolVar);
		listtoPD.Add (highVar);
		listtoPD.Add (zPosHigh);
		listtoPD.Add (hVolVar);
		listtoPD.Add (midRVar);
		listtoPD.Add (zPosMidR);
		listtoPD.Add (mRVolVar);
		listtoPD.Add (midLVar);
		listtoPD.Add (zPosMidL);
		listtoPD.Add (mLVolVar);



		listTest.Add (2.0f);
		listTest.Add (7.0f);
		listTest.Add (1.0f);
		listTest.Add (9.0f);
		listTest.Add (4.0f);



//------------------------------------------------ CREATING THE ARRAYS ---------------------

	//LOW OBSTACLES
		enemiesLow = GameObject.FindGameObjectsWithTag ("LowOBS");

		if (enemiesLow.Length > 0) {
			lowObjects = true;
			//Debug.Log (" PUTTING INTO FLOAT LIST");
			int i = 0;
			foreach (GameObject g in enemiesLow) {
					enemiesLowDists.Add (g.transform.position.z);
					//Debug.Log (enemiesLowDists [i]);
					i++;
			}

			enemiesLowDists.Sort ();

			//Debug.Log ("SORTED FLOAT LIST");
			int ii = 0;
			foreach (float f in enemiesLowDists) {
					//Debug.Log (f);
					ii++;
			}

			//Debug.Log ("SORTED GAME OBJECTS");
			int iii = 0;
			foreach (float f in enemiesLowDists) {
				int swap = 0;
				foreach (GameObject g in enemiesLow) {
					if (g.transform.position.z == f) {
						GameObject temp = g;
						enemiesLow [swap] = enemiesLow [iii];
						enemiesLow [iii] = temp;
						iii++;
					}
					swap++;
				}
			}
		}

	//HIGH OBSTACLES

		enemiesHigh = GameObject.FindGameObjectsWithTag ("HighOBS");

		if (enemiesHigh.Length > 0) {
			highObjects = true;
			int j = 0;
			foreach (GameObject g in enemiesHigh) {
					enemiesHighDists.Add (g.transform.position.z);
					j++;
			}

			enemiesHighDists.Sort ();

			int jj = 0;
			foreach (float f in enemiesHighDists) {
					jj++;
			}
			int jjj = 0;
			foreach (float f in enemiesHighDists) {
				int swap = 0;
				foreach (GameObject g in enemiesHigh) {
					if (g.transform.position.z == f) {
						GameObject temp = g;
						enemiesHigh [swap] = enemiesHigh [jjj];
						enemiesHigh [jjj] = temp;
						jjj++;
					}
					swap++;
				}
			}
		}



	//MID RIGHT OBSTACLES

		enemiesMidR = GameObject.FindGameObjectsWithTag ("MidROBS");

		if (enemiesMidR.Length > 0) {
			midRObjects = true;
			int k = 0;
			foreach (GameObject g in enemiesMidR) {
					enemiesMidRDists.Add (g.transform.position.z);
					k++;
			}

			enemiesMidRDists.Sort ();

			int kk = 0;
			foreach (float f in enemiesMidRDists) {
					//Debug.Log (f);
					kk++;
			}
			int kkk = 0;
			foreach (float f in enemiesMidRDists) {
				int swap = 0;
				foreach (GameObject g in enemiesMidR) {
					if (g.transform.position.z == f) {
						GameObject temp = g;
						enemiesMidR [swap] = enemiesMidR [kkk];
						enemiesMidR [kkk] = temp;
						kkk++;
					}
					swap++;
				}
			}
		}


	//MID LEFT OBSTACLES
		
		enemiesMidL = GameObject.FindGameObjectsWithTag ("MidLOBS");
		
		if (enemiesMidL.Length > 0) {
			midLObjects = true;
			int k = 0;
			foreach (GameObject g in enemiesMidL) {
				enemiesMidLDists.Add (g.transform.position.z);
				k++;
			}
			
			enemiesMidLDists.Sort ();
			
			int kk = 0;
			foreach (float f in enemiesMidLDists) {
				//Debug.Log (f);
				kk++;
			}
			int kkk = 0;
			foreach (float f in enemiesMidLDists) {
				int swap = 0;
				foreach (GameObject g in enemiesMidL) {
					if (g.transform.position.z == f) {
						GameObject temp = g;
						enemiesMidL [swap] = enemiesMidL [kkk];
						enemiesMidL [kkk] = temp;
						kkk++;
					}
					swap++;
				}
			}
		}


	}



// -------------------------------------------------   UPDATE !!!! -----------------------------------------------------
	void Update () {

	
	// --------------------- LOW
		if (lowObjects) {
			distanceLow = Vector3.Distance (enemiesLow [l].transform.position, transform.position);

			if(ln > enemiesLow.Length-1)
			{}
			else{
				distanceLowNext = Vector3.Distance (enemiesLow [ln].transform.position, transform.position);
			}

			if (distanceLowNext < distanceLow) {
				if(ln+1 > enemiesLow.Length-1) 
				{}
				else{
					l++;
					ln++;
				}
			}

			zPosLow = enemiesLow [l].transform.position.z - transform.position.z;

			if(zPosLow < 1 && zPosLow > -1){ //Limiting it to not go below 1, since 0. values makes the sound go nuts.
				zPosLow = 1;
			}

			lowVar =  Mathf.Abs( 1 / zPosLow * 100);
			lVolVar =  40 - Mathf.Abs(20* Mathf.Log10(Mathf.Abs(zPosLow)));
			if(lVolVar < 0)
				lVolVar = 0;
			//Debug.Log(zPosLow+ "  V: "+lowVar);
		

			if (zPosLow < 0) { // For Doppler
				f2Low = 1;
				if(shouldPlaySoundL){
					Debug.Log("PLAY A SOUND");
					heroScript.PlayDodgeSound();
					shouldPlaySoundL = false;
				}

			}
			if (zPosLow > 0) {
				f2Low = 0;
				shouldPlaySoundL = true;
			}


		}


	// --------------------- HIGH
		if (highObjects) {
			distanceHigh = Vector3.Distance (enemiesHigh [h].transform.position, transform.position);

			if(hn > enemiesHigh.Length-1)
			{
			}
			else{
				distanceHighNext = Vector3.Distance (enemiesHigh [hn].transform.position, transform.position);
			}

			if (distanceHighNext < distanceHigh) {
				if(hn+1 > enemiesHigh.Length-1) {
				}
				else{
					h++;
					hn++;
				}
			}

			zPosHigh = enemiesHigh [h].transform.position.z - transform.position.z;

			if (zPosHigh < 0) {
				f2High = 1;
				if(shouldPlaySoundH){
					Debug.Log("PLAY A SOUND");
					heroScript.PlayDodgeSound();
					shouldPlaySoundH = false;
				}
			}
			if (zPosHigh > 0) {
				f2High = 0;
				shouldPlaySoundH = true;
			}


			if(zPosHigh < 1 && zPosHigh > 0){
				zPosHigh = 1;
			}
			else if(zPosHigh < 0 && zPosHigh > -1){
				zPosHigh = 1;
			}

			highVar = Mathf.Abs( 1 / zPosHigh * 100);
			hVolVar =  30 - Mathf.Abs(20* Mathf.Log10(Mathf.Abs(zPosHigh)));
			if(hVolVar < 0)
				hVolVar = 0;

		}


	// --------------------- MID RIGHT
		if (midRObjects) {
			distanceMidR = Vector3.Distance (enemiesMidR [mr].transform.position, transform.position);

			if(mrn > enemiesMidR.Length-1)
			{
			}
			else{
				distanceMidRNext = Vector3.Distance (enemiesMidR [mrn].transform.position, transform.position);
			}
			
			if (distanceMidRNext < distanceMidR) {
					mr++;
					mrn++;
			}

			zPosMidR = enemiesMidR [mr].transform.position.z - transform.position.z;

			if(zPosMidR < 1 && zPosMidR > -1){ 
				zPosMidR = 1;
			}

			midRVar = Mathf.Abs( 1 / zPosMidR * 100);
			mRVolVar =  40 - Mathf.Abs(20* Mathf.Log10(Mathf.Abs(zPosMidR)));
			if(mRVolVar < 0)
				mRVolVar = 0;

			if (zPosMidR < 0) {	 //Doppler
				f2MidR = 1;
				if(shouldPlaySoundMR){
					Debug.Log("PLAY A SOUND");
					heroScript.PlayDodgeSound();
					shouldPlaySoundMR = false;
				}
			}
			if (zPosMidR > 0) {
				f2MidR = 0;
				shouldPlaySoundMR = true;
			}

			//PAN
			/*if (enemiesMidR [mr].transform.position.x > 0) {
				pan = 1;
				newPan = midRVar;
				if(newPan > 45){
					newPan = 45;
				}
					//Debug.Log("RIGHT!");
			} else {
				pan = 0;
				newPan = midRVar*(-1);
				if(newPan < -45){
					newPan = -45;
				}
			}*/
		}


	// --------------- MID LEFT
		if (midLObjects) {
			distanceMidL = Vector3.Distance (enemiesMidL [ml].transform.position, transform.position);

			if (mln > enemiesMidL.Length - 1) {
			} else {
					distanceMidLNext = Vector3.Distance (enemiesMidL [mln].transform.position, transform.position);
			}

			if (distanceMidLNext < distanceMidL) {
					ml++;
					mln++;
			}

			zPosMidL = enemiesMidL [ml].transform.position.z - transform.position.z;

			if (zPosMidL < 1 && zPosMidL > -1) { 
					zPosMidL = 1;
			}

			midLVar = Mathf.Abs (1 / zPosMidL * 100);
			mLVolVar = 40 - Mathf.Abs (20 * Mathf.Log10 (Mathf.Abs (zPosMidL)));
			if (mLVolVar < 0)
					mLVolVar = 0;

			if (zPosMidL < 0) {	 //Doppler
					f2MidL = 1;
				if(shouldPlaySoundML){
					Debug.Log("PLAY A SOUND");
					heroScript.PlayDodgeSound();
					shouldPlaySoundML = false;
				}
			}
			if (zPosMidL > 0) {
				f2MidL = 0;
				shouldPlaySoundML = true;
			}

		}




		listtoPD [0] = lowVar;
		listtoPD [1] = f2Low;
		listtoPD [2] = lVolVar;

		listtoPD [3] = highVar;
		listtoPD [4] = f2High;
		listtoPD [5] = hVolVar;

		listtoPD [6] = midRVar;
		listtoPD [7] = f2MidR;
		listtoPD [8] = mRVolVar;
		
		listtoPD [9] = midLVar;
		listtoPD [10] = f2MidL;
		listtoPD [11] = mLVolVar;

		OSCHandler.Instance.SendMessageToClient ("pdThing", "/127.0.0.1", listtoPD);

		// http://en.flossmanuals.net/pure-data/network-data/osc/ 


	}


	void OnApplicationQuit(){

		//SENDING 0

		listtoPD [0] = 0;
		listtoPD [1] = 0;
		listtoPD [2] = 0;
		listtoPD [3] = 0;
		listtoPD [4] = 0;
		listtoPD [5] = 0;
		listtoPD [6] = 0;
		listtoPD [7] = 0;
		listtoPD [8] = 0;
		listtoPD [9] = 0;
		listtoPD [10] = 0;
		listtoPD [11] = 0;

		OSCHandler.Instance.Init(); //Need to initialize it again, because apparently it gets deleted before this function is executed.

		OSCHandler.Instance.SendMessageToClient ("pdThing", "/127.0.0.1", listtoPD);

	}


}
