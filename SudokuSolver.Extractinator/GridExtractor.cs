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

   /// <summary>
   /// Calls a python script that will extract the data from the image
   /// </summary>
   /// <param name="image">the image path</param>
   /// <returns>the data from the image in a format suitable for the SudokuSolver</returns>
   public static int[,] ReadDataFromImage(string image)
   {

      return new int[,] { };
   }

    
}