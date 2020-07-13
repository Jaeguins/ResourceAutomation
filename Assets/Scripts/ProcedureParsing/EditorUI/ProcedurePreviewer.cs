using UnityEditor;
using UnityEngine;

namespace ProcedureParsing.EditorUI {

    public class ProcedurePreviewer : EditorWindow {
        ProcedureParser _parser = new ProcedureParser();
        private TextAsset temp;
        
        void OnGUI() {
            temp = (TextAsset) EditorGUILayout.ObjectField(temp, typeof(TextAsset), false);
            if (GUILayout.Button("Preview")) {
                _parser.Import(temp.text);
            }
            if (_parser.Command != null) {
                foreach (var com in _parser.Command) {
                    EditorGUILayout.LabelField(com.ToString());
                }
            }
            

        }
        [MenuItem("ProvisGames/데이터시트 임포터")]
        static void Init() {
            ProcedurePreviewer window = (ProcedurePreviewer) GetWindow(typeof(ProcedurePreviewer));
            window.Show();
        }
    }

}