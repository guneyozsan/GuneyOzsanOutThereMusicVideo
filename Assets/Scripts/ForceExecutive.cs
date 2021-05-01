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

public class ForceExecutive : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private Force[] forces;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        forces = new Force[0];
    }

    private void Update ()
    {
        if (forces.Length == 0)
        {
            return;
        }
        
        Vector3 compoundForce = Vector3.zero;
        
        foreach (Force force in forces)
        {
            Vector3 deltaPosition = transform.position - force.Center;
            float distanceSquared = deltaPosition.sqrMagnitude;

            if (distanceSquared == 0)
            {
                continue;
            }
            
            compoundForce += force.Charge * deltaPosition / distanceSquared;
        }

        if (compoundForce == Vector3.zero)
        {
            return;
        }
        
        myRigidbody.AddForce(compoundForce);
    }

    public void SetForces(Force[] forces)
    {
        this.forces = forces;
    }
}