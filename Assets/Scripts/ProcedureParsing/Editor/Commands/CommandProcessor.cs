using System;
using System.Collections.Generic;

namespace ProcedureParsing.Commands {

    public class CommandProcessor {
        private static Dictionary<string, ICommand> _commands;
        public static void SetCommands(Dictionary<string, ICommand> map) {
            _commands = map;
        }
        public static CommandProcess GetReaction(string type) {
            return _commands[type].Reaction;
        }
        public static CommandProcess GetValidation(string type) {
            return _commands[type].Validation;
        }

        public static int GetPriority(string type) {
            return _commands[type].GetPriority;
        }

        public static int CompareCommand(Command x, Command y) { //positive when x is bigger
            int diff = GetPriority(x.Type) - GetPriority(y.Type);
            if (diff != 0) return diff;
            diff = x.Priority - y.Priority;
            if (diff != 0) return diff;
            diff = string.Compare(x.Target, y.Target, StringComparison.Ordinal);
            if (diff != 0) return diff;

            return string.Compare(x.SubTarget, y.SubTarget, StringComparison.Ordinal);

            

        }
    }

}