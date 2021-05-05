using System.Linq;
using UnityEngine;

public class Word
{
    public Word(Vector3 position, WordArgs args, string content)
    {
        Position = position;
        CharacterGridSizeWidth = args.CharacterGridSizeWidth;
        CharacterGridSizeHeight = args.CharacterGridSizeHeight;
        HorizontalParticleCountPerGridCell = args.HorizontalParticleCountPerGridCell;
        VerticalParticleCountPerGridCell = args.VerticalParticleCountPerGridCell;
        PaddingBetweenGridCells = args.PaddingBetweenGridCells;
        PaddingBetweenParticlesInCells = args.PaddingBetweenParticlesInCells;
        Content = content;

        Characters = new Character[Content.Length];

        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i] = new Character(Content[i]);
        }
        
        ParticleCount = GetParticleCount();
    }

    public Character[] Characters { get; private set; }

    public Vector3 Position { get; private set; }

    public int CharacterGridSizeWidth { get; private set; }
    public int CharacterGridSizeHeight { get; private set; }
    
    public int HorizontalParticleCountPerGridCell { get; private set; }
    public int VerticalParticleCountPerGridCell { get; private set; }
    
    public float PaddingBetweenGridCells { get; private set; }
    public float PaddingBetweenParticlesInCells { get; private set; }

    public int ParticleCount { get; private set; }

    // the characters written in empty and non-empty characters.
    private string Content { get; set; }
    
    private int GetParticleCount()
    {
        int particlesPerGridCell = HorizontalParticleCountPerGridCell * VerticalParticleCountPerGridCell;
        return Characters.Sum(
            character => particlesPerGridCell * character.BinaryGrid.GetCellStateCount(true));
    }
}