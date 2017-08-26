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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningTitles : MonoBehaviour
{
    [SerializeField]
    Transform planetesimalPrefab;
    Transform planetesimal;

    Word[] openingTitles;

    class Word
    {
	    public Vector3 location; // position of the word in 3d space
        public int verticalParticleSlotsPerLetter; // letter height and width in particle slots
        public int horizontalParticleSlotsPerLetter;
        public int horizontalParticlesPerSlot;
        public int verticalParticlesPerSlot;
        public float slotPadding; // space between each particle slot
        public float particlePadding; // space between each particle in a single slot
        public string code; // the letters written in empty and non-empty characters.
                            // code should fit the parameters for particle slots per letter.
    }


    void Start () {
        openingTitles = InitializeOpeningTitles();
        
        Transform gravityTarget = GetComponent<PlayAnimation>().sun;

        Transform planetesimalParent = new GameObject("Planetesimals").transform;

        for (int i = 0; i < openingTitles.Length; i++) {
            float rowLength = openingTitles[i].code.Length / openingTitles[i].verticalParticleSlotsPerLetter;
            float slotPadding = openingTitles[i].slotPadding;
            int horizontalParticleSlotsPerLetter = openingTitles[i].horizontalParticleSlotsPerLetter;

            for (int j = 0; j < openingTitles[i].code.Length; j++) { 
                if (openingTitles[i].code[j].ToString() != " ")
                {
                    float currentRow = Mathf.FloorToInt(j / rowLength);
                    float y = -1 * slotPadding * currentRow;

                    // Puts space between letters
                    float letterPadding = slotPadding * (j / horizontalParticleSlotsPerLetter);

                    // Because the word is coded as a single string, this offsets each line of particle slots back to paragraph indent. 
                    float offsetLineToParagraphIndent = -1 * currentRow * slotPadding * (rowLength + rowLength / horizontalParticleSlotsPerLetter);

                    float x = slotPadding * j + letterPadding + offsetLineToParagraphIndent;

                    for (int k = 0; k < openingTitles[i].horizontalParticlesPerSlot; k++)
                    {
                        for (int l = 0; l < openingTitles[i].verticalParticlesPerSlot; l++)
                        {
                            planetesimal = Instantiate(planetesimalPrefab, openingTitles[i].location + new Vector3(x + k * openingTitles[i].particlePadding, y - l * openingTitles[i].particlePadding, 0), Quaternion.identity, planetesimalParent);
                            planetesimal.GetComponent<Gravity>().SetTarget(gravityTarget);
                            planetesimal.tag = "Planet";
                        }
                    }
                }
            }
        }
    }



    Word[] InitializeOpeningTitles()
    {
        Word[] openingTitles = new Word[4];

        openingTitles[0] = new Word()
        {
            location = new Vector3(-62f, 15f, 4.6f),
            horizontalParticleSlotsPerLetter = 5,
            verticalParticleSlotsPerLetter = 5,
            horizontalParticlesPerSlot = 2,
            verticalParticlesPerSlot = 2,
            slotPadding = 2,
            particlePadding = 0.96f,
            code =  "88888" + "8   8" + "8   8" + "88888" + "8   8" +
                    "8    " + "8   8" + "88  8" + "8    " + "8   8" +
                    "8  88" + "8   8" + "8 8 8" + "8888 " + " 8 8 " +
                    "8   8" + "8   8" + "8  88" + "8    " + "  8  " +
                    "88888" + "88888" + "8   8" + "88888" + "  8  "
        };

        openingTitles[1] = new Word()
        {
            location = new Vector3(10f, 15f, 4.6f),
            horizontalParticleSlotsPerLetter = 5,
            verticalParticleSlotsPerLetter = 5,
            horizontalParticlesPerSlot = 2,
            verticalParticlesPerSlot = 2,
            slotPadding = 2,
            particlePadding = 0.96f,
            code =  "88888" + "88888" + " 8888" + "  8  " + "8   8" +
                    "8   8" + "   8 " + "8    " + " 8 8 " + "88  8" +
                    "8   8" + "  8  " + " 888 " + " 888 " + "8 8 8" +
                    "8   8" + " 8   " + "    8" + "8   8" + "8  88" +
                    "88888" + "88888" + "8888 " + "8   8" + "8   8"
        };

        openingTitles[2] = new Word()
        {
            location = new Vector3(-50f, 0f, 4.6f),
            horizontalParticleSlotsPerLetter = 5,
            verticalParticleSlotsPerLetter = 5,
            horizontalParticlesPerSlot = 2,
            verticalParticlesPerSlot = 2,
            slotPadding = 2,
            particlePadding = 0.96f,
            code =  "88888" + "8   8" + "88888" +
                    "8   8" + "8   8" + "  8  " +
                    "8   8" + "8   8" + "  8  " +
                    "8   8" + "8   8" + "  8  " +
                    "88888" + "88888" + "  8  "
        };

        openingTitles[3] = new Word()
        {
            location = new Vector3(-2f, 0f, 4.6f),
            horizontalParticleSlotsPerLetter = 5,
            verticalParticleSlotsPerLetter = 5,
            horizontalParticlesPerSlot = 2,
            verticalParticlesPerSlot = 2,
            slotPadding = 2,
            particlePadding = 0.96f,
            code =  "88888" + "8   8" + "88888" + "8888 " + "88888" +
                    "  8  " + "8   8" + "8    " + "8   8" + "8    " +
                    "  8  " + "88888" + "8888 " + "8888 " + "8888 " +
                    "  8  " + "8   8" + "8    " + "8 8  " + "8    " +
                    "  8  " + "8   8" + "88888" + "8  8 " + "88888"
        };

        return openingTitles;
    }
	
}
