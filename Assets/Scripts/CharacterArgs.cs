using UnityEngine;

public class CharacterArgs
{
    public CharacterArgs(Vector2Int subGridSize, Vector2 subGridCellSize,
        Vector2 spaceBetweenGridCells)
    {
        SubGridSize = subGridSize;
        SubGridCellSize = subGridCellSize;
        SpaceBetweenGridCells = spaceBetweenGridCells;
    }

    public Vector2Int SubGridSize { get; private set; }
    public Vector2 SubGridCellSize { get; private set; }
    public Vector2 SpaceBetweenGridCells { get; private set; }
}