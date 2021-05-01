using UnityEngine;

public struct Force
{
    private readonly float charge;
    private readonly Vector3 center;

    public Force(float charge, Vector3 center)
    {
        this.charge = charge;
        this.center = center;
    }

    public float Charge { get { return charge; } }
    public Vector3 Center { get { return center; } }
}