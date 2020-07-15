using System.IO;
using UnityEngine;
#if UNITY_EDITOR

#endif

namespace ProcedureParsing.Logger {

    public class ProcedureLogger {
        private const string _editorFolder = "Editor",
                             _logFolder = "Log";
        public bool IsOpen => _isOpen;
        private string _name;
        private bool _isOpen;
        private StreamWriter _writer;
        private StreamReader _reader;
        private FileStream _fileStream = null;
        public void Reopen(string name) {
            if (_fileStream != null) Close();
            string path = Path.Combine(Application.persistentDataPath, _editorFolder, _logFolder);
            DirectoryInfo di = Directory.CreateDirectory(path);
            _fileStream = File.Create(Path.Combine(path, name));
            _writer = new StreamWriter(_fileStream);
            _reader = new StreamReader(_fileStream);

            _isOpen = true;
        }

        public void WriteLog(string msg) {
            if (!_isOpen) {
                Debug.LogWarning("Log File Not Open");
                return;
            }
            _writer.WriteLine($"{System.DateTime.Now:T} : {msg}");
        }

        public void Close() {
            _writer?.Close();
            _reader?.Close();
            _fileStream.Close();
            _fileStream = null;
            _isOpen = false;
        }
    }

}