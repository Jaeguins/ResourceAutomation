using System;
using System.ComponentModel;

namespace ProcedureParsing.Commands {

    public static class CommandProcessor {
        private static ICommand[] _commands = {null, new CreateCommand(), new MoveCommand(), null, new SetCommand()};
        public static CommandProcess GetReaction(CommandType type) {
            return _commands[(int) type].Reaction;
        }
        public static CommandProcess GetValidation(CommandType type) {
            return _commands[(int) type].Validation;
        }

        public static int GetPriority(CommandType type) {
            switch (type) {
                case CommandType.Create:
                    return 1;
                case CommandType.Move:
                    return 0;
                case CommandType.Set:
                    return 2;
                case CommandType.Log:
                    return -1;
                default:
                    throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(CommandType));
            }
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