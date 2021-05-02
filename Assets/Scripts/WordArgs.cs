public class WordArgs
{
    public WordArgs(int verticalParticleSlotsPerLetter, int horizontalParticleSlotsPerLetter,
        int horizontalParticlesPerSlot, int verticalParticlesPerSlot, int slotPadding, float particlePadding)
    {
        VerticalParticleSlotsPerLetter = verticalParticleSlotsPerLetter;
        HorizontalParticleSlotsPerLetter = horizontalParticleSlotsPerLetter;
        HorizontalParticlesPerSlot = horizontalParticlesPerSlot;
        VerticalParticlesPerSlot = verticalParticlesPerSlot;
        SlotPadding = slotPadding;
        ParticlePadding = particlePadding;
    }
    
    public int VerticalParticleSlotsPerLetter { get; private set; } // letter height and width in particle slots
    public int HorizontalParticleSlotsPerLetter { get; private set; }
    public int HorizontalParticlesPerSlot { get; private set; }
    public int VerticalParticlesPerSlot { get; private set; }
    public float SlotPadding { get; private set; } // space between each particle slot
    public float ParticlePadding { get; private set; } // space between each particle in a single slot
}