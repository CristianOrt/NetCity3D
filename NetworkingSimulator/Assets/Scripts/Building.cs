using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {
	// The name of the building
    gameManager gameMgr;

    public List<string> badCars = new List<string>();

	public string name;

	// These are the values for the car color that might be bad based on the professor preference
	public bool red;
	public bool blue;
	public bool green;
	public bool yellow;

	// These are the values for the car size that might be bad based on the professor preference	\
	public bool large;
	public bool median;
	public bool small;

	// Bool if the bad car upgrades have been chosen
	public bool badCarsChosen;

	// These variables are used for toggling what the professor wants in terms of the type of car it is
	public bool ambulance;
	public bool fireTruck;
	public bool Tanker;
	public bool Truck;
	public bool Hearse;
	public bool IceCream;
	public bool policeCar;
	public bool Taxi;

    public int badNumberCars = 0;

	// This is for the amount of life the building itself has
	public int life = 0;
	GUIStyle boxInformation;

	bool showInformation;

	public Camera myCam; 

	// Use this for initialization
	void Start () {
		// This is initializing all of the values of life
		if (this.tag == "school") {
			name = "school";
			life = 2;
		} 
		else if (this.tag == "Hospatal") {
			name = "Hospatal";
			life = 4;
		}
		else if (this.tag == "Bank") {
			name = "Bank";
			life = 10;
		}

		else if (this.tag == "Police_station") {
			name = "Police Station";
			life = 10;
		}

		else if (this.tag == "Building2") {
			name = "Store";
			life = 5;
		}
		else if (this.tag == "Petrol") {
			name = "Gas";
			life = 3;
		}
		else if (this.tag == "House") {
			name = "House";
			life = 3;
		}


		boxInformation = new GUIStyle ();
		boxInformation.fontSize = 18;
		boxInformation.normal.textColor = Color.green;
	
		myCam = GameObject.Find("Main Camera").GetComponent<Camera>();

		showInformation = false;
		gameMgr = GameObject.Find("GameObject").GetComponent<gameManager>();
		red = gameMgr.red;
		blue = gameMgr.blue;
		green = gameMgr.green;

		large = gameMgr.large;
		median = gameMgr.median;
		small = gameMgr.small;

		ambulance = gameMgr.ambulance;
		fireTruck = gameMgr.fireTruck;
		Tanker = gameMgr.Tanker;
		Truck = gameMgr.Truck;
		Hearse = gameMgr.Hearse;
		IceCream = gameMgr.IceCream;
		policeCar = gameMgr.policeCar;
		Taxi = gameMgr.Taxi;
	}
	
	// Update is called once per frame
	void Update () {
		//This is to check for what type of color the security gate will look for
		if (red && !badCars.Contains("Red")) badCars.Add("Red"); 
		else if (!red && badCars.Contains("Red")) badCars.Remove("Red");

		else if (green && !badCars.Contains("Green")) badCars.Add("Green"); 
		else if (!green && badCars.Contains("Green")) badCars.Remove("Green");

		else if (blue && !badCars.Contains("Blue"))  badCars.Add("Blue");
		else if (!blue && badCars.Contains("Blue"))  badCars.Remove("Blue"); 

		else if (yellow && !badCars.Contains("Yellow"))  badCars.Add("Yellow");
		else if (!yellow && badCars.Contains("Yellow"))  badCars.Remove("Yellow");

		if (small && !badCars.Contains("Small"))  badCars.Add("Small"); 
		else if (!small && badCars.Contains("Small"))  badCars.Remove("Small");

		else if (median && !badCars.Contains("Medium"))  badCars.Add("Medium"); 
		else if (!median && badCars.Contains("Medium"))  badCars.Remove("Medium"); 

		else if (large && !badCars.Contains("Large"))  badCars.Add("Large");
		else if (!large && badCars.Contains("Large"))  badCars.Remove("Large");

		if (ambulance && !badCars.Contains("Ambulance")) badCars.Add("Ambulance"); 
		else if (!ambulance && badCars.Contains("Ambulance")) badCars.Remove("Ambulance");

		else if (fireTruck && !badCars.Contains("FireTruck"))  badCars.Add("FireTruck");
		else if (!fireTruck && badCars.Contains("FireTruck"))  badCars.Remove("FireTruck");

		else if (Tanker && !badCars.Contains("Tanker"))  badCars.Add("Tanker");
		else if (!Tanker && badCars.Contains("Tanker"))  badCars.Remove("Tanker");

		else if (Truck && !badCars.Contains("Truck"))  badCars.Add("Truck");
		else if (!Truck && badCars.Contains("Truck"))  badCars.Remove("Truck");

		else if (Hearse && !badCars.Contains("Hearse"))  badCars.Add("Hearse");
		else if (!Hearse && badCars.Contains("Hearse"))  badCars.Remove("Hearse"); 

		else if (IceCream && !badCars.Contains("IceCream"))  badCars.Add("IceCream");
		else if (!IceCream && badCars.Contains("IceCream"))  badCars.Remove("IceCream"); 

		else if (policeCar && !badCars.Contains("PoliceCar"))  badCars.Add("PoliceCar");
		else if (!policeCar && badCars.Contains("PoliceCar"))  badCars.Remove("PoliceCar"); 
	}

    void OnCollisionEnter(Collision col) {
        int amount = 300;
        if ( col.gameObject.tag == "car" ) {
			print (red);
            Car colCar = col.gameObject.GetComponent<Car>();

            Destroy(col.gameObject);
            gameMgr.activeCars.Remove(colCar);
            gameMgr.cash += amount;
        } 
    }

	public void setBuildingBools(bool r, bool g, bool b, bool y, bool s, bool m, bool l, bool a, bool f, bool ta, bool tr, bool h, bool p, bool i) {
		red = r; blue = b; yellow = y; green = g;
		small = s; median = m; large = l;
		ambulance = a; fireTruck = f; Tanker = ta; Truck = tr;
		Hearse = h; policeCar = p; IceCream = i;
	}


	void OnMouseOver(){
		if (Input.GetMouseButtonDown (1))
			life -= 1;
		}
	void OnGUI(){

		}

}