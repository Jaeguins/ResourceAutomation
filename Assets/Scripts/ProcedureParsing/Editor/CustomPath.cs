using System;
using System.Collections.Generic;
using System.Linq;
using ProcedureParsing.Commands;
using UnityEditor;
using UnityEngine;

namespace ProcedureParsing {

    public class CustomPath {
        public const char PathDiff = '/';
        public const string ObjPrefix = "o_",
                            CompPrefix = "c_",
                            RefPrefix = "r_",
                            ExtensionDiff = ".";
        public const string PrefabExtension = ".prefab";
        public const string AssetExtension = ".asset";
        public string FullPath;
        
        public string this[int index] => _pathParts[index];
        public int Length => _pathParts.Length;
        private string _lastPathForPathPart;
        private string[] _lastParts;
        
        
        private void RecalculateFilePath() {
            _filePath = string.Empty;
            for (int index = 0; index < Length; index++) {
                string t = this[index];
                if (t.Contains(ExtensionDiff)) {
                    _filePath+= $"{PathDiff}{t}";
                    _fileName = t;
                    _fileNameIndex = index;
                    break;
                }

                _filePath += $"{PathDiff}{t}";
            }
            _filePath=_filePath.Substring(1);
            _lastPathForFilePath = FullPath;
        }
        private string _lastPathForFilePath;

        public string FileName {
            get {
                if (_lastPathForFilePath != FullPath) {
                    RecalculateFilePath();
                }
                return _fileName;
            }
        }
        private string _fileName;

        public string FilePath{
            get {
                if (_lastPathForFilePath != FullPath) {
                    RecalculateFilePath();
                }
                return _filePath;
            }
        }
        private string _filePath;
        
        public int FileNameIndex{
            get {
                if (_lastPathForFilePath != FullPath) {
                    RecalculateFilePath();
                }
                return _fileNameIndex;
            }
        }
        private int _fileNameIndex;

        private string[] _pathParts {
            get {
                if (_lastPathForPathPart != FullPath) {
                    _lastParts = FullPath.Split(PathDiff);
                    _lastPathForPathPart = FullPath;
                }
                return _lastParts;
            }
        }

        public override string ToString() {
            return FullPath;
        }

        //Example(Prefab) :
        //Assets/Resources/Commons/Furnitures/1st/FirstDiorama/FurnitureName/AllocPrefab.prefab/o_Grids/o_FirstRoomGrid/c_RoomGrid/r_size/r_x
        //Example(ScriptableObject) :
        //Assets/Resources/Commons/Furnitures/1st/FirstDiorama/FurnitureName/ScriptableData.asset/r_size/r_x
        public CustomPath(string fullPath) {
            FullPath = fullPath;
            if (FullPath.StartsWith($"{PathDiff}")) {
                FullPath=FullPath.Substring(1);
            }
        }
        public CustomPath GenerateHigherPath(int altIndex) {
            int targetIndex = _pathParts.Length - altIndex - 1;
            if (targetIndex < _pathParts.Length && targetIndex > 0) {
                string targetPath = _pathParts[targetIndex];
                return new CustomPath(FullPath.Substring(0, FullPath.IndexOf(targetPath, StringComparison.Ordinal) + targetPath.Length));
            }
            return new CustomPath("");
            
        }
        public CustomPath GenerateLowerPath(string additional) {
            return new CustomPath($"{FullPath}{PathDiff}{additional}");
        }
        public string FromLast(int index) {
            return _pathParts[_pathParts.Length - 1 - index];
        }

        public static List<string> FindAssetOnlyName(string path) => FindAssetOnlyName(new CustomPath(path));
        public static List<string> FindAssetOnlyName(CustomPath path) {
            if (!Application.isEditor) return null;
            List<string> ret=new List<string>();
            string fileName = string.Empty;
            foreach (string t in path._pathParts) {
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
            
            int tempIndex = path.FileNameIndex+1;

            if (path.FileName.EndsWith(PrefabExtension)) {
                GameObject targetGameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path.FilePath);
                if (targetGameObject == null) {
                    return null;
                }
                Transform targetObject = targetGameObject.transform;
                while (path[tempIndex].Contains(ObjPrefix)) {
                    targetObject = targetObject.Find(path[tempIndex].Substring(2));
                    tempIndex++;
                }

                string componentName = path[tempIndex++].Substring(2);
                IProcedureParsable targetComponent = targetObject.GetComponent(componentName) as IProcedureParsable;
                if (targetComponent == null) {
                    return null;
                }

                return targetComponent;
            }
            else if (path.FilePath.EndsWith(AssetExtension)) {
                IProcedureParsable targetScriptableObject =
                    AssetDatabase.LoadAssetAtPath<ScriptableObject>(path.FilePath) as IProcedureParsable;
                if (targetScriptableObject == null) {
                    return null;
                }
                return targetScriptableObject;
            }
            return null;
        }

    }

}