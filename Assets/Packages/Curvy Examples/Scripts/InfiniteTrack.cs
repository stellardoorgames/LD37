// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.Curvy.Generator.Modules;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.Curvy.Shapes;
using FluffyUnderware.DevTools;
using UnityEngine.UI;

namespace FluffyUnderware.Curvy.Examples
{

    public class InfiniteTrack : MonoBehaviour {
        public CurvySpline TrackSpline; // empty Spline object we create the path in
        public CurvyController Controller; // controller to use
        public Material RoadMaterial; // material for the extrusion
        public Text TxtStats; // the UI text holding the statistics

        [Positive]
        public float CurvationX = 10; // X-axis angle randomness
        [Positive]
        public float CurvationY = 10; // Y-axis angle randomness
        [Positive]
        public float CPStepSize = 20; // distance between CPs
        [Positive]
        public int HeadCP = 3; // CP's to build in front of extrusion
        [Positive]
        public int TailCP = 2; // CP's to keep behind extrusion
        
        [Min(3)]
        public int Sections = 6; // # of Extrusions to use
        [Min(1)]
        public int SectionCPCount = 2; // # of CP's to use for a single extrusion


        int mInitState = 0;
        bool mUpdateSpline;
        int mUpdateIn;
        
        CurvyGenerator[] mGenerators;
        int mCurrentGen;
        float lastSectionEndV;
        Vector3 mDir;

        TimeMeasure timeSpline = new TimeMeasure(30);
        TimeMeasure timeCG = new TimeMeasure(1);
        

        IEnumerator Start()
        {
            while (!TrackSpline.IsInitialized)
                yield return 0;
            InvokeRepeating("updateStats", 1, 0.25f);
        }

        void FixedUpdate()
        {
            if (mInitState==0)
                StartCoroutine("setup");

            if (mInitState==2 && mUpdateSpline)
                advanceTrack();
                
        }

        // setup everything
        IEnumerator setup()
        {
            mInitState = 1;
            
            mGenerators = new CurvyGenerator[Sections];

            // Add some start CP's to the spline
            TrackSpline.Add().position = Vector3.zero;
            TrackSpline.Add().position = new Vector3(0, 0, CPStepSize);
            mDir = Vector3.forward;
            int num = TailCP + HeadCP + Sections * SectionCPCount - 1;
            for (int i = 0; i < num; i++)
                addTrackCP();
            
            TrackSpline.Refresh();
            // setting anchors so new CP's won't change orientation of existing ones
            for (int i = TailCP; i < TrackSpline.ControlPointCount-HeadCP; i += SectionCPCount)
                TrackSpline.ControlPoints[i].OrientationAnchor = true;

            // build Curvy Generators
            for (int i = 0; i < Sections; i++)
            {
                mGenerators[i] = buildGenerator();
                mGenerators[i].name = "Generator " + i;
            }
            // and wait until they're initialized
            for (int i = 0; i < Sections; i++)
                while (!mGenerators[i].IsInitialized)
                    yield return 0;

            // let all generators do their extrusion
            for (int i = 0; i < Sections; i++)
                updateSectionGenerator(mGenerators[i], i * SectionCPCount + TailCP, (i+1) * SectionCPCount + TailCP);

            mInitState = 2;
            mUpdateIn = SectionCPCount;
            // finally place the controller
            Controller.AbsolutePosition = TrackSpline.ControlPoints[TailCP+2].Distance;
            Controller.InitialPosition= TrackSpline.ControlPoints[TailCP+2].Distance; // just to be sure
        }

