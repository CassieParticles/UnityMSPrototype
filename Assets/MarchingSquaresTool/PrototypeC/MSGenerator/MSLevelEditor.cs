using MarchingSquaresTool.PrototypeC.FileLoader;

namespace MarchingSquaresTool.PrototypeC.MSGenerator
{
    public class MSLevelEditor: MSLevelSelector
    {
        public void SaveLevel()
        {
            LevelFileHandler.StoreLevel(Generator.Grid,levelName);
        }
    }
}