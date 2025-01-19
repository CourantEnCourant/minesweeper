public class Coordinate
{
    public int Row { get; private set; }
    public int Column { get; private set; }

    public Coordinate(int row, int column)
    {
        Row = row;
        Column = column;
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