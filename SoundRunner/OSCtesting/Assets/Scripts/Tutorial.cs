using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		if (Application.loadedLevel == 2){
			GUI.Box (new Rect (Screen.width / 2 - 320,Screen.height / 2-200,640,100), 
			        "\n Welcome to “Sound Runner” the game where hearing is more important than seeing. \n " +
			        "The purpose of this game is to see if one of the most important factors of a game “the visuals” \n" +
			        "can be replaced with continuous audio feedback. The difference from this game and a normal runner game \n" +
			        "is that you only have the sound to guide you in when to dodge the objects.");
			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2, 50, 30), "Ready")){
				enabled = false;
				Application.LoadLevel(8);
			}
			
		}

		if (Application.loadedLevel == 3){
			GUI.Box (new Rect (Screen.width / 2 - 320,Screen.height / 2-300,640,70), 

			         "\nThe first part of the tutorial is left side objects. When the panning is in the left ear, \n" +
			         "it means an object on the left side is approaching, the rise in volume indicates a closing object.");

			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2-200, 50, 30), "Next")){
				enabled = false;
				Application.LoadLevel(4);
			}
		}

		if (Application.loadedLevel == 4){
			GUI.Box (new Rect (Screen.width / 2 - 320,Screen.height / 2-300,640,50), 
			         
			         "\nRight side objects work the same way as left side objects, the panning just shifts to the right ear.");
			
			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2-200, 50, 30), "Next")){
				enabled = false;
				Application.LoadLevel(5);
			}
		}

		if (Application.loadedLevel == 5){
			GUI.Box (new Rect (Screen.width / 2 - 340,Screen.height / 2-300,680,70), 

			         "\nThe third object is the jump to dodge object. The sound works in the same way as the previous examples.\n" +
			         "Try to pay some attention to the gravity of the game to see if you can learn to time your jumps based on the sound.");
			
			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2-200, 50, 30), "Next")){
				enabled = false;
				Application.LoadLevel(6);
			}
		}

		if (Application.loadedLevel == 6){
			GUI.Box (new Rect (Screen.width / 2 - 340,Screen.height / 2-300,680,70), 

			         "\nIn opposition to the previous jump to dodge object, the game also has a duck to dodge object.\n" +
			         "Try to remember the different sound indicators as the final part of the tutorial, multiple objects, will begin.");

			
			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2-200, 50, 30), "Next")){
				enabled = false;
				Application.LoadLevel(7);
			}
		}

		if (Application.loadedLevel == 7){
			GUI.Box (new Rect (Screen.width / 2 - 340,Screen.height / 2-300,680,70), 
			         
			         "\nThis part of the tutorial you will need to dodge multiple objects in the same environment.\n" +
			         "Remember to learn the sounds properly as there will be no visual objects in the next part.\n");
			
			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2-200, 50, 30), "Next")){
				enabled = false;
				Application.LoadLevel(9);
			}
		}

		if (Application.loadedLevel == 8){
			GUI.Box (new Rect (Screen.width / 2 - 250,Screen.height / 2-300,500,80), 
			         
			         "\nThis is a free environment to learn the controls of the game,\n" +
			         "press W to jump, A to go left, S to crouch and D to go right.\n" + 
			         "Please select if you have been designated as a Auditory tester or Visual tester.\n" );

			if (GUI.Button(new Rect(Screen.width / 2 - 150,Screen.height / 2-200, 100, 30), "Test Auditory")){
				enabled = false;
				Application.LoadLevel(3);
			}
			if (GUI.Button(new Rect(Screen.width / 2 + 60,Screen.height / 2-200, 100, 30), "Test Visual")){
				enabled = false;
				Application.LoadLevel(1);
			}
		}

		if (Application.loadedLevel == 9){
			GUI.Box (new Rect (Screen.width / 2 - 340,Screen.height / 2-300,680,70), 
			         
			         "\nThis is the last part of the tutorial, to continue to the game you will need to complete this level\n" +
			         "Remember to learn the sounds properly as there will be no visual objects in the game and it will only get harder.\n");
		}

		if (Application.loadedLevel == 11){
			GUI.Box (new Rect (Screen.width / 2 - 340,Screen.height / 2-300,680,80), 
			         
			         "\nIf you don't feel ready yet, please take your time and press ''Replay Tutorial'' to try the two last parts of the game\n" +
			         "THE GAME TAKES AROUND 4 MINUTES WHEN YOU START IT. \n");
			
			if (GUI.Button(new Rect(Screen.width / 2 - 150,Screen.height / 2-200, 100, 30), "Replay Tutorial")){
				enabled = false;
				Application.LoadLevel(7);
			}
			if (GUI.Button(new Rect(Screen.width / 2 + 60,Screen.height / 2-200, 100, 30), "Play Game")){
				enabled = false;
				Application.LoadLevel(10);
			}
		}
	}
}
