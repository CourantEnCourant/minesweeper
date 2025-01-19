using System.Data.Common;
using game;

public class Controller
{
    private Coordinate _currentCoordinate = new Coordinate(0, 0);
    
    public void Move(Coordinate coordinate)
    {
        _currentCoordinate = coordinate;
    }

    public Coordinate GetCurrentLocation()
    {
        return _currentCoordinate;
    }

    public Coordinate Click()
    {
        return _currentCoordinate;
    }

    public string AimingAt()
    {
        return $"Controller aiming at: ({_currentCoordinate.Row}, {_currentCoordinate.Column})";
    }
}