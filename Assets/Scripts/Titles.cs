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



public class Title
{
    Word[] words;

    Vector3 velocity = Vector3.zero;
    static List<Planetesimal> allPlanetesimalsUsed = new List<Planetesimal>();
    List<Planetesimal> planetesimalsUsed = new List<Planetesimal>();

    public Title(Word[] words)
    {
        this.words = new Word[words.Length];

        for (int i = 0; i < words.Length; i++)
        {
            this.words[i] = words[i];
        }
    }

    public Word this[int index]
    {
        get
        {
            return words[index];
        }
        set
        {
            words[index] = value;
        }
    }

    public int Length
    {
        get
        {
            return words.Length;
        }
    }

    public int ParticleCount
    {
        get
        {
            int particleCount = 0;

            for (int i = 0; i < words.Length; i++)
            {
                int particlesPerSlot = words[i].HorizontalParticlesPerSlot * words[i].VerticalParticlesPerSlot;

                for (int j = 0; j < words[i].Length; j++)
                {
                    particleCount += particlesPerSlot * words[i][j].OccupiedSlotsCount;
                }

            }

            return particleCount;
        }
    }

    public void FormTitle(float time, float particleDelay, bool randomSelection, bool sphericalLerp)
    {
        int planetesimalIndex = (Space.planetesimals.Count - ParticleCount) / 2;
        int currentParticleCount = 0;

        Vector3 xVector = new Vector3(1, 0, 0);
        Vector3 yVector = new Vector3(0, 1, 0);

        // word
        for (int i = 0; i < Length; i++)
        {
            Vector3 wordLocation = this[i].Location;
            float letterSize = (this[i].SlotPadding + Mathf.Max(0, this[i].ParticlePadding - 1)) * (this[i].HorizontalParticleSlotsPerLetter + 1);

            // letter
            for (int j = 0; j < this[i].Length; j++)
            {
                Vector3 letterLocation = (j * letterSize) * xVector;

                // slot y
                for (int k = 0; k < this[i][j].Slots.GetLength(0); k++)
                {
                    Vector3 slotLocationY = k * (this[i].SlotPadding + Mathf.Max(0, this[i].ParticlePadding - 1)) * yVector;

                    // slot x
                    for (int l = 0; l < this[i][j].Slots.GetLength(1); l++)
                    {
                        Vector3 slotLocationX = l * (this[i].SlotPadding + Mathf.Max(0, this[i].ParticlePadding - 1)) * xVector;
                        
                        if (this[i][j].Slots[k, l])
                        {
                            Vector3 slotLocation = wordLocation + letterLocation + slotLocationX - slotLocationY;

                            for (int m = 0; m < this[i].VerticalParticlesPerSlot; m++)
                            {
                                for (int n = 0; n < this[i].HorizontalParticlesPerSlot; n++)
                                {
                                    Vector3 target = slotLocation + new Vector3(m * this[i].ParticlePadding, n * this[i].ParticlePadding, 0);

                                    if(randomSelection)
                                    {
                                        do
                                        {
                                            planetesimalIndex = UnityEngine.Random.Range(0, Space.planetesimals.Count - 1);
                                        }
                                        while (Space.planetesimals[planetesimalIndex].InUse);
                                    }

                                    Space.planetesimals[planetesimalIndex].MoveTo(target, time, currentParticleCount * particleDelay, sphericalLerp);
                                    allPlanetesimalsUsed.Add(Space.planetesimals[planetesimalIndex]);
                                    planetesimalsUsed.Add(Space.planetesimals[planetesimalIndex]);
                                    currentParticleCount++;

                                    if (!randomSelection)
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
    }

    public void SpreadTitle(float range, float time, float delay, bool randomSelection, bool sphericalLerp)
    {
        int firstPlanetesimalIndex = (Space.planetesimals.Count - ParticleCount) / 2;

        for (int i = firstPlanetesimalIndex; i < Space.planetesimals.Count - firstPlanetesimalIndex; i++)
        {
            Planetesimal planetesimal;

            if (randomSelection)
            {
                planetesimal = allPlanetesimalsUsed[i - firstPlanetesimalIndex];
            }
            else
            {
                planetesimal = Space.planetesimals[i];
            }
            planetesimal.SpreadAround(range, time, (i - firstPlanetesimalIndex) * delay, sphericalLerp);
        }
    }
}



public class Word
{
    public Vector3 Location { get; private set; } // position of the word in 3d space
    public int VerticalParticleSlotsPerLetter { get; private set; } // letter height and width in particle slots
    public int HorizontalParticleSlotsPerLetter { get; private set; }
    public int HorizontalParticlesPerSlot { get; private set; }
    public int VerticalParticlesPerSlot { get; private set; }
    public float SlotPadding { get; private set; } // space between each particle slot
    public float ParticlePadding { get; private set; } // space between each particle in a single slot
    public string Code { get; private set; } // the letters written in empty and non-empty characters.
                                             // code should fit the parameters for particle slots per letter.
    Letter[] letters;

    public Word(Vector3 location,
        int verticalParticleSlotsPerLetter, int horizontalParticleSlotsPerLetter,
        int horizontalParticlesPerSlot, int verticalParticlesPerSlot,
        int slotPadding, float particlePadding,
        string code)
    {
        Location = location;
        VerticalParticleSlotsPerLetter = verticalParticleSlotsPerLetter;
        HorizontalParticleSlotsPerLetter = horizontalParticleSlotsPerLetter;
        HorizontalParticlesPerSlot = horizontalParticlesPerSlot;
        VerticalParticlesPerSlot = verticalParticlesPerSlot;
        SlotPadding = slotPadding;
        ParticlePadding = particlePadding;
        Code = code;

        letters = new Letter[Code.Length];

        for (int i = 0; i < letters.Length; i++)
        {
            letters[i] = new Letter(Code[i]);
        }
    }

    public Letter this[int index]
    {
        get
        {
            return letters[index];
        }
        set
        {
            letters[index] = value;
        }
    }

    public int Length
    {
        get
        {
            return letters.Length;
        }
    }
}



public class Letter
{
    public bool[,] Slots { get; private set; }

    int occupiedSlotsCount;

    public Letter(char letter)
    {
        Slots = new bool[alphabet[letter][0].Length, alphabet[letter].Length];

        occupiedSlotsCount = 0;

        for (int i = 0; i < alphabet[letter].Length; i++)
        {
            for (int j = 0; j < alphabet[letter][i].Length; j++)
            {
                if (Char.IsWhiteSpace(alphabet[letter][i][j]))
                {
                    Slots[i, j] = false;
                }
                else
                {
                    Slots[i, j] = true;
                    occupiedSlotsCount++;
                }
            }
        }
    }



    public int OccupiedSlotsCount
    {
        get
        {
            return occupiedSlotsCount;
        }
    }



    Dictionary<char, string[]> alphabet = new Dictionary<char, string[]>
    {
        { 'A', new string[] {
            "  0  ",
            " 0 0 ",
            " 000 ",
            "0   0",
            "0   0"} },
        { 'B', new string[] {
            "0000 ",
            "0   0",
            "0000 ",
            "0   0",
            "0000 "} },
        { 'C', new string[] {
            " 0000",
            "0    ",
            "0    ",
            "0    ",
            " 0000"} },
        { 'D', new string[] {
            "0000 ",
            "0   0",
            "0   0",
            "0   0",
            "0000 "} },
        { 'E', new string[] {
            "00000",
            "0    ",
            "0000 ",
            "0    ",
            "00000"} },
        { 'F', new string[] {
            "00000",
            "0    ",
            "0000 ",
            "0    ",
            "0    "} },
        { 'G', new string[] {
            "00000",
            "0    ",
            "0  00",
            "0   0",
            "00000"} },
        { 'H', new string[] {
            "0   0",
            "0   0",
            "00000",
            "0   0",
            "0   0"} },
        { 'I', new string[] {
            "00000",
            "  0  ",
            "  0  ",
            "  0  ",
            "00000"} },
        { 'J', new string[] {
            "00000",
            "  0  ",
            "  0  ",
            "  0  ",
            "00   "} },
        { 'K', new string[] {
            "0   0",
            "0  0 ",
            "000  ",
            "0  0 ",
            "0   0"} },
        { 'L', new string[] {
            "0    ",
            "0    ",
            "0    ",
            "0    ",
            "00000"} },
        { 'M', new string[] {
            "0   0",
            "00 00",
            "0 0 0",
            "0   0",
            "0   0"} },
        { 'N', new string[] {
            "0   0",
            "00  0",
            "0 0 0",
            "0  00",
            "0   0"} },
        { 'O', new string[] {
            " 000 ",
            "0   0",
            "0   0",
            "0   0",
            " 000 "} },
        { 'P', new string[] {
            "0000 ",
            "0   0",
            "0000 ",
            "0    ",
            "0    "} },
        { 'Q', new string[] {
            " 000 ",
            "0   0",
            "0 0 0",
            "0  00",
            " 0000"} },
        { 'R', new string[] {
            "0000 ",
            "0   0",
            "0000 ",
            "0  0 ",
            "0   0"} },
        { 'S', new string[] {
            " 0000",
            "0    ",
            " 000 ",
            "    0",
            "0000 "} },
        { 'T', new string[] {
            "00000",
            "  0  ",
            "  0  ",
            "  0  ",
            "  0  "} },
        { 'U', new string[] {
            "0   0",
            "0   0",
            "0   0",
            "0   0",
            " 000 "} },
        { 'V', new string[] {
            "0   0",
            "0   0",
            " 0 0 ",
            " 0 0 ",
            "  0  "} },
        { 'W', new string[] {
            "0   0",
            "0   0",
            "0 0 0",
            "00 00",
            "0   0"}  },
        { 'X', new string[] {
            "0   0",
            " 0 0 ",
            "  0  ",
            " 0 0 ",
            "0   0"} },
        { 'Y', new string[] {
            "0   0",
            " 0 0 ",
            "  0  ",
            "  0  ",
            "  0  "} },
        { 'Z', new string[] {
            "00000",
            "   0 ",
            "  0  ",
            " 0   ",
            "00000"}  },
         { ' ', new string[] {
            "     ",
            "     ",
            "     ",
            "     ",
            "     "}  }
    };
}
