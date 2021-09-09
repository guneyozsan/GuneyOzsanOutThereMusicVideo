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

    public void FormTitle(float formationDuration, float particleDelay, bool isRandomSelection,
        bool isSphericalLerp)
    {
        List<Planetesimal> planetesimals = Space.Planetesimals;
        int planetesimalIndex = (planetesimals.Count - particleCount) / 2;
        int currentParticleCount = 0;

        foreach (Word word in words)
        {
            CharacterArgs characterArgs = word.CharacterArgs;
            
            Vector2 gridCellSize =
                Vector2.Scale(characterArgs.SubGridSize, characterArgs.SubGridCellSize)
                + characterArgs.SpaceBetweenGridCells;
            Debug.Assert(gridCellSize == new Vector2(2.3f, 2.3f));
            
            float kerning = 1f * gridCellSize.x;
            Vector3 wordPosition = word.Position;
            
            for (int characterIndex = 0; characterIndex < word.Characters.Length; characterIndex++)
            {
                Character character = word.Characters[characterIndex];
                float characterSizeX = character.Grid.ColCount * gridCellSize.x;
                float characterOffsetX = characterIndex * (characterSizeX + kerning);

                for (int gridRow = 0; gridRow < character.Grid.RowCount; gridRow++)
                {
                    float gridOffsetY = -gridRow * gridCellSize.y;
                    float gridPositionY = wordPosition.y + gridOffsetY;

                    for (int gridCol = 0; gridCol < character.Grid.ColCount; gridCol++)
                    {
                        if (!character.Grid.GetCellState(gridRow, gridCol))
                        {
                            continue;
                        }

                        float gridOffsetX = gridCol * gridCellSize.x;
                        float gridPositionX = wordPosition.x + characterOffsetX + gridOffsetX;

                        for (int subGridRow = 0; subGridRow < characterArgs.SubGridSize.y; subGridRow++)
                        {
                            for (int subGridCol = 0; subGridCol < characterArgs.SubGridSize.x; subGridCol++)
                            {
                                var particlePosition = new Vector3(
                                    gridPositionX + subGridCol * characterArgs.SubGridCellSize.x,
                                    gridPositionY + subGridRow * characterArgs.SubGridCellSize.y,
                                    wordPosition.z);

                                if (isRandomSelection)
                                {
                                    do
                                    {
                                        planetesimalIndex = Random.Range(
                                            0, planetesimals.Count - 1);
                                    }
                                    while (planetesimals[planetesimalIndex].IsAllocated);
                                }

                                Planetesimal planetesimal = planetesimals[planetesimalIndex];
                                planetesimal.MoveTo(particlePosition, formationDuration, isSphericalLerp, 
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