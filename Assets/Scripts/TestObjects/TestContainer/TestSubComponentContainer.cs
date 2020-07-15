using System;
using System.Collections.Generic;
using ProcedureParsing;
using ProcedureParsing.Commands;
using ProcedureParsing.Containers;
using TestObjects.TestObjects;

namespace TestObjects.TestContainer {

    [Serializable]
    public class TestSubComponentContainer : JsonParsed {

        public const string IdStringValue = "r_StringValue";
        public const string IdIntValue= "r_IntValue";

        public const string IdDataBool = "r_data_BoolValue";
        public const string IdDataStr= "r_data_StrValue";


        private const string _reference = "o_SubObject/c_TestSubComponent";
        public string Name,
                      StringValue;
        public int IntValue;
        public TestStruct DataSet;
        public override IEnumerable<Command> GenerateCommand(CustomPath path) {
            List<Command> ret=new List<Command>();
            CustomPath refPath = GetReferencePath(path);
            ret.Add(new Command(DefaultCommandType.Set,refPath.GenerateLowerPath(IdStringValue).FullPath,StringValue));
            ret.Add(new Command(DefaultCommandType.Set,refPath.GenerateLowerPath(IdIntValue).FullPath,IntValue.ToString()));
            
            ret.Add(new Command(DefaultCommandType.Set,refPath.GenerateLowerPath(IdDataBool).FullPath,DataSet.BoolVal.ToString()));
            ret.Add(new Command(DefaultCommandType.Set,refPath.GenerateLowerPath(IdDataStr).FullPath,DataSet.StrVal));
            return ret;
        }
        public override CustomPath GetReferencePath(CustomPath path) {
            return path.GenerateLowerPath(_reference);
        }
    }

}