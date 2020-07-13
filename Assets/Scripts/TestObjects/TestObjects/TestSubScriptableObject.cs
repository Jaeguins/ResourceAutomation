using System.IO;
using ProcedureParsing.Commands;
using UnityEngine;

namespace TestObjects.TestObjects {
    [CreateAssetMenu(fileName = "TestSubScriptableObject1", menuName = "Test/TestSubScriptableObject")]
    public class TestSubScriptableObject : ScriptableObject, IProcedureParsable {
        public string StrValue;

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

        public void Initialize() { }
    }
}