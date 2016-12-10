// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.Curvy.Generator.Modules;
using FluffyUnderware.DevToolsEditor;
using UnityEditorInternal;

namespace FluffyUnderware.CurvyEditor.Generator.Modules
{
    [CustomEditor(typeof(InputGameObject))]
    public class InputGameObjectsEditor : CGModuleEditor<InputGameObject>
    {
        int selectedIndex;

        protected override void SetupArrayEx(DTFieldNode node, DevTools.ArrayExAttribute attribute)
        {
            node.ArrayEx.drawElementCallback = onGameObjectGUI;
            node.ArrayEx.onSelectCallback = (ReorderableList l) => { selectedIndex = l.index; };
            node.ArrayEx.onAddCallback = (ReorderableList l) =>
            {
                Target.GameObjects.Insert(Mathf.Clamp(l.index + 1, 0, Target.GameObjects.Count), new CGGameObjectProperties());
                EditorUtility.SetDirty(Target);
            };
        }

       
        
        void onGameObjectGUI(Rect rect, int index, bool isActive, bool isFocused)
        {
            var prop = serializedObject.FindProperty(string.Format("m_GameObjects.Array.data[{0}]",  index ));
            if (prop != null)
            {
                rect.height = EditorGUIUtility.singleLineHeight;
                
                var mshProp=prop.FindPropertyRelative("m_Object");
                mshProp.objectReferenceValue=EditorGUI.ObjectField(rect, mshProp.objectReferenceValue, typeof(GameObject), true);
            }
        }
        

        void OnPropertiesGUI()
        {
            var prop=serializedObject.FindProperty(string.Format("m_GameObjects.Array.data[{0}]", selectedIndex ));
            if (prop!=null)
            {
                EditorGUILayout.PropertyField(prop.FindPropertyRelative("m_Translation"));
                EditorGUILayout.PropertyField(prop.FindPropertyRelative("m_Rotation"));
                EditorGUILayout.PropertyField(prop.FindPropertyRelative("m_Scale"));
            }
        }

        protected override void OnCustomInspectorGUI()
        {
            base.OnCustomInspectorGUI();
            if (Target && selectedIndex < Target.GameObjects.Count && Target.GameObjects[selectedIndex].Object)
            {
                GUILayout.Space(5);
                bool open=true;
                CurvyGUI.Foldout(ref open, "Properties");
                OnPropertiesGUI();
            }
        }
    
    }
   
}
