using SudokuSolver;
using static System.Linq.Enumerable;


// {-1,-1,-1,  -1,-1,-1,  -1,-1,-1},
// -1 is an empty cell

// super hard? https://puzzling.stackexchange.com/questions/252/how-do-i-solve-the-worlds-hardest-sudoku
// var boardValues = new int[,]
// {
//     {8,-1,-1,  -1,-1,-1,  -1,-1,-1},
//     {-1,-1,3,  6,-1,-1,  -1,-1,-1},
//     {-1,7,-1,  -1,9,-1,  2,-1,-1},
//     
//     {-1,5,-1,  -1,-1,7,  -1,-1,-1},
//     {-1,-1,-1,  -1,4,5,   7,-1,-1},
//     {-1,-1,-1,  1,-1,-1,  -1,3,-1},
//     
//     {-1,-1,1,  -1,-1,-1,  -1,6,8},
//     {-1,-1,8,  5,-1,-1,  -1,1,-1},
//     {-1,9,-1,  -1,-1,-1,  4,-1,-1},
// };


// easy board
// var boardValues = new int[,]
// {
//     {-1,7,-1,  -1,2,-1,  -1,4,6},
//     {-1,6,-1,  -1,-1,-1,   8,9,-1},
//     {2,-1,-1,  8,-1,-1,   7,1,5},
//     
//     {-1,8,4,   -1,9,7,   -1,-1,-1},
//     {7,1,-1,   -1,-1,-1,  -1,5,9},
//     {-1,-1,-1,  1,3,-1,   4,8,-1},
//     
//     {6,9,7,    -1,-1,2,    -1,-1,8},
//     {-1,5,8,   -1,-1,-1,   -1,6,-1},
//     {4,3,-1,   -1,8,-1,    -1,7,-1},
// };

// hard board
// var boardValues = new int[,]
// {
//     {-1,-1,4,  -1,-1,-1,  -1,-1,-1},
//     {8,1,-1,  -1,-1,9,    2,7,-1},
//     {-1,-1,7,  -1,5,-1,   9,-1,-1},
//     
//     {1,-1,5,  -1,-1,6,  -1,-1,3},
//     {3,-1,-1,  1,-1,-1,  -1,-1,-1},
//     {-1,-1,-1,  5,7,-1,  -1,6,-1},
//     
//     {4,5,1,  -1,9,-1,  -1,-1,-1},
//     {-1,3,-1,  -1,1,5,  -1,-1,-1},
//     {9,-1,-1,  -1,3,-1,  -1,-1,6},
// };

// expert board
var boardValues = new int[,]
{
    {4,-1,-1,   6,-1,-1,   -1,1,-1},
    {-1,-1,-1,  -1,-1,-1,   6,-1,-1},
    {-1,-1,-1,  -1,-1,9,    8,-1,2},
    
    {-1,7,-1,   4,-1,-1,  -1,-1,-1},
    {-1,-1,-1,  -1,6,-1,  -1,-1,-1},
    {1,-1,5,    9,-1,-1,  -1,4,-1},
    
    {5,-1,-1,  -1,-1,-1,  -1,8,-1},
    {2,-1,8,    1,-1,-1,  -1,-1,-1},
    {-1,-1,-1,  -1,-1,-1,  7,3,-1},
};


// y is top to bottom , x left to right
// 0,0 is top left cell
// 8,0 is bottom left
var sudoku = new Sudoku(boardValues);

Sudoku.PrintBoard(sudoku.CurrentBoard);

Console.WriteLine("Press [Enter] to start solving....");
Console.ReadKey();



for (var i = 0; i < 10_000; i++)
{
    // collapse the board, getting all states from start to end
    var collapseIterations = sudoku.Collapse();
            
    // display all states
    foreach (var boardState in collapseIterations)
    {
        Sudoku.PrintBoard(boardState);
    }

    if (!sudoku.ValidSolve())
    {
        sudoku.Reset();
        Sudoku.PrintBoard(sudoku.CurrentBoard);
    }
    else
    {
        Console.WriteLine($"Solved on iteration {i + 1}");
        Sudoku.PrintBoard(sudoku.CurrentBoard);
        break;
    }
}







