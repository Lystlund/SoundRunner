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
	private float lowVar;
	private float highVar;
	private float midVar;
	private float zPosLow;
	private float zPosHigh;
	private float zPosMid;
	public float pan;

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
		listtoPD.Add (pan);


		List<float> listTest = new List<float>();
		listTest.Add (2.0f);
		listTest.Add (7.0f);
		listTest.Add (1.0f);
		listTest.Add (9.0f);
		listTest.Add (4.0f);

	//LOW OBSTACLES
		enemiesLow = GameObject.FindGameObjectsWithTag ("LowOBS");

		Debug.Log ("PUTTING INTO FLOAT LIST");
		int i = 0;
		foreach (GameObject g in enemiesLow) {
			enemiesLowDists.Add(g.transform.position.z);
			Debug.Log(enemiesLowDists[i]);
			i++;
		}

		enemiesLowDists.Sort ();

		Debug.Log ("SORTED FLOAT LIST");
		int ii = 0;
		foreach (float f in enemiesLowDists) {
			Debug.Log(f);
			ii++;
		}

		Debug.Log ("SORTED GAME OBJECTS");
		int iii = 0;
		foreach (float f in enemiesLowDists) {
			int swap = 0;
			foreach(GameObject g in enemiesLow){
				if(g.transform.position.z == f){
					GameObject temp = g;
					enemiesLow[swap] = enemiesLow[iii];
					enemiesLow[iii] = temp;
					iii++;
				}
				swap++;
			}

			Debug.Log(iii);
		}



	//HIGH OBSTACLES

		enemiesHigh = GameObject.FindGameObjectsWithTag ("HighOBS");

		int j = 0;
		foreach (GameObject g in enemiesHigh) {
			enemiesHighDists.Add(g.transform.position.z);
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
			foreach(GameObject g in enemiesHigh){
				if(g.transform.position.z == f){
					GameObject temp = g;
					enemiesHigh[swap] = enemiesHigh[jjj];
					enemiesHigh[jjj] = temp;
					jjj++;
				}
				swap++;
			}
		}




	//MID OBSTACLES

		enemiesMid = GameObject.FindGameObjectsWithTag ("MidOBS");

		int k = 0;
		foreach (GameObject g in enemiesMid) {
			enemiesMidDists.Add(g.transform.position.z);
			k++;
		}
		
		enemiesMidDists.Sort ();
		
		int kk = 0;
		foreach (float f in enemiesMidDists) {
			Debug.Log(f);
			kk++;
		}
		int kkk = 0;
		foreach (float f in enemiesMidDists) {
			int swap = 0;
			foreach(GameObject g in enemiesMid){
				if(g.transform.position.z == f){
					GameObject temp = g;
					enemiesMid[swap] = enemiesMid[kkk];
					enemiesMid[kkk] = temp;
					kkk++;
				}
				swap++;
			}
		}


		/*
		int k = 0;
		foreach (GameObject g in enemiesMid) {
			distanceMid = Vector3.Distance(enemiesMid[k].transform.position, transform.position);
			if(distanceMid < closestMid){
				closestMid = distanceMid;
				enemyMid = g;
			}
			k++;
		}*/

	}
	
	// Update is called once per frame
	void Update () {


	
	//LOW
		distanceLow = Vector3.Distance(enemiesLow[l].transform.position, transform.position);
		distanceLowNext = Vector3.Distance(enemiesLow[ln].transform.position, transform.position);

		if (distanceLowNext < distanceLow) {

			l++;
			ln++;
		}
		zPosLow = enemiesLow[l].transform.position.z - transform.position.z;


		if(zPosLow<0){	 								//REALIZED I MIGHT HAVE TO CHANGE THIS INTO PD
			f2Low = (f *(c/(c + v)))/350.0f;
		}
		if(zPosLow>0){
			f2Low = (f *(c/(c - v)))/350.0f;
		}

		lowVar = 1/distanceLow*100;
		if (lowVar > 50) {
			lowVar = 50;	
		}
		Debug.Log (lowVar);

	//HIGH
		distanceHigh = Vector3.Distance(enemiesHigh[h].transform.position, transform.position);
		distanceHighNext = Vector3.Distance(enemiesHigh[hn].transform.position, transform.position);
		
		if (distanceHighNext < distanceHigh) {
			h++;
			hn++;
		}
		zPosHigh = enemiesHigh[h].transform.position.z - transform.position.z;


		//distanceHigh = Vector3.Distance(enemyHigh.transform.position, transform.position);
		//zPosHigh = enemyHigh.transform.position.z - transform.position.z;
		
		if(zPosHigh<0){	 								//REALIZED I MIGHT HAVE TO CHANGE THIS INTO PD
			f2High = (f *(c/(c + v)))/350.0f;
		}
		if(zPosHigh>0){
			f2High = (f *(c/(c - v)))/350.0f;
		}
		
		highVar = 1/distanceHigh*100;

	//MID
		distanceMid = Vector3.Distance(enemiesMid[m].transform.position, transform.position);
		distanceMidNext = Vector3.Distance(enemiesMid[mn].transform.position, transform.position);
		
		if (distanceMidNext < distanceMid) {
			m++;
			mn++;
		}
		zPosMid = enemiesMid[m].transform.position.z - transform.position.z;

		
		if(zPosMid<0){	 								//REALIZED I MIGHT HAVE TO CHANGE THIS INTO PD
			f2Mid = (f *(c/(c + v)))/350.0f;
		}
		if(zPosMid>0){
			f2Mid = (f *(c/(c - v)))/350.0f;
		}
		
		midVar = 1/distanceMid*100;


		//Panning: 0 is left. 1 is right. 2 is both.

		//Debug.Log (enemiesMid [l].transform.position.x);
		if (enemiesMid[m].transform.position.x > 0) {
			pan = 1;
			//Debug.Log("RIGHT!");
		}
		else{
			pan = 0;
			//Debug.Log("LEFT!");
		}





		listtoPD [0] = lowVar;
		listtoPD [1] = f2Low;

		listtoPD [2] = highVar;
		listtoPD [3] = f2High;

		listtoPD [4] = midVar;
		listtoPD [5] = f2Mid;

		listtoPD [6] = pan;

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
}
