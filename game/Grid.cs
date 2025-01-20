using System.Data.Common;
using System.Runtime.Intrinsics.X86;


public class Grid
{
    private int Column;
    private int Row;
    private int MineNum;
    private int MinesDetected;
    public List<List<Cell>> Field { get; set; }

    public Grid(int row, int column, int mineNum, Coordinate firstMoveCoordinate)
    {
        this.Row = row;
        this.Column = column;
        this.MineNum = mineNum;
        this.MinesDetected = 0;
        this.Field = _GenerateField();
        _PlaceMines(firstMoveCoordinate);
    }

    private List<List<Cell>> _GenerateField() 
        // A double `for` loop to generate mine-free field
    {
        var field = new List<List<Cell>>();
        for (var i = 0; i <= Row; i++)
        {
            var column = new List<Cell>();
            for (var i2 = 0; i2 <= Column; i2++)
            {
                var cell = new Cell(rowIndex:i, columnIndex:i2, hasMine:false);
                column.Add(cell);
            }
            field.Add(column);
        }
        return field;
    }

    private void _PlaceMines(Coordinate firstCoordinate)
        // Place mines randomly
    {
        Random random = new Random();
        for (int i = 0; i < MineNum;)
        {
            // Create random coordinate
            int currentRow = random.Next(Row);
            int currentColumn = random.Next(Column);
            var currentMove = new Coordinate(currentRow, currentColumn);
        
            // Check if the current coordinate already has a mine or is the first move
            bool noMineYet = !Field[currentRow][currentColumn].HasMine;
            bool notFirstMove = currentMove != firstCoordinate;
        
            // Place mine if cell is clean and it's not the first move
            if (noMineYet && notFirstMove)
            {
                Field[currentRow][currentColumn] = new Cell(currentRow, currentColumn, hasMine:true);
                i++;
            }
        }
    }

    public void CalculateAdjacent(Cell currentCell)
        // Calculate adjacent mine number for a single cell
    {
        currentCell.AdjacentMineCount = 0;
        Console.WriteLine($"Current cell coordination: {currentCell.CellCoordinate}");
        var currentRowIndex = currentCell.CellCoordinate.Row;
        var currentColumnIndex = currentCell.CellCoordinate.Column;

        
        for (var row = currentRowIndex - 1; row <= currentRowIndex + 1; row++)
        {
            for (var column = currentColumnIndex - 1; column <= currentColumnIndex + 1; column++)
            {
                if (row >= 0 && row < Field.Count && column >= 0 && column < Field[row].Count)
                {
                    if (row == currentRowIndex && column == currentColumnIndex)
                        continue;

                    var neighborCell = Field[row][column];
                    if (neighborCell.HasMine)
                    {
                        currentCell.AdjacentMineCount++;
                    }
                }
            }
        }
    }


    public void Display()
    {
        foreach (var row in Field)
        {
            foreach (var cell in row)
            {
                Console.Write(cell);
            }
            Console.Write("\n");
        }
    }

    public bool Win()
    {
        return MinesDetected == MineNum;
    } 

    public void Flag(Coordinate coordinate)
    {
        var cell = Field[coordinate.Row][coordinate.Column];
        cell.Flag();
        if (cell.Demine())
        {
            MinesDetected++;
        }
    }


    public void Reveal(Coordinate coordinate)
    {
        Field[coordinate.Row][coordinate.Column].Reveal();
    }

    public bool HasMine(Coordinate coordinate)
    {
        return Field[coordinate.Row][coordinate.Column].HasMine;
    }
}