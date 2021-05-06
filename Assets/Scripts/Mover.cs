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

using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mover : MonoBehaviour
{
    private Rigidbody myRigidbody;

    public event Action DestinationReached;

    private void Awake()
    {
        myRigidbody = transform.GetComponent<Rigidbody>();
    }

    public void MoveTo(Vector3 targetPosition, float durationSeconds, bool isSphericalLerp,
        float delaySeconds)
    {
        StartCoroutine(MoveToAfterDelayCoroutine(targetPosition, durationSeconds,
            isSphericalLerp, delaySeconds));
    }

    public void SpreadAround(float range, float timeSeconds, bool isSphericalLerp,
        float delaySeconds)
    {
        StartCoroutine(SpreadAroundAfterDelayCoroutine(range, timeSeconds, isSphericalLerp,
            delaySeconds));
    }

    private IEnumerator MoveToAfterDelayCoroutine(Vector3 targetPosition, float durationSeconds,
        bool isSphericalLerp, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        StopAllCoroutines();
        StartCoroutine(MoveToCoroutine(targetPosition, durationSeconds, isSphericalLerp));
    }

    private IEnumerator MoveToCoroutine(Vector3 targetPosition, float durationSeconds,
        bool isSphericalLerp)
    {
        myRigidbody.velocity = Vector3.zero;
        Vector3 initialPosition = transform.position;

        float t = 0;

        while (t <= 1)
        {
            transform.position = isSphericalLerp
                ? Vector3.Slerp(initialPosition, targetPosition, Mathf.SmoothStep(
                    0, 1, t))
                : Vector3.Lerp(initialPosition, targetPosition, Mathf.SmoothStep(
                    0, 1, t));

            t += Time.deltaTime / durationSeconds;
            yield return null;
        }

        while (true)
        {
            transform.position = targetPosition;
            yield return null;
        }
    }

    private IEnumerator SpreadAroundAfterDelayCoroutine(float range, float timeSeconds,
        bool isSphericalLerp, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        StopAllCoroutines();
        StartCoroutine(SpreadAroundCoroutine(range, timeSeconds, isSphericalLerp));
    }

    private IEnumerator SpreadAroundCoroutine(float range, float timeSeconds, bool isSphericalLerp)
    {
        myRigidbody.velocity = Vector3.zero;
        Vector3 initialPosition = transform.position;
        // TODO: Change direction distribution from random point in cube to random point in sphere.
        var randomDirection = new Vector3(Random.value, Random.value, Random.value);
        Vector3 targetPosition = initialPosition + range * (randomDirection - Vector3.one / 2f);
        float t = 0;

        while (t <= 1)
        {
            transform.position = isSphericalLerp
                ? Vector3.Slerp(initialPosition, targetPosition, t)
                : Vector3.Lerp(initialPosition, targetPosition, t);

            t += Time.deltaTime / timeSeconds;
            yield return null;
        }
        
        
        // TODO: Check if re-setting position was a necessary hack or not.
        Vector3 finalPosition = transform.position;
        // TODO: Use last-frame velocity vector to match slerp direction.
        myRigidbody.velocity = (targetPosition - initialPosition) / timeSeconds;
        transform.position = finalPosition;

        if (DestinationReached != null)
        {
            DestinationReached.Invoke();
        }
    }
}
