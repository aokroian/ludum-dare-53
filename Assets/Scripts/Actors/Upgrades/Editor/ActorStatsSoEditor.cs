using UnityEditor;

namespace Actors.Upgrades.Editor
{
    [CustomEditor(typeof(DynamicActorStats))]
    public class ActorStatsEditor : UnityEditor.Editor
    {
        private UnityEditor.Editor _editorInstance;

        private void OnEnable()
        {
            _editorInstance = null;
        }

        public override void OnInspectorGUI()
        {
            var dynamicActorStats = (DynamicActorStats) target;
            if (_editorInstance == null)
                _editorInstance = CreateEditor(dynamicActorStats.ActorStatsSo);
            base.OnInspectorGUI(); 
            
            EditorGUILayout.Space();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Actor Stats", EditorStyles.boldLabel);
            _editorInstance.DrawDefaultInspector();
        }
    }
}