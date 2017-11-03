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

    Rigidbody Rigidbody { get; set; }
    Gravity Gravity { get; set; }
    Mover Mover { get; set; }
    public bool InUse { get; private set; }

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Gravity = GetComponent<Gravity>();
        Mover = GetComponent<Mover>();

        Mover.MoverFinished += OnMoverFinished;
    }

    void OnDestroy()
    {
        Mover.MoverFinished -= OnMoverFinished;
    }

    public void OnMoverFinished()
    {
        InUse = false;
    }

    public void MoveTo(Vector3 target, float time, float delay, bool sphericalLerp)
    {
        InUse = true;
        Mover.MoveTo(target, time, delay, sphericalLerp);
    }

    public void SpreadAround(float range, float time, float delay, bool sphericalLerp)
    {
        InUse = true;
        Mover.SpreadAround(range, time, delay, sphericalLerp);
    }

    public void SetGravityForce(float gravityForce)
    {
        Gravity.SetForce(gravityForce);
    }

    public void SetVelocity(Vector3 velocity)
    {
        Rigidbody.velocity = velocity;
    }
    
    public void SetFree()
    {
        InUse = false;
    }
}