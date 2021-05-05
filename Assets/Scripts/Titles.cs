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
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Title
{
    private readonly Word[] words;
    private readonly List<Planetesimal> planetesimalsUsed = new List<Planetesimal>();

    private readonly int particleCount;

    public Title(IEnumerable<Word> words)
    {
        this.words = words.ToArray();
        particleCount = GetParticleCount();
    }

    private int GetParticleCount()
    {
        return words.Sum(word => word.ParticleCount);
    }

    public void FormTitle(float time, float particleDelay, bool isRandomSelection,
        bool isSphericalLerp)
    {
        List<Planetesimal> planetesimals = Space.Planetesimals;
        int iPlanetesimal = (planetesimals.Count - particleCount) / 2;
        int currentParticleCount = 0;

        foreach (Word word in words)
        {
            // TODO: Check if Mathf.Max(0, word.ParticlePadding - 1) should be multiplied by HorizontalParticlesPerSlot.
            float totalSlotPadding = word.SlotPadding + Mathf.Max(0, word.ParticlePadding - 1);
            float characterSizeX = (word.HorizontalParticleSlotsPerCharacter + 1) * totalSlotPadding;
            float wordParticlePadding = word.ParticlePadding;
            Vector3 wordPosition = word.Position;
            float particlePositionZ = wordPosition.z;
            
            for (int characterIndex = 0; characterIndex < word.Characters.Length; characterIndex++)
            {
                Character character = word.Characters[characterIndex];
                float characterShiftX = characterIndex * characterSizeX;

                for (int slotRowIndex = 0; slotRowIndex < character.Slots.GetLength(0);
                    slotRowIndex++)
                {
                    float slotShiftY = -1 * slotRowIndex * totalSlotPadding;
                    float particlePositionY = wordPosition.y + slotShiftY;

                    for (int slotColIndex = 0; slotColIndex < character.Slots.GetLength(1);
                        slotColIndex++)
                    {
                        if (!character.Slots[slotRowIndex, slotColIndex])
                        {
                            continue;
                        }

                        float slotShiftX = slotColIndex * totalSlotPadding;
                        float particlePositionX = wordPosition.x + characterShiftX + slotShiftX;

                        for (int particleRowIndex = 0;
                            particleRowIndex < word.VerticalParticlesPerSlot; particleRowIndex++)
                        {
                            for (int particleColumnIndex = 0;
                                particleColumnIndex < word.HorizontalParticlesPerSlot;
                                particleColumnIndex++)
                            {
                                // TODO: Check if particleRowIndex and particleColumnIndex should change places in vector calculation.
                                var target = new Vector3(
                                    particlePositionX + particleRowIndex * wordParticlePadding,
                                    particlePositionY + particleColumnIndex * wordParticlePadding,
                                    particlePositionZ);

                                if (isRandomSelection)
                                {
                                    do
                                    {
                                        iPlanetesimal = UnityEngine.Random.Range(0,
                                            planetesimals.Count - 1);
                                    }
                                    while (planetesimals[iPlanetesimal].IsAllocated);
                                }

                                Planetesimal planetesimal = planetesimals[iPlanetesimal];
                                planetesimal.MoveTo(target, time, isSphericalLerp, 
                                    currentParticleCount * particleDelay);
                                planetesimalsUsed.Add(planetesimal);
                                currentParticleCount++;

                                if (!isRandomSelection)
                                {
                                    iPlanetesimal++;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void SpreadTitle(float range, float time, bool isSphericalLerp, float delay)
    {
        for (int i = 0; i < planetesimalsUsed.Count; i++)
        {
            planetesimalsUsed[i].SpreadAround(range, time, isSphericalLerp, i * delay);
        }
    }
}