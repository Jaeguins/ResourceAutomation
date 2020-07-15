using System;
using System.Collections.Generic;
using ProcedureParsing;
using ProcedureParsing.Commands;
using ProcedureParsing.Containers;
using TestObjects.TestObjects;

namespace TestObjects.TestContainer {

    [Serializable]
    public class TestSubScriptableObjectContainer : ContainerFactory {
        public const string IdStrValue = "r_StrValue";
        public string Name,
                      StrValue;
        public override IEnumerable<Command> GenerateCommand(CustomPath path) {
            List<Command> ret = new List<Command>();
            CustomPath objPath = GetReferencePath(path);
            ret.Add(new Command(CommandType.Create,nameof(TestSubScriptableObject),objPath.FullPath));
            ret.Add(new Command(CommandType.Set, objPath.GenerateLowerPath(IdStrValue).FullPath, StrValue));
            return ret;
        }
        public override CustomPath GetReferencePath(CustomPath path) {
            return path.GenerateLowerPath(Name + CustomPath.AssetExtension);
        }
    }

}