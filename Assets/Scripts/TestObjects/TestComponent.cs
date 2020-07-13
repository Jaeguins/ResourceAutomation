using System.Collections.Generic;
using ProcedureParsing.Commands;
using UnityEngine;

namespace TestObjects {

    public class TestComponent :MonoBehaviour,IProcedureParsable{
        public int IntValue;
        public string StringValue;
        public List<float> FloatValues;
        public BoxCollider Collider;
        public TestSubComponent SubComponent;
        public void Set(string location, string value) {
            switch (location) {

            }
        }
    }

}