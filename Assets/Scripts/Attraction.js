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

// Definition of the Gravity and other attraction forces. Controlled by adjusting the "forceMultipler" variable at PlayAnimation.js.

#pragma strict

var target : Transform;
var forceMultiplier : int;

function Start () {
	forceMultiplier = 0;
}

function Update () {
	 GetComponent.<Rigidbody>().AddForce (forceMultiplier * (transform.position - target.position)/Mathf.Pow(Vector3.Distance(transform.position, target.position), 2));
	 
	 // My first trial. Left here for reference purposes.
	 // rigidbody.AddForce (0 * (Vector3.zero - transform.position)/Mathf.Pow(Vector3.Distance(Vector3.zero, transform.position), 2)); 
}