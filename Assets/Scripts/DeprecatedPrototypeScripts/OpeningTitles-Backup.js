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

class TextPixelColumn {
    var textPixelColumnArray : boolean[];
}

class TextPixelWord {
	var textPixelWordArray : TextPixelColumn [];
	var openingTitlePosition : Vector3;
}

var openingTitles : TextPixelWord [] = new TextPixelWord [4];
//var openingTitlePosition : Vector3 [] = new Vector3 [4];

var newPlanet : Transform;
static var planet : Transform;

function Start () {
// MUSIC VIDEO PARAMETERS (disable when working on the cover artwork)
	openingTitles[0].openingTitlePosition = Vector3(-62, 15, 4.6);
	openingTitles[1].openingTitlePosition = Vector3(10, 15, 4.6);
	openingTitles[2].openingTitlePosition = Vector3(-50, 0, 4.6);
	openingTitles[3].openingTitlePosition = Vector3(-2, 0, 4.6);
	
// COVER ART PARAMETERS (disable when working on the music video)
//	openingTitles[0].openingTitlePosition = Vector3(-29, 41, 22);
//	openingTitles[1].openingTitlePosition = Vector3(-29, 26, 22);
//	openingTitles[2].openingTitlePosition = Vector3(-17, -16, 22);
//	openingTitles[3].openingTitlePosition = Vector3(-29, -34, 22);	
	
	var targetAssign : Transform = GetComponent(PlayAnimation).sun;
	
	for (var i : int = 0; i < openingTitles.Length; i++) {
		for (var j : int = 0; j < openingTitles[i].textPixelWordArray.Length; j++) {
			for (var k : int = 0; k < openingTitles[i].textPixelWordArray[j].textPixelColumnArray.Length; k++) { 
				if (openingTitles[i].textPixelWordArray[j].textPixelColumnArray[k]) {
					planet = Instantiate (newPlanet, openingTitles[i].openingTitlePosition + Vector3(2*j + 2*parseInt(j/5), -2*k, 0), Quaternion.identity);
					planet.parent = transform;
					planet.GetComponent(Attraction).target = targetAssign;
					planet.tag = "Planet";
					
					planet = Instantiate (newPlanet, openingTitles[i].openingTitlePosition + Vector3(2*j + 2*parseInt(j/5) + 0.96, -2*k, 0), Quaternion.identity);
					planet.parent = transform;
					planet.GetComponent(Attraction).target = targetAssign;
					planet.tag = "Planet";
					
					planet = Instantiate (newPlanet, openingTitles[i].openingTitlePosition + Vector3(2*j + 2*parseInt(j/5), -2*k - 0.96, 0), Quaternion.identity);
					planet.parent = transform;
					planet.GetComponent(Attraction).target = targetAssign;
					planet.tag = "Planet";
					
					planet = Instantiate (newPlanet, openingTitles[i].openingTitlePosition + Vector3(2*j + 2*parseInt(j/5) + 0.95, -2*k - 0.96, 0), Quaternion.identity);
					planet.parent = transform;
					planet.GetComponent(Attraction).target = targetAssign;
					planet.tag = "Planet";
				}
			}
		}
	}
	
}

function Update () {

}