using System.Collections.Generic;
using UnityEngine;

public class TwinGalaxyAnimation
{
    private readonly float initialRadius;
    private readonly float finalRadius;
    private readonly float radiusExponentialGrowthPower;
    
    private readonly float radialOffset;
    private readonly float verticalOscillationMagnitude;
    private readonly Vector3 twinGalaxyPosition;

    private readonly float initialMass;
    private readonly float finalMass;
    private readonly float massExponentialGrowthPower;
    
    private readonly float initialRotationSpeed;
    private readonly float finalRotationSpeed;
    private readonly float rotationSpeedExponentialGrowthPower;
    
    private float rotationSpeed;

    public TwinGalaxyAnimation(
        float initialRadius, float finalRadius, float radiusExponentialGrowthPower,
        float initialRotationSpeed, float finalRotationSpeed, float rotationSpeedExponentialGrowthPower,
        float radialOffset, float verticalOscillationMagnitude,
        Vector3 twinGalaxyPosition, float initialMass, float finalMass,
        float massExponentialGrowthPower)
    {
        
        this.initialRadius = initialRadius;
        this.finalRadius = finalRadius;
        this.radiusExponentialGrowthPower = radiusExponentialGrowthPower;
        
        this.radialOffset = radialOffset;
        this.verticalOscillationMagnitude = verticalOscillationMagnitude;
        this.twinGalaxyPosition = twinGalaxyPosition;

        this.initialRotationSpeed = initialRotationSpeed;
        this.finalRotationSpeed = finalRotationSpeed;
        this.rotationSpeedExponentialGrowthPower = rotationSpeedExponentialGrowthPower;
        
        this.initialMass = initialMass;
        this.finalMass = finalMass;
        this.massExponentialGrowthPower = massExponentialGrowthPower;

        rotationSpeed = 0;
    }

    public Force[] GetForces(float t, float animationDuration)
    {
        // TODO: Check if moving variables to fields worth as optimization.
        
        float tNormalized = t / animationDuration;

        // Position
        float radius = MathUtility.ExponentialInterpolation(
            initialRadius, finalRadius, radiusExponentialGrowthPower, tNormalized);
        // TODO: Check if 60f is seconds per minute or custom parameter.
        // TODO: Try to make independent of t and dependent to tNormalized.
        float angle = rotationSpeed * 60f * t + radialOffset;
        float momentaryRadiusX = radius * Mathf.Cos(angle);
        float momentaryRadiusY = -radius * Mathf.Sin(angle);
        var orbit = new Vector3(
            momentaryRadiusX,
            verticalOscillationMagnitude * momentaryRadiusX, 
            momentaryRadiusY);

        // Mass
        float mass = MathUtility.ExponentialInterpolation(initialMass, finalMass, 
            massExponentialGrowthPower, tNormalized);

        // TODO: Check if rotationSpeed can be made local variable and moved before radius declaration.
        // NOTE: This could be moved before "float radius" above as local variable "float rotationSpeed".
        rotationSpeed = MathUtility.ExponentialInterpolation(
            initialRotationSpeed, finalRotationSpeed, rotationSpeedExponentialGrowthPower, tNormalized);
        
        return new[]
        {
            new Force(mass, twinGalaxyPosition + orbit),
            new Force(mass, twinGalaxyPosition - orbit)
        };
    }
    
    // TODO: Remove unused code here after checking all parameters are refactored correctly.
	// public static void UpdateFrame(int firstBar, int lastBar)
 //    {
 //        normalizedT = t / (Sequencer.BarDuration * (lastBar - firstBar + 1));
 //        radius = MathUtility.ExponentialInterpolation(55f, 0f, 2.6f, normalizedT);
 //        radialOffset = 0.6f;
 //        
 //        float angle = radian + radialOffset;
 //        float radiusTimesCosAngle = radius * Mathf.Cos(angle);
 //        orbit = new Vector3(
 //            radiusTimesCosAngle,
 //            0.37f * radiusTimesCosAngle, 
 //            -radius * Mathf.Sin(angle));
 //        twinGalaxyPosition = new Vector3(0f, 0f, 50f);
 //        forceCenters = new List<Vector3> {
 //             orbit + twinGalaxyPosition,
 //            -orbit + twinGalaxyPosition
 //        };
 //        
 //        mass = MathUtility.ExponentialInterpolation(0f, -220f, 0.40f, normalizedT);
 //        
 //        AnimationManager.SetForces(mass, forceCenters);
 //        
 //        rotationSpeed = MathUtility.ExponentialInterpolation(0.0029f, 0.0095f, 14f, normalizedT);
 //        radian = rotationSpeed * 60f * t;
 //        t += Time.deltaTime;
 //    }
}