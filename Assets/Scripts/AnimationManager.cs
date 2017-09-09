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

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    Transform planetesimalPrefab;

    public static Title openingTitles;
    public static Title openingTitlesWide;
    public static Title partOneTitles;
    public static Title partOneTitlesWide;

    public Transform newSun;

    [NonSerialized]
    public Transform sun;

    int currentBar = 0;
    int currentRegion = 0;

    Gravity gravity;



    void Start()
    {
        openingTitles = new Title(new Word[] {
            new Word(new Vector3(-62f, 15f, -8.4f), 5, 5, 2, 2, 2, 1.1f, "GUNEY"),
            new Word(new Vector3(10f, 15f, -8.4f), 5, 5, 2, 2, 2, 1.1f, "OZSAN"),
            new Word(new Vector3(-50f, 0f, -8.4f), 5, 5, 2, 2, 2, 1.1f, "OUT"),
            new Word(new Vector3(-2f, 0f, -8.4f), 5, 5, 2, 2, 2, 1.1f, "THERE")
        });

        openingTitlesWide = new Title(new Word[] {
            new Word(new Vector3(-66f, 15f, -8.4f), 5, 5, 2, 2, 2, 1.5f, "GUNEY"),
            new Word(new Vector3(6f, 15f, -8.4f), 5, 5, 2, 2, 2, 1.5f, "OZSAN"),
            new Word(new Vector3(-54f, 0f, -8.4f), 5, 5, 2, 2, 2, 1.5f, "OUT"),
            new Word(new Vector3(-6f, 0f, -8.4f), 5, 5, 2, 2, 2, 1.5f, "THERE")
        });

        partOneTitles = new Title(new Word[] {
            new Word(new Vector3(-35f, 15f, -8.4f), 5, 5, 2, 2, 2, 1.1f, "PART I"),
            new Word(new Vector3(-30f, 0f, -8.4f), 5, 5, 2, 2, 2, 1.1f, "PROBE"),
        });

        partOneTitlesWide = new Title(new Word[] {
            new Word(new Vector3(-39f, 15f, -8.4f), 5, 5, 2, 2, 2, 1.5f, "PART I"),
            new Word(new Vector3(-34f, 0f, -8.4f), 5, 5, 2, 2, 2, 1.5f, "PROBE"),
        });

        Transform gravityTarget = GetComponent<AnimationManager>().sun;
        Transform planetesimalParent = new GameObject("Planetesimals").transform;

        int cubeSideLength = MathUtility.ClosestCubeRoot(openingTitles.ParticleCount, true);
        float particlePadding = 1f;
        float alignmentAdjustment = cubeSideLength / 2;

        for (int i = 0; i < cubeSideLength; i++)
        {
            float x = i * particlePadding - alignmentAdjustment;

            for (int j = 0; j < cubeSideLength; j++)
            {
                float y = j * particlePadding - alignmentAdjustment;

                for (int k = 0; k < cubeSideLength; k++)
                {
                    float z = k * particlePadding - alignmentAdjustment;

                    Space.planetesimals.Add(new Planetesimal(Instantiate(planetesimalPrefab, new Vector3(x, y, z), Quaternion.identity, planetesimalParent)));
                }
            }
        }

        //for (int i = 0; i < openingTitles.Length; i++)
        //{
        //    int particlesPerSlot = openingTitles[i].HorizontalParticlesPerSlot * openingTitles[i].VerticalParticlesPerSlot;
        //    for (int j = 0; j < openingTitles[i].Length; j++)
        //    {
        //        openingTitleParticleCount += particlesPerSlot * openingTitles[i][j].OccupiedSlotsCount;
        //    }


        //    float rowLength = openingTitles[i].Code.Length / openingTitles[i].VerticalParticleSlotsPerLetter;
        //    float slotPadding = openingTitles[i].SlotPadding;
        //    int horizontalParticleSlotsPerLetter = openingTitles[i].HorizontalParticleSlotsPerLetter;

        //    for (int j = 0; j < openingTitles[i].Code.Length; j++)
        //    {
        //        if (openingTitles[i].Code[j].ToString() != " ")
        //        {
        //            float currentRow = Mathf.FloorToInt(j / rowLength);
        //            float y = -1 * slotPadding * currentRow;

        //            // Puts space between letters
        //            float letterPadding = slotPadding * (j / horizontalParticleSlotsPerLetter);

        //            // Because the word is coded as a single string, this offsets each line of particle slots back to paragraph indent. 
        //            float offsetLineToParagraphIndent = -1 * currentRow * slotPadding * (rowLength + rowLength / horizontalParticleSlotsPerLetter);

        //            float x = slotPadding * j + letterPadding + offsetLineToParagraphIndent;

        //            for (int k = 0; k < openingTitles[i].HorizontalParticlesPerSlot; k++)
        //            {
        //                for (int l = 0; l < openingTitles[i].VerticalParticlesPerSlot; l++)
        //                {
        //                    planetesimal = Instantiate(planetesimalPrefab, openingTitles[i].Location + new Vector3(x + k * openingTitles[i].ParticlePadding, y - l * openingTitles[i].ParticlePadding, 0), Quaternion.identity, planetesimalParent);
        //                    planetesimal.GetComponent<Gravity>().SetTarget(gravityTarget);
        //                    planetesimal.tag = "Planet";
        //                }
        //            }
        //        }
        //    }
        //}

    }



    void Update()
    {
        if (Sequencer.CurrentRegionId == 2)
        {
            if (currentRegion != Sequencer.CurrentRegionId)
            {
                currentRegion = Sequencer.CurrentRegionId;

                sun = Instantiate(newSun, new Vector3(0, 0, 0), Quaternion.identity);
                sun.localScale = new Vector3(5, 5, 5);
                sun.tag = "Sun";
#if UNITY_EDITOR
                sun.name = "PyramidSun";
#endif
            }

            if (currentBar != Sequencer.CurrentBar)
            {
                currentBar = Sequencer.CurrentBar;
            }
        }
        else if (Sequencer.CurrentRegionId == 4)
        {
            if (currentRegion != Sequencer.CurrentRegionId)
            {
                currentRegion = Sequencer.CurrentRegionId;
                Title.FormTitle(openingTitles, 18.70125f);
            }

            if (Sequencer.CurrentBar == 14 && currentBar != 14)
            {
                Title.FormTitle(openingTitlesWide, 6.33f);
            }

            if (currentBar != Sequencer.CurrentBar)
            {
                currentBar = Sequencer.CurrentBar;
            }
        }
        else if (Sequencer.CurrentRegionId == 5)
        {
            if (currentRegion != Sequencer.CurrentRegionId)
            {
                currentRegion = Sequencer.CurrentRegionId;
                Title.SetPlanetesimalsFree();
            }

            if (Sequencer.CurrentBar == 18 && currentBar != 18)
            {
                Title.FormTitle(partOneTitles, 12.4675f);
            }
            else if (Sequencer.CurrentBar == 22 && currentBar != 22)
            {
                Title.FormTitle(partOneTitlesWide, 6.33f);
            }
            else if (Sequencer.CurrentBar == 24 && currentBar != 24)
            {
                Title.SetPlanetesimalsFree();
            }

            if (currentBar != Sequencer.CurrentBar)
            {
                currentBar = Sequencer.CurrentBar;
            }
        }
        else if (Sequencer.CurrentRegionId >= 6 && Sequencer.CurrentRegionId <= 8 && currentBar != Sequencer.CurrentBar)
        {
            currentRegion = Sequencer.CurrentRegionId;
            SwitchAnimation(0, -300, 0);
            currentBar = Sequencer.CurrentBar;
        }
        else if (Sequencer.CurrentRegionId >= 10 && Sequencer.CurrentRegionId <= 24 && currentBar != Sequencer.CurrentBar)
        {
            currentRegion = Sequencer.CurrentRegionId;
            SwitchAnimation(1, -300, 0);
            currentBar = Sequencer.CurrentBar;
        }
        else if (Sequencer.CurrentRegionId == 9 || Sequencer.CurrentRegionId == 25)
        {
            currentRegion = Sequencer.CurrentRegionId;
            TurnOffAnimation(0);
            currentBar = Sequencer.CurrentBar;
            sun.GetComponent<Collider>().enabled = false;
            sun.GetComponent<Renderer>().enabled = false;
            sun.GetComponent<Transform>().localScale = Vector3.Lerp(sun.GetComponent<Transform>().localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime);
        }
    }



    void SwitchAnimation(int switcher, int gravityForce, int antiGravityForce)
    {
        if (currentBar % 2 == switcher)
        {
            foreach (Planetesimal planetesimal in Space.planetesimals)
            {
                planetesimal.Gravity.forceMultiplier = gravityForce;
            }
        }
        else
        {
            foreach (Planetesimal planetesimal in Space.planetesimals)
            {
                planetesimal.Gravity.forceMultiplier = antiGravityForce;
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
