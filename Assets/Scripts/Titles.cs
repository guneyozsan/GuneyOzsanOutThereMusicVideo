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

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        int planetesimalIndex = (planetesimals.Count - particleCount) / 2;
        int currentParticleCount = 0;

        foreach (Word word in words)
        {
            // TODO: Check if Mathf.Max(0, word.ParticlePadding - 1) should be multiplied by HorizontalParticlesPerGridCell.
            float totalGridCellPadding = word.PaddingBetweenGridCells + Mathf.Max(0, word.PaddingBetweenParticlesInCells - 1);
            float characterSizeX = (word.CharacterGridSizeWidth + 1) * totalGridCellPadding;
            float paddingBetweenParticlesInCells = word.PaddingBetweenParticlesInCells;
            Vector3 wordPosition = word.Position;
            float particlePositionZ = wordPosition.z;
            
            for (int characterIndex = 0; characterIndex < word.Characters.Length; characterIndex++)
            {
                Character character = word.Characters[characterIndex];
                float characterShiftX = characterIndex * characterSizeX;

                for (int gridRow = 0; gridRow < character.BinaryGrid.RowCount; gridRow++)
                {
                    float gridOffsetY = -1 * gridRow * totalGridCellPadding;
                    float particlePositionY = wordPosition.y + gridOffsetY;

                    for (int gridCol = 0; gridCol < character.BinaryGrid.ColCount; gridCol++)
                    {
                        if (!character.BinaryGrid.GetCellState(gridRow, gridCol))
                        {
                            continue;
                        }

                        float gridOffsetX = gridCol * totalGridCellPadding;
                        float particlePositionX = wordPosition.x + characterShiftX + gridOffsetX;

                        for (int cellRow = 0; cellRow < word.VerticalParticleCountPerGridCell;
                            cellRow++)
                        {
                            for (int cellCol = 0; cellCol < word.HorizontalParticleCountPerGridCell;
                                cellCol++)
                            {
                                // TODO: Check if cellRow and cellCol should change places in vector calculation.
                                var target = new Vector3(
                                    particlePositionX + cellRow * paddingBetweenParticlesInCells,
                                    particlePositionY + cellCol * paddingBetweenParticlesInCells,
                                    particlePositionZ);

                                if (isRandomSelection)
                                {
                                    do
                                    {
                                        planetesimalIndex = UnityEngine.Random.Range(0,
                                            planetesimals.Count - 1);
                                    }
                                    while (planetesimals[planetesimalIndex].IsAllocated);
                                }

                                Planetesimal planetesimal = planetesimals[planetesimalIndex];
                                planetesimal.MoveTo(target, time, isSphericalLerp, 
                                    currentParticleCount * particleDelay);
                                planetesimalsUsed.Add(planetesimal);
                                currentParticleCount++;

                                if (!isRandomSelection)
                                {
                                    planetesimalIndex++;
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