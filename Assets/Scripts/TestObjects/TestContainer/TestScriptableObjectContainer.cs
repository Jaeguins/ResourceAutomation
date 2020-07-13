using System;
using System.Collections.Generic;
using ProcedureParsing;
using ProcedureParsing.Commands;
using ProcedureParsing.Containers;
using TestObjects.TestObjects;

namespace TestObjects.TestContainer {

    [Serializable]
    public class TestScriptableObjectContainer : ContainerFactory {

        public const string IdIntValue = "r_IntValue";
        public const string IdStringValue= "r_StringValue";
        public const string IdFloatValuesLength = "r_FloatValues_L";
        public const string IdFloatValues= "r_FloatValues";
        public const string IdSubObject= "r_SubObject";





        private const string _extension = ".asset";
        public TestSubScriptableObjectContainer SubObject;
        public string Name;
        public int IntValue;
        public string StringValue;
        public List<float> FloatValues;
        public override CustomPath GetReferencePath(CustomPath path) {
            return path.GenerateLowerPath(Name + _extension);
        }
        public override IEnumerable<Command> GenerateCommand(CustomPath path) {
            List<Command> ret = new List<Command>();

            CustomPath objPath = GetReferencePath(path);
            ret.Add(new Command(CommandType.Create, nameof(TestScriptableObject), objPath.FullPath));
            
            ret.Add(new Command(CommandType.Set,objPath.GenerateLowerPath(IdIntValue).FullPath,IntValue.ToString()));
            ret.Add(new Command(CommandType.Set,objPath.GenerateLowerPath(IdStringValue).FullPath,StringValue));
            ret.Add(new Command(CommandType.Set,objPath.GenerateLowerPath(IdFloatValuesLength).FullPath,FloatValues.Count.ToString()));
            for(int i=0;i<FloatValues.Capacity;i++){
                ret.Add(new Command(CommandType.Set,objPath.GenerateLowerPath($"{IdFloatValues}/{i}").FullPath,FloatValues[i].ToString()));
            }
            ret.AddRange(SubObject.GenerateCommand(path));
            ret.Add(new Command(CommandType.Set,objPath.GenerateLowerPath(IdSubObject).FullPath,SubObject.GetReferencePath(path).FullPath));
            return ret;
        }
    }

}