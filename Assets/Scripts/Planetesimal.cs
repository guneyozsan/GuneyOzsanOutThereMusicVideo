using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetesimal {

    public Transform Transform { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Gravity Gravity { get; private set; }
    public Mover Mover { get; private set; }

    public Planetesimal(Transform planetesimal)
    {
        Transform = planetesimal.transform;
        Rigidbody = planetesimal.GetComponent<Rigidbody>();
        Gravity = planetesimal.GetComponent<Gravity>();
        Mover = planetesimal.GetComponent<Mover>();
    }
}
