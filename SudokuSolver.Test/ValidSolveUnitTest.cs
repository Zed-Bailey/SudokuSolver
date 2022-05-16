using Xunit;


namespace SudokuSolver.Test;

// https://dev.to/moe23/learn-unit-test-with-net-6-with-xunit-and-moq-k9i

public class ValidSolveUnitTest
{
    [Fact]
    public void ValidSolveOne()
    {
        // this is a valid board
        var board = new int[,]
        {
            {8,7,5, 9,2,1, 3,4,6},
            {3,6,1, 7,5,4, 8,9,2},
            {2,4,9, 8,6,3, 7,1,5},
            
            {5,8,4 ,6,9,7, 1,2,3},
            {7,1,3, 2,4,8, 6,5,9},
            {9,2,6, 1,3,5, 4,8,7},
            
            {6,9,7, 4,1,2, 5,3,8},
            {1,5,8, 3,7,9, 2,6,4},
            {4,3,2, 5,8,6, 9,7,1}
        };
        // create a new sudoku object and check that the board is valid
        var sudoku = new Sudoku(board);
        Assert.True(sudoku.ValidSolve());
    }

    [Fact]
    public void InvalidSolve()
    {
        // this is an invalid board with duplicate 1's
        var board = new int[,]
        {
            {8,7,5, 9,2,1, 3,4,6},
            {3,6,1, 7,5,4, 8,9,2},
            {2,4,9, 8,6,3, 7,1,5},
            
            {5,8,4 ,6,9,7, 1,2,3},
            {7,1,3, 2,4,8, 6,5,9},
            {9,2,6, 1,3,5, 4,8,7},
                         
            {6,9,7, 4,1,2, 1,3,8}, 
            {1,5,8, 3,7,9, 2,6,4},
            {4,3,2, 5,8,6, 9,7,1}
        };
        // create a new sudoku object and check that the board is valid
        var sudoku = new Sudoku(board);
        Assert.False(sudoku.ValidSolve());
    }

    [Fact]
    public void InvalidSolveRow45()
    {
     
        // the sum of the first row is 45 but it has duplicate numbers
        // this is a valid board
        var board = new int[,]
        {
            {1,2,3, 5,5,6, 7,8,8},
            {3,6,1, 7,5,4, 8,9,2},
            {2,4,9, 8,6,3, 7,1,5},
            
            {5,8,4 ,6,9,7, 1,2,3},
            {7,1,3, 2,4,8, 6,5,9},
            {9,2,6, 1,3,5, 4,8,7},
            
            {6,9,7, 4,1,2, 5,3,8},
            {1,5,8, 3,7,9, 2,6,4},
            {4,3,2, 5,8,6, 9,7,1}
        };
        // create a new sudoku object and check that the board is valid
        var sudoku = new Sudoku(board);
        Assert.False(sudoku.ValidSolve());
    }
    
    [Fact]
    public void InvalidSolveColumn45()
    {
        // the sum of the first column is 45 but it has duplicate numbers
        // each row has no duplicates though 
        var board = new int[,]
        {
            {1,2,3, 4,5,6, 7,8,9},
            {2,3,4, 5,6,7, 8,9,1},
            {3,4,5, 6,7,8, 9,1,2},
            
            {5,6,7 ,8,9,1, 2,3,4},
            {5,6,7 ,8,9,1, 2,3,4},
            {6,7,8, 9,1,2, 3,4,5},
            
            {7,8,9, 1,2,3, 4,5,6},
            {8,6,1, 2,3,4, 5,6,7},
            {8,6,1, 2,3,4, 5,6,7}
        };
        // create a new sudoku object and check that the board is valid
        var sudoku = new Sudoku(board);
        Assert.False(sudoku.ValidSolve());
        
    }
    
}