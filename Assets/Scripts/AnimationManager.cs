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
    Camera camera;

    public static Title openingTitlesMusic;
    public static Title openingTitlesBy;
    public static Title openingTitlesComposer;
    public static Title partOneTitlesPartNumber;
    public static Title partOneTitlesPartName;
    public static Title partTwoTitlesPartNumber;
    public static Title partTwoTitlesPartName;

    float alignY = 0;
    int currentAnimationBar = 0;
    int currentAnimationBeat = 0;
    Gravity gravity;
    Vector3 defaultCameraLocation;

    Vector3 target = Vector3.zero;
    List<Vector3> targets = new List<Vector3>() {
        Vector3.zero
    };

    float rad = 0;
    float r = 0;
    float t = 0;

#if UNITY_EDITOR
    List<GameObject> pilotObjects = new List<GameObject>();
#endif // UNITY_EDITOR

    void Start()
    {
        defaultCameraLocation = camera.transform.position;

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
                    openingTitlesMusic.SpreadTitle(10, 0.8f * Sequencer.BarDuration, 0.002f, true, false);
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
                    Transform cameraPlanetesimal = Instantiate(planetesimalPrefab, camera.transform.position, Quaternion.identity);
                    Space.planetesimals.Add(cameraPlanetesimal.GetComponent<Planetesimal>());
                    camera.transform.SetParent(cameraPlanetesimal);
                }
                break;
        }

        int firstBarOfTwinGalaxy = 30;
        int lastBarOfTwinGalaxy = 55;

        if ((Sequencer.CurrentBar >= firstBarOfTwinGalaxy) && (Sequencer.CurrentBar < (lastBarOfTwinGalaxy + 1)))
        {
            float sequenceLength = Sequencer.BarDuration * (lastBarOfTwinGalaxy + 1 - firstBarOfTwinGalaxy);
            int k = 1 + Sequencer.CurrentBar - firstBarOfTwinGalaxy;
            float speedPower = 1.03f;
            float speed = 0.0016f;// * (Mathf.Pow(sequenceLength - k, speedPower) / Mathf.Pow(1f, speedPower));

            float initialR = 20f;
            float maxR = 35f;
            float rPower = 0.5f;
            float r = initialR + (maxR - initialR) * (Mathf.Pow(t, rPower) / Mathf.Pow(sequenceLength, rPower));

            float zOffset = -9.4f;
            targets = new List<Vector3>() {
                new Vector3( r * Mathf.Sin(rad), -0.5f * r * Mathf.Cos(rad), -r * Mathf.Cos(rad) + zOffset),
                new Vector3(-r * Mathf.Sin(rad),  0.5f * r * Mathf.Cos(rad),  r * Mathf.Cos(rad) + zOffset)
            };
            rad = speed * 60f * t;

            float forcePower = 1f / 3f;
            float force = -1f * Mathf.Pow(t, forcePower) * 100f / Mathf.Pow(sequenceLength, forcePower);

            SwitchAnimation(new float[] { 1.8f * force, 0.9f * force }, targets);

            t += Time.deltaTime;
        }

        if ((currentAnimationBar != Sequencer.CurrentBar)
            || (currentAnimationBeat != Sequencer.CurrentBeat))
        {
            if ((Sequencer.CurrentBar >= 16) && (Sequencer.CurrentBar < firstBarOfTwinGalaxy)
                && (Sequencer.CurrentBeat == 1))
            {
                int k = Sequencer.CurrentBar - 16;
                SwitchAnimation(new float[] { -1f * (10f + 2.5f * k), k / 30f }, Vector3.zero);
            }
            else if ((Sequencer.CurrentBar >= 32) && (Sequencer.CurrentBar < 56)
                && (Sequencer.CurrentBeat >= 1))
            {
                //if (Sequencer.CurrentBar == 32
                //    && (Sequencer.CurrentBeat == 1))
                //{
                    
                //}
                //else if (Sequencer.CurrentBar == 34
                //    && Sequencer.CurrentBeat == 2)
                //{
                //    print(Sequencer.CurrentBar + ":" + Sequencer.CurrentBeat);
                //    target = new Vector3(38.7f, 19, -9.5f);
                //}
                //else if (Sequencer.CurrentBar >= 36 && Sequencer.CurrentBar < 37
                //    && (Sequencer.CurrentBeat == 1))
                //{
                //    print(Sequencer.CurrentBar + ":" + Sequencer.CurrentBeat);
                //    target = new Vector3(-53.5f, -20, -9.5f);
                //}
                //else if (Sequencer.CurrentBar == 40
                //    && (Sequencer.CurrentBeat == 1))
                //{
                //    print(Sequencer.CurrentBar + ":" + Sequencer.CurrentBeat);
                //    target = new Vector3(53.5f, -20, -9.5f);
                //}

                //SwitchAnimation(1, force, force, targets);
            }
            else if (Sequencer.CurrentBar >= 56 && Sequencer.CurrentBar < 60)
            {
                SetGravity(0, Vector3.zero);
            }
            else if (Sequencer.CurrentBar >= 60)
            {
                SwitchAnimation(new float[] { -300, 0 }, Vector3.zero);
            }

            currentAnimationBar = Sequencer.CurrentBar;
            currentAnimationBeat = Sequencer.CurrentBeat;
        }
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

    void SwitchAnimation(float[] gravityForce, Vector3 target)
    {
        List<Vector3> targets = new List<Vector3> { target };
        SwitchAnimation(gravityForce, targets);
    }

    void SwitchAnimation(float[] gravityForce, List<Vector3> targets)
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

        int planetesimalsPerTarget = Space.planetesimals.Count / targets.Count;
        float force;
        int lastPlanetesimalIndex;

        for (int i = 0; i < targets.Count; i++)
        {
            if ((i + 1) * planetesimalsPerTarget >= Space.planetesimals.Count)
                lastPlanetesimalIndex = Space.planetesimals.Count;
            else
                lastPlanetesimalIndex = (i + 1) * planetesimalsPerTarget;

            force = 0;

            for (int j = 0; j < gravityForce.Length; j++)
            {
                if (currentAnimationBar % gravityForce.Length == j)
                    force = gravityForce[j];
            }

            for (int j = (i * planetesimalsPerTarget); j < lastPlanetesimalIndex; j++)
            {
                Space.planetesimals[j].SetGravityForce(force, targets[i]);
            }

#if UNITY_EDITOR
            if (i > pilotObjects.Count - 1)
            {
                pilotObjects.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
                Destroy(pilotObjects[i].GetComponent<SphereCollider>());
            }
            pilotObjects[i].transform.position = targets[i];
#endif // UNITY_EDITOR
        }
    }
}
