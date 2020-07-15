using System.Collections.Generic;

namespace ProcedureParsing.Commands.Reactions {

    public class LogCommand:ICommand {
        public IEnumerable<Command> DoNothing(ProcedureParser context, string Target, string Subtarget) {
            return null;
        }
        public CommandProcess Reaction => DoNothing;
        public CommandProcess Validation => DoNothing;
        public int GetPriority => -1;
    }

}