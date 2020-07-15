using System.Collections.Generic;
using UnityEditor;

namespace ProcedureParsing.Commands {

    public class MoveCommand:ICommand{
        public const string MoveTo = "move to";
        public const string MoveFrom = "moved from";
        private IEnumerable<Command> ReactionOfMove(ProcedureParser context, string target, string subTarget) {
            CustomPath targetPath=new CustomPath(target),subTargetPath=new CustomPath(subTarget);
            
            AssetDatabase.MoveAsset(targetPath.FilePath, subTargetPath.FilePath);
            context.ChangeAllCommandPath(targetPath.FilePath, subTargetPath.FilePath);
            return null;
        }
        private IEnumerable<Command> ValidateOfMove(ProcedureParser context, string target, string subTarget) {
            return null;
        }
        public CommandProcess Reaction =>ReactionOfMove;
        public CommandProcess Validation => ValidateOfMove;
    }

}