#Class #Core 
Level Grid is the container for a collection of [[Cell]]s that make up a complete rigid body, during runtime it represents a singular connected rigid body, however when building a level, it starts as a single grid.
-X=Left, +X=Right, -Y=Up, +Y=Down
## Members
Grid - [[Cell]][] (Get, private set) - *Array representing the 2D grid of cells*
origin - Vector2Int - *The 2D position of the node at (0,0), so adding sides doesn't displace existing values*
size - Vector2Int - *The size of the 2D grid*
resized - bool - *Has the grid been resized since it was last checked*
changes - Stack(Vector2Int) - *Positions that have been updated*
## Properties
this[int x, int y] -> [[Cell]] - *Get the cell at position (x, y), accounting for origin*
Resized -> bool - *Get whether the grid has been resized, and set resized to false*
UpdatedCell -> Vector2Int? - *Pops a position off the updated position stack, if none exist, returns null*
## Functions
### Public
AddRowTop() -> Null - *Adds a row on top of the grid*
AddRowBottom() -> Null - *Adds a row underneath the grid*
AddColumnLeft() -> Null - *Adds a column to the left of the grid*
AddColumnRight() -> Null - *Adds a column to the right of the grid*
### Private
GetIndex(int x, int y)-> int - *Converts grid co-ordinate to index of array, accounting for origin*
