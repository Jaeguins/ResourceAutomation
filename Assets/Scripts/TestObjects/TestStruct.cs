using System;
using ProcedureParsing.Commands;

namespace TestObjects {
    [Serializable]
    public struct TestStruct:IProcedureParsable {
        public bool BoolVal;
        public string StrVal;
        public void Set(string location, string value) {
            switch (location) {

            }
        }
    }

}