// Guney Ozsan - Out There (Music Video) - Demo style real time music video made with Unity 3D.
// Copyright (C) 2017 Guney Ozsan

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// ---------------------------------------------------------------------

#pragma strict
var newSun : Transform;
var newPlanet : Transform;

static var sun : Transform;
static var planet : Transform;

static var timeKeeper : TimeKeeper;
static var compareBar : int;

var testScript : Attraction;

function Awake () {
	sun = Instantiate (newSun, Vector3(0, 0, 13), Quaternion.identity);
	sun.localScale = Vector3(5, 5, 5);
	sun.parent = transform;
	sun.tag = "Sun";
	
	//Camera.main.GetComponent(SmoothFollow).target = sun;
	//Instantiate (planet, sun.transform.position + Vector3(10, 0, 0), Quaternion.identity);

/*
	for (var i : int = 0; i < 1000; i++) {
		planet = Instantiate (newPlanet, Vector3(2*i, 0, 0), Quaternion.identity);
		planet.parent = transform;
		planet.GetComponent(Attraction).target = sun;
		planet.rigidbody.velocity = Vector3(0,0,.2);
	}
*/
}

function Start () {
	timeKeeper = GetComponent(TimeKeeper);
	compareBar = timeKeeper.fastForwardToBar;
}

function Update () {
	//sun.transform.RotateAround (Vector3.zero, Vector3.up, 20 * Time.deltaTime);
	
	sun.transform.Rotate (0, 50 * Time.deltaTime, 0);
	
	if (timeKeeper.currentRegionID == 2 && compareBar != timeKeeper.currentBar) {
		switchAnimation (0, 10, 0);
		compareBar = timeKeeper.currentBar;
	}
	else if (timeKeeper.currentRegionID == 3 && compareBar != timeKeeper.currentBar) {
		switchAnimation (2, 0, -70);
		compareBar = timeKeeper.currentBar;
	}
	else if (timeKeeper.currentRegionID == 4 && compareBar != timeKeeper.currentBar) {
		switchAnimation (2, 0, -70);
		compareBar = timeKeeper.currentBar;
	}
	else if (timeKeeper.currentRegionID >= 5 && timeKeeper.currentRegionID <= 8 && compareBar != timeKeeper.currentBar) {
		switchAnimation (0, -300, 0);
		compareBar = timeKeeper.currentBar;
	}
	else if (timeKeeper.currentRegionID >= 10 && timeKeeper.currentRegionID <= 24 && compareBar != timeKeeper.currentBar) {
		switchAnimation (1, -300, 0);
		compareBar = timeKeeper.currentBar;
	}
	else if (timeKeeper.currentRegionID == 9 || timeKeeper.currentRegionID == 25) {
		turnOffAnimation (0);
		compareBar = timeKeeper.currentBar;
		sun.GetComponent(Collider).enabled = false;
		sun.GetComponent(Renderer).enabled = false;
		sun.GetComponent(Transform).localScale = Vector3.Lerp(sun.GetComponent(Transform).localScale, Vector3(0.1, 0.1, 0.1), Time.deltaTime);
	}
	
	
}



function switchAnimation (switcher : int, gravityForce : int, antiGravityForce : int) {
	if (compareBar%2 == switcher) {
		for(var planet : GameObject in GameObject.FindGameObjectsWithTag("Planet")) {
	    		testScript = planet.GetComponent(Attraction); 
	  			testScript.forceMultiplier = gravityForce;
		}
	}
	else {
		for(var planet : GameObject in GameObject.FindGameObjectsWithTag("Planet")) {
	    		testScript = planet.GetComponent(Attraction); 
	  			testScript.forceMultiplier = antiGravityForce;
		}
	}
}

function turnOffAnimation (antiGravityForce : int) {
	for(var planet : GameObject in GameObject.FindGameObjectsWithTag("Planet")) {
    		testScript = planet.GetComponent(Attraction); 
  			testScript.forceMultiplier = antiGravityForce;
	}
}