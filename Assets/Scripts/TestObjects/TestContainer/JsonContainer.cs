﻿using System;
using System.Collections.Generic;
using ProcedureParsing;
using ProcedureParsing.Commands;
using ProcedureParsing.Containers;

namespace TestObjects.TestContainer {

    [Serializable]
    public class JsonContainer : JsonParsed { //Root Json Parsed Target
        private const string _basePath = "Assets/Resources/TestContainerDirectory";

        public List<TestScriptableObjectContainer> TestScriptableObjects;
        public List<TestComponentContainer> TestPrefabs;

        public override IEnumerable<Command> GenerateCommand(CustomPath path) {
            List<Command> ret = new List<Command>();
            CustomPath refPath = GetReferencePath(path);
            foreach (TestScriptableObjectContainer t in TestScriptableObjects) {
                ret.AddRange(t.GenerateCommand(refPath));
            }
            foreach (TestComponentContainer t in TestPrefabs) {
                ret.AddRange(t.GenerateCommand(refPath));
            }
            return ret;
        }
        public override CustomPath GetReferencePath(CustomPath path) {
            return new CustomPath(_basePath);
        }
    }

}