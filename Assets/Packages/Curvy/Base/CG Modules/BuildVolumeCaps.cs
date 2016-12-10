// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using FluffyUnderware.DevTools.Extensions;
using FluffyUnderware.DevTools;
using FluffyUnderware.Curvy.Utils;
using UnityEngine.Serialization;
using FluffyUnderware.Curvy.ThirdParty.LibTessDotNet;

namespace FluffyUnderware.Curvy.Generator.Modules
{
    [ModuleInfo("Build/Volume Caps",ModuleName="Volume Caps", Description="Build volume caps")]
    [HelpURL(CurvySpline.DOCLINK + "cgbuildvolumecaps")]
    public class BuildVolumeCaps : CGModule
    {
        
        [HideInInspector]
        [InputSlotInfo(typeof(CGVolume))]
        public CGModuleInputSlot InVolume = new CGModuleInputSlot();

        [HideInInspector]
        [InputSlotInfo(typeof(CGVolume),Optional=true,Array=true)]
        public CGModuleInputSlot InVolumeHoles = new CGModuleInputSlot();

        // change this to fit your requirements
        [HideInInspector]
        [OutputSlotInfo(typeof(CGVMesh),Array=true)]
        public CGModuleOutputSlot OutVMesh = new CGModuleOutputSlot();

        #region ### Serialized Fields ###

        [Tab("General")]
        [SerializeField]
        CGYesNoAuto m_StartCap = CGYesNoAuto.Auto;
        [SerializeField]
        CGYesNoAuto m_EndCap = CGYesNoAuto.Auto;
        [SerializeField, FormerlySerializedAs("m_ReverseNormals")]
        bool m_ReverseTriOrder;
        [SerializeField]
        bool m_GenerateUV = true;

        [Tab("Start Cap")]
        [Inline]
        [SerializeField]
        CGMaterialSettings m_StartMaterialSettings = new CGMaterialSettings();
        [Label("Material")]
        [SerializeField]
        Material m_StartMaterial;
        [Tab("End Cap")]
        [SerializeField]
        bool m_CloneStartCap = true;
        [AsGroup(Invisible = true)]
        [GroupCondition("m_CloneStartCap", false)]
        [SerializeField]
        CGMaterialSettings m_EndMaterialSettings = new CGMaterialSettings();
        [Group("Default/End Cap")]
        [Label("Material")]
        [FieldCondition("m_CloneStartCap", false)]
        [SerializeField]
        Material m_EndMaterial;

        #endregion

        #region ### Public Properties ###

        public bool GenerateUV
        {
            get { return m_GenerateUV; }
            set
            {
                if (m_GenerateUV != value)
                    m_GenerateUV = value;
                Dirty = true;
            }
        }

        public bool ReverseTriOrder
        {
            get { return m_ReverseTriOrder; }
            set
            {
                if (m_ReverseTriOrder != value)
                    m_ReverseTriOrder = value;
                Dirty = true;
            }
        }

        public CGYesNoAuto StartCap
        {
            get { return m_StartCap; }
            set
            {
                if (m_StartCap != value)
                    m_StartCap = value;
                Dirty = true;
            }
        }

        public Material StartMaterial
        {
            get { return m_StartMaterial; }
            set
            {
                if (m_StartMaterial != value)
                    m_StartMaterial = value;
                Dirty = true;
            }
        }

        public CGMaterialSettings StartMaterialSettings
        {
            get { return m_StartMaterialSettings; }
        }

        public CGYesNoAuto EndCap
        {
            get { return m_EndCap; }
            set
            {
                if (m_EndCap != value)
                    m_EndCap = value;
                Dirty = true;
            }
        }

        public bool CloneStartCap
        {
            get { return m_CloneStartCap; }
            set
            {
                if (m_CloneStartCap != value)
                    m_CloneStartCap = value;
                Dirty = true;
            }
        }

        public CGMaterialSettings EndMaterialSettings
        {
            get { return m_EndMaterialSettings; }
        }

        public Material EndMaterial
        {
            get { return m_EndMaterial; }
            set
            {
                if (m_EndMaterial != value)
                    m_EndMaterial = value;
                Dirty = true;
            }
        }

        #endregion

        #region ### Private Fields & Properties ###
        #endregion

        #region ### Unity Callbacks ###
        /*! \cond UNITY */

        protected override void Awake()
        {
            base.Awake();

            if (StartMaterial == null)
                StartMaterial = CurvyUtility.GetDefaultMaterial();
            if (EndMaterial == null)
                EndMaterial = CurvyUtility.GetDefaultMaterial();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            GenerateUV = m_GenerateUV;
            ReverseTriOrder = m_ReverseTriOrder;
            StartCap = m_StartCap;
            EndCap = m_EndCap;
        }
#endif

