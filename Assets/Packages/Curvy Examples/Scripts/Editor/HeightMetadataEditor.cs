// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using UnityEditor;
using System.Collections;
using FluffyUnderware.DevToolsEditor;
using FluffyUnderware.Curvy.Examples;

namespace FluffyUnderware.Curvy.ExamplesEditor
{
    
    [CustomEditor(typeof(HeightMetadata))]
    public class HeightMetadataEditor : DTEditor<HeightMetadata>
    {
        
        [DrawGizmo(GizmoType.Active|GizmoType.NonSelected|GizmoType.InSelectionHierarchy)]
        static void GizmoDrawer(HeightMetadata data, GizmoType context)
        {
            if (CurvyGlobalManager.ShowMetadataGizmo && data.Spline.ShowGizmos)
            {
                Vector3 p = data.ControlPoint.position;
                p.y += HandleUtility.GetHandleSize(data.ControlPoint.position) * 0.3f;
                Handles.Label(p, data.Value.ToString(), DTStyles.BackdropHtmlLabel);
            }
        }
    }
}
