// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.DevTools;

namespace FluffyUnderware.Curvy.Shapes
{
    /// <summary>
    /// Circle Shape (2D)
    /// </summary>
    [CurvyShapeInfo("2D/Circle")]
    [RequireComponent(typeof(CurvySpline))]
    [AddComponentMenu("Curvy/Shape/Circle")]
    public class CSCircle : CurvyShape2D
    {
        [Positive(Tooltip="Number of Control Points")]
        [SerializeField]
        int m_Count=4;
        public int Count
        {
            get { return m_Count; }
            set
            {
                int v = Mathf.Max(2, value);
                if (m_Count != v)
                {
                    m_Count = v;
                    Dirty = true;
                }
            }
        }

        [SerializeField]
        float m_Radius = 1;
        public float Radius
        {
            get { return m_Radius;}
            set 
            {
                float v = Mathf.Max(0, value);
                if (m_Radius != v)
                {
                    m_Radius = v;
                    Dirty = true;
                }
            }
        }


#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            Count = m_Count;
            Radius = m_Radius;
        }
#endif

        protected override void Reset()
        {
            base.Reset();
            Count = 4;
            Radius = 1;
        }

        protected override void ApplyShape()
        {
            PrepareSpline(CurvyInterpolation.Bezier);
            PrepareControlPoints(Count);
            float d = 360f * Mathf.Deg2Rad / Count;
            for (int i = 0; i < Count; i++)
                Spline.ControlPoints[i].localPosition = new Vector3(Mathf.Sin(d * i) * Radius, Mathf.Cos(d * i) * Radius, 0);
        }

      


       

    }


}
