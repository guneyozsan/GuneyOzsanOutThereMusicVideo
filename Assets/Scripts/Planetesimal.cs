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

using UnityEngine;

public class Planetesimal : MonoBehaviour
{
    private ForceApplier forceApplier;
    private Mover mover;

    // If the planetesimal is being used for constructing a shape or free.
    public bool IsAllocated { get; private set; }

    private void Awake()
    {
        forceApplier = GetComponent<ForceApplier>();
        
        mover = GetComponent<Mover>();
        mover.DestinationReached += OnDestinationReached;
    }

    private void OnDestroy()
    {
        mover.DestinationReached -= OnDestinationReached;
    }

    public void MoveTo(Vector3 target, float time, bool isSphericalLerp, float delayBeforeMoving)
    {
        SetAllocation(true);
        mover.MoveTo(target, time, isSphericalLerp, delayBeforeMoving);
    }

    public void SpreadAround(float range, float time, bool isSphericalLerp, float delay)
    {
        SetAllocation(true);
        mover.SpreadAround(range, time, isSphericalLerp, delay);
    }

    public void SetForces(Force[] forces)
    {
        forceApplier.SetForces(forces);
    }

    public void SetFree()
    {
        SetAllocation(false);
    }

    private void OnDestinationReached()
    {
        SetAllocation(false);
    }

    private void SetAllocation(bool value)
    {
        IsAllocated = value;
        forceApplier.enabled = !value;
    }
}