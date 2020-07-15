using System.Collections.Generic;
using System.IO;
using ProcedureParsing;
using ProcedureParsing.Commands;
using TestObjects.TestContainer;
using UnityEngine;

namespace TestObjects.TestObjects {

    [CreateAssetMenu(fileName = "TestScriptableObject1", menuName = "Test/TestScriptableObject")]
    public partial class TestScriptableObject : ScriptableObject{
        public int IntValue;
        public string StringValue;
        public List<float> FloatValues;
        public TestSubScriptableObject SubObject;
    }
    public partial class TestScriptableObject:IProcedureParsable{

        public void Set(string location, string value) {
            string[] parsed = location.Split('/');
            switch (parsed[0]) {
                case TestScriptableObjectContainer.IdStringValue:
                {
                    StringValue = value;
                }
                    break;
                case TestScriptableObjectContainer.IdIntValue:
                {
                    IntValue = int.Parse(value);
                }
                    break;
                case TestScriptableObjectContainer.IdFloatValuesLength:
                {
                    int length = int.Parse(value);
                    Debug.Log($"Parsed Length = {length}");
                    float[] temp = new float[length];
                    FloatValues = new List<float>();
                    FloatValues.AddRange(temp);
                }
                    break;
                case TestScriptableObjectContainer.IdFloatValues:
                {
                    int index = int.Parse(parsed[1]);
                    if (index < FloatValues.Count) FloatValues[index] = float.Parse(value);
                }
                    break;
                case TestScriptableObjectContainer.IdSubObject:
                {
                    SubObject = (TestSubScriptableObject) CustomPath.FindAssetWithPath(new CustomPath(value));
                }
                    break;

                default:
                    throw new InvalidDataException();
            }
        }

        public string Get(string location) {
            string[] parsed = location.Split('/');
            switch (parsed[0]) {
                case TestScriptableObjectContainer.IdStringValue:
                    return StringValue;
                case TestScriptableObjectContainer.IdIntValue:
                    return IntValue.ToString();
                case TestScriptableObjectContainer.IdFloatValuesLength:
                    return FloatValues.Count.ToString();
                case TestScriptableObjectContainer.IdFloatValues:
                    return FloatValues[int.Parse(parsed[1])].ToString();
                case TestScriptableObjectContainer.IdSubObject:
                    return UnityEditor.AssetDatabase.GetAssetPath(SubObject);
                default:
                    Debug.Log(location);
                    throw new InvalidDataException();
            }
        }

        public void InitializeAsset() { }
    }

}