using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinGalaxyAnimation {

    static float radian = 0;
    static float t = 0;

    static List<Vector3> targets = new List<Vector3>() {
        Vector3.zero
    };
	
	public static void UpdateFrame(int firstBar, int lastBar)
    {
        float sequenceLength = Sequencer.BarDuration * (lastBar + 1 - firstBar);
        float rotationSpeed = 0.002f;

        float initialR = 18f;
        float maxR = 55f;
        float rGrowthPower = 0.5f;
        float r = initialR + (maxR - initialR) * (Mathf.Pow(t, rGrowthPower) / Mathf.Pow(sequenceLength, rGrowthPower));

        float radialOffset = 0.6f;
        Vector3 orbit = new Vector3(
                    r * Mathf.Cos(radian + radialOffset),
            0.37f * r * Mathf.Cos(radian + radialOffset),
                   -r * Mathf.Sin(radian + radialOffset));
        Vector3 offset = new Vector3(0, 0, +10f);
        targets = new List<Vector3>() {
             orbit + offset,
            -orbit + offset
        };
        radian = rotationSpeed * 60f * t;

        float maxGravityForce = -100f;
        float forceGrowthPower = 1f / 2.8f;
        float force = maxGravityForce * Mathf.Pow(t, forceGrowthPower) / Mathf.Pow(sequenceLength, forceGrowthPower);

        AnimationManager.SetGravityPerBar(new float[] { 0.65f * force, 1.70f * force}, targets, 1);
        t += Time.deltaTime;
    }
}
