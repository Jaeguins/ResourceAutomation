using System.Collections.Generic;
using System.IO;
using ProcedureParsing.Commands;
using UnityEngine;

namespace TestObjects.TestObjects {
    [CreateAssetMenu(fileName="TestScriptableObject1",menuName="Test/TestScriptableObject")]
    public class TestScriptableObject :ScriptableObject ,IProcedureParsable{
        public int IntValue;
        public string StringValue;
        public List<float> FloatValues;
        public TestSubScriptableObject SubObject;

        public void Set(string location, string value) {
            switch (location) {
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

        public void Initialize() {
            
        }
    }

}