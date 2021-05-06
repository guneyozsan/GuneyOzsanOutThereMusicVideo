using System.Linq;
using UnityEngine;

public class Word
{
    public Word(string content, Vector3 position, CharacterArgs characterArgs)
    {
        Position = position;
        CharacterArgs = characterArgs;
        Content = content;

        Characters = new Character[Content.Length];

        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i] = new Character(Content[i]);
        }
        
        ParticleCount = GetParticleCount();
    }

    public Character[] Characters { get; private set; }

    public CharacterArgs CharacterArgs { get; private set; }

    public Vector3 Position { get; private set; }

    public int ParticleCount { get; private set; }

    // the characters written in empty and non-empty characters.
    private string Content { get; set; }
    
    private int GetParticleCount()
    {
        Vector2Int subGridSize = CharacterArgs.SubGridSize;
        int particlesPerGridCell = subGridSize.x * subGridSize.y;
        return Characters.Sum(
            character => particlesPerGridCell * character.Grid.GetCellStateCount(true));
    }
}