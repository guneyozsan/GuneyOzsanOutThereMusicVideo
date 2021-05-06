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
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Planetesimal planetesimalPrefab;
    [SerializeField] private Camera mainCamera;

    private Title openingTitlesMusic;
    private Title openingTitlesBy;
    private Title openingTitlesComposer;
    private Title partOneTitlesPartNumber;
    private Title partOneTitlesPartName;
    private Title partTwoTitlesPartNumber;
    private Title partTwoTitlesPartName;

    private int currentBar;
    private int currentBeat;

    private TwinGalaxyAnimation twinGalaxyAnimation;

#if UNITY_EDITOR
    private static List<GameObject> pilotObjects = new List<GameObject>();
    private float twinGalaxyTime;
#endif

    private void Awake()
    {
        InitializeTitles();
        InstantiatePlanetesimals(planetesimalPrefab, 11 * Vector3.one,
            Vector3.one);

        twinGalaxyAnimation = new TwinGalaxyAnimation(
            55f, 0f, 2.6f,
            0.0029f, 0.0095f, 14f,
            0.6f, 0.37f, new Vector3(0f, 0f, 50f),
            0f, -220f, 0.40f);
        twinGalaxyTime = 0f;
    }

    private void Update()
    {
        int currentSequencerBar = Sequencer.CurrentBar;
        int currentSequencerBeat = Sequencer.CurrentBeat;
        
        UpdateTitleAnimations(currentSequencerBar);
        UpdateGravityAnimations(currentSequencerBar, currentSequencerBeat);

        currentBar = currentSequencerBar;
        currentBeat = currentSequencerBeat;
    }

    private static void SetForces(float charge, List<Vector3> centers)
    {
#if UNITY_EDITOR
        if (pilotObjects.Count > centers.Count)
        {
            for (int i = centers.Count; i < pilotObjects.Count; i++)
            {
                Destroy(pilotObjects[i]);
            }
            
            pilotObjects = pilotObjects.GetRange(0, centers.Count);
        }
#endif

        var forces = new Force[centers.Count];

        for (int i = 0; i < centers.Count; i++)
        {
            forces[i] = new Force(charge, centers[i]);
#if UNITY_EDITOR
            if (i > pilotObjects.Count - 1)
            {
                GameObject pilot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Collider pilotCollider = pilot.GetComponent<Collider>();
                
                if (pilotCollider != null)
                {
                    Destroy(pilotCollider);
                }
                
                pilotObjects.Add(pilot);
            }
            
            pilotObjects[i].transform.position = centers[i];
#endif
        }

        SetForces(forces);
    }

    private static void SetForces(Force[] forces)
    {
        foreach (Planetesimal planetesimal in Space.Planetesimals)
        {
            planetesimal.SetForces(forces);
        }
    }

    private static void SetForces(float charge, Vector3 center)
    {
        var centers = new List<Vector3> { center };
        SetForces(charge, centers);
    }

    private static void SetForcePerBar(IList<float> charges, Vector3 center, int periodBars,
        int initialBar)
    {
        var centers = new List<Vector3> { center };
        SetForcePerBar(charges, centers, periodBars, initialBar);
    }

    private static void SetForcePerBar(IList<float> charges, List<Vector3> centers,
        int periodBars, int initialBar)
    {
        int forceCount = charges.Count;
        float currentBarInSlice = (float) (Sequencer.CurrentBar - initialBar) / periodBars;
        
        for (int i = 0; i < forceCount; i++)
        {
            if (!Mathf.Approximately(i, currentBarInSlice % forceCount))
            {
                continue;
            }
            
            SetForces(charges[i], centers);
            break;
        }
    }

    private static void InstantiatePlanetesimals(Planetesimal planetesimalPrefab,
        Vector3 cellCountPerDimension, Vector3 planetesimalCellSize)
    {
        Transform planetesimalParent = new GameObject("Planetesimals").transform;

        for (int iX = 0; iX < cellCountPerDimension.x; iX++)
        {
            float x = - cellCountPerDimension.x / 2f + iX * planetesimalCellSize.x;

            for (int iY = 0; iY < cellCountPerDimension.y; iY++)
            {
                float y = - cellCountPerDimension.y / 2f + iY * planetesimalCellSize.y;

                for (int iZ = 0; iZ < cellCountPerDimension.z; iZ++)
                {
                    float z = - cellCountPerDimension.z / 2f + iZ * planetesimalCellSize.z;
                    
                    Planetesimal planetesimal = Instantiate(planetesimalPrefab,
                        new Vector3(x, y, z), Quaternion.identity, planetesimalParent);
                    Space.Planetesimals.Add(planetesimal);
                }
            }
        }
    }

    private void InitializeTitles()
    {
        var characterArgs = new CharacterArgs(new Vector2Int(2, 2),
            new Vector2(1f, 1f), new Vector2(0.3f, 0.3f));

        openingTitlesMusic = new Title(new Word[] {
            new Word(
                "OUT", 
                new Vector3(-19.1f, 15f, 0f),
                characterArgs),
            new Word(
                "THERE",
                new Vector3(-32.85f, -5.2f, 0f),
                characterArgs)
        });
        openingTitlesBy = new Title(new Word[] {
            new Word(
                "BY",
                new Vector3(-11f, 7f, -9.4f),
                characterArgs)
        });
        openingTitlesComposer = new Title(new Word[] {
            new Word(
                "GUNEY",
                new Vector3(-66f, 7f, -9.4f),
                characterArgs),
            new Word(
                "OZSAN",
                new Vector3(8f, 7f, -9.4f),
                characterArgs)
        });
        partOneTitlesPartNumber = new Title(new Word[] {
            new Word(
                "PART I",
                new Vector3(-39.5f, 18f, -9.4f),
                characterArgs)
        });
        partOneTitlesPartName = new Title(new Word[] {
            new Word(
                "APPROACH", 
                new Vector3(-53.45f, -11.3f, -9.4f),
                characterArgs)
        });
        partTwoTitlesPartNumber = new Title(new Word[] {
            new Word(
                "PART II",
                new Vector3(-46.65f, 18f, -9.4f),
                characterArgs)
        });
        partTwoTitlesPartName = new Title(new Word[] {
            new Word(
                "PROBE",
                new Vector3(-32.85f, -11.3f, -9.4f),
                characterArgs)
        });
    }

    private void UpdateTitleAnimations(int currentSequencerBar)
    {
        switch (currentSequencerBar)
        {
            case 4:
                if (currentBar != currentSequencerBar)
                {
                    openingTitlesMusic.FormTitle(12f * Sequencer.BarDuration, 
                        0.005f, true, false);
                    SetForces(0f, Vector3.zero);
                }
                break;

            case 16:
                if (currentBar != currentSequencerBar)
                {
                    openingTitlesMusic.SpreadTitle(10f, 0.8f * Sequencer.BarDuration,
                        false, 0.001f);
                    partOneTitlesPartNumber.FormTitle(14.25f * Sequencer.BarDuration,
                        0.001f, true, true);
                    partOneTitlesPartName.FormTitle(14.25f * Sequencer.BarDuration,
                        0.001f, true, true);
                }
                break;

            case 32:
                if (currentBar != currentSequencerBar)
                {
                    partOneTitlesPartNumber.SpreadTitle(10f, 1f * Sequencer.BarDuration,
                        false, 0.05f);
                }
                break;

            case 36:
                if (currentBar != currentSequencerBar)
                {
                    partOneTitlesPartName.SpreadTitle(10f, 1f * Sequencer.BarDuration,
                        false, 0.05f);
                }
                break;
                
            case 57:
                if (currentBar != currentSequencerBar)
                {
                    partTwoTitlesPartNumber.FormTitle(2f * Sequencer.BarDuration,
                        0.007f, true, true);
                    partTwoTitlesPartName.FormTitle(2f * Sequencer.BarDuration,
                        0.014f, true, true);
                }
                break;

            case 61:
                if (currentBar != currentSequencerBar)
                {
                    partTwoTitlesPartNumber.SpreadTitle(10f, .5f * Sequencer.BarDuration,
                        false, 0.0025f);
                    partTwoTitlesPartName.SpreadTitle(10f, .5f * Sequencer.BarDuration,
                        false, 0.0025f);
                }
                break;

            case 100:
                if (currentBar != currentSequencerBar)
                {
                    Planetesimal cameraPlanetesimal = Instantiate(planetesimalPrefab,
                        mainCamera.transform.position, Quaternion.identity);
                    Space.Planetesimals.Add(cameraPlanetesimal);
                    mainCamera.transform.SetParent(cameraPlanetesimal.transform);
                }
                break;
        }
    }

    private void UpdateGravityAnimations(int currentSequencerBar, int currentSequencerBeat)
    {
        const int firstBarOfSequence = 18;
        const int firstBarOfTwinGalaxy = 32;
        const int lastBarOfTwinGalaxy = 57;
        bool firstTimeInBarAndBeat = currentBar != Sequencer.CurrentBar 
                                     || currentBeat != Sequencer.CurrentBeat;

        if (currentSequencerBar >= firstBarOfSequence
            && currentSequencerBar < firstBarOfTwinGalaxy
            && currentSequencerBeat == 1)
        {
            SetForcePerBar(new[] { -65f, 0f }, new Vector3(0f, 0f, -17f),
                2, firstBarOfSequence);
        }
        else if (currentSequencerBar >= firstBarOfTwinGalaxy
                 && currentSequencerBar < lastBarOfTwinGalaxy + 1)
        {
            float animationDuration = Sequencer.BarDuration
                                      * (lastBarOfTwinGalaxy - firstBarOfTwinGalaxy + 1);
            SetForces(twinGalaxyAnimation.GetForces(twinGalaxyTime, animationDuration));
            twinGalaxyTime += Time.deltaTime;
        }
        else if (currentSequencerBar >= lastBarOfTwinGalaxy && currentSequencerBar < 60)
        {
            if (firstTimeInBarAndBeat)
            {
                SetForces(-30f, Vector3.zero);
            }
        }
        else if (currentSequencerBar >= 60 && currentSequencerBeat == 1)
        {
            if (firstTimeInBarAndBeat)
            {
                SetForcePerBar(new[] { 0f, -300f }, Vector3.zero, 1, 60);
            }
        }
    }
}
