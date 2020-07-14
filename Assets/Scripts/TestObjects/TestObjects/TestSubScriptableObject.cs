using System.IO;
using ProcedureParsing.Commands;
using TestObjects.TestContainer;
using UnityEngine;

namespace TestObjects.TestObjects {
    [CreateAssetMenu(fileName = "TestSubScriptableObject1", menuName = "Test/TestSubScriptableObject")]
    public class TestSubScriptableObject : ScriptableObject, IProcedureParsable {
        public string StrValue;

        public void Set(string location, string value) {
            string[] parsed = location.Split('/');
            switch (parsed[0]) {
                case TestSubScriptableObjectContainer.IdStrValue:
                    StrValue = value;
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public string Get(string location) {
            string[] parsed = location.Split('/');
            switch (parsed[0]) {
                case TestSubScriptableObjectContainer.IdStrValue:
                    return StrValue;
                default:
                    throw new InvalidDataException();
            }
        }

        public void InitializeAsset() { }
    }
}