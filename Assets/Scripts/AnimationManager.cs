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
    public static Title partTwoTitlesPartNumber;
    public static Title partTwoTitlesPartName;

    //[NonSerialized]
    //public Transform sun;

    float alignY = 0;

    int animationCurrentBar = 0;

    Gravity gravity;



    void Start()
    {
        openingTitlesMusic = new Title(new Word[] {
            new Word(new Vector3(-19.1f, 15f, 0), 5, 5, 2, 2, 2, 1.3f, "OUT"),
            new Word(new Vector3(-32.85f, -5.2f, 0), 5, 5, 2, 2, 2, 1.3f, "THERE"),
        });

        openingTitlesBy = new Title(new Word[] {
            new Word(new Vector3(-11f, 7f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "BY"),
        });

        openingTitlesComposer = new Title(new Word[] {
            new Word(new Vector3(-66f, 7f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "GUNEY"),
            new Word(new Vector3(8f, 7f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "OZSAN"),
        });

        partOneTitlesPartNumber = new Title(new Word[] {
            new Word(new Vector3(-39.5f, 18f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "PART I"),
        });

        partOneTitlesPartName = new Title(new Word[] {
            new Word(new Vector3(-53.45f, -11.3f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "APPROACH"),
        });

        partTwoTitlesPartNumber = new Title(new Word[] {
            new Word(new Vector3(-39.5f, 18f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "PART II"),
        });

        partTwoTitlesPartName = new Title(new Word[] {
            new Word(new Vector3(-32.85f, -11.3f, -9.4f), 5, 5, 2, 2, 2, 1.3f, "PROBE"),
        });

        Transform planetesimalParent = new GameObject("Planetesimals").transform;

        //int cubeSideLength = MathUtility.ClosestCubeRoot(
        //    openingTitlesMusic.ParticleCount
        //    + partOneTitlesPartNumber.ParticleCount
        //    + partOneTitlesPartName.ParticleCount, true);
        //print(cubeSideLength);
        int cubeSideLength = 11;

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
                    Space.planetesimals.Add(Instantiate(planetesimalPrefab, new Vector3(x, y + alignY, z), Quaternion.identity, planetesimalParent).GetComponent<Planetesimal>());
                }
            }
        }
    }

    bool exploded;

    void Update()
    {
#if UNITY_EDITOR
        Time.timeScale = Debugging.PlaybackSpeed;
#endif

        switch (Sequencer.CurrentBar)
        {
            case 4:
                if (Sequencer.CurrentBeat == 1 && !exploded)
                {
                    openingTitlesMusic.FormTitle(12f * Sequencer.BarDurationF, 0.005f, true, false);
                    exploded = true;
                }

                if (animationCurrentBar != Sequencer.CurrentBar)
                {

//                    sun = Instantiate(newSun, new Vector3(0, alignY, 0), Quaternion.identity);
//                    sun.localScale = new Vector3(5, 5, 5);
//                    sun.tag = "Sun";
//#if UNITY_EDITOR
//                    sun.name = "PyramidSun";
//#endif
                    SetGravity(0);
                }

                break;

            case 16:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    openingTitlesMusic.SpreadTitle(10, 0.8f * Sequencer.BarDurationF, 0.002f, true, false);
                    partOneTitlesPartNumber.FormTitle(14.25f * Sequencer.BarDurationF, 0.001f, true, true);
                    partOneTitlesPartName.FormTitle(14.25f * Sequencer.BarDurationF, 0.001f, true, true);
                }
                break;
                
            case 32:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    partOneTitlesPartNumber.SpreadTitle(10, 1.1f * Sequencer.BarDurationF, 0.002f, true, false);
                    partOneTitlesPartName.SpreadTitle(5, 17f * Sequencer.BarDurationF, 0.002f, true, false);
                }
                break;

            case 38:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    partOneTitlesPartName.SpreadTitle(10, 1.1f * Sequencer.BarDurationF, 0.002f, true, false);
                }
                break;

            case 60:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    partTwoTitlesPartNumber.FormTitle(3f * Sequencer.BarDurationF, 0.007f, true, true);
                    partTwoTitlesPartName.FormTitle(3f * Sequencer.BarDurationF, 0.011f, true, true);
                }
                break;
                
            case 65:
                if (animationCurrentBar != Sequencer.CurrentBar)
                {
                    partTwoTitlesPartNumber.SpreadTitle(10, 0.5f * Sequencer.BarDurationF, 0.0025f, true, false);
                    partTwoTitlesPartName.SpreadTitle(10, 0.5f * Sequencer.BarDurationF, 0.0025f, true, false);
                }
                break;
        }


        if (animationCurrentBar != Sequencer.CurrentBar)
        {
            if (Sequencer.CurrentBar >= 16 && Sequencer.CurrentBar < 56)
            {
                int k = Sequencer.CurrentBar - 16;
                SwitchAnimation(1, -1 * (10 + 2.5f * k), k / 8);
            }
            else if (Sequencer.CurrentBar >= 56 && Sequencer.CurrentBar < 60)
            {
                SetGravity(0);
            }
            else if (Sequencer.CurrentBar >= 60)
            {
                SwitchAnimation(1, -300, 0);
            }

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
            Space.planetesimals[i].SetGravityForce(gravityForce);
        }
    }



    void SwitchAnimation(int switcher, float gravityForce, float antiGravityForce)
    {
        if (animationCurrentBar % 2 == switcher)
        {
            foreach (Planetesimal planetesimal in Space.planetesimals)
            {
                planetesimal.SetGravityForce(gravityForce);
            }
        }
        else
        {
            foreach (Planetesimal planetesimal in Space.planetesimals)
            {
                planetesimal.SetGravityForce(antiGravityForce);
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
