#StaticClass #FileHandling
Level file handler is used to store and load levels made up of [[Cell]]s, contains functions involved in file handling using levels
## Members
LevelPath - (Readonly) string - *The relative path to the folder levels are stored in*
## Functions
### Public
StoreLevel - ([[Cell]][] cells, uint width, uint height,uint originX, uint originY, string title)->null - *Takes an array of cells making up a level, and stores it into a .lvl file with the title*
StoreLevel - ([[BodyGrid]] level, string title) -> null - *Calls StoreArray above, using [[BodyGrid]] as container*
LoadLevel - (string title, out uint width, out uint height, out uint originX, out uint originY) -> [[Cell]][] - *Gets the level data for the level with the title, width and height give the size of the level, originX and Y give the origin*
LoadLevel - (string title) -> [[BodyGrid]] - *Gets the level data, and returns it packaged into [[BodyGrid]] object*
DeleteLevel - (string title)->null - *Deletes the level with the title*