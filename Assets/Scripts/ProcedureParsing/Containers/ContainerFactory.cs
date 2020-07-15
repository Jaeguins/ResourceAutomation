﻿using System;
using System.Collections.Generic;
using ProcedureParsing.Commands;

namespace ProcedureParsing.Containers {

    [Serializable]
    public abstract class ContainerFactory {
        public abstract IEnumerable<Command> GenerateCommand(CustomPath path);
        public abstract CustomPath GetReferencePath(CustomPath path);
    }

}