        // build a generator
        CurvyGenerator buildGenerator()
        {
            // Create the Curvy Generator
            var gen = CurvyGenerator.Create();
            gen.AutoRefresh = false;
            // Create Modules
            var path = gen.AddModule<InputSplinePath>();
            var shape = gen.AddModule<InputSplineShape>();
            var extrude = gen.AddModule<BuildShapeExtrusion>();
            var vol = gen.AddModule<BuildVolumeMesh>();
            var msh = gen.AddModule<CreateMesh>();
            // Create Links between modules
            path.OutputByName["Path"].LinkTo(extrude.InputByName["Path"]);
            shape.OutputByName["Shape"].LinkTo(extrude.InputByName["Cross"]);
            extrude.OutputByName["Volume"].LinkTo(vol.InputByName["Volume"]);
            vol.OutputByName["VMesh"].LinkTo(msh.InputByName["VMesh"]);
            // Set module properties
            path.Spline = TrackSpline;
            path.UseCache = true;
            var rectShape = shape.SetManagedShape<CSRectangle>();
            rectShape.Width = 20;
            rectShape.Height = 2;
            extrude.Optimize = false;
            vol.Split = false;
            vol.SetMaterial(0, RoadMaterial);
            vol.MaterialSetttings[0].SwapUV = true;

            msh.Collider = CGColliderEnum.None;
            return gen;
        }

        // advance the track
        void advanceTrack()
        {
            timeSpline.Start();
            // we need to store the controller's position here, because Controller.AdaptOnChange doesn't handle multiple CP operations (adding,deleting)
            float pos = Controller.AbsolutePosition;
            //remove oldest section's CP
            for (int i = 0; i < SectionCPCount; i++)
            {
                pos -= TrackSpline.ControlPoints[0].Length; // update controller's position, so the ship won't jump
                TrackSpline.ControlPoints[0].Delete();
            }
            // add new section's CP
            for (int i = 0; i < SectionCPCount; i++)
                addTrackCP();
            // refresh the spline, so orientation will be auto-calculated
            TrackSpline.Refresh();
            TrackSpline.ControlPoints[TrackSpline.ControlPointCount - 1].OrientationAnchor = true;
            // just calling it again to fix the orientation anchor
            TrackSpline.Refresh();

            // set the controller to the old position
            Controller.AbsolutePosition = pos;
            mUpdateSpline = false;
            timeSpline.Stop();
            // update generators in 0.2 seconds
            Invoke("advanceSections", 0.2f);
        }

        // update all CGs
        void advanceSections()
        {
            // set oldest CG to render path for new section
            var cur = mGenerators[mCurrentGen++];
            int num = TrackSpline.ControlPointCount - HeadCP - 1;
            updateSectionGenerator(cur, num - SectionCPCount, num);

            if (mCurrentGen == Sections)
                mCurrentGen = 0;
        }

        void updateStats()
        {
            TxtStats.text = string.Format("Spline Update: {0:0.00} ms\nGenerator Update: {1:0.00} ms", timeSpline.AverageMS, timeCG.AverageMS);
        }

        // set a CG to render only a portion of a spline
        void updateSectionGenerator(CurvyGenerator gen, int startCP, int endCP)
        {
            // Set Track segment we want to use
            var path = gen.FindModules<InputSplinePath>(true)[0];
            path.StartCP = TrackSpline.ControlPoints[startCP];
            path.EndCP = TrackSpline.ControlPoints[endCP];
            // Set UV-Offset to match
            var vol = gen.FindModules<BuildVolumeMesh>()[0];
            vol.MaterialSetttings[0].UVOffset.y = lastSectionEndV % 1;
            timeCG.Start();
            gen.Refresh();
            timeCG.Stop();
            // fetch the ending V to be used by next section
            var vmesh = vol.OutVMesh.GetData<CGVMesh>();
            lastSectionEndV = vmesh.UV[vmesh.Count - 1].y;
        }

        // while we travel past CP's, we update the track
        public void Track_OnControlPointReached(CurvySplineMoveEventArgs e)
        {
            if (--mUpdateIn == 0)
            {
                mUpdateSpline = true;
                mUpdateIn = SectionCPCount;
            }
        }

        // add more CP's, rotating path by random angles
        void addTrackCP()
        {
            float rndX = Random.value * CurvationX * DTUtility.RandomSign();
            float rndY = Random.value * CurvationY * DTUtility.RandomSign();
            Vector3 p = TrackSpline.ControlPoints[TrackSpline.ControlPointCount - 1].localPosition;

            mDir = Quaternion.Euler(rndX, rndY,0) * mDir;
            TrackSpline.InsertAfter(null).localPosition = p + mDir * CPStepSize;
            
        }

        

        


       

       

    }
}
