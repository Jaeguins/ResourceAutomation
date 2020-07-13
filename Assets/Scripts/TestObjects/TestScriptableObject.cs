using System.Collections.Generic;
using ProcedureParsing.Commands;
using UnityEngine;

namespace TestObjects {
    [CreateAssetMenu(fileName="TestScriptableObject1",menuName="Test/TestScriptableObject")]
    public class TestScriptableObject :ScriptableObject ,IProcedureParsable{
        public int IntValue;
        public string StringValue;
        public List<float> FloatValues;
        public TestSubScriptableObject SubObject;

        public void Set(string location, string value) {
            switch (location) {

            }
        }
    }

}