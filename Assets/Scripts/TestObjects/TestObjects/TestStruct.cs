using System;
using System.IO;
using ProcedureParsing.Commands;

namespace TestObjects.TestObjects {
    [Serializable]
    public struct TestStruct:IProcedureParsable {
        public bool BoolVal;
        public string StrVal;
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