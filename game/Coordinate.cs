public class Coordinate
{
    public int Row { get; private set; }
    public int Column { get; private set; }

    public Coordinate(int row, int column)
    {
        if (row > 8 || column > 8)
        {
            Console.WriteLine("Row and column value cannot exceed 8*8. Set current coordinate to (8 8)");
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
            Row = 8;
            Column = 8;
        }
        else
        {
            Row = row;
            Column = column;    
        }
        
    }
    
    public static bool operator ==(Coordinate left, Coordinate right)
    {
        return left.Row == right.Row && left.Column == right.Column;
    }

    public static bool operator !=(Coordinate left, Coordinate right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return $"Row :{Row}, Column: {Column}";
    }
}