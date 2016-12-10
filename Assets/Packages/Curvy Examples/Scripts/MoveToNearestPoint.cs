// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.DevTools;
using UnityEngine.UI;

namespace FluffyUnderware.Curvy.Examples
{
    [ExecuteInEditMode]
    public class MoveToNearestPoint : MonoBehaviour
    {
        public Transform Lookup;
        public CurvySpline Spline;
        public Text StatisticsText;
        public Slider Density;

        TimeMeasure Timer = new TimeMeasure(30);

        // Use this for initialization
        IEnumerator Start()
        {
            if (Spline)
                while (!Spline.IsInitialized)
                    yield return 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Spline && Spline.IsInitialized && Lookup)
            {
                
                // convert Lookup position to Spline's local space
                var lookupPos = Spline.transform.InverseTransformPoint(Lookup.position);
                // get the nearest point's TF on spline
                Timer.Start();
                float nearestTF=Spline.GetNearestPointTF(lookupPos);
                Timer.Stop();
                // convert the spline pos back to world space and set
                transform.position = Spline.transform.TransformPoint(Spline.Interpolate(nearestTF));
                StatisticsText.text =
                    string.Format("Blue Curve Cache Points: {0} \nAverage Lookup (ms): {1:0.000}", Spline.CacheSize,Timer.AverageMS);
            }
        }

        public void OnSliderChange()
        {
            Spline.CacheDensity = (int)Density.value;
        }
    }
}
