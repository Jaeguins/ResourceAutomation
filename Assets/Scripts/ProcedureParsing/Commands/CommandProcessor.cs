using System;
using System.Collections;
using System.ComponentModel;
using static System.String;

namespace ProcedureParsing.Commands {

    public static partial class CommandProcessor {
        public static CommandProcess GetReaction(CommandType type) {
            switch (type) {
                case CommandType.Create:
                    return ReactionOfCreate;
                case CommandType.Move:
                    return ReactionOfMove;
                case CommandType.Set:
                    return ReactionOfSet;
                default:
                    throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(CommandType));
            }
        }
        public static CommandProcess GetValidation(CommandType type) {
            switch (type) {
                case CommandType.Create:
                    return ValidateOfCreate;
                case CommandType.Move:
                    return ValidateOfMove;
                case CommandType.Set:
                    return ValidateOfSet;
                default:
                    throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(CommandType));
            }
        }

        public static int GetPriority(CommandType type) {
            switch (type) {
                case CommandType.Create:
                    return 1;
                case CommandType.Move:
                    return 0;
                case CommandType.Set:
                    return 2;
                default:
                    throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(CommandType));
            }
        }

        public static int CompareCommand(Command x, Command y) { //positive when x is bigger
            int diff = GetPriority(x.Type) - GetPriority(y.Type);
            if (diff != 0) return diff;
            diff = x.Priority - y.Priority;
            if (diff != 0) return diff;
            diff = Compare(x.Target, y.Target, StringComparison.Ordinal);
            if (diff != 0) return diff;

            return Compare(x.SubTarget, y.SubTarget, StringComparison.Ordinal);

            

        }
    }

}