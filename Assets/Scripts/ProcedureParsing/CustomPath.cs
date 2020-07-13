using System;
using System.Collections.Generic;
using System.Linq;
using ProcedureParsing.Commands;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

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

        public override string ToString() {
            return FullPath;
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

        public static List<string> FindAssetOnlyName(string path) => FindAssetOnlyName(new CustomPath(path));
        public static List<string> FindAssetOnlyName(CustomPath path) {
            if (!UnityEngine.Application.isEditor) return null;
            List<string> ret=new List<string>();
            string fileName = string.Empty;
            foreach (string t in path.PathParts) {
                if (t.Contains('.')) {
                    fileName = t;
                }
            }
            
            foreach (var t in AssetDatabase.GetAllAssetPaths()){
                if (t.Contains(fileName)) {
                    ret.Add(t);
                }
            }
            return ret;
        }

        public static IProcedureParsable FindAssetWithPath(CustomPath path) {
            string[] pathParts = path.PathParts;
            string tempFilePath = string.Empty;
            int index = 0;
            for (index = 0; index < pathParts.Length; index++) {
                string t = pathParts[index];
                if (t.Contains(".")) {
                    tempFilePath += $"/{t}";
                    index++;
                    break;
                }

                tempFilePath += $"/{t}";
            }

            tempFilePath = tempFilePath.Substring(1);

            if (tempFilePath.EndsWith(".prefab")) {
                GameObject targetGameObject = AssetDatabase.LoadAssetAtPath<GameObject>(tempFilePath);
                if (targetGameObject == null) {
                    return null;
                }
                Transform targetObject = targetGameObject.transform;
                while (pathParts[index].Contains("o_")) {
                    targetObject = targetObject.Find(pathParts[index].Substring(2));
                    index++;
                }

                string componentName = pathParts[index++].Substring(2);
                IProcedureParsable targetComponent = targetObject.GetComponent(componentName) as IProcedureParsable;
                if (targetComponent == null) {
                    return null;
                }

                return targetComponent;
            }
            else if (tempFilePath.EndsWith(".asset")) {
                IProcedureParsable targetScriptableObject =
                    AssetDatabase.LoadAssetAtPath<ScriptableObject>(tempFilePath) as IProcedureParsable;
                if (targetScriptableObject == null) {
                    return null;
                }
                return targetScriptableObject;
            }
            return null;
        }
    }

}