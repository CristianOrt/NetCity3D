﻿using UnityEngine;
using System.Collections;

public class shopMenu : MonoBehaviour {
	private bool shopOpen; // This is to check if the shop has been opened and ready to show it to the user
	private GameObject player; // This is to check for player manipulation, cash, levels etc.
	private int security_one_cost; // This is the amount the player has to pay for security level 1 upgrade
	private int security_two_cost; // This is the amount the player has to pay for security level 2 upgrade
	private int security_three_cost; // This is the amount the player has to pay for security level 2 upgrade
	private GUIStyle guiStyle; // This will hold the type of GUI options for the buttons and etc

	// This is for the location of the button only for upgrades and for the purpose of organization
	private int upgradeOneGUIRow;
	private int upgradeTwoGUIRow;
	private int upgradeThreeGUIRow;

	// This is for the y location of the buttons for the upgrades and organization
	private int upgradeOneGUICol;
	private int upgradeTwoGUICol;
	private int upgradeThreeGUICol;

	// This will be for the size of the GUI buttons
	private int guiWidth;
	private int guiHeight;

	// This is the images for each of the different security types
	Texture2D securityOneImage;
	Texture2D securityTwoImage;
	Texture2D securityThreeImage;

	// This is the content for which the images will be held to
	GUIContent securityOneImageContainer;
	GUIContent securityTwoImageContainer;
	GUIContent securityThreeImageContainer;

	// String value which will detect what type of security it is. The range can be L1, L2, L3
	private string securityType;

	// These are the values for car color that it will detect what car colors are allowed
	private bool red;
	private bool blue;
	private bool green;

	// These are the values for the size of the car which the gate will detect
	private bool large;
	private bool median;
	private bool small;

	// Bool if upgrade chosen
	bool upgradeChosen;



