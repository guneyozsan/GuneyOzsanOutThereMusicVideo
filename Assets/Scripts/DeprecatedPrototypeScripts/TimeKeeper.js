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

static var music : AudioSource; //for performance optimisation (o.w. audio.time ...etc is sufficient)

static var currentPart : int;
static var currentRegionID : int;
static var currentRegionContent : String;
static var currentBar : int;
static var currentBeat : int;

static var BPM : int;
static var beatDuration : double;

static var loopBackToBar : int;

static var fastForwardToBar : int;
static var fastForwardSpeed : int;
static var doFastForward : boolean; // Fast forward the clip for easy debugging.

function Start () {
	fastForwardInitialize (); // Comment out after release
	
	loopBackToBar = 60;
	
	BPM = 77;
	beatDuration = 60.0d/BPM;
	
	music = GetComponent.<AudioSource>();
	//music.time = (fastForwardToBar - 1)*4*beatDuration;
	music.time = 0;
	music.Play();
	
	//currentBar = fastForwardToBar;
	currentBar = 1;
	currentBeat = 1;
	
}

function fastForwardInitialize () {
	doFastForward = true;
	fastForwardToBar = 4; // Temporary variable to start the music video wherever you want
	fastForwardSpeed = 3;
	
	if (fastForwardToBar <= 1 || fastForwardToBar > 191) {
		fastForwardToBar = 1;
		doFastForward = false;
	}
}

function fastForward () {
//Debug.Log(Time.timeScale + " " + doFastForward);
	if (currentBar < fastForwardToBar && Time.timeScale != fastForwardSpeed) {
		Time.timeScale = fastForwardSpeed;
	}
	else {
		doFastForward = false;
		Time.timeScale = 1;
	}
}

function Update () {
	
	if (doFastForward) {
		fastForward();
	}
	
	setCurrentRegion();
	setBeats();
	//musicVideo(currentRegionID);
	loopMusic();
}

function setCurrentRegion () {
	if (music.time < 9.350) {
		currentPart = 0;
		currentRegionID = 1;
		currentRegionContent = "wind intro";
	}
	else if (music.time < 15.584) {
		currentPart = 1;
		currentRegionID = 2;
		currentRegionContent = "explosion";
	}
	else if (music.time < 21.818) {
		currentPart = 1;
		currentRegionID = 3;
		currentRegionContent = "ping sound!";
	}
	else if (music.time < 46.753) {
		currentPart = 1;
		currentRegionID = 4;
		currentRegionContent = "musical base";
	}
	else if (music.time < 96.623) {
		currentPart = 1;
		currentRegionID = 5;
		currentRegionContent = "melody";
	}
	else if (music.time < 121.558) {
		currentPart = 1;
		currentRegionID = 6;
		currentRegionContent = "bass";
	}
	else if (music.time < 146.493) {
		currentPart = 1;
		currentRegionID = 7;
		currentRegionContent = "hihat and full bass";
	}
	else if (music.time < 171.428) {
		currentPart = 1;
		currentRegionID = 8;
		currentRegionContent = "bass syncopation";
	}
	else if (music.time < 183.896) {
		currentPart = 1;
		currentRegionID = 9;
		currentRegionContent = "Part 1 to 2 bridge";
	}
	else if (music.time < 233.766) {
		currentPart = 2;
		currentRegionID = 10;
		currentRegionContent = "A: musical base";
	}
	else if (music.time < 258.701) {
		currentPart = 2;
		currentRegionID = 11;
		currentRegionContent = "A: melody";
	}
	else if (music.time < 283.636) {
		currentPart = 2;
		currentRegionID = 12;
		currentRegionContent = "AB bridge";
	}
	else if (music.time < 308.571) {
		currentPart = 2;
		currentRegionID = 13;
		currentRegionContent = "B: musical base";
	}
	else if (music.time < 333.506) {
		currentPart = 2;
		currentRegionID = 14;
		currentRegionContent = "B: melody";
	}
	else if (music.time < 358.441) {
		currentPart = 2;
		currentRegionID = 15;
		currentRegionContent = "AB bridge";
	}
	else if (music.time < 383.376) {
		currentPart = 2;
		currentRegionID = 16;
		currentRegionContent = "B: melody";
	}
	else if (music.time < 408.311) {
		currentPart = 2;
		currentRegionID = 17;
		currentRegionContent = "AB bridge";
	}
	else if (music.time < 433.246) {
		currentPart = 2;
		currentRegionID = 18;
		currentRegionContent = "A: melody";
	}
	else if (music.time < 458.181) {
		currentPart = 2;
		currentRegionID = 19;
		currentRegionContent = "AB bridge";
	}
	else if (music.time < 483.116) {
		currentPart = 2;
		currentRegionID = 20;
		currentRegionContent = "Part 2 to 1 bridge";
	}
	else if (music.time < 508.051) {
		currentPart = 1;
		currentRegionID = 21;
		currentRegionContent = "Part 1 rhythm + melody + hihat";
	}
	else if (music.time < 532.986) {
		currentPart = 1;
		currentRegionID = 22;
		currentRegionContent = "Part 1 rhythm + melody";
	}
	else if (music.time < 557.922) {
		currentPart = 1;
		currentRegionID = 23;
		currentRegionContent = "Part 1 melody + bass";
	}
	else if (music.time < 582.857) {
		currentPart = 1;
		currentRegionID = 24;
		currentRegionContent = "Part 1 melody + bass + hihat";
	}
	else { // if (music.time < 595.324) {
		currentPart = 1;
		currentRegionID = 25;
		currentRegionContent = "Part 1 to 2 bridge";
	}
}

function setBeats() {
	if (music.time > ((currentBar - 1)*4 + currentBeat)*beatDuration) {
		if (currentBeat < 4) {
			currentBeat++;
		}
		else {
			currentBeat = 1;
			currentBar++;
		}
	}
}

function loopMusic() {
	//This makes the timer loop back to bar 60 when reached the end.
	if (music.time >= 595.324) {
		music.time = (60 - 1)*4*beatDuration;
		music.Play();
		currentBar = loopBackToBar;
		currentBeat = 1;
	}
}