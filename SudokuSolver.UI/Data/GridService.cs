namespace SudokuSolver.UI.Data;

public class GridService
{
    public Grid? SudokuGrid { get; set; }

    public void ClearGrid()
    {
        // if (SudokuGrid == null) return;
        
        // SudokuGrid.Cells = new int[SudokuGrid.Height, SudokuGrid.Width];
        SudokuGrid = null;
    }
}