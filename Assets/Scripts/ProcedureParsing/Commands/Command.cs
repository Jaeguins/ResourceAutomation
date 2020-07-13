using System.Collections.Generic;

namespace ProcedureParsing.Commands {

    public struct Command {
        public CommandType Type;
        public string Target,
                      SubTarget;
        public int Priority;
        public Command(CommandType type, string target, string subtarget=default, int priority = 0) {
            Type = type;
            Target = target;
            SubTarget = subtarget;
            Priority = priority;
        }
        public override string ToString() {
            return $"({Type}) ({Target}) ({SubTarget}) ({Priority})";
        }
    }

    public delegate IEnumerable<Command> CommandProcess(ProcedureParser context, string target, string subTarget);

    
}