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
        float rotationSpeed = 0.003f;

        float initialR = 55f;
        float maxR = 0f;
        float rGrowthPower = 7f;
        float r = initialR + (maxR - initialR) * (Mathf.Pow(t, rGrowthPower) / Mathf.Pow(sequenceLength, rGrowthPower));
        Debug.Log(r);

        float radialOffset = 0.6f;
        Vector3 orbit = new Vector3(
                    r * Mathf.Cos(radian + radialOffset),
            0.37f * r * Mathf.Cos(radian + radialOffset),
                   -r * Mathf.Sin(radian + radialOffset));
        Vector3 offset = new Vector3(0, 0, 40f);
        targets = new List<Vector3>() {
             orbit + offset,
            -orbit + offset
        };
        radian = rotationSpeed * 60f * t;

        float maxGravityForce = -100f;
        float forceGrowthPower = 1f / 2.8f;
        float force = maxGravityForce * Mathf.Pow(t, forceGrowthPower) / Mathf.Pow(sequenceLength, forceGrowthPower);

        AnimationManager.SetGravityPerBar(new float[] { 2.0f * force}, targets, 1, firstBar );
        t += Time.deltaTime;
    }
}
