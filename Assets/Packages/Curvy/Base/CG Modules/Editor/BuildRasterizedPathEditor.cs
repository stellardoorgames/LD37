// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using UnityEditor;
using FluffyUnderware.Curvy.Generator.Modules;
using System.Collections.Generic;
using FluffyUnderware.Curvy.Generator;

namespace FluffyUnderware.CurvyEditor.Generator.Modules
{
    [CustomEditor(typeof(BuildRasterizedPath))]
    public class BuildRasterizedPathEditor : CGModuleEditor<BuildRasterizedPath>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            HasDebugVisuals = true;
        }

        public override void OnModuleSceneDebugGUI()
        {
            base.OnModuleSceneDebugGUI();
            var data = Target.OutPath.GetData<CGPath>();
            if (data)
            {
                Handles.matrix = Target.Generator.transform.localToWorldMatrix;
                CGEditorUtility.SceneGUIPlot(data.Position,0.1f,Color.white);
                Handles.matrix = Matrix4x4.identity;
            }
        }

        public override void OnModuleDebugGUI()
        {
            var data = Target.OutPath.GetData<CGPath>();
            if (data)
            {
                EditorGUILayout.LabelField("Samples: " + data.Count);
            }
        }
    }




}
