// Guney Ozsan - Out There (Music Video) - Real time music video in demoscene style for Unity 3D.
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public Transform newSun;

    [NonSerialized]
    public Transform sun;

    int compareBar;
    int currentRegion = 0;

    Gravity gravity;
    


    void Start()
    {
        compareBar = 0; //sequencer.fastForwardToBar;
    }



    void Update()
    {
        if (Sequencer.CurrentRegionId == 2)
        {
            if (currentRegion != Sequencer.CurrentRegionId)
            {
                sun = Instantiate(newSun, new Vector3(0, 0, 0), Quaternion.identity);
                sun.localScale = new Vector3(5, 5, 5);
                sun.tag = "Sun";
#if UNITY_EDITOR
                sun.name = "PyramidSun";
#endif
                currentRegion = Sequencer.CurrentRegionId;
            }

            if (compareBar != Sequencer.CurrentBar)
            {
                SwitchAnimation(0, 10, 0);
                compareBar = Sequencer.CurrentBar;
            }
        }
        else if (Sequencer.CurrentRegionId == 3 && compareBar != Sequencer.CurrentBar)
        {
            SwitchAnimation(2, 0, -70);
            compareBar = Sequencer.CurrentBar;
        }
        else if (Sequencer.CurrentRegionId == 4 && compareBar != Sequencer.CurrentBar)
        {
            SwitchAnimation(2, 0, -70);
            compareBar = Sequencer.CurrentBar;
        }
        else if (Sequencer.CurrentRegionId >= 5 && Sequencer.CurrentRegionId <= 8 && compareBar != Sequencer.CurrentBar)
        {
            SwitchAnimation(0, -300, 0);
            compareBar = Sequencer.CurrentBar;
        }
        else if (Sequencer.CurrentRegionId >= 10 && Sequencer.CurrentRegionId <= 24 && compareBar != Sequencer.CurrentBar)
        {
            SwitchAnimation(1, -300, 0);
            compareBar = Sequencer.CurrentBar;
        }
        else if (Sequencer.CurrentRegionId == 9 || Sequencer.CurrentRegionId == 25)
        {
            TurnOffAnimation(0);
            compareBar = Sequencer.CurrentBar;
            sun.GetComponent<Collider>().enabled = false;
            sun.GetComponent<Renderer>().enabled = false;
            sun.GetComponent<Transform>().localScale = Vector3.Lerp(sun.GetComponent<Transform>().localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime);
        }
    }



    void SwitchAnimation(int switcher, int gravityForce, int antiGravityForce)
    {
        if (compareBar % 2 == switcher)
        {
            foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
            {
                gravity = planet.GetComponent<Gravity>();
                gravity.forceMultiplier = gravityForce;
            }
        }
        else
        {
            foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
            {
                gravity = planet.GetComponent<Gravity>();
                gravity.forceMultiplier = antiGravityForce;
            }
        }
    }



    void TurnOffAnimation(int antiGravityForce)
    {
        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
        {
            gravity = planet.GetComponent<Gravity>();
            gravity.forceMultiplier = antiGravityForce;
        }
    }
}
