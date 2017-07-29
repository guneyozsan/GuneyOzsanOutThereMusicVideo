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
    public Transform newPlanet;
    static Transform planet;

    class Title
    {
        public Word[] word; // titles are made of words
	    public int numberOfWords; // number of words in the title
    }

    class Word
    {
        public string code; // the letter written in 1's and 0's
	    public Vector3 position; // position of the word in 3d space
        public int letterHeight; // letter height and width in particle slots
        public int letterWidth;
        public float slotGapSize; // space between each particle slot
        public float particleSpacing; // space between each particle in a single slot
    }


    void Start () {
        // initialize the title
        Title title = new Title();
        title.numberOfWords = 4; // total number of words in the title
        title.word = new Word[title.numberOfWords]; // create word array of the title

        // initialize the word GUNEY
        title.word[0] = new Word();
        title.word[0].position = new Vector3(-62f, 15f, 4.6f); // Position of the word
        title.word[0].letterWidth = 5; // number of particle slots
        title.word[0].letterHeight = 5; // number of particle slots	
        title.word[0].slotGapSize = 2; // space between each particle slot
        title.word[0].particleSpacing = 0.96f; // space between each particle in a single slot
        title.word[0].code = "88888" + "8   8" + "8   8" + "88888" + "8   8" +
                                "8    " + "8   8" + "88  8" + "8    " + "8   8" +
                                "8  88" + "8   8" + "8 8 8" + "8888 " + " 8 8 " +
                                "8   8" + "8   8" + "8  88" + "8    " + "  8  " +
                                "88888" + "88888" + "8   8" + "88888" + "  8  ";

        // initialize the word GUNEY
        title.word[1] = new Word();
        title.word[1].position = new Vector3(10f, 15f, 4.6f); // Position of the word
        title.word[1].letterWidth = 5; // number of particle slots
        title.word[1].letterHeight = 5; // number of particle slots	
        title.word[1].slotGapSize = 2; // space between each particle slot
        title.word[1].particleSpacing = 0.96f; // space between each particle in a single slot
        title.word[1].code = "88888" + "88888" + " 8888" + "  8  " + "8   8" +
                                "8   8" + "   8 " + "8    " + " 8 8 " + "88  8" +
                                "8   8" + "  8  " + " 888 " + " 888 " + "8 8 8" +
                                "8   8" + " 8   " + "    8" + "8   8" + "8  88" +
                                "88888" + "88888" + "8888 " + "8   8" + "8   8";

        // initialize the word GUNEY
        title.word[2] = new Word();
        title.word[2].position = new Vector3(-50f, 0f, 4.6f); // Position of the word
        title.word[2].letterWidth = 5; // number of particle slots
        title.word[2].letterHeight = 5; // number of particle slots	
        title.word[2].slotGapSize = 2; // space between each particle slot
        title.word[2].particleSpacing = 0.96f; // space between each particle in a single slot
        title.word[2].code = "88888" + "8   8" + "88888" +
                                "8   8" + "8   8" + "  8  " +
                                "8   8" + "8   8" + "  8  " +
                                "8   8" + "8   8" + "  8  " +
                                "88888" + "88888" + "  8  ";

        // initialize the word GUNEY
        title.word[3] = new Word();
        title.word[3].position = new Vector3(-2f, 0f, 4.6f); // Position of the word
        title.word[3].letterWidth = 5; // number of particle slots
        title.word[3].letterHeight = 5; // number of particle slots	
        title.word[3].slotGapSize = 2; // space between each particle slot
        title.word[3].particleSpacing = 0.96f; // space between each particle in a single slot
        title.word[3].code = "88888" + "8   8" + "88888" + "8888 " + "88888" +
                                "  8  " + "8   8" + "8    " + "8   8" + "8    " +
                                "  8  " + "88888" + "8888 " + "8888 " + "8888 " +
                                "  8  " + "8   8" + "8    " + "8 8  " + "8    " +
                                "  8  " + "8   8" + "88888" + "8  8 " + "88888";
        Transform targetAssign = GetComponent<PlayAnimation>().sun;

        // temporary variables for the x-y axises
        float x;
        float y;

        float putSpaceBetweenLetters;
        float offsetLineToParagraphIndent; // functions like the return key.
        float rowLength; // current row length. 
        float currentRow; // indicates the row number

        for (int i = 0; i < title.word.Length; i++) { // i traces every word in the title
            for (int j = 0; j < title.word[i].code.Length; j++) { // j traces every slot in the letter			
                if (title.word[i].code[j].ToString() != " ")
                {
                    rowLength = title.word[i].code.Length / title.word[i].letterHeight; // current row length. code.length/letterHeight gives the length of rows in the word. 
                    currentRow = (int)(j / rowLength); // the current row. "row" increases 1 with every row of particle slots. Every 25 times for j for the word GUNEY
                    y = -1 * title.word[i].slotGapSize * currentRow;

                    putSpaceBetweenLetters = title.word[i].slotGapSize * (int)(j / title.word[i].letterWidth); // puts space between letters
                    offsetLineToParagraphIndent = -1 * currentRow * (title.word[i].slotGapSize) * (rowLength + rowLength / title.word[i].letterWidth); // functions like the return key. offsets each line of particle slots back to paragraph indent. This is because the word is coded as a single string.
                    x = title.word[i].slotGapSize * j + putSpaceBetweenLetters + offsetLineToParagraphIndent;

                    planet = Instantiate(newPlanet, title.word[i].position + new Vector3(x, y, 0), Quaternion.identity);
                    planet.parent = transform;
                    planet.GetComponent<Attraction>().target = targetAssign;
                    planet.tag = "Planet";

                    planet = Instantiate(newPlanet, title.word[i].position + new Vector3(x + title.word[i].particleSpacing, y, 0), Quaternion.identity);
                    planet.parent = transform;
                    planet.GetComponent<Attraction>().target = targetAssign;
                    planet.tag = "Planet";

                    planet = Instantiate(newPlanet, title.word[i].position + new Vector3(x, y - title.word[i].particleSpacing, 0), Quaternion.identity);
                    planet.parent = transform;
                    planet.GetComponent<Attraction>().target = targetAssign;
                    planet.tag = "Planet";

                    planet = Instantiate(newPlanet, title.word[i].position + new Vector3(x + title.word[i].particleSpacing, y - title.word[i].particleSpacing, 0), Quaternion.identity);
                    planet.parent = transform;
                    planet.GetComponent<Attraction>().target = targetAssign;
                    planet.tag = "Planet";
                }
            }
        }
    }
	
}
