using System.Collections.Generic;
using System.Linq;

namespace ProcedureParsing.Commands {

    public struct Command {
        public CommandType Type;
        public string Target,
                      SubTarget,PastValue;
        public int Priority;
        public const string NaN = "NaN";
        public Command(CommandType type, string target, string subtarget=default,string pastValue=default, int priority = 0) {
            Type = type;
            Target = target;
            SubTarget = subtarget;
            Priority = priority;
            PastValue = pastValue;
        }
        public override string ToString() {
            return $"({Type}) ({Target}) ({SubTarget}) ({Priority})";
        }
        public string GenerateTooltipText() {
            string ret = string.Empty;

            switch (Type) {
                case CommandType.Log:
                    ret += $"<color=red>{Target}</color>";
                    break;
                case CommandType.Create:
                    ret += $"<color=green>{Target}</color>";
                    break;
                case CommandType.Move:
                {
                    CustomPath targetPath=new CustomPath(SubTarget);
                    if (PastValue == MoveCommand.MoveTo) {
                        ret += $"<color=#4080FF>{PastValue} {SubTarget}</color>";
                    } else {
                            ret+=$"<color=#4080FF>{PastValue} {Target}</color>";
                    }
                    
                }
                    break;
                case CommandType.Set:
                {
                    CustomPath targetPath=new CustomPath(Target);
                    ret += $"<color={(PastValue==NaN?"green":"black")}>{targetPath.FromLast(0).Split('_').Last()} : {PastValue} -> {SubTarget}</color>";
                }
                    break;
            }
            return ret;
        }
    }

    public delegate IEnumerable<Command> CommandProcess(ProcedureParser context, string target, string subTarget);

    
}