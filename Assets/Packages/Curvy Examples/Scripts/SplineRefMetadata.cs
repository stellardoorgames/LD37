// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;

namespace FluffyUnderware.Curvy.Examples
{
    public class SplineRefMetadata : MonoBehaviour, ICurvyMetadata
    {
        public CurvySpline Spline;
        public CurvySplineSegment CP;
        public string Options;
    }
}
