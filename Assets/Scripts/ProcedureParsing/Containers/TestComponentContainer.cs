using System;
using System.Collections.Generic;
using ProcedureParsing.Commands;
using TestObjects;

namespace ProcedureParsing.Containers {

    [Serializable]
    public class TestComponentContainer : ContainerFactory {

        public const string IdStringValue="r_StringValue";
        public const string IdIntValue="r_IntValue";
        public const string IdFloatValuesLength="r_FloatValues_L";
        public const string IdFloatValues="r_FloatValues";
        public const string IdColCenterX="r_col_centerX";
        public const string IdColCenterY="r_col_centerY";
        public const string IdColCenterZ="r_col_centerZ";
        public const string IdExtentX="r_col_extentX";
        public const string IdExtentY="r_col_extentY";
        public const string IdExtentZ="r_col_extentZ";
        public const string IdSubComponent = "r_SubComponent";




        private const string _extension = ".prefab";
        public string Name,
                      StringValue;
        public List<float> FloatValues;
        public int IntValue;
        public ColliderStruct Collider;
        public TestSubComponentContainer SubComponent;
        public override IEnumerable<Command> GenerateCommand(CustomPath path) {
            List<Command> ret=new List<Command>();
            CustomPath refPath = path.GenerateLowerPath(Name + _extension);

            ret.Add(new Command(CommandType.Create,nameof(TestComponent),refPath.FullPath));

            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdStringValue).FullPath,StringValue));
            
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdIntValue).FullPath,IntValue.ToString()));
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdFloatValuesLength).FullPath,FloatValues.Count.ToString()));
            for (int i = 0; i < FloatValues.Count; i++) {
                ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath($"{IdFloatValues}/{i}").FullPath,StringValue));
            }

            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdColCenterX).FullPath,Collider.centerX.ToString()));
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdColCenterY).FullPath,Collider.centerY.ToString()));
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdColCenterZ).FullPath,Collider.centerZ.ToString()));
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdExtentX).FullPath,Collider.extentX.ToString()));
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdExtentY).FullPath,Collider.extentY.ToString()));
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdExtentZ).FullPath,Collider.extentZ.ToString()));

            ret.AddRange(SubComponent.GenerateCommand(refPath));
            ret.Add(new Command(CommandType.Set,refPath.GenerateLowerPath(IdSubComponent).FullPath,SubComponent.GetReferencePath(refPath).FullPath));


            return ret;
        }
        public override CustomPath GetReferencePath(CustomPath path) {
            return path.GenerateLowerPath(Name+_extension);
        }

        
    }

    [Serializable]
    public struct ColliderStruct {
        public float centerX,centerY,centerZ,extentX,extentY,extentZ;
    }

}