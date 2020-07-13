using System.Collections.Generic;
using System.IO;
using ProcedureParsing.Commands;
using TestObjects.TestContainer;
using UnityEngine;

namespace TestObjects.TestObjects {
    public class TestComponent : MonoBehaviour, IProcedureParsable {
        public int IntValue;
        public string StringValue;
        public List<float> FloatValues;
        public BoxCollider Collider;
        public TestSubComponent SubComponent;

        public void Set(string location, string value) {
            string[] parsed = location.Split('/');
            switch (parsed[0]) {
                case TestComponentContainer.IdColCenterX: {
                    Vector3 center = Collider.center;
                    center.x = float.Parse(value);
                    Collider.center = center;
                }
                    break;
                case TestComponentContainer.IdColCenterY: {
                    Vector3 center = Collider.center;
                    center.y = float.Parse(value);
                    Collider.center = center;
                }
                    break;
                case TestComponentContainer.IdColCenterZ: {
                    Vector3 center = Collider.center;
                    center.z = float.Parse(value);
                    Collider.center = center;
                }
                    break;
                case TestComponentContainer.IdExtentX:
                {
                    Vector3 size = Collider.size;
                    size.x = float.Parse(value)*2;
                    Collider.size= size;
                }
                    break;
                case TestComponentContainer.IdExtentY:
                {
                    Vector3 size = Collider.size;
                    size.y = float.Parse(value)*2;
                    Collider.size= size;
                }
                    break;
                case TestComponentContainer.IdExtentZ:
                {
                    Vector3 size = Collider.size;
                    size.z = float.Parse(value)*2;
                    Collider.size= size;
                }
                    break;
                case TestComponentContainer.IdStringValue:
                    StringValue = value;
                    break;
                case TestComponentContainer.IdIntValue:
                    IntValue = int.Parse(value);
                    break;
                case TestComponentContainer.IdFloatValuesLength:
                    float[] con=new float[int.Parse(value)];
                    FloatValues = new List<float>();
                    FloatValues.AddRange(con);
                    break;
                case TestComponentContainer.IdFloatValues:
                    FloatValues[int.Parse(parsed[1])] = int.Parse(value);
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public string Get(string location) {
            switch (location) {
                default:
                    throw new InvalidDataException();
            }
        }

        public void Initialize() { }
    }
}