        public override void Reset()
        {
            base.Reset();
            StartCap = CGYesNoAuto.Auto;
            EndCap = CGYesNoAuto.Auto;
            ReverseTriOrder = false;
            GenerateUV = true;
            m_StartMaterialSettings = new CGMaterialSettings();
            m_EndMaterialSettings = new CGMaterialSettings();
        }

        /*! \endcond */
        #endregion

        #region ### Public Methods ###

        public override void Refresh()
        {
            base.Refresh();
            CGVolume vol = InVolume.GetData<CGVolume>();
            var holes = InVolumeHoles.GetAllData<CGVolume>();

            if (vol)
            {
                bool genStart = (StartCap == CGYesNoAuto.Yes || (StartCap == CGYesNoAuto.Auto && !vol.Seamless));
                bool genEnd = (EndCap == CGYesNoAuto.Yes || (EndCap == CGYesNoAuto.Auto && !vol.Seamless));

                if (!genStart && !genEnd)
                {
                    OutVMesh.SetData(null);
                    return;
                }

                var vmesh = new CGVMesh();
                Vector3[] vtStart = new Vector3[0];
                Vector3[] vtEnd = new Vector3[0];

                vmesh.AddSubMesh(new CGVSubMesh());
                CGVSubMesh submesh = vmesh.SubMeshes[0];

                if (genStart)
                {
                    #region --- Start Cap ---

                    var tess = new Tess();
                    tess.UsePooling = true;
                    tess.AddContour(make2DSegment(vol, 0));

                    for (int h = 0; h < holes.Count; h++)
                    {
                        if (holes[h].Count < 3)
                        {
                            OutVMesh.SetData(null);
                            UIMessages.Add("Hole Cross has <3 Vertices: Can't create Caps!");
                            return;
                        }
                        tess.AddContour(make2DSegment(holes[h], 0));
                    }
                    tess.Tessellate(WindingRule.EvenOdd, ElementType.Polygons, 3);
                    vtStart = UnityLibTessUtility.FromContourVertex(tess.Vertices);
                    Bounds b;
                    vmesh.Vertex = applyMat(vtStart, getMat(vol, 0, true), out b);
                    submesh.Material = StartMaterial;
                    submesh.Triangles = tess.Elements;
                    if (ReverseTriOrder)
                        flipTris(ref submesh.Triangles, 0, submesh.Triangles.Length);
                    if (GenerateUV)
                    {
                        vmesh.UV = new Vector2[vtStart.Length];
                        applyUV(vtStart, ref vmesh.UV, 0, vtStart.Length, StartMaterialSettings, b);
                    }
                    #endregion
                }

                if (genEnd)
                {
                    #region --- End Cap ---

                    var tess = new Tess();
                    tess.UsePooling = true;
                    tess.AddContour(make2DSegment(vol, 0));

                    for (int h = 0; h < holes.Count; h++)
                    {
                        if (holes[h].Count < 3)
                        {
                            OutVMesh.SetData(null);
                            UIMessages.Add("Hole Cross has <3 Vertices: Can't create Caps!");
                            return;
                        }
                        tess.AddContour(make2DSegment(holes[h], 0));
                    }
                    tess.Tessellate(WindingRule.EvenOdd, ElementType.Polygons, 3);
                    vtEnd = UnityLibTessUtility.FromContourVertex(tess.Vertices);
                    Bounds b;
                    int l = vmesh.Vertex.Length;
                    vmesh.Vertex = vmesh.Vertex.AddRange<Vector3>(applyMat(vtEnd, getMat(vol, vol.Count - 1, true), out b));
                    int[] tris = tess.Elements;
                    if (!ReverseTriOrder)
                        flipTris(ref tris, 0, tris.Length);
                    for (int i = 0; i < tris.Length; i++)
                        tris[i] += l;
                    if (!CloneStartCap && StartMaterial != EndMaterial)
                    {
                        vmesh.AddSubMesh(new CGVSubMesh(tris, EndMaterial));
                    }
                    else
                    {
                        submesh.Material = StartMaterial;
                        submesh.Triangles = submesh.Triangles.AddRange<int>(tris);
                    }

                    if (GenerateUV)
                    {
                        System.Array.Resize<Vector2>(ref vmesh.UV, vmesh.UV.Length + vtEnd.Length);
                        applyUV(vtEnd, ref vmesh.UV, vtStart.Length, vtEnd.Length, (CloneStartCap) ? StartMaterialSettings : EndMaterialSettings, b);
                    }


                    #endregion

                }


                OutVMesh.SetData(vmesh);
            }
        }

