using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinGalaxyAnimation {

    static float rad = 0;
    static float t = 0;
    static List<Vector3> targets = new List<Vector3>() {
        Vector3.zero
    };
	
	public static void UpdateFrame(int firstBar, int lastBar)
    {
        float sequenceLength = Sequencer.BarDuration * (lastBar+ 1 - firstBar);
        int k = 1 + Sequencer.CurrentBar - firstBar;
        float speed = 0.0016f;

        float initialR = 20f;
        float maxR = 35f;
        float rPower = 0.5f;
        float r = initialR + (maxR - initialR) * (Mathf.Pow(t, rPower) / Mathf.Pow(sequenceLength, rPower));

        float zOffset = -9.4f;
        targets = new List<Vector3>() {
            new Vector3( r * Mathf.Cos(rad), -0.5f * r * Mathf.Cos(rad), -r * Mathf.Sin(rad) + zOffset),
            new Vector3(-r * Mathf.Cos(rad),  0.5f * r * Mathf.Cos(rad),  r * Mathf.Sin(rad) + zOffset)
        };
        rad = speed * 60f * t;

        float forcePower = 1f / 3f;
        float force = -1f * Mathf.Pow(t, forcePower) * 100f / Mathf.Pow(sequenceLength, forcePower);

        AnimationManager.SwitchAnimation(new float[] { 1.85f * force, 0.70f * force }, targets);
        t += Time.deltaTime;
    }
}
