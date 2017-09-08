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

        while (transform.position != target)
        {
            transform.position = Vector3.Lerp(start, target, Mathf.SmoothStep(0, 1, t));
            t += Time.deltaTime / time;
            yield return null;
        }
    }
}
