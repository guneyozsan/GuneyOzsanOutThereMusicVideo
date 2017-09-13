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

    bool stoppingCoroutines;
    Vector3 halfVector = new Vector3(0.5f, 0.5f, 0.5f);


    public void MoveTo(Vector3 target, float time, float delay)
    {
        StartCoroutine(MoveThisTo(target, time, delay));
    }



    public void SpreadAround(float range, float time, float delay)
    {
        StartCoroutine(SpreadThisAround(range, time, delay));
    }



    IEnumerator MoveThisTo(Vector3 target, float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 start = transform.position;

        float t = 0;

        while (!stoppingCoroutines)
        {
            transform.position = Vector3.Slerp(start, target, Mathf.SmoothStep(0, 1, t));
            t += Time.deltaTime / time;
            yield return null;
        }
        stoppingCoroutines = false;
    }



    IEnumerator SpreadThisAround(float range, float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 start = transform.position;
        Vector3 target = transform.position + range * (new Vector3(Random.value, Random.value, Random.value) - halfVector);
        float t = 0;

        while (t <= 1)
        {
            transform.position = Vector3.Slerp(start, target, t);
            t += Time.deltaTime / time;
            if (t >= 1)
            {
                stoppingCoroutines = true;
            }
            yield return null;
        }
        transform.GetComponent<Rigidbody>().velocity = (target - start) / time;
    }
}
