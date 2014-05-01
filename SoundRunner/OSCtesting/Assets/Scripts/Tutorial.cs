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
			        "\n Welcome to “Sound Runner”, the game where hearing is more important than seeing. \n " +
			        "The purpose of this game is to see if one of the most important factors of a game “the visuals” \n" +
			        "can be replaced with continuous audio feedback. As in a normal runner game, \n " +
			        "you will need to dodge obstacles coming toward you. \n" +
			        "The difference from this game and a normal runner game is that you will only \n " +
			        "have the sound to tell you when to dodge the objects.");
			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2, 50, 30), "Ready")){
				enabled = false;
				Application.LoadLevel(8);
			}
			
		}

		if (Application.loadedLevel == 3){
			GUI.Box (new Rect (Screen.width / 2 - 320,Screen.height / 2-300,640,70), 

			         "\nThe first part of the tutorial is left side objects. When the panning is in the left ear, \n" +
			         "it means an object on the left side is approaching, the rise in volume indicates a closing object. \n " +
			         "In order to succeed, you need to dodge the object.");

			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2-200, 50, 30), "Next")){
				enabled = false;
				Application.LoadLevel(4);
			}
		}

		if (Application.loadedLevel == 4){
			GUI.Box (new Rect (Screen.width / 2 - 320,Screen.height / 2-300,640,50), 
			         
			         "\nRight side objects work the same way as left side objects, the panning just shifts to the right ear. \n" +
			         "Note that when you successfully pass an object, a slight tone plays to notify you of this.");
			
			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2-200, 50, 30), "Next")){
				enabled = false;
				Application.LoadLevel(5);
			}
		}

		if (Application.loadedLevel == 5){
			GUI.Box (new Rect (Screen.width / 2 - 340,Screen.height / 2-300,680,70), 

			         "\nThe third object is the jump to dodge object. The sound is now slightly different than the previous examples \n " +
			         "but still increases in volume as you get close.\n" +
			         "In order to dodge this, try to pay some attention to the gravity of the game \n" +
			         "so you can time your jumps based on the sound.");
			
			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2-200, 50, 30), "Next")){
				enabled = false;
				Application.LoadLevel(6);
			}
		}

		if (Application.loadedLevel == 6){
			GUI.Box (new Rect (Screen.width / 2 - 340,Screen.height / 2-300,680,70), 

			         "\nContrary to the previous obstacle, you will need to crouch under this one to dodge it.\n" +
			         "Try to remember the sound. After this, the final part of the tutorial, with multiple objects, will begin.");

			
			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2-200, 50, 30), "Next")){
				enabled = false;
				Application.LoadLevel(7);
			}
		}

		if (Application.loadedLevel == 7){
			GUI.Box (new Rect (Screen.width / 2 - 340,Screen.height / 2-300,680,70), 
			         
			         "\nIn this part of the tutorial you will need to dodge multiple objects in the same environment.\n" +
			         "Focus on learning the sounds properly as there will be no visual objects in the next part.\n");
			
			if (GUI.Button(new Rect(Screen.width / 2 - 25,Screen.height / 2-200, 50, 30), "Next")){
				enabled = false;
				Application.LoadLevel(9);
			}
		}

		if (Application.loadedLevel == 8){
			GUI.Box (new Rect (Screen.width / 2 - 250,Screen.height / 2-300,500,80), 
			         
			         "\nThis is a free environment to learn the controls of the game,\n" +
			         "Press W to jump, A to go left, S to crouch and D to go right. You can try this out now. \n" + 
			         "After you understand the controls, please select if you have been designated \n " +
			         "as a Auditory tester or Visual tester.\n" );

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
			         
			         "\nIf you don't feel ready yet, please take your time and press ''Replay Tutorial'' to try the two last parts again.\n" +
			         "THE GAME TAKES AROUND 4 MINUTES WHEN YOU START IT. \n " +
			         "We would appreciate you playing the game through to the end. \n");
			
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
