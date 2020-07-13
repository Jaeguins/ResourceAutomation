using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcedureParsing {

    public class CustomPath {
        private const char _diff = '/';
        public const string ObjPrefix = "o_",
                            CompPrefix = "c_",
                            RefPrefix = "r_";
        public string FullPath;
        private string _lastPath;
        private string[] _lastParts;
        public string[] PathParts {
            get {
                if (_lastPath != FullPath) {
                    _lastParts = FullPath.Split(_diff);
                }
                return _lastParts;
            }
        }

        //Example : Assets/Resources/Commons/Furnitures/1st/FirstDiorama/FurnitureName/AllocPrefab.prefab/o_Grids/o_FirstRoomGrid/c_RoomGrid/r_size/r_x
        //Example : Assets/Resources/Commons/Furnitures/1st/FirstDiorama/FurnitureName/ScriptableData.asset/r_size/r_x
        public CustomPath(string fullPath) {
            FullPath = fullPath;
        }
        public void Append(string more) {
            FullPath += $"/{more}";
        }
        public CustomPath GenerateHigherPath(int altIndex) {
            string targetPath = PathParts[PathParts.Length - altIndex - 1];
            return new CustomPath(FullPath.Substring(0, FullPath.IndexOf(targetPath, StringComparison.Ordinal) + targetPath.Length));
        }
        public CustomPath GenerateLowerPath(string additional) {
            return new CustomPath($"{FullPath}/{additional}");
        }
        public string FromLast(int index) {
            return PathParts[PathParts.Length - 1 - index];
        }
    }

}