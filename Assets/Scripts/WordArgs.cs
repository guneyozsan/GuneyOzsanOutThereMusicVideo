public class WordArgs
{
    public WordArgs(int verticalParticleSlotsPerCharacter, int horizontalParticleSlotsPerCharacter,
        int horizontalParticlesPerSlot, int verticalParticlesPerSlot, int slotPadding, float particlePadding)
    {
        VerticalParticleSlotsPerCharacter = verticalParticleSlotsPerCharacter;
        HorizontalParticleSlotsPerCharacter = horizontalParticleSlotsPerCharacter;
        HorizontalParticlesPerSlot = horizontalParticlesPerSlot;
        VerticalParticlesPerSlot = verticalParticlesPerSlot;
        SlotPadding = slotPadding;
        ParticlePadding = particlePadding;
    }
    
    public int VerticalParticleSlotsPerCharacter { get; private set; } // character height and width in particle slots
    public int HorizontalParticleSlotsPerCharacter { get; private set; }
    public int HorizontalParticlesPerSlot { get; private set; }
    public int VerticalParticlesPerSlot { get; private set; }
    public float SlotPadding { get; private set; } // space between each particle slot
    public float ParticlePadding { get; private set; } // space between each particle in a single slot
}