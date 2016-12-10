// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using UnityEditor;
using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.DevTools.Extensions;
using FluffyUnderware.Curvy.Generator.Modules;
using FluffyUnderware.DevToolsEditor;

namespace FluffyUnderware.CurvyEditor.Generator.Modules
{
    [CustomEditor(typeof(BuildShapeExtrusion))]
    public class BuildShapeExtrusionEditor : CGModuleEditor<BuildShapeExtrusion>
    {
        bool mEditCross;
        bool mShowEditButton;

        public override void OnModuleDebugGUI()
        {
            EditorGUILayout.LabelField("Samples Path/Cross: " + Target.PathSamples + "/" + Target.CrossSamples);
            EditorGUILayout.LabelField("Cross Sample Groups: " + Target.CrossGroups);
        }

        void CBEditCrossButton()
        {
            
            if (DTGUI.IsLayout)
                mShowEditButton = (Target.IsConfigured && Target.InCross.SourceSlot().ExternalInput != null && Target.InCross.SourceSlot().ExternalInput.SupportsIPE);
            
            if (mShowEditButton)
            {
                EditorGUI.BeginChangeCheck();
                mEditCross = GUILayout.Toggle(mEditCross, "Edit Cross", EditorStyles.miniButton);
                if (EditorGUI.EndChangeCheck())
                {
                    if (mEditCross)
                    {
                        CGGraph.SetIPE(Target.Cross, this);
                    }
                    else
                        CGGraph.SetIPE();
                }
            }
        }

        /// <summary>
        /// Called for the IPE initiator to get the TRS values for the target
        /// </summary>
        internal override void OnIPEGetTRS(out Vector3 position, out Quaternion rotation, out Vector3 scale)
        {
            position = Target.CrossPosition;
            rotation = Target.CrossRotation;
            scale = Target.GetScale(0);
        }


        
    }
}
