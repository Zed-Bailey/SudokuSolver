using static System.Linq.Enumerable;

namespace SudokuSolver;

public class Sudoku
{
    // length of board
    private const int Length = 9;
    // height of board
    private const int Height = 9;

    // a snap shot to keep the starting state of this sudoku board
    private readonly int[,] _startingBoardSnapshot;
    
    // the board that will be modified
    public Cell[,] CurrentBoard;
    
    
    public Sudoku(int[,] startValues)
    {
        CurrentBoard = InitializeBoardCells(startValues);
        _startingBoardSnapshot = startValues;
    }

    /// <summary>
    /// initializes the board with all cells.
    /// cells not marked -1 have a single value
    /// cells marked -1 have a list of possible values from 1-9
    /// </summary>
    /// <param name="fromValues">an int array to initialize cells from</param>
    /// <returns>A new 2d array with created cells</returns>
    private static Cell[,] InitializeBoardCells(int[,] fromValues)
    {
        var boardInitialized = new Cell[9, 9];
        // initialise all cells, cells marked with -1 are given a list of all possible values
        // cells with a value already are marked as un-editable
        for(var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Length; x++)
            {
                if (fromValues[y, x] != -1)
                    boardInitialized[y, x] = new Cell { PossibleValues = new List<int> { fromValues[y,x] } };
                else
                    boardInitialized[y, x] = new Cell { PossibleValues = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9} };
                
            
            }
        }

        return boardInitialized;
    }
    
    
    /// <summary>
    /// Executes a wave function collapse algorithm based on the sudoku rules
    /// May or may not return a full board as a cell may not always have a valid value
    /// simply reset the board and try again
    /// </summary>
    /// <returns>A cell board, check if the board is valid with the ValidSolve function</returns>
    public Cell[,] Solve()
    {
        // Update the boards cells
        UpdatePossibleValues(CurrentBoard);
        
        // run '10' iterations on the board choosing random values and collapsing cells
        // if the collapsed value stays true when checking the board then the board is full
        // and the loop will exit early.
        //
        // after '10' iterations the board will be returned, it may or may not be full as it's
        // possible for a cell to have no possible values due to how the random values are chosen
        // if this is the case then the board can be reset,
        // and the wave function collapse algorithm to be run again
        for (var p = 0; p < 10; p++)
        {
            
            // choose random cell and choose random value for that cell
            var randX = Random.Shared.Next(0, Length);
            var randY = Random.Shared.Next(0, Height);
            CurrentBoard[randY,randX].ChooseValue();
            // update the board as the random value has been chosen
            UpdatePossibleValues(CurrentBoard);
            
            var collapsed = true;
            // loop through all the cells in the board, check if the cell is collapsed
            // if collapsed stays true then the board is full as all cells have only 1 possible value
            for (var i = 0; i < CurrentBoard.LongLength; i++)
            {
                // convert 1d index into 2d index
                var x = i % Length;
                var y = i / Height;
                
                if (CurrentBoard[y, x].Value != -1) continue;
                
                collapsed = false;
                // choose a random value for this cell
                CurrentBoard[y,x].ChooseValue();
                // update the possible values
                UpdatePossibleValues(CurrentBoard);
                break;
            }

            if (collapsed) return CurrentBoard;

        }

        return CurrentBoard;
    }

    /// <summary>
    /// Resets the current board to a snapshot of the starting board
    /// </summary>
    public void Reset()
    {
        CurrentBoard = InitializeBoardCells(_startingBoardSnapshot);
    }
    
    /// <summary>
    /// Updates each cells possible values based on the values in it's row/column/square
    /// if a cell collapses while removing possible values then this function is recursively called
    /// </summary>
    /// <param name="cells"></param>
    private void UpdatePossibleValues(Cell[,] cells)
    {
        /*
         * Loop through all the cells in the grid
         * for each cell that is editable, remove from it's possible values
         * all values already in it's row, column or square
         *
         * if the cell collapses, recursively call this function as the cell collapsing causes
         * a ripple effect to all other cells which may in turn cause more cells to collapse 
         */
        for(var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Length; x++)
            {
                var currentCell = cells[y, x];
                
                // skip this cell if it is not editable (i.e the cell has already been collapsed)
                if (!currentCell.Editable) continue;
                
                
                // iterate over each value in the current cells column (y axis)
                for (var colCell = 0; colCell < Height; colCell++)
                {
                    var val = cells[colCell, x].Value;
                    currentCell.Remove(val);
                }
                
                
                // iterate over all cells in the current cells row (x axis)
                for (var rowCell = 0; rowCell < Length; rowCell++)
                {
                    var val = cells[y, rowCell].Value;
                    currentCell.Remove(val);
                }
                
                //
                // calculate which square the current cell belongs to
                //
                IEnumerable<int> yRange = y switch
                {
                    <= 2 => Range(0, 3),
                    <= 5 => Range(3, 3),
                    _ => Range(6, 3)
                };


                IEnumerable<int> xRange = x switch
                {
                    <= 2 => Range(0, 3),
                    <= 5 => Range(3, 3),
                    _ => Range(6, 3)
                };

                
                // iterate over each cell in the square the cell belongs to removing possible values
                foreach (var squareY in yRange)
                {
                    foreach (var squareX in xRange)
                    {
                        var value = cells[squareY, squareX].Value;
                        currentCell.Remove(value);
                    }
                }
                
                // if the cell is no longer editable then it has collapsed
                // so recursively call this function
                if (!currentCell.Editable)
                {
                    UpdatePossibleValues(cells);
                }
                
            }
        }

        // return cells;

    }
    
    /// <summary>
    /// Checks that each row and column of the board add up to 45.
    /// 1+2+3+4+5+6+7+8+9 = 45
    /// </summary>
    /// <param name="cells">The cells to check</param>
    /// <returns>Returns true when valid, false when not</returns>
    public bool ValidSolve(Cell[,] cells)
    {
        // store the sum of the row/column/square, for a row/column/square to be valid it must sum to 45
        int sum;
        // check every row
        for(var y = 0; y < Height; y++)
        {
            sum = 0;
            for (var rowCell = 0; rowCell < Length; rowCell++)
            {
            
                sum += cells[y,rowCell].Value;
            }
        
        
            if (sum != 45) return false;
        }
    
         
        for (var x = 0; x < Length; x++)
        {
            sum = 0;
            // iterate over each value in the current cells column (y axis)
            for (var colCell = 0; colCell < Height; colCell++)
            {
                sum += cells[colCell,x].Value;
            }

            if (sum != 45) return false;
        }
    
        //
        // Check that each square sums to 45.
        //
        
        // FIXME: make a static const?
        // each tuple is a square, (y,x)
        var squareRanges = new (IEnumerable<int>, IEnumerable<int>)[9]
        {
            // top row of squares
            (Range(0,3), Range(0,3)), (Range(0,3), Range(3,3)),   (Range(0,3), Range(6,3)),
            
            // middle row of squares
            (Range(3,3),Range(0,3)),  (Range(3,3),Range(3,3)),    (Range(3,3),Range(6,3)),
            
            // bottom row of squares
            (Range(6,3),Range(0,3)),  (Range(6,3),Range(3,3)),    (Range(6,3),Range(6,3))
        };
        
        foreach (var (yRange, xRange) in squareRanges)
        {
            sum = 0;
            foreach (var y in yRange)
            {
                foreach (var x in xRange)
                {
                    var value = cells[y, x].Value;
                    sum += value;
                }
            }
        
            if (sum != 45) return false;
        }
        
        // TODO
        // check for duplicate values in any row/column/square
        //
        
        

    
        return true;
    }
    
    
    private static string GetValue(Cell value) => value.Value == -1 ? "-" : value.Value.ToString();

    /// <summary>
    /// Pretty prints the board in a nice grid
    /// </summary>
    /// <param name="cells">Board to print</param>
    public static void PrintBoard(Cell[,] cells)
    {
        Console.WriteLine($"-------------------------");
    
        for (var y = 0; y < Height; y++)
        {
        
            Console.Write($"| {GetValue(cells[y,0])} {GetValue(cells[y,1])} {GetValue(cells[y,2])} ");
            Console.Write($"| {GetValue(cells[y,3])} {GetValue(cells[y,4])} {GetValue(cells[y,5])} ");
            Console.Write($"| {GetValue(cells[y,6])} {GetValue(cells[y,7])} {GetValue(cells[y,8])} |\n");

            if (y is 2 or 5 or 8)
            {
                Console.WriteLine("-------------------------");
            }
        }
    }

}