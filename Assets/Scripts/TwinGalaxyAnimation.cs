using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinGalaxyAnimation {

    static float radian = 0;
    static float t = 0;

    // Variable cache for optimization
    static List<Vector3> targets = new List<Vector3>() {
        Vector3.zero
    };
    static float normalizedT;
    static float radius;
    static float radialOffset;
    static Vector3 orbit;
    static Vector3 offset;
    static float force;
    static float rotationSpeed;
	
	public static void UpdateFrame(int firstBar, int lastBar)
    {
        normalizedT = t / (Sequencer.BarDuration * (lastBar - firstBar + 1));
        radius = MathUtility.ExponentialInterpolation(55f, 0, 2.6f, normalizedT);
        radialOffset = 0.6f;
        orbit = new Vector3(
                    radius * Mathf.Cos(radian + radialOffset),
            0.37f * radius * Mathf.Cos(radian + radialOffset),
                   -radius * Mathf.Sin(radian + radialOffset));
        offset = new Vector3(0, 0, 50f);
        targets = new List<Vector3>() {
             orbit + offset,
            -orbit + offset
        };
        force = MathUtility.ExponentialInterpolation(0f, -220f, 0.40f, normalizedT);
        AnimationManager.SetForces(force, targets);
        rotationSpeed = MathUtility.ExponentialInterpolation(0.0029f, 0.0095f, 14f, normalizedT);
        radian = rotationSpeed * 60f * t;
        t += Time.deltaTime;
    }
}
