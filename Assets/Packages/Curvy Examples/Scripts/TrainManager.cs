// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


namespace FluffyUnderware.Curvy.Examples
{
    [ExecuteInEditMode]
    public class TrainManager : MonoBehaviour
    {
        public CurvySpline Spline;
        public float Speed;
        

        public float Position;
        public float CarSize = 10;
        public float AxisDistance = 8;
        public float CarGap = 1;
        public float Limit = 0.2f;

        TrainCarManager[] Cars;

        

        void Awake()
        {
            setup();
        }

        IEnumerator Start()
        {
            while (!Spline.IsInitialized)
                yield return 0;
            setup();
        }

#if UNITY_EDITOR
        void Update()
        {
            if (!Application.isPlaying)
                setup();
        }

        void OnValidate()
        {
            setup();
        }
#endif

        void LateUpdate()
        {
            if (!Spline.IsInitialized)
                return;
            if (Cars.Length > 1)
            {
                var first = Cars[0];
                var last = Cars[Cars.Length - 1];
                if (first.FrontAxis.Spline == last.BackAxis.Spline && first.FrontAxis.RelativePosition>last.BackAxis.RelativePosition)
                {
                    for (int i = 1; i < Cars.Length; i++)
                    {
                        float delta=Cars[i-1].Position-Cars[i].Position-CarSize-CarGap;
                        if (Mathf.Abs(delta) >= Limit)
                            Cars[i].Position += delta;
                    }
                }
            }
        }

        void setup()
        {
            Cars = GetComponentsInChildren<TrainCarManager>();
            float pos = Position-CarSize/2;
            
            for (int i = 0; i < Cars.Length; i++)
            {
                Cars[i].setup();
                Cars[i].Position = pos;
                pos -= CarSize + CarGap;
            }
            
        }

        

        
    }
}
