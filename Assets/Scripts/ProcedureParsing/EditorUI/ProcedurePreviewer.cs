using System;
using System.Collections.Generic;
using System.Linq;
using ProcedureParsing.Commands;
using UnityEditor;
using UnityEngine;

namespace ProcedureParsing.EditorUI {

    public class ProcedurePreviewer : EditorWindow {
        ProcedureParser _parser = new ProcedureParser();
        private TextAsset _temp;
        private bool _rawCommandShow=false;
        private Vector2 _scrollPos;
        private FileTree _tree;
        private const string _rootDir = "Assets";
        private const string _previewText = "Preview";
        private const string _applyText = "Apply";
        public static GUIStyle PreviewerStyle;
        void OnGUI() {
            if (PreviewerStyle == null) {
                PreviewerStyle = new GUIStyle(GUI.skin.label);
                PreviewerStyle.richText = true;
                PreviewerStyle.imagePosition = ImagePosition.ImageLeft;
            }
            EditorGUIUtility.SetIconSize(Vector2.one*16);
            _temp = (TextAsset) EditorGUILayout.ObjectField(_temp, typeof(TextAsset), false);
            
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button(_previewText)) {
                _parser.Import(_temp.text);
                UpdateTree();
            }
            if (GUILayout.Button(_applyText)) {
                _parser.Apply();
            }
            GUILayout.EndHorizontal();
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            


            if (_parser.Command != null) {
                if (_tree != null) {
                    _tree.GetRootNode().Draw();
                }


                EditorGUI.indentLevel = 0;
                _rawCommandShow = EditorGUILayout.Foldout(_rawCommandShow, "RawCommands");
                if (_rawCommandShow) {
                    foreach (var com in _parser.Command) {
                        EditorGUILayout.LabelField(com.ToString());
                    }
                }
            }
            
            EditorGUILayout.EndScrollView();
            EditorGUIUtility.SetIconSize(Vector2.zero);
        }
        
        private void UpdateTree() {
            _tree = new FileTree(_rootDir, new FileNode(_rootDir));
            foreach (Command t in _parser.Command) {
                _tree.AddCommand(t);
            }
            foreach (FileNode t in _tree.AllNodes.Values) t.RecalculateIndentedName();
        }

