/// Sample Code for CS 491 Virtual And Augmented Reality Course - Fall 2017
/// written by Andy Johnson
/// 
/// makes use of various textures from the celestia motherlode - http://www.celestiamotherlode.net/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Planets : MonoBehaviour {

	int n;

	float panelHeight = 0.1F;
	float panelWidth = 30.0F;
	float panelDepth = 0.1F;

	float orbitWidth = 0.01F;
	float habWidth = 0.03F;

	float revolutionSpeed = 0.2F;

	float panelXScale = 2.0F;
	float orbitXScale = 2.0F;

	GameObject allCenter;

	ButtonHandler bh;

	string[] sol = new string[5] { "695500", "Our Sun", "sol", "G2V" , "1.0"};

	string[,] solPlanets = new string[8, 5] {
		{   "57910000",  "2440",    "0.24", "mercury", "mercury" },
		{  "108200000",  "6052",    "0.62", "venus",   "venus" },
		{  "149600000",  "6371",    "1.00", "earthmap", "earth" },
		{  "227900000",  "3400",    "1.88", "mars",     "mars" },
		{  "778500000", "69911",   "11.86", "jupiter", "jupiter" },
		{ "1433000000", "58232",   "29.46", "saturn",   "saturn" },
		{ "2877000000", "25362",   "84.01", "neptune", "uranus" },
		{ "4503000000", "24622",  "164.80", "uranus", "neptune" }
	};

	string[,] solPlanets2 = new string[8, 5] {
		{   "57910000",  "2440",    "0.24", "mercury", "mercury" },
		{  "108200000",  "6052",    "0.62", "venus",   "venus" },
		{  "149600000",  "6371",    "4.50", "earthmap", "earth" },
		{  "227900000",  "3400",    "1.88", "mars",     "mars" },
		{  "778500000", "69911",   "11.86", "jupiter", "jupiter" },
		{ "1433000000", "58232",   "29.46", "saturn",   "saturn" },
		{ "2877000000", "25362",   "84.01", "neptune", "uranus" },
		{ "4503000000", "24622",  "164.80", "uranus", "neptune" }
	};


	string[] TauCeti = new string[5] { "556400", "Tau Ceti", "gstar", "G8.5V" , "0.52"};

	string[,] TauCetiPlanets = new string[5, 5] {
		{ "15707776",  "9009",   "0.04", "venus",   "b" },
		{ "29171585", "11217",   "0.09", "venus", "c" },
		{ "55949604", "12088",   "0.26", "mercury",  "d" },
		{ "82578024", "13211",   "0.46", "mercury", "e" },
		{"201957126", "16454",   "1.75", "uranus",  "f" }
	};


	string[] Gliese581 = new string[5] { "201750", "Gliese 581", "mstar", "M3V" , "0.013"};

	string[,] Gliese581Planets = new string[3, 5] {
		{ "4188740",  "8919",   "0.009", "venus",   "e" },
		{ "6133513", "30554",   "0.014", "jupiter",   "b" },
		{"10920645", "20147",   "0.18", "neptune",  "c" }
	};

	string[] systemList = new string[10] {"SolarSystem", "TauCeti", "Gliese581"};

	//------------------------------------------------------------------------------------//

	void drawOrbit(string orbitName, float orbitRadius, Color orbitColor, float myWidth, GameObject myOrbits){

		GameObject newOrbit;
		GameObject orbits;

	
		newOrbit = new GameObject (orbitName);
		newOrbit.AddComponent<Circle> ();
		newOrbit.AddComponent<LineRenderer> ();

		newOrbit.GetComponent<Circle> ().xradius = orbitRadius;
		newOrbit.GetComponent<Circle> ().yradius = orbitRadius;

		var line = newOrbit.GetComponent<LineRenderer> ();
		line.startWidth = myWidth;
		line.endWidth = myWidth;
		line.useWorldSpace = false;

		newOrbit.GetComponent<LineRenderer> ().material.color = orbitColor;

		orbits = myOrbits;
		newOrbit.transform.parent = orbits.transform;
	
		}

	//------------------------------------------------------------------------------------//

	void dealWithPlanets (string [,] planets, GameObject thesePlanets, GameObject theseOrbits){
		GameObject newPlanetCenter;
		GameObject newPlanet;

		GameObject sunRelated;

		Material planetMaterial;

		int planetCounter;

		for (planetCounter = 0; planetCounter < planets.GetLength(0); planetCounter++) {

			float planetDistance = float.Parse (planets [planetCounter, 0]) / 149600000.0F * 10.0F;
			float planetSize = float.Parse (planets [planetCounter, 1]) * 2.0F / 10000.0F;
			float planetSpeed = -1.0F / float.Parse (planets [planetCounter, 2]) * revolutionSpeed;
			string textureName = planets [planetCounter, 3];
			string planetName = planets [planetCounter, 4];

			newPlanetCenter = new GameObject (planetName + "Center");
			newPlanetCenter.AddComponent<rotate> ();

			newPlanet = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			newPlanet.name = planetName;
			newPlanet.transform.position = new Vector3 (0, 0, planetDistance * orbitXScale);
			newPlanet.transform.localScale = new Vector3 (planetSize, planetSize, planetSize);
			newPlanet.transform.parent = newPlanetCenter.transform;

			newPlanetCenter.GetComponent<rotate> ().rotateSpeed = planetSpeed; 

			planetMaterial = new Material (Shader.Find ("Standard"));
			newPlanet.GetComponent<MeshRenderer> ().material = planetMaterial;
			planetMaterial.mainTexture = Resources.Load (textureName) as Texture;

			drawOrbit (planetName + " orbit", planetDistance * orbitXScale, Color.white, orbitWidth, theseOrbits);

			sunRelated = thesePlanets;
			newPlanetCenter.transform.parent = sunRelated.transform;
		}
	}

	//------------------------------------------------------------------------------------//

	void sideDealWithPlanets (string [,] planets, GameObject thisSide, GameObject theseOrbits){
		GameObject newPlanet;

		GameObject sunRelated;
	
		Material planetMaterial;

		int planetCounter;

		for (planetCounter = 0; planetCounter < planets.GetLength(0); planetCounter++) {

			float planetDistance = float.Parse (planets [planetCounter, 0]) / 149600000.0F * 10.0F;
			float planetSize = float.Parse (planets [planetCounter, 1]) * 1.0F / 10000.0F;
			string textureName = planets [planetCounter, 3];
			string planetName = planets [planetCounter, 4];
		
			// limit the planets to the width of the side view
			if ((panelXScale * planetDistance) < panelWidth) {

				newPlanet = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				newPlanet.name = planetName;
				newPlanet.transform.position = new Vector3 (-0.5F * panelWidth + planetDistance * panelXScale, 0, 0);
				newPlanet.transform.localScale = new Vector3 (planetSize, planetSize, 5.0F * panelDepth);

				planetMaterial = new Material (Shader.Find ("Standard"));
				newPlanet.GetComponent<MeshRenderer> ().material = planetMaterial;
				planetMaterial.mainTexture = Resources.Load (textureName) as Texture;

				sunRelated = thisSide;
				newPlanet.transform.parent = sunRelated.transform;
			}
		}
	}

	//------------------------------------------------------------------------------------//

	void sideDealWithStar (string [] star, GameObject thisSide, GameObject theseOrbits){
		GameObject newSidePanel;
		GameObject newSideSun;
		GameObject sideSunText;

		GameObject habZone;

		Material sideSunMaterial, habMaterial;

		newSidePanel = GameObject.CreatePrimitive (PrimitiveType.Cube);
		newSidePanel.name = "Side " + star[1] + " Panel";
		newSidePanel.transform.position = new Vector3 (0, 0, 0);
		newSidePanel.transform.localScale = new Vector3 (panelWidth, panelHeight, panelDepth);
		newSidePanel.transform.parent = thisSide.transform;

		newSideSun = GameObject.CreatePrimitive (PrimitiveType.Cube);
		newSideSun.name = "Side " + star[1] + " Star";
		newSideSun.transform.position = new Vector3 (-0.5F * panelWidth - 0.5F, 0, 0);
		newSideSun.transform.localScale = new Vector3 (1.0F, panelHeight*40.0F, 2.0F * panelDepth);
		newSideSun.transform.parent = thisSide.transform;

		sideSunMaterial = new Material (Shader.Find ("Unlit/Texture"));
		newSideSun.GetComponent<MeshRenderer> ().material = sideSunMaterial;
		sideSunMaterial.mainTexture = Resources.Load (star[2]) as Texture;


		sideSunText = new GameObject();
		sideSunText.name = "Side Star Name";
		sideSunText.transform.position = new Vector3 (-0.47F * panelWidth, 22.0F * panelHeight, 0);
		sideSunText.transform.localScale = new Vector3 (0.1F, 0.1F, 0.1F);
		var sunTextMesh = sideSunText.AddComponent<TextMesh>();
		sunTextMesh.text = star[1];
		sunTextMesh.fontSize = 150;
		sideSunText.transform.parent = thisSide.transform;

		float innerHab = float.Parse (star[4]) * 9.5F;
		float outerHab = float.Parse (star[4]) * 14F;


		// need to take panelXScale into account for the hab zone

		habZone = GameObject.CreatePrimitive (PrimitiveType.Cube);
		habZone.name = "Hab";
		habZone.transform.position = new Vector3 ((-0.5F * panelWidth) + ((innerHab+outerHab) * 0.5F * panelXScale), 0, 0);
		habZone.transform.localScale = new Vector3 ((outerHab - innerHab)* panelXScale, 40.0F * panelHeight, 2.0F * panelDepth);
		habZone.transform.parent = thisSide.transform;

		habMaterial = new Material (Shader.Find ("Standard"));
		habZone.GetComponent<MeshRenderer> ().material = habMaterial;
		habMaterial.mainTexture = Resources.Load ("habitable") as Texture;

	}

	//------------------------------------------------------------------------------------//

	void dealWithStar (string [] star, GameObject thisStar, GameObject theseOrbits){

		GameObject newSun, upperSun;
		Material sunMaterial;

		GameObject sunRelated;
		GameObject sunSupport;
		GameObject sunText;

		float sunScale = float.Parse(star [0]) / 100000F;
		float centerSunSize = 0.25F;

		// set the habitable zone based on the star's luminosity
		float innerHab = float.Parse (star[4]) * 9.5F;
		float outerHab = float.Parse (star[4]) * 14F;


		newSun = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		newSun.AddComponent<rotate> ();
		newSun.name = star[1];
		newSun.transform.position = new Vector3 (0, 0, 0);
		newSun.transform.localScale = new Vector3 (centerSunSize, centerSunSize, centerSunSize);

		sunRelated = thisStar;

		newSun.GetComponent<rotate> ().rotateSpeed = -0.25F; 

		sunMaterial = new Material (Shader.Find ("Unlit/Texture"));
		newSun.GetComponent<MeshRenderer> ().material = sunMaterial;
		sunMaterial.mainTexture = Resources.Load (star[2]) as Texture;

		newSun.transform.parent = sunRelated.transform;


		// copy the sun and make a bigger version above

		upperSun = Instantiate (newSun);
		upperSun.name = star[1] + " upper";
		upperSun.transform.localScale = new Vector3 (sunScale,sunScale,sunScale);
		upperSun.transform.position = new Vector3 (0, 10, 0);

		upperSun.transform.parent = sunRelated.transform;

		// draw the support between them
		sunSupport = GameObject.CreatePrimitive (PrimitiveType.Cube);
		sunSupport.transform.localScale = new Vector3 (0.1F, 10.0F, 0.1F);
		sunSupport.transform.position = new Vector3 (0, 5, 0);
		sunSupport.name = "Sun Support";

		sunSupport.transform.parent = sunRelated.transform;


		sunText = new GameObject();
		sunText.name = "Star Name";
		sunText.transform.position = new Vector3 (0, 5, 0);
		sunText.transform.localScale = new Vector3 (0.1F, 0.1F, 0.1F);
		var sunTextMesh = sunText.AddComponent<TextMesh>();
		sunTextMesh.text = star[1];
		sunTextMesh.fontSize = 200;

		sunText.transform.parent = sunRelated.transform;


		drawOrbit ("Habitable Inner Ring", innerHab * orbitXScale, Color.green, habWidth, theseOrbits);
		drawOrbit ("Habitable Outer Ring", outerHab * orbitXScale, Color.green, habWidth, theseOrbits);
	}

	//------------------------------------------------------------------------------------//

	void dealWithSystem(string[] starInfo, string[,] planetInfo, Vector3 offset, GameObject allThings, bool show3d){

		GameObject SolarCenter;
		GameObject AllOrbits;
		GameObject SunStuff;
		GameObject Planets;

		SolarCenter = new GameObject();
		AllOrbits = new GameObject();
		SunStuff = new GameObject();
		Planets = new GameObject();

		SolarCenter.name = "SolarCenter" + " " + starInfo[1];
		AllOrbits.name = "All Orbits" + " " + starInfo[1];
		SunStuff.name = "Sun Stuff" + " " + starInfo[1];
		Planets.name = "Planets" + " " + starInfo[1];

		if (show3d == true) {
			SolarCenter.transform.parent = allThings.transform;

			AllOrbits.transform.parent = SolarCenter.transform;
			SunStuff.transform.parent = SolarCenter.transform;
			Planets.transform.parent = SolarCenter.transform;

			dealWithStar (starInfo, SunStuff, AllOrbits);
			dealWithPlanets (planetInfo, Planets, AllOrbits);
		}


		// need to do this last
		SolarCenter.transform.position = offset;


		// add in second 'flat' representation
		GameObject SolarSide;
		SolarSide = new GameObject();
		SolarSide.name = "Side View of" + starInfo[1];

		sideDealWithStar (starInfo, SolarSide, AllOrbits);
		sideDealWithPlanets (planetInfo, SolarSide, AllOrbits);

		SolarSide.transform.position = new Vector3 (0, 8, 10.0F);
		SolarSide.transform.position += (offset * 0.15F);

	}

	void removeSystem(string name){
		
	}

	//------------------------------------------------------------------------------------//
		

	void Start () {


		string[,] solP = new string[243, 9];
		int colIndex = 0;
		int rowIndex = 0;
		string aLine;
		System.IO.StreamReader aFile = new System.IO.StreamReader(@"C:Assets/csv/smallP.csv");

		while ((aLine = aFile.ReadLine()) != null)
		{
			colIndex = 0;
			string[] parts = aLine.Split(',');
			foreach (string part in parts)
			{
				if (part != "") { 
					solP[rowIndex, colIndex] = part;
				}
				else
				{
					rowIndex--;
					// Debug.Log(rowIndex);

					break;
				}
				++colIndex;
			}
			++rowIndex;
		}
		aFile.Close();
		Debug.Log(rowIndex);

		GameObject allCenter = new GameObject();
		allCenter.name = "all systems";

		var systemOffset = new Vector3(0, 0, 0);
		var oneOffset = new Vector3(0, -30, 0);

		for (int i = 0; i<243; i++)
		{
			string[] starH = new string[9];
			int amount = int.Parse(solP[i, 3]);
			string[,] sys = new string[amount, 9];
			for(int j = 0; j < amount; j++)
			{
				for(int k = 0; k<9; k++)
				{
					sys[j,k] = solP[i+j, k];
					starH[k] = solP[i, k];

				}
			}
			dealWithSystem(starH, sys, systemOffset, allCenter);
			systemOffset += oneOffset;
			i = i + amount;
		}
			
		int flagsCounter = 0;

		while (flagsCounter != 4) {
			// select speed of orbitals
			// 0.25x 0.5x 1x 2x 4x
			if (bh.getSpeed () == true) {
				if (bh.getSpeedCounter () == 1) {
					updateInfo ("planetSpeed", 2, solPlanets);
					updateInfo ("planetSpeed", 2, TauCetiPlanets);
					updateInfo ("planetSpeed", 2, Gliese581Planets);
				} else if (bh.getSpeedCounter () == 2) {
					updateInfo ("planetSpeed", 4, solPlanets);
					updateInfo ("planetSpeed", 4, TauCetiPlanets);
					updateInfo ("planetSpeed", 4, Gliese581Planets);
				} else if (bh.getSpeedCounter () == 3) {
					updateInfo ("planetSpeed", 0.25, solPlanets);
					updateInfo ("planetSpeed", 0.25, TauCetiPlanets);
					updateInfo ("planetSpeed", 0.25, Gliese581Planets);
				} else if (bh.getSpeedCounter () == 4) {
					updateInfo ("planetSpeed", 0.5, solPlanets);
					updateInfo ("planetSpeed", 0.5, TauCetiPlanets);
					updateInfo ("planetSpeed", 0.5, Gliese581Planets);
				}
				flagsCounter++;

			}

			// select scale of planets
			// 0.25x 0.5x 1x 2x 4x
			if (bh.getPlanet () == true) {
				if (bh.getPlanetScaleCounter () == 1) {
					updateInfo ("planetScale", 2, solPlanets);
					updateInfo ("planetScale", 2, TauCetiPlanets);
					updateInfo ("planetScale", 2, Gliese581Planets);
				} else if (bh.getPlanetScaleCounter () == 2) {
					updateInfo ("planetScale", 4, solPlanets);
					updateInfo ("planetScale", 4, TauCetiPlanets);
					updateInfo ("planetScale", 4, Gliese581Planets);
				} else if (bh.getPlanetScaleCounter () == 3) {
					updateInfo ("planetScale", 0.25, solPlanets);
					updateInfo ("planetScale", 0.25, TauCetiPlanets);
					updateInfo ("planetScale", 0.25, Gliese581Planets);
				} else if (bh.getPlanetScaleCounter () == 4) {
					updateInfo ("planetScale", 0.5, solPlanets);
					updateInfo ("planetScale", 0.5, TauCetiPlanets);
					updateInfo ("planetScale", 0.5, Gliese581Planets);
				}
				flagsCounter++;

			}

			// select scale of orbit
			// 0.25x 0.5x 1x 2x 4x
			if (bh.getOrbit () == true) {
				if (bh.getOrbitScaleCounter() == 1) {
					updateInfo ("orbitScale", 2, solPlanets);
					updateInfo ("orbitScale", 2, TauCetiPlanets);
					updateInfo ("orbitScale", 2, Gliese581Planets);
				} else if (bh.getOrbitScaleCounter() == 2) {
					updateInfo ("orbitScale", 4, solPlanets);
					updateInfo ("orbitScale", 4, TauCetiPlanets);
					updateInfo ("orbitScale", 4, Gliese581Planets);
				} else if (bh.getOrbitScaleCounter () == 3) {
					updateInfo ("orbitScale", 0.25, solPlanets);
					updateInfo ("orbitScale", 0.25, TauCetiPlanets);
					updateInfo ("orbitScale", 0.25, Gliese581Planets);
				} else if (bh.getOrbitScaleCounter () == 4) {
					updateInfo ("orbitScale", 0.5, solPlanets);
					updateInfo ("orbitScale", 0.5, TauCetiPlanets);
					updateInfo ("orbitScale", 0.5, Gliese581Planets);

				}
				flagsCounter++;
			}

			// which one to select for 3d
			if (bh.getThreeD == true) {

				if (bh.getThreeDName.Equals("sol")){
					dealWithSystem (sol, solPlanets2, systemOffset, allCenter, true);
					systemOffset += oneOffset;

					dealWithSystem (TauCeti, TauCetiPlanets, systemOffset, allCenter, false);

					systemOffset += oneOffset;

					dealWithSystem (Gliese581, Gliese581Planets, systemOffset, allCenter, false);


				}
				else if (bh.getThreeDName.Equals("tau")){
					dealWithSystem (sol, solPlanets2, systemOffset, allCenter, false);
					systemOffset += oneOffset;

					dealWithSystem (TauCeti, TauCetiPlanets, systemOffset, allCenter, true);

					systemOffset += oneOffset;

					dealWithSystem (Gliese581, Gliese581Planets, systemOffset, allCenter, false);


				}
				else if (bh.getThreeDName.Equals("581")){
					dealWithSystem (sol, solPlanets2, systemOffset, allCenter, false);
					systemOffset += oneOffset;

					dealWithSystem (TauCeti, TauCetiPlanets, systemOffset, allCenter, false);

					systemOffset += oneOffset;

					dealWithSystem (Gliese581, Gliese581Planets, systemOffset, allCenter, true);


				}

				flagsCounter++;
			}

		}
			

		allCenter.transform.localScale = new Vector3 (0.1F, 0.1F, 0.1F);

	}

	void updateInfo(string type, double increment, string [,] file){
		int i;
		if (type.Equals ("planetSpeed")) {
			for (i = 0; i < file.GetLength (0); i++) {
				file [i, 2] *= increment;
			}

		} else if (type.Equals ("planetScale")) {
			for (i = 0; i < file.GetLength (0); i++) {
				file [i, 1] *= increment;
			}

		}
		else if (type.Equals ("orbitScale")) {
			for (i = 0; i < file.GetLength (0); i++) {
				file [i, 0] *= increment;
			}

		}
	}
	
	// Update is called once per frame
	// here you want to reset if reset is called
	void Update () {

		// reset everything
		if (Input.GetKeyDown(KeyCode.R)) {
			Destroy(allCenter);
			Start ();
		}
	}
}
