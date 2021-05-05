using System.Linq;

public class BinaryGrid
{
    private readonly bool[,] cellStateRowCol;
    
    public BinaryGrid(int rowCount, int colCount)
    {
        cellStateRowCol = new bool[rowCount, colCount];
        RowCount = rowCount;
        ColCount = colCount;
    }

    public void SetCellState(int row, int col, bool v)
    {
        cellStateRowCol[row, col] = v;
    }

    public bool GetCellState(int row, int col)
    {
        return cellStateRowCol[row, col];
    }

    public int RowCount { get; private set; }
    public int ColCount { get; private set; }

    public int GetCellStateCount(bool state)
    {
        int count = 0;
        
        for (int row = 0; row < RowCount; row++)
        {
            for (int col = 0; col < ColCount; col++)
            {
                if (cellStateRowCol[row, col] == state)
                {
                    count++;
                }
            }            
        }

        return count;
    }
}