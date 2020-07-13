using ProcedureParsing.Commands;
using UnityEngine;

namespace TestObjects {

    [CreateAssetMenu(fileName = "TestSubScriptableObject1", menuName = "Test/TestSubScriptableObject")]
    public class TestSubScriptableObject : ScriptableObject ,IProcedureParsable{
        public string StrValue;
        public void Set(string location, string value) {
            switch (location) {
                
            }
        }
    }

}