﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class mainGame : MonoBehaviour {
	gameManager gameMgr; // This gets the players information
	shopMenu shopScript; // This gets the information for the shop class, and lets player manipulate their stuff using the shop class
	GUIStyle guiCash;
	GUIStyle guiStyle;
	GUIStyle boxInformation;

	bool openingShop;  // This is a bool to open up the shop
	public bool placingSecurity; // This is a bool to check for if placing security, this is ok
	public bool showSettings; // This is a bool to check for if it is on show setting

	public Camera myCam; // The camera object
	public GameObject obj; // This is for Save Menu!!!!! TODO
    public GameObject tmpObj; // This is for Save Menu!!!!! TODO

	public float timer = 60; // A timer to calculate how long the user has been playing for
	public string time; // String of the amount of time overall that has passed
	string mins; // String of the amount of minutes that has passed
	string secs; // String of the amount of seconds that has passed


	// Use this for initialization
	void Start () {

		// This gets the component known as gameManager and sets it to the variable
		gameMgr = GameObject.Find ("GameObject").GetComponent<gameManager> ();

		// The boolean variable is going to check if the game has started 
        gameMgr.gameIsStarted = true;

		// This is to find the gameObject that holds the shopScript
		shopScript = GameObject.Find("GameObject").GetComponent<shopMenu>();
	
		// This is to allocate new GUISTYLE to the variable and set its appropriate values
		guiStyle =  new GUIStyle();
		guiStyle.fontSize = 20;
		guiStyle.normal.textColor = Color.white;

		// This is to allocate new GUISTYLE to the cash, and set its appropriate information
		guiCash =  new GUIStyle();
		guiCash.fontSize = 20;
		guiCash.normal.textColor = Color.green;
  
		boxInformation = new GUIStyle ();
		boxInformation.fontSize = 18;
		boxInformation.normal.textColor = Color.green;


		gameMgr.setCash (300);
	}
	// Update is called once per frame
	void Update () {
		// This is the timer, calculates the minutes, seconds, and the overall time.
	    timer -= Time.deltaTime;
		mins = Mathf.Floor(timer / 60).ToString("00");
		secs = Mathf.Floor(timer % 60).ToString("00");
		time = mins + ":" + secs;

        if (timer < 0){ 
           string  fileName = "HoneyPot Log" + (gameMgr.honeyCount - 1) +   ".txt";
           StreamWriter sr = File.CreateText(fileName);
            timer = 60;

            foreach (var hp in gameMgr.honeyPots) {
                hp.writeToLog(sr); 
                hp.carPIDS.Clear();


            }

        sr.Close();

        }
			}

	/**
	 * Function to calculate all of the GUI stuff for the main portion of the game
	 * @param: None
	 * @pre: None, requires initialized boolean variables
	 * @post: Loads the required GUI stuff.
	 * 		If user wants to place security, they press g and place a security gate at their mouse' designated spot
	 * 		If user presses Open Shop, the call the function to open up the menu
	 * @algorithm: Checks to see if the user clicked on the gui capabilities in the menu, if so, it launches whatever option that they clicked on 
	 */ 
	void OnGUI(){
			GUI.skin = Resources.Load ("Buttons/ButtonSkin") as GUISkin;
			Car tempScript;
			// Create a ray object, and have it trace the mousePosition from top down
			Ray ray = myCam.ScreenPointToRay (Input.mousePosition);

			// Create a hit variable that will store the value of whatever it hits
			RaycastHit hitDetected;

			Physics.Raycast (ray, out hitDetected, Mathf.Infinity);

			if (Physics.Raycast (ray, out hitDetected, Mathf.Infinity)) {
			if (hitDetected.collider.tag != "Building" && hitDetected.collider.tag != "Untagged") {
				// This gets the script from the object that it hits. 
				tempScript = hitDetected.collider.GetComponent <Car> ();
						
				GUI.Box (new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y, 150, 150), "Car Information \n");					
				GUI.Label (new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y + 20, 100, 100), 
					"Car Type: " + tempScript.carTypeString +
					" \nCar Color: " + tempScript.colorString +
					" \nCar Size : " + tempScript.sizeString, boxInformation);
			} 	/*
				else if (hitDetected.collider.tag == "tollPre") {
				// This gets the script from the object that it hits. 
				tempScript = hitDetected.collider.GetComponent <Security> ();

				GUI.Box (new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y, 150, 150), "Car Information \n");					
				GUI.Label (new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y + 20, 100, 100), 
					"Car Type: " + tempScript. +
					" \nCar Color: " + tempScript.colorString +
					" \nCar Size : " + tempScript.sizeString, boxInformation);
				}
				*/
			}
	

		// This is going to create a label with a rectangle size with the appropriate guiStyle along with the current time after retreiving it from update
        GUI.Label(new Rect(Screen.width / 2, 0, 150, 20), time, guiStyle);
        // This displays cash on GUI
        string moneyString = "$" + gameMgr.cash;
        GUI.Label(new Rect(Screen.width - 100, 0, 150, 20), moneyString, guiCash);
 
		// This outside if statement checks for if the GUI buttons should be shown onto the screen or not.
		// This checks if the security button was not pressed
		if ( (shopScript.getShopOpen() == false) && (shopScript.getSecurityType() == " ")){
			// This checks if the showSettings button was not pushed
			if (showSettings == false) 	{
						
					// Otherwise, place an interactable GUI button onto the screen called OpenShop
					if (GUI.Button (new Rect (Screen.width - 100, 35, 100, 50), "Open Shop")) {
						// This retrieves the cash amount and gives it to the shopMenu
						shopScript.playerCash = gameMgr.getCash ();

						// This will set the shopMenu Script to be true, in which it will display the appropriate GUI
						shopScript.setShopOpen (true);
						
					}

					// Or check if the user interacts with the GUI button called settings on the screen
					if (GUI.Button (new Rect (5, 5, 75, 50), "Settings")) {
						
						// This is a boolean utilized to open up another set of GUI's for loading and saving, settings
						showSettings = true;							
					}
			}

			// If showSettings is true, then a set of different functionalities will be displayed
			else {
				// Interactable GUI button for load game
				if (GUI.Button (new Rect (Screen.width / 2, (Screen.height / 2), 100, 50), "Load Game")) {
					/* Saves the game data
					 * *NOTE * shouldn't it be loading?
					 */
					gameMgr.loadSave (".txt");					
				}
				// Interactable GUI button for saving the game 
				if (GUI.Button (new Rect (Screen.width / 2, (Screen.height / 2) + 50, 100, 50), "Save Game")) {
					bool saved = false;

					// Calls the save functionality for the gameMgr
					saved = gameMgr.saveData ();	

					if (saved) {
						print ("File was successfully saved");
					} else {
						print ("File broke");
					}
				}
					
				// Interactable GUI button for quitting the game
				if (GUI.Button (new Rect (Screen.width / 2, (Screen.height / 2) + 150, 100, 50), "Quit Game")) {
					// Check to see if the user wants to save, if so, then called gameMgr.saveData, or something along those lines

					// Then quit the game entirely

				}

				// Interactable GUI button for back
				if (GUI.Button (new Rect (Screen.width / 2, (Screen.height / 2) + 200, 100, 50), "Back ")) {
					// Sets this to false, so the setting gui options will not appear
					showSettings = false;					
				}
			}
		}

		if (shopScript.getSecurityType () == "FL1" || shopScript.getSecurityType() == "FL2"  || shopScript.getSecurityType() == "FL3") {
			placingSecurity = true;
			// This will branch into placing a security gate onto the map
			if (placingSecurity == true) {
				// This checks if the user pressed the G key on the keyboard
				if (Input.GetMouseButtonDown(1)) {
					// Create a ray object, and have it trace the mousePosition from top down
					Ray vRay = myCam.ScreenPointToRay (Input.mousePosition);

					// Create a hit variable that will store the value of whatever it hits
					RaycastHit hit;
						
					// Cast a raycast from the starting position of the mouse down infinitely
					if (Physics.Raycast (vRay, out hit, Mathf.Infinity)) {

						if (hit.collider.tag == "Building") {
					
							// This is a variable that will hold the position of where the hit is detected for the mouse
							Vector3 placePosition;

							gameMgr.setCash (shopScript.playerCash);

							// Store the hit position into the placePosition
							placePosition = hit.point;

							// This will round the x and z variable, not sure if this is needed though since accuracy is much better than inaccuracy for object placement
							placePosition.x = Mathf.Round (placePosition.x);
							placePosition.z = Mathf.Round (placePosition.z);

							// instantiate a tollgate prefab as gameObject into the world (Will be called tollPre(clone), I think
							obj = Instantiate (Resources.Load ("Prefabs/tollPre", typeof(GameObject))) as GameObject;

							obj.GetComponent <Security> ().setTypes ( shopScript.ambulance, shopScript.fireTruck, shopScript.Tanker, shopScript.Truck, shopScript.Hearse, shopScript.IceCream, shopScript.policeCar,  shopScript.Taxi );
							obj.GetComponent <Security> ().setColors (shopScript.red,  shopScript.green, shopScript.blue, shopScript.yellow);
							obj.GetComponent <Security> ().setSize (shopScript.small, shopScript.median, shopScript.large);
							obj.GetComponent <Security> ().setSecurityType (shopScript.getSecurityType());

							// Change the position of it so it will be placed a little bit above the road level
							obj.transform.position = new Vector3 (placePosition.x, 0.6f, placePosition.z);	
							obj.transform.localScale *= 6;

							// This will add the gate to the list for it to be saved
							gameMgr.securityGates.Add (obj.GetComponent<Security> ());

							// Set the placing security to false, in which it won't let the user keep pressing g for more security gates
							placingSecurity = false;
							shopScript.clear ();
						}
					}
				}	
			}
		}



        if (shopScript.getSecurityType() == "HL1" || shopScript.getSecurityType() == "HL2" || shopScript.getSecurityType() == "HL3")
        {
            placingSecurity = true;

                // This checks if the user pressed the G key on the keyboard
                if (Input.GetMouseButtonDown(1))
                {
                    // Create a ray object, and have it trace the mousePosition from top down
                    Ray vRay = myCam.ScreenPointToRay(Input.mousePosition);

                    // Create a hit variable that will store the value of whatever it hits
                    RaycastHit hit;

                    // Cast a raycast from the starting position of the mouse down infinitely
                    if (Physics.Raycast(vRay, out hit, Mathf.Infinity))
                    {
                        Debug.Log(hit.collider.tag);
                        if (hit.collider.tag == "Building")
                        {

                            // This is a variable that will hold the position of where the hit is detected for the mouse
                            Vector3 placePosition;

                            // Store the hit position into the placePosition
                            placePosition = hit.point;

                            // This will round the x and z variable, not sure if this is needed though since accuracy is much better than inaccuracy for object placement
                            placePosition.x = Mathf.Round(placePosition.x);
                            placePosition.z = Mathf.Round(placePosition.z);

                            // instantiate a tollgate prefab as gameObject into the world (Will be called tollPre(clone), I think
                            obj = Instantiate(Resources.Load("Prefabs/HoneySpoon", typeof(GameObject))) as GameObject;

                            obj.GetComponent<HoneyPot>().setList(shopScript.honeyFlags);
                            obj.GetComponent<HoneyPot>().setLevel(shopScript.honeyLevel);
                            obj.GetComponent<HoneyPot>().setMenuBools(shopScript.red, shopScript.green, shopScript.blue, shopScript.yellow,
                                shopScript.small, shopScript.median, shopScript.large, shopScript.ambulance, shopScript.fireTruck, shopScript.Tanker,
                                shopScript.Truck, shopScript.Hearse, shopScript.policeCar, shopScript.IceCream);

                            // Change the position of it so it will be placed a little bit above the road level
                            obj.transform.position = new Vector3(placePosition.x, 0.6f, placePosition.z);

                            // Set the placing security to false, in which it won't let the user keep pressing g for more security gates
                            gameMgr.honeyPots.Add( obj.GetComponent<HoneyPot>() );
                            placingSecurity = false;
                            shopScript.clear();
                            shopScript.honeyFlags.Clear();
                        }
                    }
                }
        }



		if (shopScript.getShopOpen () == true) {
			Time.timeScale = 0;
		}
	}
}

