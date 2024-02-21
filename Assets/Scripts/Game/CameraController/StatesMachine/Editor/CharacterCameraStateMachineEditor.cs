using Game.CameraController.StatesMachine.Enums;
using Tools.CSharp;
using UnityEditor;
using UnityEngine;

namespace Game.CameraController.StatesMachine.Editor
{
    [CustomEditor(typeof(CharacterCameraStateMachine))]
    public class CharacterCameraStateMachineEditor : UnityEditor.Editor
    {
        private CharacterCameraControllerState _state;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Space(10);
            GUILayout.Label("Custom editor:");
            
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

            _state = (CharacterCameraControllerState) EditorGUILayout.EnumPopup("State", _state);
            if (GUILayout.Button("Apply"))
            {
                var stateMachine = (CharacterCameraStateMachine) target;
                stateMachine.SetState(_state).Run();
            }
            
            GUILayout.EndHorizontal();
        }
    }
}