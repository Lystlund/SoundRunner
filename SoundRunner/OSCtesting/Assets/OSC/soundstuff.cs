using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class soundstuff : MonoBehaviour {

	private Dictionary<string, ServerLog> servers;
	private List<float> listtoPD = new List<float>();	

	public GameObject[] enemiesLow;
	public List<float> enemiesLowDists = new List<float> ();

	public GameObject[] enemiesHigh;
	public List<float> enemiesHighDists = new List<float> ();

	public GameObject[] enemiesMid;
	public List<float> enemiesMidDists = new List<float> ();

	public GameObject enemyLow;
	public GameObject enemyHigh;
	public GameObject enemyMid;

	public float[] testingArray;
	private float lowVar = 0;
	private float highVar = 0;
	private float midVar = 0;
	private float zPosLow = 0;
	private float zPosHigh = 0;
	private float zPosMid = 0;
	public float pan = 2;
	public float newPan = 0;

	private float distanceLow;
	private float distanceHigh;
	private float distanceMid;

	private float distanceLowNext;
	private float distanceHighNext;
	private float distanceMidNext;

	float closestLow = Mathf.Infinity;
	float closestHigh = Mathf.Infinity;
	float closestMid = Mathf.Infinity;

	int l = 0;
	int ln = 1;
	int h = 0;
	int hn = 1;
	int m = 0;
	int mn = 1;

	public float c = 34.0f;
	//λ2 = λ (1 + v/c)	
	public float v = 10.0f;	
	public float f = 440.0f;
	public float f2Low = 440.0f;
	public float f2High = 440.0f;
	public float f2Mid = 440.0f;

	bool lowObjects = false;
	bool highObjects = false;
	bool midObjects = false;


	// Use this for initialization
	void Start () {

		OSCHandler.Instance.Init(); //init OSC
		OSCHandler.Instance.SendMessageToClient ("pdThing", "/127.0.0.1", listtoPD);
		//OSCHandler.Instance.SendMessage ("hey");

		servers = new Dictionary<string, ServerLog>();

		pan = 2;

		listtoPD.Add (lowVar);
		listtoPD.Add (zPosLow);
		listtoPD.Add (highVar);
		listtoPD.Add (zPosHigh);
		listtoPD.Add (midVar);
		listtoPD.Add (zPosMid);
		listtoPD.Add (newPan);


		List<float> listTest = new List<float>();
		listTest.Add (2.0f);
		listTest.Add (7.0f);
		listTest.Add (1.0f);
		listTest.Add (9.0f);
		listTest.Add (4.0f);

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

					//Debug.Log (iii);
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



	//MID OBSTACLES

		enemiesMid = GameObject.FindGameObjectsWithTag ("MidOBS");

		if (enemiesMid.Length > 0) {
			midObjects = true;
			int k = 0;
			foreach (GameObject g in enemiesMid) {
					enemiesMidDists.Add (g.transform.position.z);
					k++;
			}

			enemiesMidDists.Sort ();

			int kk = 0;
			foreach (float f in enemiesMidDists) {
					//Debug.Log (f);
					kk++;
			}
			int kkk = 0;
			foreach (float f in enemiesMidDists) {
					int swap = 0;
					foreach (GameObject g in enemiesMid) {
							if (g.transform.position.z == f) {
									GameObject temp = g;
									enemiesMid [swap] = enemiesMid [kkk];
									enemiesMid [kkk] = temp;
									kkk++;
							}
							swap++;
					}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {


	
	//LOW
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


			if (zPosLow < 0) {	 								//REALIZED I MIGHT HAVE TO CHANGE THIS INTO PD
					f2Low = (f * (c / (c + v))) / 350.0f;
			}
			if (zPosLow > 0) {
					f2Low = (f * (c / (c - v))) / 350.0f;
			}


			if(zPosLow < 1 && zPosLow > 0){
				zPosLow = 1;
			}
			else if(zPosLow < 0 && zPosLow > -1){
				zPosLow = 1;
			}

			lowVar = Mathf.Abs( 1 / zPosLow * 100);
			//if (lowVar > 50) {
			//		lowVar = 50;	
			//}
			//Debug.Log (lowVar);
		}
	//HIGH
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


			//distanceHigh = Vector3.Distance(enemyHigh.transform.position, transform.position);
			//zPosHigh = enemyHigh.transform.position.z - transform.position.z;

			if (zPosHigh < 0) {	 								//REALIZED I MIGHT HAVE TO CHANGE THIS INTO PD
					f2High = (f * (c / (c + v))) / 350.0f;
			}
			if (zPosHigh > 0) {
					f2High = (f * (c / (c - v))) / 350.0f;
			}
			if(zPosHigh < 1 && zPosHigh > 0){
				zPosHigh = 1;
			}
			else if(zPosHigh < 0 && zPosHigh > -1){
				zPosHigh = 1;
			}
			highVar = Mathf.Abs( 1 / zPosHigh * 100);
		}
	//MID
		if (midObjects) {
			distanceMid = Vector3.Distance (enemiesMid [m].transform.position, transform.position);

			if(mn > enemiesMid.Length-1)
			{
			}
			else{
				distanceMidNext = Vector3.Distance (enemiesMid [mn].transform.position, transform.position);
			}

			//Debug.Log(enemiesMid[m]+ " "+enemiesMid[mn]);
			Debug.Log(distanceMidNext+ " "+distanceMid);
			
			if (distanceMidNext < distanceMid) {
				//if(mn+1 == enemiesMid.Length) {
			//	}
				//else{
					m++;
					mn++;
				//}
			}

			zPosMid = enemiesMid [m].transform.position.z - transform.position.z;


			if (zPosMid < 0) {	 								//REALIZED I MIGHT HAVE TO CHANGE THIS INTO PD
					f2Mid = (f * (c / (c + v))) / 350.0f;
			}
			if (zPosMid > 0) {
					f2Mid = (f * (c / (c - v))) / 350.0f;
			}

			if(zPosMid < 1 && zPosMid > 0){
				zPosMid = 1;
			}
			else if(zPosMid < 0 && zPosMid > -1){
				zPosMid = 1;
			}
			midVar = Mathf.Abs( 1 / zPosMid* 100);
			//if(midVar > 70 || midVar < -70){
			//	midVar = 70;
			//}


			//Panning: 0 is left. 1 is right. 2 is both.

			//OLD PAN
			if (enemiesMid [m].transform.position.x > 0) {
				pan = 1;
				newPan = midVar;
				if(newPan > 45){
					newPan = 45;
				}
					//Debug.Log("RIGHT!");
			} else {
				pan = 0;
				newPan = midVar*(-1);
				if(newPan < -45){
					newPan = -45;
				}
					//Debug.Log("LEFT!");
			}
			//Debug.Log(newPan);


		}

		listtoPD [0] = lowVar;
		listtoPD [1] = f2Low;

		listtoPD [2] = highVar;
		listtoPD [3] = f2High;

		listtoPD [4] = midVar;
		listtoPD [5] = f2Mid;

		listtoPD [6] = newPan;

		OSCHandler.Instance.SendMessageToClient ("pdThing", "/127.0.0.1", listtoPD);

		// http://en.flossmanuals.net/pure-data/network-data/osc/ 



		//Opposite Doppler. Don't use this.
		/*
		if(zPos<0){	 
			λ2 = (λ *((c + v)/c))/350.0f;
		}	 
		if(zPos>0){	 
			λ2 = (λ *((c - v)/c))/350.0f;
		}*/


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

		OSCHandler.Instance.Init(); //init OSC

		OSCHandler.Instance.SendMessageToClient ("pdThing", "/127.0.0.1", listtoPD);

	}


}
