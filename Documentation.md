## Classes and methods
* Only main classes and methods are documented to cover non-trivial core-functionalities
* `Class.Method()` means static method
* `Method()` means a normal method

### Cli
* Handles IO operation with player and computer
* Only contains static methods as we don't need multiple instances of Cli
* Methods:
  * `Cli.InitializeGame()`: configure a game session
  * `Cli._play()`: a private method nested inside `Cli.InitializeGame()`

### Grid
* Our playground
* Methods are quite straight forward. Please refer to their docstring

### Cell
* `Flag()`: flag a cell, corresponds to `right-click` in a normal minsweeper game
* `Reveal()`: reveal a cell, corresponds to `left-click`
* `ToString()`: override to customize output
  * N.B. In current version, to simplify testing, mines are not hidden, and are marked as `M`

### Coordinate
* Convert two int into a `Coordinate` object, for readability

### Parser
* `Parser.Input2Coordinate(string input, string pattern)`
  * Uses regular expression to parse player input

### Controller
* Imitate behavior of a mouse
* `Move(Coordinate)` to move around
* `Click`: choice between left/right click is done by `Cli`class, who processes player input

## Leaderboard
* Leaderboard uses `dictionary` and `.json` to record player's performance
* Each win gives 1 point
* `Leaderboard.ReadFrom(string path)`: read from a file and generate a `Dictionary`
* `Leaderboard.Record`: if player wins, allow to enter his name into leaderboard
* `Leaderboard.Output(Dictionary leaderboard, string path)`: save a leaderboard dictionary to file




