using System.Collections.Generic;

namespace ProcedureParsing.Commands {

    public interface ICommand {
        CommandProcess Reaction { get; }
        CommandProcess Validation { get; }
        int GetPriority { get; }
    }
    public struct Command {
        public string Type;
        public string Target,
                      SubTarget,PastValue;
        public int Priority;
        public const string NaN = "NaN";
        public Command(string type, string target, string subtarget=default,string pastValue=default, int priority = 0) {
            Type = type;
            Target = target;
            SubTarget = subtarget;
            Priority = priority;
            PastValue = pastValue;
        }
        public override string ToString() {
            return $"({Type}) ({Target}) ({SubTarget}) ({Priority})";
        }
        
    }

    public delegate IEnumerable<Command> CommandProcess(ProcedureParser context, string target, string subTarget);

}