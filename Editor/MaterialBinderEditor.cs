using UnityEngine;
using UnityEditor;
using UEditor = UnityEditor.Editor;

namespace Sztorm.MaterialBinder.Editor
{
    [CustomEditor(typeof(MaterialBinder))]
    public class MaterialBinderEditor : UEditor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Generate Bindings"))
            {
                MaterialBinder unboxedTarget = (MaterialBinder)target;
                unboxedTarget.CreateBindingsFiles();
                AssetDatabase.Refresh();
            }
        }
    }
}