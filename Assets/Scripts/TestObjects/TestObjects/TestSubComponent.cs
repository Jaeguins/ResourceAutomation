using System.IO;
using ProcedureParsing.Commands;
using UnityEngine;

namespace TestObjects.TestObjects {

    public class TestSubComponent :MonoBehaviour,IProcedureParsable{
        public int IntValue;
        public string StringValue;
        public TestStruct DataSet;
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