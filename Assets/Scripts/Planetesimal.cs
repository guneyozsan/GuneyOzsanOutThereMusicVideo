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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetesimal : MonoBehaviour {

    new Rigidbody rigidbody;
    Gravity gravity;
    Mover mover;

    // If the planetesimal is being used for constructing a shape or free.
    public bool InUse { get; private set; }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        gravity = GetComponent<Gravity>();
        mover = GetComponent<Mover>();

        mover.MoverFinished += OnMoverFinished;
    }

    void OnDestroy()
    {
        mover.MoverFinished -= OnMoverFinished;
    }

    public void OnMoverFinished(Vector3 velocity)
    {
        InUse = false;
        SetVelocity(velocity);
    }

    public void MoveTo(Vector3 target, float time, float delay, bool sphericalLerp)
    {
        InUse = true;
        mover.MoveTo(target, time, delay, sphericalLerp);
    }

    public void SpreadAround(float range, float time, float delay, bool sphericalLerp)
    {
        InUse = true;
        mover.SpreadAround(range, time, delay, sphericalLerp);
    }

    public void SetGravityForce(float gravityForce, Vector3 target)
    {
        gravity.SetForce(gravityForce, target);
    }

    public void SetGravityForce(Gravity.ForceVector[] forceVectors)
    {
        gravity.SetForce(forceVectors);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rigidbody.velocity = velocity;
    }
    
    public void SetFree()
    {
        InUse = false;
    }
}