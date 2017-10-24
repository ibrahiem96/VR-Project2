using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

	public int orbitScale = 0;
	public int planetScale = 0;
	public int revolutionSpeed = 0;
	public bool threeDCounter = 0;

	string threeDName;

	public int getOrbitScaleCounter(){
		return orbitScale;
	}
	public int getPlanetScaleCounter(){
		return planetScale;
	}
	public int getSpeedCounter(){
		return revolutionSpeed;
	}
	public int get3dSelection(){
		return threeDCounter;
	}


	public bool orbit;
	public bool speed;
	public bool threeD;
	public bool planet;

	public bool getOrbit(){
		return orbit;
	}

	public bool getSpeed(){
		return speed;
	}

	public bool getThreeD(){
		return threeD;
	}

	public bool getPlanet(){
		return planet;
	}

	public string getThreeDName(){
		return threeDName;
	}


	void OnTriggerEnter(Collider other){
		if (other.tag == "VRController"){
			if (orbit == true){
				// check if we should reset scale
				if (orbitScale == 5){
					//TODO: add functionality
					orbitScale = 0;
				}
				//increase scale
				else {
					//TODO: add functionality
					orbitScale++;	
				}
			}
			else if (planet == true){
				// check if we should reset scale
				if (planetScale == 5){
					//TODO: add functionality
					planetScale = 0;
				}
				//increase scale
				else {
					//TODO: add functionality
					planetScale++;	
				}
			}
			else if (speed == true){
				if (revolutionSpeed == 5){
					//TODO: add functionality
					revolutionSpeed = 0;
				}
				else {
					//TODO: add functionality
					revolutionSpeed++;
				}
			}
			else if (scaleTo3d == true){
				// show in 3d next to our solar system
			}
		}
	}


}
