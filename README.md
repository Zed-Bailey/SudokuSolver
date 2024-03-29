# Sudoku Solver
A sudoku solver using the wavefunction collapse algorithm

This project was inspired by [this video](https://youtu.be/2SuvO4Gi7uY)


the function will recursivley call itself when ever a cells state changes, this causes a ripple effect which will update the possible 
states of remaining cells which may cause them to collapse, this is the ripple effect.
When a cell can only has 1 state then it is considered collapsed.


This continues until either all cells are collapsed or a conjunction happens where a cell cannot have a value as it would break the rules of the game
should this happen, the board is reset and the collapse function called again.

Should a full board be made, it is checked based on sudoku's rules
- every row should sum to 45 and contain no duplicates
- every column should sum to 45 and contain no duplicates
- every square should sum to 45 and contain no duplicates

The reason for the 45 sum is because it's the fastest check to see if a row is valid, 1+2+3+4+5+6+7+8+9 = 45
A row/column/square can still sum to 45 with duplicates so a duplicate check is performed afterwards.

Should any of these conditions fail, then the board is reset and the collapse function redone.


## Solver Interface
To use the web interface, navigate to `SudokuSolver.UI` and execute `dotnet run` the interface will be available on localhost


Enter your sudoku puzzle or select one of the buttons for a pre made problem

<img width="500" alt="initial screen" src=".github/initial_screen.png">

The solve screen, press 'Start Solving' to run the algorithm. You can adjust the time between state changes with the slider

<img width="500" alt="solve screen" src=".github/solve_screen.png">


## Solving Demo

<img width="500" alt="sudoku solver in action gif" src=".github/SudokuSolverInAction.gif">
