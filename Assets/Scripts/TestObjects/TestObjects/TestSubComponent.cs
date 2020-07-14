using System.IO;
using ProcedureParsing.Commands;
using TestObjects.TestContainer;
using UnityEngine;

namespace TestObjects.TestObjects {

    public class TestSubComponent :MonoBehaviour,IProcedureParsable{
        public int IntValue;
        public string StringValue;
        public TestStruct DataSet;
        public void Set(string location, string value) {
            string[] parsed = location.Split('/');
            switch (parsed[0]) {
                case TestSubComponentContainer.IdStringValue:
                    StringValue = value;
                    break;
                case TestSubComponentContainer.IdIntValue:
                    IntValue = int.Parse(value);
                    break;
                case TestSubComponentContainer.IdDataBool:
                    DataSet.BoolVal= bool.Parse(value);
                    break;
                case TestSubComponentContainer.IdDataStr:
                    DataSet.StrVal= value;
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public string Get(string location) {
            switch (location) {
                case TestSubComponentContainer.IdStringValue:
                    return StringValue;
                case TestSubComponentContainer.IdIntValue:
                    return IntValue.ToString();
                case TestSubComponentContainer.IdDataBool:
                    return DataSet.BoolVal.ToString();
                case TestSubComponentContainer.IdDataStr:
                    return DataSet.StrVal;
                default:
                    throw new InvalidDataException();
            }
        }

        public void InitializeAsset() {
            
        }
    }

}