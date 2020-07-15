using System.Collections.Generic;

namespace ProcedureParsing.Commands {

    public class SetCommand:ICommand{
        private static IEnumerable<Command> ReactionOfSet(ProcedureParser context, string target, string subTarget) {
            List<Command> ret = new List<Command>();
            CustomPath path = new CustomPath(target);
            IProcedureParsable tempTarget = CustomPath.FindAssetWithPath(path);
            if (tempTarget == null) {
                ret.Add(new Command(CommandType.Log, "CannotFoundTarget", $"Set {target} into {subTarget}"));
                return ret;
            }

            if (path.FromLast(1).StartsWith(CustomPath.RefPrefix)) {
                tempTarget.Set($"{path.FromLast(1)}/{path.FromLast(0)}", subTarget);
            } else {
                tempTarget.Set(path.FromLast(0), subTarget);
            }
            return null;
        }

        private static IEnumerable<Command> ValidateOfSet(ProcedureParser context, string target, string subTarget) {
            List<Command> ret = new List<Command>();
            ret.Add(new Command(CommandType.Set, target, subTarget));
            CustomPath path = new CustomPath(target);
            IProcedureParsable tempTarget = CustomPath.FindAssetWithPath(path);
            if (tempTarget == null) {
                if (context.WillBeCreated(new CustomPath(path.FilePath))) {
                    Command buff = ret[0];
                    buff.PastValue = Command.NaN;
                    ret[0] = buff;
                    return ret;
                }
                ret.Clear();
                ret.Add(new Command(CommandType.Log, "CannotFoundTarget", $"Set {target} into {subTarget}"));
                return ret;
            }
            if (path.FromLast(1).StartsWith(CustomPath.RefPrefix)) {
                string tempValue = tempTarget.Get($"{path.FromLast(1)}/{path.FromLast(0)}");
                if (tempValue == subTarget) {
                    ret.Clear();
                } else {
                    Command buff = ret[0];
                    buff.PastValue = tempValue;
                    ret[0] = buff;
                }
            } else {
                string tempValue = tempTarget.Get(path.FromLast(0));
                if (tempValue == subTarget) {
                    ret.Clear();
                } else {
                    Command buff = ret[0];
                    buff.PastValue = tempValue;
                    ret[0] = buff;
                }
            }
            return ret;
        }
        public CommandProcess Reaction => ReactionOfSet;
        public CommandProcess Validation => ValidateOfSet;
    }

}