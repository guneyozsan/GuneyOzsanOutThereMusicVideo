public class WordArgs
{
    public WordArgs(int characterGridSizeHeight, int characterGridSizeWidth,
        int horizontalParticleCountPerGridCell, int verticalParticleCountPerGridCell,
        float paddingBetweenGridCells, float paddingBetweenParticlesInCells)
    {
        CharacterGridSizeHeight = characterGridSizeHeight;
        CharacterGridSizeWidth = characterGridSizeWidth;
        HorizontalParticleCountPerGridCell = horizontalParticleCountPerGridCell;
        VerticalParticleCountPerGridCell = verticalParticleCountPerGridCell;
        PaddingBetweenGridCells = paddingBetweenGridCells;
        PaddingBetweenParticlesInCells = paddingBetweenParticlesInCells;
    }

    // Character height and width
    public int CharacterGridSizeWidth { get; private set; }
    public int CharacterGridSizeHeight { get; private set; }

    public int HorizontalParticleCountPerGridCell { get; private set; }
    public int VerticalParticleCountPerGridCell { get; private set; }

    public float PaddingBetweenGridCells { get; private set; }
    public float PaddingBetweenParticlesInCells { get; private set; }
}