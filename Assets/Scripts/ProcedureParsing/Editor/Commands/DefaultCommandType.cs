namespace ProcedureParsing.Commands {

    public static class DefaultCommandType {
        /// <summary>
        /// Internal temporal type for logging, message is target
        /// </summary>
        public const string Log = "Log";
        /// <summary>
        /// creating asset at target with type in sub-target
        /// </summary>
        public const string Create = "Create";
        /// <summary>
        /// moving asset at target to sub-target
        /// </summary>
        public const string Move = "Move";
        /// <summary>
        /// set value of target to sub-target
        /// </summary>
        public const string Set = "Set";
    }

}