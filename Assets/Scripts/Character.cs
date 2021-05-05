using System.Collections.Generic;

public class Character
{
    public Character(char character)
    {
        Dictionary<char,string[]> charactersMap = GridFont.CharactersMap;
        int rowCount = charactersMap[character].Length;
        int colCount = charactersMap[character][0].Length;
        Slots = new bool[colCount, rowCount];

        NonemptySlotsCount = 0;

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                if (char.IsWhiteSpace(charactersMap[character][row][col]))
                {
                    Slots[row, col] = false;
                }
                else
                {
                    Slots[row, col] = true;
                    NonemptySlotsCount++;
                }
            }
        }
    }

    public int NonemptySlotsCount { get; private set; }
    
    public bool[,] Slots { get; private set; }
}