        [MenuItem("ProvisGames/데이터시트 임포터")]
        static void Init() {
            ProcedurePreviewer window = (ProcedurePreviewer) GetWindow(typeof(ProcedurePreviewer));

            window.Show();
        }
    }

    internal class FileNode : Node<string> {
        public FileNode(string guid, FileTree tree = null) : base(guid) {
            Tree = tree;
        }
        public List<Command> Commands = new List<Command>();
        internal FileTree Tree;
        private bool _isOpen = true;
        private string _displayName = string.Empty;
        public void RecalculateIndentedName() {
            _displayName = Id.Split('/').Last();
        }
        public void Draw() {
            EditorGUI.indentLevel = Depth;
            if (Children.Count > 0) {
                GUIContent label;
                if (_displayName.StartsWith(CustomPath.ObjPrefix)) {
                    label = EditorGUIUtility.IconContent("d_GameObject Icon");
                } else if (_displayName.StartsWith(CustomPath.CompPrefix)) {
                    label = EditorGUIUtility.IconContent("cs Script Icon");
                } else if (_displayName.StartsWith(CustomPath.RefPrefix)) {
                    label = EditorGUIUtility.IconContent("d_curvekeyframeweighted");
                } else if (_displayName.EndsWith(CustomPath.PrefabExtension)) {
                    label = EditorGUIUtility.IconContent("d_Prefab Icon");
                } else if (_displayName.EndsWith(CustomPath.AssetExtension)) {
                    label = EditorGUIUtility.IconContent("ScriptableObject Icon");
                } else {
                    label = EditorGUIUtility.IconContent("FolderEmpty Icon");
                }
                label.text = $"{_displayName.Split('_').Last()}";
                _isOpen = EditorGUILayout.Foldout(_isOpen, label);
            } else
                _isOpen = true;

            if (_isOpen) {
                if (Commands != null) {
                    EditorGUI.indentLevel++;
                    foreach (Command command in Commands) {
                        GUIContent commandLabel=EditorGUIUtility.IconContent("d_curvekeyframeweighted");
                        commandLabel.text = command.GenerateTooltipText();
                        EditorGUILayout.LabelField(commandLabel, ProcedurePreviewer.PreviewerStyle);
                    }
                }
                foreach (string child in Children) {
                    Tree.GetNode(child).Draw();
                }
            }
        }
        public override string ToString() {
            return Id;
        }
    }

    internal class FileTree : Tree<string, FileNode> {
        public FileTree(string root, FileNode rootFile) : base(root, rootFile) {
            rootFile.Tree = this;
        }
        public void AddCommand(Command command) {
            string targetPath = new CustomPath(command.Target).FullPath,
                   subTargetPath = new CustomPath(command.SubTarget).FullPath;
            switch (command.Type) {
                case CommandType.Log:
                    GetRootNode().Commands.Add(command);
                    break;
                case CommandType.Create:
                    AddPath(subTargetPath);
                    GetNode(subTargetPath).Commands.Add(command);
                    break;
                case CommandType.Move:
                {
                    Command cpy = command;
                    cpy.PastValue = CommandProcessor.MoveTo;
                    AddPath(targetPath);
                    GetNode(targetPath).Commands.Add(cpy);
                    cpy.PastValue = CommandProcessor.MoveFrom;
                    AddPath(subTargetPath);
                    GetNode(subTargetPath).Commands.Add(cpy);
                }
                    break;
                case CommandType.Set:
                    AddPath(targetPath);
                    GetNode(targetPath).Commands.Add(command);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AddPath(string path) {
            string[] map = path.Split(CustomPath.PathDiff);
            string now = map[0];
            for (int i = 0; i < map.Length - 1; i++) {
                if (GetNode(now) == default) {
                    AddPath(now);
                }
                if (i < map.Length - 2) now += $"{CustomPath.PathDiff}{map[i + 1]}";
            }
            FileNode node = new FileNode(path, this);
            AllNodes.Add(node.Id, node);
            FileNode parent = GetNode(now);
            node.Parent = parent.Id;
            parent.Children.Add(node.Id);
            node.Depth = parent.Depth + 1;
        }
    }

    internal class Tree<TKey, TNode> where TNode : Node<TKey> {
        public Tree(TKey root, TNode rootNode) {
            this.Root = root;
            this.AllNodes = new Dictionary<TKey, TNode>();
            this.AllNodes.Add(root, rootNode);
        }
        public Tree(TKey root, IDictionary<TKey, TNode> files) {
            this.Root = root;
            this.AllNodes = new Dictionary<TKey, TNode>(files);
            this.MaxDepth = this.AllNodes.Max(node => node.Value.Depth);
        }
        internal Dictionary<TKey, TNode> AllNodes;
        public TKey Root;
        public int MaxDepth { get; }
        public TNode GetRootNode() {
            if (!this.AllNodes.ContainsKey(Root)) return default;
            return this.AllNodes[Root];
        }
        public TNode GetNode(TKey key) {
            if (!this.AllNodes.ContainsKey(key)) return default;
            return this.AllNodes[key];
        }
    }

    internal class Node<TKey> {
        public Node(TKey guid) {
            this.Id = guid;
            this.Parent = default;
            this.Depth = 0;
            this.Children = new HashSet<TKey>();
        }
        public TKey Id { get; }
        public TKey Parent { get; set; }
        public HashSet<TKey> Children { get; private set; }
        public int Depth { get; set; }
        public void AddChild(TKey child) {
            this.Children.Add(child);
        }
        public bool HasChild(TKey child) {
            return this.Children.Contains(child);
        }
    }

}