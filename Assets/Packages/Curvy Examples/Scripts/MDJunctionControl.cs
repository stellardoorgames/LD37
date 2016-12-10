// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;
using UnityEngine.UI;

namespace FluffyUnderware.Curvy.Examples
{
    public class MDJunctionControl : CurvyMetadataBase, ICurvyMetadata
    {
        public bool UseJunction;

        public void Toggle()
        {
            UseJunction = !UseJunction;
        }
        
    }
}
