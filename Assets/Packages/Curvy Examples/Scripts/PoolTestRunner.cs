// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Components;
using UnityEngine.UI;

namespace Curvy.Examples
{
    public class PoolTestRunner : MonoBehaviour
    {
        public CurvySpline Spline;
        public Text PoolCountInfo;

        void Start()
        {
            checkForSpline();
        }

        void Update()
        {
            

            PoolCountInfo.text = string.Format("Control Points in Pool: {0}",CurvyGlobalManager.Instance.ControlPointPool.Count);
        }

        void checkForSpline()
        {
            if (Spline == null)
            {
                Spline = CurvySpline.Create();
                Camera.main.GetComponent<CurvyGLRenderer>().Add(Spline);
                for (int i = 0; i < 4; i++)
                    AddCP();
            }
        }

        public void AddCP()
        {
            checkForSpline();
            Spline.Add(Random.insideUnitCircle * 50);
        }

        public void DeleteCP()
        {
            if (Spline && Spline.ControlPointCount > 0)
            {
                int idx = Random.Range(0, Spline.ControlPointCount - 1);
                Spline.ControlPoints[idx].Delete();
                Spline.Refresh();
            }
        }

        public void ClearSpline()
        {
            if (Spline)
                Spline.Clear();
        }

        public void DeleteSpline()
        {
            if (Spline)
                Spline.Destroy();
        }
    }
}
