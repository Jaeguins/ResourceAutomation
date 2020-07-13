using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace ProcedureParsing.Commands {
    public static partial class CommandProcessor {
        private static IEnumerable<Command> ReactionOfSet(ProcedureParser context,  string target,string subTarget) {
            

            List<Command> ret = new List<Command>();
            CustomPath path = new CustomPath(target);
            IProcedureParsable tempTarget=CustomPath.FindAssetWithPath(path);
            if (tempTarget == null) {
                ret.Add(new Command(CommandType.Log, "CannotFoundTarget"));
                return ret;
            }

            if (path.FromLast(1).StartsWith("r_")) {
                tempTarget.Set($"{path.FromLast(1)}/{path.FromLast(0)}",subTarget);
            }
            return null;
        }

        private static IEnumerable<Command> ValidateOfSet(ProcedureParser context, string target, string subTarget) {
            List<Command> ret = new List<Command>();
            CustomPath path = new CustomPath(target);
            IProcedureParsable tempTarget=CustomPath.FindAssetWithPath(path);
            if (tempTarget == null) {
                if (context.WillBeCreated(path.GenerateHigherPath(1))) {
                    Debug.Log($"{path} will be create");
                    return null;
                }
                ret.Add(new Command(CommandType.Log, "CannotFoundTarget"));
                return ret;
            }

            if (tempTarget.Get(path.FromLast(0)) == subTarget) {
                return ret;
            }
            else return null;
        }
    }
}