	// Use this for initialization
	void Start () {
		// This is going to initialize all of the variables at the get go
		shopOpen = false; // The shop will be closed from the very beginning unless otherwise noted
		security_one_cost = 400; // This is the amount the player has to pay for security level 1 upgrade
		security_two_cost = 800; // This is the amount the player has to pay for security level 2 upgrade
		security_three_cost = 1200; // This is the amount the player has to pay for security level 2 upgrade
		guiStyle = new GUIStyle(); // Allocate memory to the gui Style Variable


		// Change the font size to 20
		guiStyle.fontSize = 20;

		// Change the color to white
		guiStyle.normal.textColor = Color.white;

		// This is to set the current x and y boundaries of the gui's
		upgradeOneGUIRow =  10;
		upgradeTwoGUIRow = 200;
		upgradeThreeGUIRow  = 400;

		upgradeOneGUICol = 30;
		upgradeTwoGUICol = 230;
		upgradeThreeGUICol = 430;


		// Initialize the images
		securityOneImage = 	(Texture2D) Resources.Load ("Images/FirewallIcon");
		securityTwoImage = (Texture2D)Resources.Load ("Images/HoneyPotIcon");
		securityThreeImage = (Texture2D)Resources.Load ("Images/Security");

		securityOneImageContainer = new GUIContent();
		securityOneImageContainer.image = securityOneImage;

		securityTwoImageContainer = new GUIContent();
		securityTwoImageContainer.image = securityTwoImage;

		securityThreeImageContainer = new GUIContent();
		securityThreeImageContainer.image = securityThreeImage;

		// String value is set to a default value
		securityType = " ";
	
		// Set default values as false
		red = false;;
		blue = false;
		green = false;
		large = false;
		median = false;
		small = false;
		upgradeChosen = false;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		if (shopOpen == true) {
			
			// Otherwise, place an interactable GUI button onto the screen called OpenShop
			GUI.BeginGroup(new Rect(Screen.width/2 - 400, Screen.height/2 -300, 800, 600));

			if (upgradeChosen) {
				if (securityType == "L1") {
					GUI.Box (new Rect (0, 0, 600, 600), "Purchase Options");
					red = GUI.Toggle (new Rect (10, 60, 100, 30), red, "Red");
					green = GUI.Toggle (new Rect (10, 160, 100, 30), green, "Green");
					blue = GUI.Toggle (new Rect (10, 260, 100, 30), blue, "Blue");

					if ((red && !blue && !green) || (!red && blue && !green) || (!red && !blue && green)) {
						small = GUI.Toggle (new Rect (140, 60, 100, 30), small, "Small");
						median = GUI.Toggle (new Rect (140, 160, 100, 30), median, "Medium");
						large = GUI.Toggle (new Rect (140, 260, 100, 30), large, "Large");

						if ((small && !median && !large) || (!small && median && !large) || (!small && !median && large)) {
							{
								if (GUI.Button (new Rect (240, 400- (128*2) + 128, 128, 50), "Purchase")) {
									upgradeChosen = false;
									shopOpen = false;
									Time.timeScale = 1;
								}
							}
						}
					}
					if (GUI.Button (new Rect (240, 400-128 + 128, 128, 50), "Cancel Purchase")) {
						clear ();
						upgradeChosen = false;
						Time.timeScale = 1;
	
					}
				}

				if (securityType == "L2") {
					GUI.Box (new Rect (0, 0, 600, 600), "Purchase Options");
					red = GUI.Toggle (new Rect (10, 60, 100, 30), red, "Red");
					green = GUI.Toggle (new Rect (10, 160, 100, 30), green, "Green");
					blue = GUI.Toggle (new Rect (10, 260, 100, 30), blue, "Blue");

					if ((red && blue && !green) || (!red && blue && green) || (red && !blue && green)) {
						small = GUI.Toggle (new Rect (140, 60, 100, 30), small, "Small");
						median = GUI.Toggle (new Rect (140, 160, 100, 30), median, "Medium");
						large = GUI.Toggle (new Rect (140, 260, 100, 30), large, "Large");

						if ((small && median && !large) || (!small && median && large) || (small && !median && large)) {
							{
								if (GUI.Button (new Rect (240, 400- (128*2) + 128, 128, 50), "Purchase")) {
									upgradeChosen = false;
									shopOpen = false;
									Time.timeScale = 1;

								}
							}
						}
					}
					if (GUI.Button (new Rect (240, 400-128 + 128, 128, 50), "Cancel Purchase")) {
						clear ();
						upgradeChosen = false;
						Time.timeScale = 1;

					}

				}
			}
				else {
				// This is to contain all of the different buying options
				GUI.Box (new Rect (0, 0, 800, 600), "Shop");


				// First set of upgrades for the security for the pictures
				GUI.Label (new Rect (upgradeOneGUIRow, upgradeOneGUICol, 128, 128), securityOneImageContainer);
				GUI.Label (new Rect (upgradeOneGUIRow, upgradeTwoGUICol, 128, 128), securityTwoImageContainer);
				GUI.Label (new Rect (upgradeOneGUIRow, upgradeThreeGUIRow, 128, 128), securityThreeImageContainer);

				// This is the second set of upgrades for the pictures
				GUI.Label (new Rect (upgradeTwoGUIRow, upgradeOneGUICol, 128, 128), securityOneImageContainer);
				GUI.Label (new Rect (upgradeTwoGUIRow, upgradeTwoGUICol, 128, 128), securityTwoImageContainer);
				GUI.Label (new Rect (upgradeTwoGUIRow, upgradeThreeGUIRow, 128, 128), securityThreeImageContainer);

				// This is the third set of upgrades for the pictures
				GUI.Label (new Rect (upgradeThreeGUIRow, upgradeOneGUICol, 128, 128), securityOneImageContainer);
				GUI.Label (new Rect (upgradeThreeGUIRow, upgradeTwoGUICol, 128, 128), securityTwoImageContainer);
				GUI.Label (new Rect (upgradeThreeGUIRow, upgradeThreeGUIRow, 128, 128), securityThreeImageContainer);



				// This button is here for upgrading to security option 1
				if (GUI.Button (new Rect (upgradeOneGUIRow, upgradeOneGUICol + 128, 128, 50), "Security One")) {
					securityType = "L1";
					upgradeChosen = true;
				}

				// This button is here for upgrading to security option 2
				if (GUI.Button (new Rect (upgradeOneGUIRow, upgradeTwoGUICol + 128, 128, 50), "HoneyPot One")) {
					print ("This is working");
				}

				// This button is here for upgrading to security option 3
				if (GUI.Button (new Rect (upgradeOneGUIRow, upgradeThreeGUICol + 128, 128, 50), "IDS One")) {
					print ("This is working");
				}

				// This button is here for upgrading to security option 1
				if (GUI.Button (new Rect (upgradeTwoGUIRow, upgradeOneGUICol + 128, 128, 50), "Security Two")) {
					securityType = "L2";
					upgradeChosen = true;
				}

				// This button is here for upgrading to security option 2
				if (GUI.Button (new Rect (upgradeTwoGUIRow, upgradeTwoGUICol + 128, 128, 50), "HoneyPot Two")) {
					print ("This is working");
				}

				// This button is here for upgrading to security option 3
				if (GUI.Button (new Rect (upgradeTwoGUIRow, upgradeThreeGUICol + 128, 128, 50), "IDS Two")) {
					print ("This is working");
				}

				// This button is here for upgrading to security option 1
				if (GUI.Button (new Rect (upgradeThreeGUIRow, upgradeOneGUICol + 128, 128, 50), "Security Three")) {
					securityType = "L3";
				}

				// This button is here for upgrading to security option 2
				if (GUI.Button (new Rect (upgradeThreeGUIRow, upgradeTwoGUICol + 128, 128, 50), "HoneyPot Three")) {
					print ("This is working");
				}

				// This button is here for upgrading to security option 3
				if (GUI.Button (new Rect (upgradeThreeGUIRow, upgradeThreeGUICol + 128, 128, 50), "IDS Three")) {
					print ("This is working");
				}


				// This button is here to close down the shop and place the old GUI buttons on the screen
				if (GUI.Button (new Rect (800 - 128, upgradeThreeGUICol + 128, 128, 50), "Close Shop")) {
					shopOpen = false;
					upgradeChosen = false;
					clear ();
					print (securityType);
					Time.timeScale = 1;
					print ("Closing down Shop");
				}
			}
			// necessary function call for beginGroup
			GUI.EndGroup ();	

		}
	}


	/**
	 * Function that sets the boolean value for shopOpen
	 * @param: Requires a boolean to be set to
	 * @pre: None, requires initialized boolean variable of true or false
	 * @post: Sets shopOpen to if the user clicked on the gui
	 * @algorithm: Sets whatever came in from the parameter to true or false 
	 */ 
	public void setShopOpen(bool set){
		shopOpen = set;
	}


	/**
	 * Function that gets the boolean value for shopOpen
	 * @param: None
	 * @pre: None, requires initialized boolean variable of true or false
	 * @post: None
	 * @algorithm: Gets whatever shopOpen was set to 
	 */ 
	public bool getShopOpen(){
		return shopOpen;
	}

	public string getSecurityType(){
		return securityType;
	}

	public void clear(){
		red = green = blue = small = median = large = false;
		securityType = " ";
	}
}