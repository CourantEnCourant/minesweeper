public class Cell
{
    public Coordinate CellCoordinate { get; set; }
    public bool HasMine { get; set; }
    public int AdjacentMineCount { get; set; }
    public bool Flagged { get;  private set; }
    private bool _revealed;
    public bool Demined { get; set; }
    
    public Cell(int rowIndex, int columnIndex, bool hasMine)
    {
        CellCoordinate = new Coordinate(rowIndex, columnIndex);
        HasMine = hasMine;
        Flagged = false;
        _revealed = false;
        Demined = false;
    }

    public void Flag()
    {
        Flagged = true;
    }
    
    public void Reveal()
    {
        _revealed = true;
        Flagged = false;
    }

    public bool Demine()
    {
        if (HasMine && Flagged)
            Demined = true;
        return Demined;
    }

    public override string ToString()
    {   // Pay attention to priority. e.g. Flagged should precede HasMine.
        if (Flagged)
            return " F ";
        if (HasMine)
            return " M ";
        if (_revealed)
            return $" {AdjacentMineCount.ToString()} ";
        return " - ";
    }
}