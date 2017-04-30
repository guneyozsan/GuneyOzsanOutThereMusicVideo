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

// Displays the current time, measure and song part for tracking our place in the song and debugging.

#pragma strict

static var timeKeeper : TimeKeeper;

function Start () {
	timeKeeper = GetComponent(TimeKeeper);
}

function OnGUI () {
    GUI.Label (
    	Rect (10, 10, 200, 100), 
    	"Bar:   " + timeKeeper.currentBar.ToString()
    	+ ":" + timeKeeper.currentBeat.ToString()
    	+ "      Time:   " + parseInt(timeKeeper.music.time*1000).ToString() + "                                 "
    	+ "-------------------------------------------       "
    	+ "Part:       " + timeKeeper.currentPart.ToString() + "                                 "
    	+ "Region:   " + timeKeeper.currentRegionID.ToString() + "                                 "
    	+ timeKeeper.currentRegionContent
    );
}	