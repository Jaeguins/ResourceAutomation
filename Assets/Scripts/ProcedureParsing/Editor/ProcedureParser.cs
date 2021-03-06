﻿using System.Collections.Generic;
using ProcedureParsing.Commands;
using ProcedureParsing.Commands.Reactions;
using ProcedureParsing.Containers;
using ProcedureParsing.Logger;
using UnityEngine;

namespace ProcedureParsing {

    public class ProcedureParser {
        private ProcedureLogger _logger;
        private List<Command> _commands;
        public List<Command> Command => _commands;
        public ProcedureParser() {
            _logger = new ProcedureLogger();
            Dictionary<string, ICommand> commandMap = new Dictionary<string, ICommand>();
            commandMap.Add(DefaultCommandType.Log,new LogCommand());
            commandMap.Add(DefaultCommandType.Create,new CreateCommand());
            commandMap.Add(DefaultCommandType.Move,new MoveCommand());
            commandMap.Add(DefaultCommandType.Set,new SetCommand());
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
            JsonParsed container = JsonUtility.FromJson<JsonParsed>(json);
            _commands.AddRange(container.GenerateCommand(null));
            Sort();
        }

        private void Validate() {
            List<Command> validated = new List<Command>();
            for (int index = 0; index < _commands.Count; index++) {
                Command com = _commands[index];
                IEnumerable<Command> resultBuffer = CommandProcessor.GetValidation(com.Type)(this, com.Target, com.SubTarget);
                if (resultBuffer == null) {
                    validated.Add(com);
                } else {
                    foreach (Command t in resultBuffer) {
                        validated.Add(t);
                    }
                }
            }
            _commands = validated;
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

            for (int index = 0; index < _commands.Count; index++) {
                Command com = _commands[index];
                List<Command> resultBuffer = new List<Command>();
                IEnumerable<Command> reactions = CommandProcessor.GetReaction(com.Type)(this, com.Target, com.SubTarget);
                if (reactions != null) resultBuffer.AddRange(reactions);
                if (resultBuffer.Count > 0 && resultBuffer[0].Type == DefaultCommandType.Log) {
                    Debug.Log(resultBuffer[0].Target);
                    _logger.WriteLog(resultBuffer[0].Target);
                }
            }

            _logger.Close();
        }

        public void ChangeAllCommandPath(string oldVal, string newVal) {
            for (int index = 0; index < _commands.Count; index++) {
                Command temp = _commands[index];
                temp.Target = temp.Target.Replace(oldVal, newVal);
                temp.SubTarget = temp.SubTarget.Replace(oldVal, newVal);
                _commands[index] = temp;
            }
        }

        public bool WillBeCreated(CustomPath Path) {
            foreach (Command t in _commands) {
                if (t.Type == DefaultCommandType.Create && t.SubTarget.Contains(Path.FullPath)) {
                    return true;
                }
            }

            return false;
        }
    }

}