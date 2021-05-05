using System.Collections.Generic;

public class Character
{
    public Character(char character)
    {
        Dictionary<char,string[]> charactersMap = GridFont.CharactersMap;
        int rowCount = charactersMap[character].Length;
        int colCount = charactersMap[character][0].Length;
        BinaryGrid = new BinaryGrid(rowCount, colCount);

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                bool isCellFilled = !char.IsWhiteSpace(charactersMap[character][row][col]);
                BinaryGrid.SetCellState(row, col, isCellFilled);
            }
        }
    }
    
    public BinaryGrid BinaryGrid { get; private set; }
}