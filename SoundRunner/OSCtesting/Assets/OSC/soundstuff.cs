using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class soundstuff : MonoBehaviour {

	private Dictionary<string, ServerLog> servers;
	private float sendVar;
	public GameObject enemy;

	public float c = 34.0f;
	//λ2 = λ (1 + v/c)	
	public float v = 10.0f;	
	public float λ = 440.0f;	
	public float λ2 = 440.0f;	

	// Use this for initialization
	void Start () {

		OSCHandler.Instance.Init(); //init OSC
		OSCHandler.Instance.SendMessageToClient ("pdThing", "/127.0.0.1", sendVar);
		//OSCHandler.Instance.SendMessage ("hey");

		servers = new Dictionary<string, ServerLog>();
	
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


		if(distance>0){	 
			λ2 = λ *((1 + v)/c);
		}	 
		if(distance<0){	 
				λ2 = λ *((1 - v)/c);
		}

		sendVar = λ2;
		Debug.Log (sendVar);

		//testVar += Time.deltaTime*10;

		OSCHandler.Instance.SendMessageToClient ("pdThing", "/127.0.0.1", sendVar);

		// http://en.flossmanuals.net/pure-data/network-data/osc/ 
	}
}
