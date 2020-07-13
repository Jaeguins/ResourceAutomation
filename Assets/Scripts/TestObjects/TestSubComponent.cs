using ProcedureParsing.Commands;
using UnityEngine;

namespace TestObjects {

    public class TestSubComponent :MonoBehaviour,IProcedureParsable{
        public int IntValue;
        public string StringValue;
        public TestStruct DataSet;
        public void Set(string location, string value) {
            switch (location) {

            }
        }
    }

}