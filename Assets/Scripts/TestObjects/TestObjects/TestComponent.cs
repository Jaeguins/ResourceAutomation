using System.Collections.Generic;
using System.IO;
using ProcedureParsing.Commands;
using TestObjects.TestContainer;
using UnityEditor;
using UnityEngine;

namespace TestObjects.TestObjects {
    public class TestComponent : MonoBehaviour, IProcedureParsable {
        public int IntValue;
        public string StringValue;
        public List<float> FloatValues=new List<float>();
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
                    int index = int.Parse(parsed[1]);
                    if(index<FloatValues.Count)
                        FloatValues[index] = float.Parse(value);
                    break;
                default:
                    throw new InvalidDataException(location);
            }
        }

        public string Get(string location) {
            string[] parsed = location.Split('/');
            switch (parsed[0]) {
                case TestComponentContainer.IdColCenterX:
                    return Collider.center.x.ToString();
                case TestComponentContainer.IdColCenterY:
                    return Collider.center.y.ToString();
                case TestComponentContainer.IdColCenterZ:
                    return Collider.center.z.ToString();
                    
                case TestComponentContainer.IdExtentX:
                    return (Collider.size.x/2).ToString();
                case TestComponentContainer.IdExtentY:
                    return (Collider.size.y/2).ToString();
                case TestComponentContainer.IdExtentZ:
                    return (Collider.size.z/2).ToString();
                case TestComponentContainer.IdStringValue:
                    return StringValue;
                case TestComponentContainer.IdIntValue:
                    return IntValue.ToString();
                case TestComponentContainer.IdFloatValuesLength:
                    return FloatValues.Count.ToString();
                case TestComponentContainer.IdFloatValues:
                    return FloatValues[int.Parse(parsed[1])].ToString();
                default:
                    throw new InvalidDataException(location);
            }
        }

        public void InitializeAsset() {
            Collider = gameObject.AddComponent<BoxCollider>();
            TestSubComponent sub=new GameObject("SubObject").AddComponent<TestSubComponent>();
            sub.transform.parent = transform;
            SubComponent = sub;
        }
    }
}