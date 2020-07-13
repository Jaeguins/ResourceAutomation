using System;
using System.Collections.Generic;
using UnityEditor;

namespace ProcedureParsing.Commands {

    public static partial class CommandProcessor {
        private static IEnumerable<Command> ReactionOfMove(ProcedureParser context, string target, string subTarget) {
            AssetDatabase.MoveAsset(target, subTarget);
            context.ChangeAllCommandPath(target, subTarget);
            return null;
        }
        private static IEnumerable<Command> ValidateOfMove(ProcedureParser context, string target, string subTarget) {
            return null;
        }
    }

}