namespace SudokuSolver.UI.Data;

public class Grid
{
    public int Height;
    public int Width;
    public int[,] Cells;

    public Grid(int width, int height)
    {
        Height = height;
        Width = width;
        Cells = new int[Height, Width];
    }
}