using System.Collections;
using System.Linq;
using UnityEngine;

public class Word : IEnumerator, IEnumerable
{
    private int position = -1;
    
    public Word(Vector3 position, WordArgs args, string content)
    {
        Position = position;
        VerticalParticleSlotsPerLetter = args.VerticalParticleSlotsPerLetter;
        HorizontalParticleSlotsPerLetter = args.HorizontalParticleSlotsPerLetter;
        HorizontalParticlesPerSlot = args.HorizontalParticlesPerSlot;
        VerticalParticlesPerSlot = args.VerticalParticlesPerSlot;
        SlotPadding = args.SlotPadding;
        ParticlePadding = args.ParticlePadding;
        Content = content;

        Letters = new Letter[Content.Length];

        for (int i = 0; i < Letters.Length; i++)
        {
            Letters[i] = new Letter(Content[i]);
        }
        
        ParticleCount = GetParticleCount();
    }

    // IEnumerator and IEnumerable require these methods.
    public IEnumerator GetEnumerator()
    {
        return this;
    }
    
    // IEnumerator
    public bool MoveNext()
    {
        position++;
        return position < Letters.Length;
    }
    
    // IEnumerable
    public void Reset()
    {
        position = 0;
    }
    
    // IEnumerable
    public object Current { get { return Letters[position];} }
    
    public Letter[] Letters { get; private set; }

    // position of the word in 3d space
    public Vector3 Position { get; private set; }
    
    // letter height and width in particle slots
    public int VerticalParticleSlotsPerLetter { get; private set; }
    public int HorizontalParticleSlotsPerLetter { get; private set; }
    public int HorizontalParticlesPerSlot { get; private set; }
    public int VerticalParticlesPerSlot { get; private set; }
    
    // space between each particle slot
    public float SlotPadding { get; private set; }
    
    // space between each particle in a single slot
    public float ParticlePadding { get; private set; }
    public int ParticleCount { get; private set; }

    // the letters written in empty and non-empty characters.
    private string Content { get; set; }
    
    private int GetParticleCount()
    {
        int particlesPerSlot = HorizontalParticlesPerSlot * VerticalParticlesPerSlot;
        return Letters.Sum(letter => particlesPerSlot * letter.NonemptySlotsCount);
    }
}