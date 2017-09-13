﻿// Guney Ozsan - Out There (Music Video) - Real time procedural music video in demoscene style for Unity 3D.
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

    public static Title openingTitlesMusic;
    public static Title openingTitlesBy;
    public static Title openingTitlesComposer;
    public static Title partOneTitlesPartNumber;
    public static Title partOneTitlesPartName;

    public Transform newSun;

    [NonSerialized]
    public Transform sun;

    float alignY = 0;

    int animationCurrentBar = 0;
    int currentRegion = 0;

    Gravity gravity;



    void Start()
    {
        openingTitlesMusic = new Title(new Word[] {
            new Word(new Vector3(-55f, 17f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "OUT"),
            new Word(new Vector3(-2f, 17f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "THERE"),
        });

        openingTitlesBy = new Title(new Word[] {
            new Word(new Vector3(-11f, 7f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "BY"),
        });

        openingTitlesComposer = new Title(new Word[] {
            new Word(new Vector3(-66f, 7f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "GUNEY"),
            new Word(new Vector3(8f, 7f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "OZSAN"),
        });

        partOneTitlesPartNumber = new Title(new Word[] {
            new Word(new Vector3(-30f, 7f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "PART I"),
        });

        partOneTitlesPartName = new Title(new Word[] {
            new Word(new Vector3(-25f, 7f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "PROBE"),
        });

        Transform gravityTarget = GetComponent<AnimationManager>().sun;
        Transform planetesimalParent = new GameObject("Planetesimals").transform;

        //int cubeSideLength = MathUtility.ClosestCubeRoot(openingTitlesBy.ParticleCount, true);
        int cubeSideLength = MathUtility.ClosestCubeRoot(896, true);

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
                    Space.planetesimals.Add(new Planetesimal(Instantiate(planetesimalPrefab, new Vector3(x, y + alignY, z), Quaternion.identity, planetesimalParent)));
                }
            }
        }
    }



    void Update()
    {
        switch (Sequencer.CurrentBar)
        {
            case 4:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    currentRegion = Sequencer.CurrentRegionId;

                    sun = Instantiate(newSun, new Vector3(0, alignY, 0), Quaternion.identity);
                    sun.localScale = new Vector3(5, 5, 5);
                    sun.tag = "Sun";
#if UNITY_EDITOR
                    sun.name = "PyramidSun";
#endif
                    SetGravity(0);
                }
                break;

            case 7:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    Title.FormTitle(openingTitlesMusic, 10, 0.05f, true);
                    SetGravity(-3);
                }
                break;

            case 20:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    Title.SpreadTitle(openingTitlesMusic, 30, 3 * (float)Sequencer.BarDuration, 0.018f, false);
                }
                break;

            case 114:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    Title.FormTitle(partOneTitlesPartNumber, 5f, 0.012f, true);
                }
                break;

            case 117:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    Title.FormTitle(partOneTitlesPartName, 5f, 0, true);
                }
                break;

            case 113:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    Title.FormTitle(openingTitlesComposer, 4f, 0.018f, true);
                }
                break;

            case 119:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    Title.SpreadTitle(openingTitlesComposer, 20, 8, 0.018f, true);
                }
                break;

            case 120:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    Title.SetPlanetesimalsFree();
                    Title.FormTitle(openingTitlesComposer, 4f, 0.018f, true);
                }
                break;

            case 116:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    Title.SetPlanetesimalsFree();
                    Title.FormTitle(partOneTitlesPartNumber, 4f, 0.018f, true);
                }
                break;
        }

        if (animationCurrentBar != Sequencer.CurrentBar)
        {
            animationCurrentBar = Sequencer.CurrentBar;
        }

        //------------------------

//        if (Sequencer.CurrentRegionId == 2)
//        {
//            if (currentRegion != Sequencer.CurrentRegionId)
//            {
//                currentRegion = Sequencer.CurrentRegionId;

//                sun = Instantiate(newSun, new Vector3(0, 0, 0), Quaternion.identity);
//                sun.localScale = new Vector3(5, 5, 5);
//                sun.tag = "Sun";
//#if UNITY_EDITOR
//                sun.name = "PyramidSun";
//#endif
//            }

