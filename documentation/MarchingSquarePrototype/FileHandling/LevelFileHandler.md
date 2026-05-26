#StaticClass #FileHandling
Level file handler is used to store and load levels made up of [[Cell]]s, contains functions involved in file handling using levels

==**Members**==
LevelPath - (Readonly) string (The relative path to the folder levels are stored in)

==**Functions**==
StoreLevel - ([[Cell]][] cells, uint width, uint height, string title)->null (Takes an array of cells making up a level, and stores it into a .lvl file with the title)
LoadLevel - (string title, ref uint width, ref uint height) -> [[Cell]][] (Gets the level data for the level with the title, width and height give the size of the level)
DeleteLevel - (string title)->null (Deletes the level with the title)