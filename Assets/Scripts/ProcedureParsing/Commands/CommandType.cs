﻿namespace ProcedureParsing.Commands {

    public enum CommandType {
        /// <summary>
        /// Internal temporal type for logging
        /// </summary>
        Log = 0,
        /// <summary>
        /// creating asset at target with type in sub-target
        /// </summary>
        Create = 1,
        /// <summary>
        /// moving asset at target to sub-target
        /// </summary>
        Move = 2,
        /// <summary>
        /// deleting asset at target
        /// </summary>
        Delete = 3,
        /// <summary>
        /// set value of target to sub-target
        /// </summary>
        Set = 4
    }

}