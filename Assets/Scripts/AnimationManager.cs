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
    [SerializeField]
    Camera mainCamera;

    public static Title openingTitlesMusic;
    public static Title openingTitlesBy;
    public static Title openingTitlesComposer;
    public static Title partOneTitlesPartNumber;
    public static Title partOneTitlesPartName;
    public static Title partTwoTitlesPartNumber;
    public static Title partTwoTitlesPartName;

    float alignY = 0;
    static int currentAnimationBar = 0;
    static int currentAnimationBeat = 0;
    Vector3 defaultCameraLocation;

    Vector3 target = Vector3.zero;
    List<Vector3> targets = new List<Vector3>() {
        Vector3.zero
    };
    
#if UNITY_EDITOR
    static List<GameObject> pilotObjects = new List<GameObject>();
#endif // UNITY_EDITOR

    void Start()
    {
        defaultCameraLocation = mainCamera.transform.position;

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
        //planetesimalParent.gameObject.AddComponent<Rotator>().Initialize(0.005f, 10f);

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

    void Update()
    {
        switch (Sequencer.CurrentBar)
        {
            case 4:
                if (currentAnimationBar != Sequencer.CurrentBar)
                {
                    openingTitlesMusic.FormTitle(12f * Sequencer.BarDuration, 0.005f, true, false);
                    SetGravity(0, Vector3.zero);
                }

                break;

            case 16:
                if (currentAnimationBar != Sequencer.CurrentBar)
                {
                    openingTitlesMusic.SpreadTitle(10, 0.8f * Sequencer.BarDuration, 0.001f, true, false);
                    partOneTitlesPartNumber.FormTitle(14.25f * Sequencer.BarDuration, 0.001f, true, true);
                    partOneTitlesPartName.FormTitle(14.25f * Sequencer.BarDuration, 0.001f, true, true);
                }
                break;

            case 32:
                if (currentAnimationBar != Sequencer.CurrentBar)
                {
                    partOneTitlesPartNumber.SpreadTitle(10, 1f * Sequencer.BarDuration, 0.05f, false, false);
                }
                break;

            case 36:
                if (currentAnimationBar != Sequencer.CurrentBar)
                {
                    partOneTitlesPartName.SpreadTitle(10, 1f * Sequencer.BarDuration, 0.05f, false, false);
                }
                break;
                
            case 60:
                if (currentAnimationBar != Sequencer.CurrentBar)
                {
                    partTwoTitlesPartNumber.FormTitle(3f * Sequencer.BarDuration, 0.007f, true, true);
                    partTwoTitlesPartName.FormTitle(3f * Sequencer.BarDuration, 0.014f, true, true);
                }
                break;

            case 65:
                if (currentAnimationBar != Sequencer.CurrentBar)
                {
                    partTwoTitlesPartNumber.SpreadTitle(10, 0.5f * Sequencer.BarDuration, 0.0025f, true, false);
                    partTwoTitlesPartName.SpreadTitle(10, 0.5f * Sequencer.BarDuration, 0.0025f, true, false);
                }
                break;

            case 100:
                if (currentAnimationBar != Sequencer.CurrentBar)
                {
                    Transform cameraPlanetesimal = Instantiate(planetesimalPrefab, mainCamera.transform.position, Quaternion.identity);
                    Space.planetesimals.Add(cameraPlanetesimal.GetComponent<Planetesimal>());
                    mainCamera.transform.SetParent(cameraPlanetesimal);
                }
                break;
        }

        int firstBarOfSequence = 18;
        int firstBarOfTwinGalaxy = 32;
        int lastBarOfTwinGalaxy = 55;

        if ((Sequencer.CurrentBar >= firstBarOfSequence) && (Sequencer.CurrentBar < firstBarOfTwinGalaxy)
            && (Sequencer.CurrentBeat == 1))
        {
            SetGravityPerBar(new float[] { -65, 0, -65, 0 }, new Vector3(0, 0, -17), 2, firstBarOfSequence);
            //if (FirstTimeInBarAndBeat())
            //SetGravity(-65f, new Vector3(0, 0, -17));
        }
        //else if ((Sequencer.CurrentBar >= 20) && (Sequencer.CurrentBar < firstBarOfTwinGalaxy)
        //    && (Sequencer.CurrentBeat == 1))
        //{
        //    if (FirstTimeInBarAndBeat())
        //    {
        //        int k = Sequencer.CurrentBar - 18;
        //        float gravityForce = -65f + 1.5f * k;
        //        SetGravityPerBar(new float[] { 0f, gravityForce }, new Vector3(0, 0, -17), 1);
        //    }
        //}
        else if ((Sequencer.CurrentBar >= firstBarOfTwinGalaxy) && (Sequencer.CurrentBar < (lastBarOfTwinGalaxy + 1)))
        {
            TwinGalaxyAnimation.UpdateFrame(firstBarOfTwinGalaxy, lastBarOfTwinGalaxy);
        }
        else if (Sequencer.CurrentBar >= lastBarOfTwinGalaxy && Sequencer.CurrentBar < 60)
        {
            if (FirstTimeInBarAndBeat())
                SetGravity(0, Vector3.zero);
        }
        else if (Sequencer.CurrentBar >= 60
            && (Sequencer.CurrentBeat == 1))
        {
            if (FirstTimeInBarAndBeat())
                SetGravityPerBar(new float[] { 0, -300f }, Vector3.zero, 1, 60);
        }

        currentAnimationBar = Sequencer.CurrentBar;
        currentAnimationBeat = Sequencer.CurrentBeat;
    }

    bool FirstTimeInBarAndBeat()
    {
        if ((currentAnimationBar != Sequencer.CurrentBar)
            || (currentAnimationBeat != Sequencer.CurrentBeat))
            return true;
        else
            return false;
    }

    void SetGravity(float gravityForce, Vector3 target)
    {
        List<Vector3> targets = new List<Vector3> { target };
        SetGravity(gravityForce, targets);
    }

    void SetGravity(float gravityForce, List<Vector3> targets)
    {
        int planetesimalsPerTarget = Space.planetesimals.Count / targets.Count;

        for (int i = 0; i < targets.Count; i++)
        {
            int lastPlanetesimalIndex;

            if ((i + 1) * planetesimalsPerTarget >= Space.planetesimals.Count)
                lastPlanetesimalIndex = Space.planetesimals.Count;
            else
                lastPlanetesimalIndex = (i + 1) * planetesimalsPerTarget;

            for (int j = (i * planetesimalsPerTarget); j < lastPlanetesimalIndex; j++)
            {
                Space.planetesimals[j].SetGravityForce(gravityForce, targets[i]);
            }
        }
    }

    public static void SetGravityPerBar(float[] gravityForces, Vector3 target, int perBar, int initialBar)
    {
        List<Vector3> targets = new List<Vector3> { target };
        SetGravityPerBar(gravityForces, targets, perBar, initialBar);
    }

    // Takes an array of gravity forces and sets it each bar.
    public static void SetGravityPerBar(float[] gravityForces, List<Vector3> targets, int perBar, int initialBar)
    {
#if UNITY_EDITOR
        if (pilotObjects.Count > targets.Count)
        {
            for (int i = targets.Count; i < pilotObjects.Count; i++)
            {
                Destroy(pilotObjects[i]);
            }
            pilotObjects = pilotObjects.GetRange(0, targets.Count);
        }
#endif // UNITY_EDITOR

        //int planetesimalsPerTarget = Space.planetesimals.Count / targets.Count;
        //int lastPlanetesimalIndex;

        Gravity.ForceVector[] forceVectors = new Gravity.ForceVector[targets.Count];

        for (int i = 0; i < gravityForces.Length; i++)
        {
            if (((float)(Sequencer.CurrentBar - initialBar) / perBar) % (float)gravityForces.Length == i)
            {
                for (int j = 0; j < targets.Count; j++)
                {
                    forceVectors[j] = new Gravity.ForceVector(gravityForces[i], targets[j]);
                    print(gravityForces[i] + " " + targets[j]);
                    //if ((i + 1) * planetesimalsPerTarget >= Space.planetesimals.Count)
                    //    lastPlanetesimalIndex = Space.planetesimals.Count;
                    //else
                    //    lastPlanetesimalIndex = (i + 1) * planetesimalsPerTarget;

                    //for (int j = 0; j < gravityForces.Length; j++)
                    //{
                    //    if (((float)Sequencer.CurrentBar / perBar) % (float)gravityForces.Length == j)
                    //    {
                    //        force = gravityForces[j];
                    //        print(gravityForces[j]);

                    //        for (int k = (i * planetesimalsPerTarget); k < lastPlanetesimalIndex; k++)
                    //        {
                    //            Space.planetesimals[k].SetGravityForce(force, targets[i]);
                    //        }
                    //    }
                    //}
#if UNITY_EDITOR
                    if (j > pilotObjects.Count - 1)
                    {
                        pilotObjects.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
                        Destroy(pilotObjects[j].GetComponent<SphereCollider>());
                    }
                    pilotObjects[j].transform.position = targets[j];
#endif // UNITY_EDITOR
                }

                for (int k = 0; k < Space.planetesimals.Count; k++)
                {
                    Space.planetesimals[k].SetGravityForce(forceVectors);
                }
            }
        }

    }
}
