namespace SudokuSolver.UI.Data;

public class GridService
{
    public Sudoku? SudokuGrid { get; set; }

    public void ClearGrid()
    {
        SudokuGrid = null;
    }
}