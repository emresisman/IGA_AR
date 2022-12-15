using Runtime.Utilities;
using UnityEditor;

namespace Editor
{
    public class MenuItems
    {
        [MenuItem("Tools/Create Building JSON")]
        private static void CreateBuildingJson()
        {
            JsonSerializer.CreateBuildingJson();
        }
        
        [MenuItem("Tools/Create Plane JSON")]
        private static void CreatePlaneJson()
        {
            JsonSerializer.CreatePlaneJson();
        }
    }
}