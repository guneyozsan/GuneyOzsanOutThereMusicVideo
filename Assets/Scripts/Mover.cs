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

public class Mover : MonoBehaviour
{
    public event System.Action MoverFinished;

    Vector3 halfVector = new Vector3(0.5f, 0.5f, 0.5f);

    public void MoveTo(Vector3 target, float time, float delay, bool sphericalLerp)
    {
        StartCoroutine(DelayMoveTo(target, time, delay, sphericalLerp));
    }

    IEnumerator DelayMoveTo(Vector3 target, float time, float delay, bool sphericalLerp)
    {
        yield return new WaitForSeconds(delay);
        StopAllCoroutines();
        StartCoroutine(PerformMoveTo(target, time, sphericalLerp));
    }

    IEnumerator PerformMoveTo(Vector3 target, float time, bool sphericalLerp)
    {
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 start = transform.position;

        float t = 0;

        while (t <= 1)
        {
            if (sphericalLerp)
            {
                transform.position = Vector3.Lerp(start, target, Mathf.SmoothStep(0, 1, t));
            }
            else
            {
                transform.position = Vector3.Slerp(start, target, Mathf.SmoothStep(0, 1, t));
            }
            t += Time.deltaTime / time;
            yield return null;
        }

        while (true)
        {
            transform.position = target;
            yield return null;
        }
    }

    public void SpreadAround(float range, float time, float delay, bool sphericalLerp)
    {
        StartCoroutine(DelaySpreadAround(range, time, delay, sphericalLerp));
    }

    IEnumerator DelaySpreadAround(float range, float time, float delay, bool sphericalLerp)
    {
        yield return new WaitForSeconds(delay);
        StopAllCoroutines();
        StartCoroutine(PerformSpreadAround(range, time, sphericalLerp));
    }

    IEnumerator PerformSpreadAround(float range, float time, bool sphericalLerp)
    {
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 start = transform.position;
        Vector3 target = transform.position + range * (new Vector3(Random.value, Random.value, Random.value) - halfVector);
        float t = 0;

        while (t <= 1)
        {
            if (sphericalLerp)
            {
                transform.position = Vector3.Slerp(start, target, t);
            }
            else
            {
                transform.position = Vector3.Lerp(start, target, t);
            }

            t += Time.deltaTime / time;
            yield return null;
        }
        // Allows the object to keep floating to the direction it is moved.
        transform.GetComponent<Rigidbody>().velocity = (target - start) / time;
        if (MoverFinished != null) MoverFinished();
    }
}
