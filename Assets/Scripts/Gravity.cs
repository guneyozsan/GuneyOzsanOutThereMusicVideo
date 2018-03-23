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

// Definition of the Gravity and other attraction forces. Controlled by adjusting the "forceMultipler" variable at PlayAnimation.js.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody body;
    ForceVector[] forceVectors;
    ForceVector force;

    Vector3 compoundForce;

    public struct ForceVector {
        public float forceMagnitude;
        public Vector3 target;

        public ForceVector (float forceMagnitude, Vector3 target)
        {
            this.forceMagnitude = forceMagnitude;
            this.target = target;
        }
    }

    void Start()
    {
        body = GetComponent<Rigidbody>();
        forceVectors = new ForceVector[0];
    }

    void Update ()
    {
        compoundForce = Vector3.zero;
        for (int i = 0; i < forceVectors.Length; i++)
        {
            if (Mathf.Pow(Vector3.Distance(transform.position, forceVectors[i].target), 2) != 0)
                    compoundForce += forceVectors[i].forceMagnitude * (transform.position - forceVectors[i].target) / Mathf.Pow(Vector3.Distance(transform.position, forceVectors[i].target), 2);
        }
        body.AddForce(compoundForce);
    }

    public void SetForce(float forceMagnitude, Vector3 target)
    {
        forceVectors = new ForceVector[] { new ForceVector(forceMagnitude, target) };
    }

    public void SetForce(ForceVector[] forceVectors)
    {
        this.forceVectors = forceVectors;
    }
}
