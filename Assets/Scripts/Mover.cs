// Guney Ozsan - Out There (Music Video) - Real time procedural music video in demoscene style for Unity 3D.
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public void MoveTo(Vector3 target, float time)
    {
        StartCoroutine(MoveThisTo(target, time));
    }

    IEnumerator MoveThisTo(Vector3 target, float time)
    {
        Vector3 start = transform.position;

        float t = 0;

        while (true)
        {
            transform.position = Vector3.Slerp(start, target, Mathf.SmoothStep(0, 1, t));
            t += Time.deltaTime / time;
            yield return null;
        }
    }
}
