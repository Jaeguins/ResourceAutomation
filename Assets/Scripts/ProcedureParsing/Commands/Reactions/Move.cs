using System.Collections.Generic;
using UnityEditor;

namespace ProcedureParsing.Commands {

    public static partial class CommandProcessor {
        public const string MoveTo = "move to";
        public const string MoveFrom = "moved from";
        private static IEnumerable<Command> ReactionOfMove(ProcedureParser context, string target, string subTarget) {
            CustomPath targetPath=new CustomPath(target),subTargetPath=new CustomPath(subTarget);
            
            AssetDatabase.MoveAsset(targetPath.FilePath, subTargetPath.FilePath);
            context.ChangeAllCommandPath(targetPath.FilePath, subTargetPath.FilePath);
            return null;
        }
        private static IEnumerable<Command> ValidateOfMove(ProcedureParser context, string target, string subTarget) {
            return null;
        }
    }

}