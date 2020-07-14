using UnityEditor;
using UnityEngine;

namespace ProcedureParsing.EditorUI {
    public class ProcedurePreviewer : EditorWindow {
        ProcedureParser _parser = new ProcedureParser();
        private TextAsset temp;
        private bool rawCommandShow;
        private Vector2 scrollPos;

        void OnGUI() {
            
            temp = (TextAsset) EditorGUILayout.ObjectField(temp, typeof(TextAsset), false);
            if (GUILayout.Button("Preview")) {
                _parser.Import(temp.text);
            }
            if (GUILayout.Button("Apply")) {
                _parser.Apply();
            }
            scrollPos=EditorGUILayout.BeginScrollView(scrollPos);
            if (_parser.Command != null) {
                rawCommandShow = EditorGUILayout.Foldout(rawCommandShow, "RawCommands");
                if (rawCommandShow) {
                    foreach (var com in _parser.Command) {
                        EditorGUILayout.LabelField(com.ToString());
                    }
                }
            }
            EditorGUILayout.EndScrollView();
            
        }

        [MenuItem("ProvisGames/데이터시트 임포터")]
        static void Init() {
            ProcedurePreviewer window = (ProcedurePreviewer) GetWindow(typeof(ProcedurePreviewer));
            window.Show();
        }
    }
}