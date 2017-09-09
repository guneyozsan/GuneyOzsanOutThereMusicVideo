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
