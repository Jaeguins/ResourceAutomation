using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static System.String;

namespace ProcedureParsing.Commands {

    public class CreateCommand:ICommand {
        private IEnumerable<Command> ReactionOfCreate(ProcedureParser context, string target, string subTarget) {
            List<Command> ret = new List<Command>();
            IProcedureParsable targetAsset = CreateNewObject(target);
            CustomPath targetPath = new CustomPath(subTarget);
            CustomPath assetPath = null;
            {
                int altIndex = 0;
                for (; altIndex < targetPath.Length; altIndex++) {
                    if (targetPath.FromLast(altIndex).Contains(CustomPath.ExtensionDiff)) {
                        break;
                    }
                }
                assetPath = targetPath.GenerateHigherPath(altIndex);
            }
            Debug.LogWarning(assetPath?.FullPath);

            CustomPath tempPath = new CustomPath(Empty);
            for (int i = 0; i < assetPath.Length - 1; i++) {
                tempPath = tempPath.GenerateLowerPath(assetPath[i]);
                if (!AssetDatabase.IsValidFolder(tempPath.FullPath)) {
                    Debug.Log($"{tempPath} creating directory");
                    AssetDatabase.CreateFolder(tempPath.GenerateHigherPath(1).FullPath, tempPath.FromLast(0));
                }
            }
            if (targetAsset is ScriptableObject) {
                AssetDatabase.CreateAsset(targetAsset as ScriptableObject, assetPath.FullPath);
                targetAsset.InitializeAsset();
            } else if (targetAsset is Component) {
                targetAsset.InitializeAsset();
                PrefabUtility.SaveAsPrefabAsset((targetAsset as Component).gameObject, assetPath.FullPath);
                Object.DestroyImmediate((targetAsset as Component).gameObject);
            } else {
                ret.Add(new Command(CommandType.Log, "Invalid Type"));
                return ret;
            }

            return null;
        }

        private IEnumerable<Command> ValidateOfCreate(ProcedureParser context, string target, string subTarget) {
            List<Command> ret = new List<Command>();
            CustomPath path = new CustomPath(subTarget);
            IProcedureParsable tempTarget = CustomPath.FindAssetWithPath(path);
            if (tempTarget != null) {
                Debug.Log($"{path} already exists.");
                return ret;
            } else {
                List<string> tempTargets = CustomPath.FindAssetOnlyName(path);
                if (tempTargets.Count > 2) {
                    Debug.Log($"{path} : have more than one.");
                    ret.Add(new Command(CommandType.Log, $"{path} : have more than one.", $"Create {target} in {subTarget}"));
                    return ret;
                } else if (tempTargets.Count == 0) {
                    return null;
                } else {
                    ret.Add(new Command(CommandType.Move, tempTargets[0], subTarget));
                    context.ChangeAllCommandPath(path.FilePath, tempTargets[0]);
                    return ret;
                }
            }
        }
        public virtual IProcedureParsable CreateNewObject(string type) {
            IProcedureParsable ret = null;
            switch (type) {
                // case nameof(TestComponent):
                //     ret = new GameObject().AddComponent<TestComponent>();
                //     break;
                // case nameof(TestSubComponent):
                //     ret = new GameObject().AddComponent<TestSubComponent>();
                //     break;
                // case nameof(TestScriptableObject):
                //     ret = ScriptableObject.CreateInstance<TestScriptableObject>();
                //     break;
                // case nameof(TestSubScriptableObject):
                //     ret = ScriptableObject.CreateInstance<TestSubScriptableObject>();
                //     break;
                //TODO
            }
            return ret;
        }
        public CommandProcess Reaction => ReactionOfCreate;
        public CommandProcess Validation => ValidateOfCreate;
    }

}