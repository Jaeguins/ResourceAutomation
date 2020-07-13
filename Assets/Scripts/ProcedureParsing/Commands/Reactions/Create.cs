using System.Collections.Generic;
using TestObjects;
using UnityEditor;
using UnityEngine;

namespace ProcedureParsing.Commands {
    public static partial class CommandProcessor {
        private static IEnumerable<Command> ReactionOfCreate(ProcedureParser context, string target, string subTarget) {
            return null;
        }

        private static IEnumerable<Command> ValidateOfCreate(ProcedureParser context, string target, string subTarget) {
            List<Command> ret = new List<Command>();
            CustomPath path=new CustomPath(subTarget);
            IProcedureParsable tempTarget = CustomPath.FindAssetWithPath(path);
            if (tempTarget != null) {
                Debug.Log($"{path} already exists.");
                return ret;
            }
            else {
                List<string> tempTargets = CustomPath.FindAssetOnlyName(path);
                if (tempTargets.Count > 2) {
                    Debug.Log($"{path} : have more than one.");
                    ret.Add(new Command(CommandType.Log,$"{path} : have more than one."));
                    return ret;
                }
                else if (tempTargets.Count == 0) {
                    return null;
                }
                else {
                    ret.Add(new Command(CommandType.Move,tempTargets[0],subTarget));
                    return ret;
                }
            }
        }
    }
}