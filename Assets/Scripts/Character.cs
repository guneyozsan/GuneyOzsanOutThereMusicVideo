using System.Collections.Generic;

public class Character
{
    public Character(char character)
    {
        Dictionary<char,string[]> charactersMap = GridFont.CharactersMap;
        Grid = GetBinaryGrid(character, charactersMap);
    }

    public BinaryGrid Grid { get; private set; }

    private static BinaryGrid GetBinaryGrid(char character, IDictionary<char,
        string[]> charactersMap)
    {
        int rowCount = charactersMap[character].Length;
        int colCount = charactersMap[character][0].Length;
        
        var binaryGrid = new BinaryGrid(rowCount, colCount);

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                bool isCellFilled = !char.IsWhiteSpace(charactersMap[character][row][col]);
                binaryGrid.SetCellState(row, col, isCellFilled);
            }
        }

        return binaryGrid;
    }
}