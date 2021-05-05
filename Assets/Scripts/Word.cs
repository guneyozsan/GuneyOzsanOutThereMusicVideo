using System.Linq;
using UnityEngine;

public class Word
{
    public Word(Vector3 position, WordArgs args, string content)
    {
        Position = position;
        VerticalParticleSlotsPerCharacter = args.VerticalParticleSlotsPerCharacter;
        HorizontalParticleSlotsPerCharacter = args.HorizontalParticleSlotsPerCharacter;
        HorizontalParticlesPerSlot = args.HorizontalParticlesPerSlot;
        VerticalParticlesPerSlot = args.VerticalParticlesPerSlot;
        SlotPadding = args.SlotPadding;
        ParticlePadding = args.ParticlePadding;
        Content = content;

        Characters = new Character[Content.Length];

        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i] = new Character(Content[i]);
        }
        
        ParticleCount = GetParticleCount();
    }

    public Character[] Characters { get; private set; }

    // position of the word in 3d space
    public Vector3 Position { get; private set; }
    
    // character height and width in particle slots
    public int VerticalParticleSlotsPerCharacter { get; private set; }
    public int HorizontalParticleSlotsPerCharacter { get; private set; }
    public int HorizontalParticlesPerSlot { get; private set; }
    public int VerticalParticlesPerSlot { get; private set; }
    
    // space between each particle slot
    public float SlotPadding { get; private set; }
    
    // space between each particle in a single slot
    public float ParticlePadding { get; private set; }
    public int ParticleCount { get; private set; }

    // the characters written in empty and non-empty characters.
    private string Content { get; set; }
    
    private int GetParticleCount()
    {
        int particlesPerSlot = HorizontalParticlesPerSlot * VerticalParticlesPerSlot;
        return Characters.Sum(character => particlesPerSlot * character.NonemptySlotsCount);
    }
}