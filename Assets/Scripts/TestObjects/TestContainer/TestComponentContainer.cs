using System;
using System.Collections.Generic;
using ProcedureParsing;
using ProcedureParsing.Commands;
using ProcedureParsing.Containers;
using TestObjects.TestObjects;
using UnityEngine;

namespace TestObjects.TestContainer {

    [Serializable]
    public class TestComponentContainer : ContainerFactory {

        public const string IdStringValue="r_StringValue";
        public const string IdIntValue="r_IntValue";
        public const string IdFloatValuesLength="r_FloatValues_L";
        public const string IdFloatValues="r_FloatValues_V";
        public const string IdColCenterX="r_col_centerX";
        public const string IdColCenterY="r_col_centerY";
        public const string IdColCenterZ="r_col_centerZ";
        public const string IdExtentX="r_col_extentX";
        public const string IdExtentY="r_col_extentY";
        public const string IdExtentZ="r_col_extentZ";
        public const string IdSubComponent = "r_SubComponent";
        public const string IdComponent = "c_TestComponent";




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
            CustomPath compPath= refPath.GenerateLowerPath(IdComponent);
            ret.Add(new Command(CommandType.Create,nameof(TestComponent),compPath.FullPath));

            ret.Add(new Command(CommandType.Set,compPath.GenerateLowerPath(IdStringValue).FullPath,StringValue));
            
            ret.Add(new Command(CommandType.Set,compPath.GenerateLowerPath(IdIntValue).FullPath,IntValue.ToString()));
            ret.Add(new Command(CommandType.Set,compPath.GenerateLowerPath(IdFloatValuesLength).FullPath,FloatValues.Count.ToString()));
            for (int i = 0; i < FloatValues.Count; i++) {
                ret.Add(new Command(CommandType.Set,compPath.GenerateLowerPath($"{IdFloatValues}/{i}").FullPath,FloatValues[i].ToString()));
            }

            ret.Add(new Command(CommandType.Set,compPath.GenerateLowerPath(IdColCenterX).FullPath,Collider.centerX.ToString()));
            ret.Add(new Command(CommandType.Set,compPath.GenerateLowerPath(IdColCenterY).FullPath,Collider.centerY.ToString()));
            ret.Add(new Command(CommandType.Set,compPath.GenerateLowerPath(IdColCenterZ).FullPath,Collider.centerZ.ToString()));
            ret.Add(new Command(CommandType.Set,compPath.GenerateLowerPath(IdExtentX).FullPath,Collider.extentX.ToString()));
            ret.Add(new Command(CommandType.Set,compPath.GenerateLowerPath(IdExtentY).FullPath,Collider.extentY.ToString()));
            ret.Add(new Command(CommandType.Set,compPath.GenerateLowerPath(IdExtentZ).FullPath,Collider.extentZ.ToString()));

            ret.AddRange(SubComponent.GenerateCommand(refPath));


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