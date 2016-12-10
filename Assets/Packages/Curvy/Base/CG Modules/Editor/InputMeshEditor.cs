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
    [CustomEditor(typeof(InputMesh))]
    public class InputMeshEditor : CGModuleEditor<InputMesh>
    {
        int selectedIndex;

        protected override void SetupArrayEx(DTFieldNode node, DevTools.ArrayExAttribute attribute)
        {
            node.ArrayEx.drawElementCallback = OnMeshGUI;
            node.ArrayEx.onSelectCallback = (ReorderableList l) => { selectedIndex = l.index; };
            node.ArrayEx.onAddCallback = (ReorderableList l) =>
            {
                Target.Meshes.Insert(Mathf.Clamp(l.index + 1, 0, Target.Meshes.Count), new CGMeshProperties());
                EditorUtility.SetDirty(Target);
            };
        }

       

        void OnMeshGUI(Rect rect, int index, bool isActive, bool isFocused)
        {
            var prop = serializedObject.FindProperty(string.Format("m_Meshes.Array.data[{0}]",  index ));
            if (prop != null)
            {
                rect.height = EditorGUIUtility.singleLineHeight;
                rect.width -= 100;
                var mshProp=prop.FindPropertyRelative("m_Mesh");
                mshProp.objectReferenceValue=EditorGUI.ObjectField(rect, mshProp.objectReferenceValue, typeof(Mesh), false);

                rect.x += rect.width;
                EditorGUI.LabelField(rect, getFormattedMeshInfo(mshProp.objectReferenceValue as Mesh),DTStyles.HtmlLabel);
            }
        }

        void OnPropertiesGUI()
        {
            var prop=serializedObject.FindProperty(string.Format("m_Meshes.Array.data[{0}]", selectedIndex ));
            if (prop!=null)
            {
                var matProp = prop.FindPropertyRelative("m_Material");
                if (matProp != null)
                {
                    var l = new ReorderableList(serializedObject, matProp, true, true, false, false);
                    l.drawHeaderCallback = (Rect rect) => { GUI.Label(rect, "Materials for " + Target.Meshes[selectedIndex].Mesh.name); };
                    l.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                    {
                        rect.height = EditorGUIUtility.singleLineHeight;
                        var pMat = prop.FindPropertyRelative(string.Format("m_Material.Array.data[{0}]",index ));
                        pMat.objectReferenceValue = EditorGUI.ObjectField(rect, pMat.objectReferenceValue, typeof(Material), false);
                    };
                    l.DoLayoutList();
                }
                EditorGUILayout.PropertyField(prop.FindPropertyRelative("m_Translation"));
                EditorGUILayout.PropertyField(prop.FindPropertyRelative("m_Rotation"));
                EditorGUILayout.PropertyField(prop.FindPropertyRelative("m_Scale"));
            }
        }

        protected override void OnCustomInspectorGUI()
        {
            base.OnCustomInspectorGUI();
            if (Target && selectedIndex < Target.Meshes.Count && Target.Meshes[selectedIndex].Mesh)
            {
                GUILayout.Space(5);
                bool open=true;
                CurvyGUI.Foldout(ref open, "Properties");
                OnPropertiesGUI();
            }
        }
        
        string getFormattedMeshInfo(Mesh msh)
        {
            if (msh)
            {
                string has = "<color=#008000>";
                string dont = "<color=#800000>";
                string close = "</color>";
                string norm = (msh.normals.Length > 0) ? has : dont;
                string tan = (msh.tangents.Length > 0) ? has : dont;
                string uv = (msh.uv.Length > 0) ? has : dont;
                string uv2 = (msh.uv.Length > 0) ? has : dont;
                return string.Format("{1}Nor{0} {2}Tan{0} {3}UV{0} {4}UV2{0}", close, norm, tan, uv, uv2);
            }
            else return "";
        }
    }
   
}