        #endregion

        #region ### Privates ###
        /*! \cond PRIVATE */

        Matrix4x4 getMat(CGVolume vol, int index, bool inverse)
        {
            if (inverse)
            {
                var Q = Quaternion.LookRotation(vol.Direction[index], vol.Normal[index]);
                return Matrix4x4.TRS(vol.Position[index], Q, Vector3.one);
            }
            else
            {
                var Q = Quaternion.Inverse(Quaternion.LookRotation(vol.Direction[index], vol.Normal[index]));
                return Matrix4x4.TRS(-(Q * vol.Position[index]), Q, Vector3.one);
            }
        }


        void flipTris(ref int[] indices, int start, int end)
        {
            int tmp;
            for (int i = start; i < end; i += 3)
            {
                tmp = indices[i];
                indices[i] = indices[i + 2];
                indices[i + 2] = tmp;
            }
        }

        Vector3[] applyMat(Vector3[] vt, Matrix4x4 mat, out Bounds bounds)
        {
            var res = new Vector3[vt.Length];
            float lx = float.MaxValue;
            float ly = float.MaxValue;
            float hx = float.MinValue;
            float hy = float.MinValue;

            for (int i = 0; i < vt.Length; i++)
            {
                lx = Mathf.Min(vt[i].x, lx);
                ly = Mathf.Min(vt[i].y, ly);
                hx = Mathf.Max(vt[i].x, hx);
                hy = Mathf.Max(vt[i].y, hy);
                res[i] = mat.MultiplyPoint(vt[i]);
            }
            Vector3 sz = new Vector3(Mathf.Abs(hx - lx), Mathf.Abs(hy - ly));
            bounds = new Bounds(new Vector3(lx + sz.x / 2, ly + sz.y / 2, 0), sz);
            return res;
        }



        /// <summary>
        /// trs vertices to eliminate Z and eleminate duplicates
        /// </summary>
        ContourVertex[] make2DSegment(CGVolume vol, int index)
        {
            Matrix4x4 m = getMat(vol, index, false);
            int idx = vol.GetSegmentIndex(index);

            ContourVertex[] res = new ContourVertex[vol.CrossSize];
            for (int i = 0; i < vol.CrossSize; i++)
                res[i] = m.MultiplyPoint(vol.Vertex[idx + i]).ContourVertex();

            return res;
        }

        // Attention: p needs to be 2D (X/Y-Plane)
        void applyUV(Vector3[] vts, ref Vector2[] uvArray, int index, int count, CGMaterialSettings mat, Bounds bounds)
        {
            float u, v;
            float w = bounds.size.x;
            float h = bounds.size.y;

            float mx = bounds.min.x;
            float my = bounds.min.y;

            float fx = mat.UVScale.x;
            float fy = mat.UVScale.y;

            switch (mat.KeepAspect)
            {
                case CGKeepAspectMode.ScaleU:
                    float sw = w * mat.UVScale.x;
                    float sh = h * mat.UVScale.y;
                    fx *= sw / sh;
                    break;
                case CGKeepAspectMode.ScaleV:
                    float sw1 = w * mat.UVScale.x;
                    float sh1 = h * mat.UVScale.y;
                    fy *= sh1 / sw1;
                    break;
            }

            if (mat.UVRotation != 0)
            {
                float uvRotRad = (mat.UVRotation) * Mathf.Deg2Rad;
                float sn = Mathf.Sin(uvRotRad);
                float cs = Mathf.Cos(uvRotRad);
                float ox, oy;
                float fx2 = fx * 0.5f;
                float fy2 = fy * 0.5f;
                for (int i = 0; i < count; i++)
                {
                    u = (vts[i].x - mx) / w * fx;
                    v = (vts[i].y - my) / h * fy;
                    ox = u - fx2;
                    oy = v - fy2;
                    u = (cs * ox - sn * oy + fx2) + mat.UVOffset.x;
                    v = (sn * ox + cs * oy + fy2) + mat.UVOffset.y;

                    uvArray[i + index] = (mat.SwapUV) ? new Vector2(v, u) : new Vector2(u, v);

                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    u = mat.UVOffset.x + (vts[i].x - mx) / w * fx;
                    v = mat.UVOffset.y + (vts[i].y - my) / h * fy;
                    uvArray[i + index] = (mat.SwapUV) ? new Vector2(v, u) : new Vector2(u, v);
                }
            }



        }


        /*! \endcond */
        #endregion

    }
}
