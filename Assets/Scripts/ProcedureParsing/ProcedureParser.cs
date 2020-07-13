using System.Collections.Generic;
using ProcedureParsing.Commands;
using ProcedureParsing.Containers;
using ProcedureParsing.Logger;
using UnityEngine;

namespace ProcedureParsing {

    public class ProcedureParser {
        private ProcedureLogger _logger;
        private List<Command> _commands;
        public List<Command> Command=>_commands;
        public ProcedureParser() {
            _logger = new ProcedureLogger();
        }

        public void Import(string json) {
            Parse(json);
            Validate();
        }

        private void Sort() {
            _commands.Sort(CommandProcessor.CompareCommand);
        }
        private void Parse(string json) {
            _commands = new List<Command>();
            JsonContainer container=JsonUtility.FromJson<JsonContainer>(json);
            _commands.AddRange(container.GenerateCommand(null));
            Sort();
        }

        private void Validate() {
            List<Command> validated = new List<Command>();
            foreach (Command com in _commands) {
                Command[] resultBuffer = CommandProcessor.GetValidation(com.Type)(com.Target, com.SubTarget);
                if (resultBuffer == null) {
                    validated.Add(com);
                } else {
                    foreach (Command t in resultBuffer) {
                        validated.Add(t);
                    }
                }
            }
            Sort();
        }
        public void Reset() {
            _commands = null;
        }
        public void Apply() {
            if (_commands == null) {
                Debug.LogWarning("Nothing in queue.");
                return;
            }
            _logger.Reopen($"Procedure_{System.DateTime.Now:yyyy-MM-dd-HH-mm-ss}.log");

            foreach (Command com in _commands) {
                Command[] resultBuffer = CommandProcessor.GetReaction(com.Type)(com.Target, com.SubTarget);
                if (resultBuffer.Length > 0 && resultBuffer[0].Type == CommandType.Log) {
                    Debug.Log(resultBuffer[0].Target);
                    _logger.WriteLog(resultBuffer[0].Target);
                }
            }
            _logger.Close();
        }
    }

}