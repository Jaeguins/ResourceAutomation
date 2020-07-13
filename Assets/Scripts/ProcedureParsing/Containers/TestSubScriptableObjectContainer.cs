using System;
using System.Collections.Generic;
using ProcedureParsing.Commands;
using TestObjects;

namespace ProcedureParsing.Containers {

    [Serializable]
    public class TestSubScriptableObjectContainer : ContainerFactory {
        private const string _extension = ".asset";
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
            return path.GenerateLowerPath(Name + _extension);
        }
    }

}