//            if (Sequencer.CurrentBar == 6 && currentBar != 6)
//            {
//                Title.FormTitle(openingTitlesName   , 7f, 0.05f);
//            }

//            if (currentBar != Sequencer.CurrentBar)
//            {
//                currentBar = Sequencer.CurrentBar;
//            }
//        }
//        else if (Sequencer.CurrentRegionId == 4)
//        {
//            if (currentRegion != Sequencer.CurrentRegionId)
//            {
//                currentRegion = Sequencer.CurrentRegionId;
//                //Title.FormTitle(openingTitlesBy, 5f, 0.03f);
//            }

//            if (Sequencer.CurrentBar == 14 && currentBar != 14)
//            {
//                //Title.FormTitle(openingTitlesWide, 6.33f);
//            }

//            if (currentBar != Sequencer.CurrentBar)
//            {
//                currentBar = Sequencer.CurrentBar;
//            }
//        }
        //else if (Sequencer.CurrentRegionId == 5)
        //{
        //    if (currentRegion != Sequencer.CurrentRegionId)
        //    {
        //        currentRegion = Sequencer.CurrentRegionId;
        //        Title.SetPlanetesimalsFree();
        //    }

        //    if (Sequencer.CurrentBar == 18 && currentBar != 18)
        //    {
        //        Title.FormTitle(partOneTitles, 12.4675f, 0);
        //    }
        //    else if (Sequencer.CurrentBar == 22 && currentBar != 22)
        //    {
        //        Title.FormTitle(partOneTitlesWide, 6.33f, 0);
        //    }
        //    else if (Sequencer.CurrentBar == 24 && currentBar != 24)
        //    {
        //        Title.SetPlanetesimalsFree();
        //    }

        //    if (currentBar != Sequencer.CurrentBar)
        //    {
        //        currentBar = Sequencer.CurrentBar;
        //    }
        //}
        //else if (Sequencer.CurrentRegionId >= 6 && Sequencer.CurrentRegionId <= 8 && currentBar != Sequencer.CurrentBar)
        //{
        //    currentRegion = Sequencer.CurrentRegionId;
        //    SwitchAnimation(0, -300, 0);
        //    currentBar = Sequencer.CurrentBar;
        //}
        //else if (Sequencer.CurrentRegionId >= 10 && Sequencer.CurrentRegionId <= 24 && currentBar != Sequencer.CurrentBar)
        //{
        //    currentRegion = Sequencer.CurrentRegionId;
        //    SwitchAnimation(1, -300, 0);
        //    currentBar = Sequencer.CurrentBar;
        //}
        //else if (Sequencer.CurrentRegionId == 9 || Sequencer.CurrentRegionId == 25)
        //{
        //    currentRegion = Sequencer.CurrentRegionId;
        //    TurnOffAnimation(0);
        //    currentBar = Sequencer.CurrentBar;
        //    sun.GetComponent<Collider>().enabled = false;
        //    sun.GetComponent<Renderer>().enabled = false;
        //    sun.GetComponent<Transform>().localScale = Vector3.Lerp(sun.GetComponent<Transform>().localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime);
        //}
    }



    void SetGravity(float gravityForce)
    {
        for (int i = 0; i < Space.planetesimals.Count; i++)
        {
            Space.planetesimals[i].Gravity.SetForce(gravityForce);
        }
    }



    void SwitchAnimation(int switcher, int gravityForce, int antiGravityForce)
    {
        if (animationCurrentBar % 2 == switcher)
        {
            foreach (Planetesimal planetesimal in Space.planetesimals)
            {
                planetesimal.Gravity.SetForce(gravityForce);
            }
        }
        else
        {
            foreach (Planetesimal planetesimal in Space.planetesimals)
            {
                planetesimal.Gravity.SetForce(antiGravityForce);
            }
        }
    }



    void TurnOffAnimation(int antiGravityForce)
    {
        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
        {
            gravity = planet.GetComponent<Gravity>();
            gravity.SetForce(antiGravityForce);
        }
    }
}
