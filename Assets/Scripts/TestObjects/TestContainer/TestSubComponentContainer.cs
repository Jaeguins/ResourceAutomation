using System;
using System.Collections.Generic;
using ProcedureParsing;
using ProcedureParsing.Commands;
using ProcedureParsing.Containers;
using TestObjects.TestObjects;

namespace TestObjects.TestContainer {

    [Serializable]
    public class TestSubComponentContainer : ContainerFactory {

        public const string IdName = "r_Name";
        public const string IdStringValue = "r_StringValue";
        public const string IdIntValue= "r_IntValue";
        public const string IdTestBool = "r_test_BoolValue";
        public const string IdTestStr= "r_test_StrValue";


        private const string _reference = "o_SubObject/c_SubComponent";
        public string Name,
                      StringValue;
        public int IntValue;
        public TestStruct DataSet;
        public override IEnumerable<Command> GenerateCommand(CustomPath path) {
            List<Command> ret=new List<Command>();
            CustomPath refPath = GetReferencePath(path);
            ret.Add(new Command(CommandType.Create,nameof(TestSubComponent),refPath.FullPath));
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdName).FullPath,Name));
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdStringValue).FullPath,StringValue));
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdIntValue).FullPath,IntValue.ToString()));
            
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdTestBool).FullPath,DataSet.BoolVal.ToString()));
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdTestStr).FullPath,DataSet.StrVal));

            return ret;
        }
        public override CustomPath GetReferencePath(CustomPath path) {
            return path.GenerateLowerPath(_reference);
        }
    }

}