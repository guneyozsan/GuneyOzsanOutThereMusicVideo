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

public class Titles : MonoBehaviour
{
    [SerializeField]
    Transform planetesimalPrefab;
    Transform planetesimal;



    class Title
    {
        private Word[] words;

        public Title(int wordCount)
        {
            words = new Word[wordCount];
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
    }



    class Word
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



    class Letter
    {
        public bool[,] Slots { get; private set; }

        int occupiedSlotsCount;

        public Letter (char letter)
        {
            Slots = new bool[letters[letter][0].Length, letters[letter].Length];

            occupiedSlotsCount = 0;

            for (int i = 0; i < letters[letter].Length; i++)
            {
                for (int j = 0; j < letters[letter][i].Length; j++)
                {
                    if (Char.IsWhiteSpace(letters[letter][i][j]))
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



        Dictionary<char, string[]> letters = new Dictionary<char, string[]>
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
                "00000"}  }
        };
    }



    void Start () {
        Title openingTitles = SetOpeningTitles();

        Transform gravityTarget = GetComponent<PlayAnimation>().sun;

        Transform planetesimalParent = new GameObject("Planetesimals").transform;

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

                    planetesimal = Instantiate(planetesimalPrefab, new Vector3(x, y, z), Quaternion.identity, planetesimalParent);
                    planetesimal.GetComponent<Gravity>().SetTarget(Vector3.zero);
                    Space.planetesimals.Add(planetesimal);
                }
            }
        }
    }
    


    Title SetOpeningTitles()
    {
        Title openingTitles = new Title(4);

        openingTitles[0] = new Word(new Vector3(-62f, 15f, 4.6f), 5, 5, 2, 2, 2, 0.96f, "GUNEY");
        openingTitles[1] = new Word(new Vector3(10f, 15f, 4.6f), 5, 5, 2, 2, 2, 0.96f, "OZSAN");
        openingTitles[2] = new Word(new Vector3(-50f, 0f, 4.6f), 5, 5, 2, 2, 2, 0.96f, "OUT");
        openingTitles[3] = new Word(new Vector3(-2f, 0f, 4.6f), 5, 5, 2, 2, 2, 0.96f, "THERE");

        return openingTitles;
    }
	
}
