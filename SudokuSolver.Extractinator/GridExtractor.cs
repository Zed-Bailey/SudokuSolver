using OpenCvSharp;

namespace SudokuSolver.Extractinator;

// this sudoku grid extractor is ported from c++ following the below articles
// https://aishack.in/tutorials/sudoku-grabber-opencv-detection/

/// <summary>
/// extracts the sudoku grid from an image
/// when done with the class make sure to call the DisposeOf function to release any resources
/// </summary>
public class GridExtractor
{

    private ResourcesTracker _tracker = new();
    private Mat _sudokuGrid;
    
    public GridExtractor(string imagePath)
    {
        _sudokuGrid = _tracker.T(new Mat("static_sudoku.jpg", ImreadModes.Grayscale));
        using var outerBox = new Mat(_sudokuGrid.Size(), MatType.CV_8UC1);
    }

    /// <summary>
    /// Run pre-processing operations on the image
    /// </summary>
    public void PreProcess()
    {
        
    }
    
    void DisposeOf()
    {
        _tracker.Dispose();
    }
    
}