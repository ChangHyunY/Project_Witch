namespace Anchor.Unity.UGui.Dialog.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.UI;
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(ComStaticRegister))]
    public class DialogGenerate : Editor
    {
        ComStaticRegister script;

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Register"))
            {

            }

            base.OnInspectorGUI();
        }
    }
}