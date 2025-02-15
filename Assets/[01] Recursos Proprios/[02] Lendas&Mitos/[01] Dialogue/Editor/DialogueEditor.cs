using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

namespace LendasEMitos.Dialogue.Editor
{

    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;
        Vector2 scrollPostition;

        [NonSerialized] GUIStyle nodeStyle;
         [NonSerialized] GUIStyle playerNodeStyle;
        [NonSerialized] DialogueNode draggingNode = null;
        [NonSerialized] Vector2 draggingOffset;
        [NonSerialized] DialogueNode creatingNode = null;
        [NonSerialized] DialogueNode deletingNode = null;
        [NonSerialized] DialogueNode linkingParentNode = null;
        [NonSerialized] bool DraggingCanvas = false;
        [NonSerialized] Vector2 draggingCanvasOffset;

        const float canvasSize = 4000;
        const float canvasBackground = 50;


        [MenuItem("Dialogue/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OpenDialogue(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            playerNodeStyle = new GUIStyle();
            playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            playerNodeStyle.padding = new RectOffset(20, 20, 20, 20);
            playerNodeStyle.border = new RectOffset(12, 12, 12, 12);
        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                selectedDialogue = newDialogue;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("no dialogue selected");
                return;
            }

            ProcessEvents();

            scrollPostition = EditorGUILayout.BeginScrollView(scrollPostition);

            Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
            Texture2D backgroundTexture = Resources.Load("background") as Texture2D;
            Rect texCoords = new Rect(0, 0, canvasSize / canvasBackground, canvasSize / canvasBackground);
            GUI.DrawTextureWithTexCoords(canvas, backgroundTexture, texCoords);

            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                DrawConnections(node);
            }
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                DrawNode(node);

            }
            EditorGUILayout.EndScrollView();

            if (creatingNode != null)
            {
                selectedDialogue.CreateNote(creatingNode);
                creatingNode = null;
            }
            if (deletingNode != null)
            {
                selectedDialogue.DeleteNote(deletingNode);
                deletingNode = null;
            }
        }


        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = getNodeAtPoint(Event.current.mousePosition + scrollPostition);
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;
                }
                else
                {
                    DraggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPostition;
                    Selection.activeObject = selectedDialogue;

                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && DraggingCanvas)
            {
                scrollPostition = draggingCanvasOffset - Event.current.mousePosition;
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseUp && DraggingCanvas)
            {
                DraggingCanvas = false;
            }

        }

        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = nodeStyle;
            if (node.GetCharacterSpeaking()==0)
            {
                style= playerNodeStyle;
                
            }
            GUILayout.BeginArea(node.GetRect(), style);

            node.SetCharacterSpeaking(EditorGUILayout.Popup(node.GetCharacterSpeaking(), selectedDialogue.Characters.ToArray()));
            node.SetText(EditorGUILayout.TextArea(node.GetText()));

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("-"))
            {
                deletingNode = node;
            }
            DrawLinkButtons(node);
            if (GUILayout.Button("+"))
            {
                creatingNode = node;
            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void DrawLinkButtons(DialogueNode node)
        {
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("link"))
                {
                    linkingParentNode = node;
                }
            }
            else if (linkingParentNode == node)
            {
                if (GUILayout.Button("cancel"))
                {
                    linkingParentNode = null;
                }
            }
            else if (linkingParentNode.GetChildren().Contains(node.name))
            {
                if (GUILayout.Button("Unlink"))
                {
                    linkingParentNode.RemoveChildren(node.name);
                    linkingParentNode = null;
                }
            }
            else
            {
                if (GUILayout.Button("child"))
                {
                    Undo.RecordObject(selectedDialogue, "Add Dialogue Link");
                    linkingParentNode.AddChildren(node.name);
                    linkingParentNode = null;
                }
            }


        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.GetRect().center.x, node.GetRect().center.y);
            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {

                Vector3 endPosition = new Vector2(childNode.GetRect().center.x, childNode.GetRect().center.y);
                if (childNode.GetRect().xMax < startPosition.x)
                {
                    endPosition = new Vector2(childNode.GetRect().xMax, endPosition.y);
                }
                else if (childNode.GetRect().xMin > startPosition.x)
                {
                    endPosition = new Vector2(childNode.GetRect().xMin, endPosition.y);
                }
                if (childNode.GetRect().yMax < startPosition.y)
                {
                    endPosition = new Vector2(endPosition.x, childNode.GetRect().yMax);
                }
                else if (childNode.GetRect().yMin > startPosition.y)
                {
                    endPosition = new Vector2(endPosition.x, childNode.GetRect().yMin);
                }

                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startPosition, endPosition,
                    startPosition + controlPointOffset,
                    endPosition - controlPointOffset,
                    Color.white, null, 4f);

            }
        }

        private DialogueNode getNodeAtPoint(Vector2 mousePosition)
        {
            DialogueNode foundNode = null;
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if (node.GetRect().Contains(mousePosition))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }
    }
}