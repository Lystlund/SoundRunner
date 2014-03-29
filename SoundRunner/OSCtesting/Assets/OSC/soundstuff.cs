using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class soundstuff : MonoBehaviour {

	private Dictionary<string, ServerLog> servers;
	private List<float> listtoPD = new List<float>();	
	private float sendVar;
	public GameObject enemy;
	private float zPos;

	public float c = 34.0f;
	//λ2 = λ (1 + v/c)	
	public float v = 10.0f;	
	public float λ = 440.0f;	
	public float λ2 = 440.0f;	

	// Use this for initialization
	void Start () {

		OSCHandler.Instance.Init(); //init OSC
		OSCHandler.Instance.SendMessageToClient ("pdThing", "/127.0.0.1", listtoPD);
		//OSCHandler.Instance.SendMessage ("hey");

		servers = new Dictionary<string, ServerLog>();

		listtoPD.Add (sendVar);
		listtoPD.Add (zPos);
	
	}
	
	// Update is called once per frame
	void Update () {
		//OSCHandler.Instance.UpdateLogs();
		//servers = OSCHandler.Instance.Servers;

 
		//foreach (Object respawn in respawns) 
			//{ Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation) as GameObject;	 }


		enemy = GameObject.FindGameObjectWithTag ("Enemy");	 
		//Herot = GameObject.FindGameObjectsWithTag ("HeroTag");
		float distance = Vector3.Distance(enemy.transform.position, transform.position);
		zPos = enemy.transform.position.z - transform.position.z;
		//Debug.Log (enemy.transform.position.z - transform.position.z);


		if(zPos<0){	 
			λ2 = (λ *(c/(c + v)))/150.0f;
		}	 
		if(zPos>0){	 
			λ2 = (λ *(c/(c - v)))/150.0f;
		}

		/*
		if(zPos<0){	 
			λ2 = 1.05f;
		}	 
		if(zPos>0){	 
			λ2 = 1.5f;
		}*/

		sendVar = 1/distance*300; //300 For Freqmod. 100 for ampmod
		//Debug.Log (Mathf.Abs(transform.parent.rigidbody.velocity.z*10));

		listtoPD [0] = sendVar;
		listtoPD[1] = λ2;
		OSCHandler.Instance.SendMessageToClient ("pdThing", "/127.0.0.1", listtoPD);

		// http://en.flossmanuals.net/pure-data/network-data/osc/ 
	}
}
