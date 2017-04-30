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

class Title {
	var word : Word []; // titles are made of words
	var numberOfWords : int; // number of words in the title
	
}

class Word {
	var code : String; // the letter written in 1's and 0's
	var position : Vector3; // position of the word in 3d space
	var letterHeight : int; // letter height and width in particle slots
	var letterWidth : int;
	var slotGapSize : float; // space between each particle slot
	var particleSpacing : float; // space between each particle in a single slot
}

var newPlanet : Transform;
static var planet : Transform;

function Start () {
	
	// initialize the title
	var title : Title = new Title();
	title.numberOfWords = 4; // total number of words in the title
	title.word = new Word [title.numberOfWords]; // create word array of the title
	
	// initialize the word GUNEY
	title.word[0] = new Word();
	title.word[0].position = Vector3(-62, 15, 4.6); // Position of the word
	title.word[0].letterWidth = 5; // number of particle slots
	title.word[0].letterHeight = 5; // number of particle slots	
	title.word[0].slotGapSize = 2; // space between each particle slot
	title.word[0].particleSpacing = 0.96; // space between each particle in a single slot
	title.word[0].code = 	"88888" + "8   8" + "8   8" + "88888" + "8   8" + 
							"8    " + "8   8" + "88  8" + "8    " + "8   8" + 
							"8  88" + "8   8" + "8 8 8" + "8888 " + " 8 8 " + 
							"8   8" + "8   8" + "8  88" + "8    " + "  8  " + 
							"88888" + "88888" + "8   8" + "88888" + "  8  ";

	// initialize the word GUNEY
	title.word[1] = new Word();
	title.word[1].position = Vector3(10, 15, 4.6); // Position of the word
	title.word[1].letterWidth = 5; // number of particle slots
	title.word[1].letterHeight = 5; // number of particle slots	
	title.word[1].slotGapSize = 2; // space between each particle slot
	title.word[1].particleSpacing = 0.96; // space between each particle in a single slot
	title.word[1].code = 	"88888" + "88888" + " 8888" + "  8  " + "8   8" + 
							"8   8" + "   8 " + "8    " + " 8 8 " + "88  8" + 
							"8   8" + "  8  " + " 888 " + " 888 " + "8 8 8" + 
							"8   8" + " 8   " + "    8" + "8   8" + "8  88" + 
							"88888" + "88888" + "8888 " + "8   8" + "8   8";

	// initialize the word GUNEY
	title.word[2] = new Word();
	title.word[2].position = Vector3(-50, 0, 4.6); // Position of the word
	title.word[2].letterWidth = 5; // number of particle slots
	title.word[2].letterHeight = 5; // number of particle slots	
	title.word[2].slotGapSize = 2; // space between each particle slot
	title.word[2].particleSpacing = 0.96; // space between each particle in a single slot
	title.word[2].code = 	"88888" + "8   8" + "88888" +
							"8   8" + "8   8" + "  8  " +
							"8   8" + "8   8" + "  8  " +
							"8   8" + "8   8" + "  8  " +
							"88888" + "88888" + "  8  ";

	// initialize the word GUNEY
	title.word[3] = new Word();
	title.word[3].position = Vector3(-2, 0, 4.6); // Position of the word
	title.word[3].letterWidth = 5; // number of particle slots
	title.word[3].letterHeight = 5; // number of particle slots	
	title.word[3].slotGapSize = 2; // space between each particle slot
	title.word[3].particleSpacing = 0.96; // space between each particle in a single slot
	title.word[3].code = 	"88888" + "8   8" + "88888" + "8888 " + "88888" + 
							"  8  " + "8   8" + "8    " + "8   8" + "8    " + 
							"  8  " + "88888" + "8888 " + "8888 " + "8888 " + 
							"  8  " + "8   8" + "8    " + "8 8  " + "8    " + 
							"  8  " + "8   8" + "88888" + "8  8 " + "88888";
	var targetAssign : Transform = GetComponent(PlayAnimation).sun;
	
	// temporary variables for the x-y axises
	var x : float;
	var y : float;
	
	var putSpaceBetweenLetters : float;
	var offsetLineToParagraphIndent : float; // functions like the return key.
	var rowLength : float; // current row length. 
	var currentRow : float; // indicates the row number
	
	for (var i : int = 0; i < title.word.Length; i++) { // i traces every word in the title
		for (var j : int = 0; j < title.word[i].code.Length ; j++) { // j traces every slot in the letter			
			if (title.word[i].code[j] != " ") {
				rowLength = title.word[i].code.length / title.word[i].letterHeight; // current row length. code.length/letterHeight gives the length of rows in the word. 
				currentRow = parseInt(j / rowLength); // the current row. "row" increases 1 with every row of particle slots. Every 25 times for j for the word GUNEY
				y = -1 * title.word[i].slotGapSize * currentRow;
				
				putSpaceBetweenLetters = title.word[i].slotGapSize * parseInt(j / title.word[i].letterWidth); // puts space between letters
				offsetLineToParagraphIndent = -1 * currentRow * (title.word[i].slotGapSize) * (rowLength + rowLength/title.word[i].letterWidth); // functions like the return key. offsets each line of particle slots back to paragraph indent. This is because the word is coded as a single string.
				x = title.word[i].slotGapSize*j + putSpaceBetweenLetters + offsetLineToParagraphIndent;
				
				planet = Instantiate (newPlanet, title.word[i].position + Vector3(x							, y							, 0), Quaternion.identity);
				planet.parent = transform;
				planet.GetComponent(Attraction).target = targetAssign;
				planet.tag = "Planet";
				
				planet = Instantiate (newPlanet, title.word[i].position + Vector3(x + title.word[i].particleSpacing	, y							, 0), Quaternion.identity);
				planet.parent = transform;
				planet.GetComponent(Attraction).target = targetAssign;
				planet.tag = "Planet";
				
				planet = Instantiate (newPlanet, title.word[i].position + Vector3(x							, y - title.word[i].particleSpacing	, 0), Quaternion.identity);
				planet.parent = transform;
				planet.GetComponent(Attraction).target = targetAssign;
				planet.tag = "Planet";
				
				planet = Instantiate (newPlanet, title.word[i].position + Vector3(x + title.word[i].particleSpacing	, y - title.word[i].particleSpacing	, 0), Quaternion.identity);
				planet.parent = transform;
				planet.GetComponent(Attraction).target = targetAssign;
				planet.tag = "Planet";
			}
		}
	}
	
//	
//	
//	
//	
//	
//	
//	
//	
//	for (i = 0; i < title.word[0].column.Length; i++) {
//		title.word[0].column[i].pixelIsFilled
//	}
//	
//	
//	title.word[1].column = new column [25]; // 25 columns required for 5 letter word OZSAN
//	title.word[2].column = new column [15]; // 15 columns required for 3 letter word OUT
//	title.word[3].column = new column [25]; // 25 columns required for 5 letter word THERE
//	
//	// Initialize the number of pixels in each column which is 5.
//	for (var ii : int = 0; ii < title.word.Length; ii++) {
//		for (var jj : int = 0; jj < title.word[ii].particleWordColumn.Length; jj++) {
//			title.word[ii].column[jj].pixelIsFilled = new boolean [5];
//		}
//	}
//		
//	// Position of each letter.
//// MUSIC VIDEO PARAMETERS (disable when working on the cover artwork)
//	
//	title.word[1].position = Vector3(10, 15, 4.6); // Position of the word OZSAN
//	title.word[2].position = Vector3(-50, 0, 4.6); // Position of the word OUT
//	title.word[3].position = Vector3(-2, 0, 4.6); // Position of the word THERE
//	
//// COVER ART PARAMETERS (disable when working on the music video)
////	title.word[0].position = Vector3(-29, 41, 22);
////	title.word[1].position = Vector3(-29, 26, 22);
////	title.word[2].position = Vector3(-17, -16, 22);
////	title.word[3].position = Vector3(-29, -34, 22);	
//	
//	var targetAssign : Transform = GetComponent(PlayAnimation).sun;
//	
//	// Creates cubes (planets)
//	
//	for (var i : int = 0; i < title.word.Length; i++) {
//		for (var j : int = 0; j < title.word[i].column.Length; j++) {
//			for (var k : int = 0; k < title.word[i].column[j].pixelIsFilled.Length; k++) { 
//				if (title.word[i].column[j].pixelIsFilled[k]) {
//					planet = Instantiate (newPlanet, title.word[i].position + Vector3(2*j + 2*parseInt(j/5), -2*k, 0), Quaternion.identity);
//					planet.parent = transform;
//					planet.GetComponent(Attraction).target = targetAssign;
//					planet.tag = "Planet";
//					
//					planet = Instantiate (newPlanet, title.word[i].position + Vector3(2*j + 2*parseInt(j/5) + 0.96, -2*k, 0), Quaternion.identity);
//					planet.parent = transform;
//					planet.GetComponent(Attraction).target = targetAssign;
//					planet.tag = "Planet";
//					
//					planet = Instantiate (newPlanet, title.word[i].position + Vector3(2*j + 2*parseInt(j/5), -2*k - 0.96, 0), Quaternion.identity);
//					planet.parent = transform;
//					planet.GetComponent(Attraction).target = targetAssign;
//					planet.tag = "Planet";
//					
//					planet = Instantiate (newPlanet, title.word[i].position + Vector3(2*j + 2*parseInt(j/5) + 0.95, -2*k - 0.96, 0), Quaternion.identity);
//					planet.parent = transform;
//					planet.GetComponent(Attraction).target = targetAssign;
//					planet.tag = "Planet";
//				}
//			}
//		}
//	}
	
}

function Update () {
	
}

//function createWord (title.word : ParticleWord, word : String) {
	
//}