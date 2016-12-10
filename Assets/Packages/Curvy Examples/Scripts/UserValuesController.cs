// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.DevTools;

namespace FluffyUnderware.Curvy.Examples
{

    /// <summary>
    /// Example custom Controller
    /// </summary>
    public class UserValuesController : SplineController
    {
        
        [RangeEx(0, 30)]
        [SerializeField]
        float m_MaxHeight = 5f; // The height over ground to use as default

        public float MaxHeight
        {
            get { return m_MaxHeight; }
            set
            {
                if (m_MaxHeight != value)
                    m_MaxHeight = value;
            }
        }

        /// <summary>
        /// This is called just after the SplineController has been initialized
        /// </summary>
        protected override void UserAfterInit()
        {
            setHeight();
        }
        /// <summary>
        /// This is called just after the SplineController updates
        /// </summary>
        protected override void UserAfterUpdate()
        {
            setHeight();
        }


        void setHeight()
        {
            // Get the interpolated UserValue (stored in slot 0) for the current position (for SplineController, RelativePosition means TF)
            var v = Spline.InterpolateUserValue(RelativePosition, 0);
            // In our case we store a percentage (0..1) in the UserValue, so we multiply with MaxHeight to set the actual height.
            // Note that position and rotation  has been set by the SplineController previously, so we just translate here using the local y-axis
            transform.Translate(0, v.x * MaxHeight, 0, Space.Self);
        }


    }
}
