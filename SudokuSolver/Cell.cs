namespace SudokuSolver;

public class Cell
{
    /// <summary>
    /// Is this cell editable, returns true when the length PossibleValues > 1
    /// </summary> 
    public bool Editable => PossibleValues.Count > 1;

    /// <summary>
    /// The cells possible values
    /// </summary>
    public List<int> PossibleValues { get; set; }

    /// <summary>
    /// The cells value. equal to -1 if length of PossibleValues > 1
    /// </summary>
    public int Value => PossibleValues.Count > 1 ? -1 : PossibleValues[0];
    
    
    
    /// <summary>
    /// Removes the value from the cells possible values, will only remove the value if the cell is editable
    /// </summary>
    /// <param name="value">The value to remove</param>
    public void Remove(int value)
    {
        if (Editable)
            PossibleValues.Remove(value);
    }
    
    /// <summary>
    /// Randomly choose a value from the cells possible values
    /// removes all other values in the array
    /// </summary>
    public void ChooseValue()
    {
        if(!Editable) return;
        
        // choose random index
        var index = Random.Shared.Next(0,PossibleValues.Count);
        // remove all other values except for the one randomly chosen
        var value = PossibleValues[index];
        PossibleValues = new List<int>{value};
    }
    
    
    public string DisplayPossibleValues()
    {
        return Editable ? string.Join(",", PossibleValues) : Value.ToString();
    }
}