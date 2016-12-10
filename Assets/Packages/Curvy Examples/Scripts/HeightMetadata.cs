// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.DevTools;

namespace FluffyUnderware.Curvy.Examples
{
    public class HeightMetadata : CurvyMetadataBase, ICurvyInterpolatableMetadata<float>
    {
        [SerializeField]
        [RangeEx(0,1,Slider=true)]
        float m_Height;

        

        public object Value
        {
            get { return m_Height; }
        }

        public object InterpolateObject(ICurvyMetadata b, float f)
        {
            var mdb = b as HeightMetadata;
            return (mdb != null) ? Mathf.Lerp((float)Value, (float)mdb.Value, f) : Value;
        }

        public float Interpolate(ICurvyMetadata b, float f)
        {
            return (float)InterpolateObject(b, f);
        }

        
    }

}
