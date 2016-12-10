// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
// /*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:True,rprd:True,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:2865,x:35876,y:35458,varname:node_2865,prsc:2|diff-3827-OUT,spec-2191-OUT,gloss-4832-OUT,emission-33-OUT,difocc-9818-OUT,spcocc-9818-OUT;n:type:ShaderForge.SFN_Tex2d,id:7736,x:30241,y:34515,varname:tex_b_edge,prsc:2,tex:1769c1724fffab2499432e75c63f041e,ntxv:0,isnm:False|UVIN-9746-OUT,TEX-1879-TEX;n:type:ShaderForge.SFN_Tex2d,id:8866,x:30241,y:34677,varname:tex_c_edge,prsc:2,tex:1769c1724fffab2499432e75c63f041e,ntxv:0,isnm:False|UVIN-5224-OUT,TEX-1879-TEX;n:type:ShaderForge.SFN_Tex2d,id:9143,x:30241,y:34354,varname:tex_a_edge,prsc:2,tex:1769c1724fffab2499432e75c63f041e,ntxv:0,isnm:False|UVIN-3378-OUT,TEX-1879-TEX;n:type:ShaderForge.SFN_Tex2d,id:2992,x:30572,y:35285,ptovrint:False,ptlb:SurfaceTexAttributes,ptin:_SurfaceTexAttributes,varname:_Curvature,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:81c59fc3c3aabf44b8b16a992b51cc99,ntxv:2,isnm:False;n:type:ShaderForge.SFN_NormalVector,id:3649,x:28910,y:33830,prsc:2,pt:False;n:type:ShaderForge.SFN_Transform,id:5135,x:29109,y:33830,varname:node_5135,prsc:2,tffrom:0,tfto:1|IN-3649-OUT;n:type:ShaderForge.SFN_Abs,id:4638,x:29109,y:33675,varname:node_4638,prsc:2|IN-5135-XYZ;n:type:ShaderForge.SFN_Power,id:5548,x:29462,y:33842,varname:node_5548,prsc:2|VAL-4638-OUT,EXP-4199-OUT;n:type:ShaderForge.SFN_Slider,id:4199,x:29030,y:34027,ptovrint:False,ptlb:TriplanarSharpness,ptin:_TriplanarSharpness,varname:_Sharpness,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:6.265205,max:30;n:type:ShaderForge.SFN_ComponentMask,id:5486,x:29792,y:33558,varname:node_5486,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5548-OUT;n:type:ShaderForge.SFN_ComponentMask,id:9393,x:29792,y:33709,varname:node_9393,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-5548-OUT;n:type:ShaderForge.SFN_ComponentMask,id:8724,x:29792,y:33892,varname:node_8724,prsc:2,cc1:2,cc2:-1,cc3:-1,cc4:-1|IN-5548-OUT;n:type:ShaderForge.SFN_Divide,id:3869,x:30244,y:33846,varname:divide_mask,prsc:2|A-5548-OUT,B-3640-OUT;n:type:ShaderForge.SFN_Add,id:8130,x:30025,y:33690,varname:node_8130,prsc:2|A-5486-OUT,B-9393-OUT,C-8724-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:4421,x:28806,y:34519,varname:node_4421,prsc:2;n:type:ShaderForge.SFN_ObjectPosition,id:4344,x:28806,y:34663,varname:node_4344,prsc:2;n:type:ShaderForge.SFN_Subtract,id:2611,x:29066,y:34519,varname:node_2611,prsc:2|A-4421-XYZ,B-4344-XYZ;n:type:ShaderForge.SFN_Transform,id:2564,x:29274,y:34519,varname:node_2564,prsc:2,tffrom:0,tfto:1|IN-2611-OUT;n:type:ShaderForge.SFN_Vector1,id:6551,x:29274,y:34692,varname:node_6551,prsc:2,v1:1;n:type:ShaderForge.SFN_Divide,id:3383,x:29487,y:34519,varname:node_3383,prsc:2|A-2564-XYZ,B-6551-OUT;n:type:ShaderForge.SFN_ComponentMask,id:3378,x:29823,y:34354,varname:node_3378,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-3383-OUT;n:type:ShaderForge.SFN_ComponentMask,id:9746,x:29823,y:34517,varname:node_9746,prsc:2,cc1:0,cc2:2,cc3:-1,cc4:-1|IN-3383-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5224,x:29823,y:34679,varname:node_5224,prsc:2,cc1:1,cc2:2,cc3:-1,cc4:-1|IN-3383-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:1879,x:29823,y:34865,ptovrint:False,ptlb:TriplanarAttributes,ptin:_TriplanarAttributes,varname:_Edge,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1769c1724fffab2499432e75c63f041e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:3640,x:30244,y:33669,varname:node_3640,prsc:2|A-8130-OUT,B-8130-OUT,C-8130-OUT;n:type:ShaderForge.SFN_Slider,id:4674,x:32254,y:34871,ptovrint:False,ptlb:CurvatureWearFactor,ptin:_CurvatureWearFactor,varname:_EdgeFactor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Code,id:3351,x:32655,y:34851,varname:node_3351,prsc:2,code:cgBlAHQAdQByAG4AIABwAG8AdwAgACgAcwBhAHQAdQByAGEAdABlACAAKABtAGEAcwBrACAAKgAgACgAKAAxAC0AaABlAGkAZwBoAHQAKQAgACoAIAAwAC4ANQAgACsAIABoAGUAaQBnAGgAdAAgACoAIAAyACkAIAAqACAAZgBhAGMAdABvAHIAKQAsACAAMgAxACkAOwA=,output:0,fname:HeightLerp3,width:247,height:112,input:0,input:0,input:0,input_1_label:height,input_2_label:factor,input_3_label:mask|A-9119-OUT,B-4674-OUT,C-9235-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5715,x:32191,y:34311,cmnt:Triplanar albedo,varname:node_5715,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-1536-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2648,x:30786,y:34164,varname:node_2648,prsc:2,cc1:2,cc2:-1,cc3:-1,cc4:-1|IN-3869-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2924,x:30786,y:33849,varname:node_2924,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3869-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5323,x:30786,y:34004,varname:node_5323,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-3869-OUT;n:type:ShaderForge.SFN_Multiply,id:2590,x:31401,y:34043,cmnt:Directional masking,varname:node_2590,prsc:2|A-2488-OUT,B-2924-OUT;n:type:ShaderForge.SFN_Multiply,id:799,x:31401,y:34193,varname:node_799,prsc:2|A-4389-OUT,B-5323-OUT;n:type:ShaderForge.SFN_Multiply,id:8526,x:31401,y:34350,varname:node_8526,prsc:2|A-5860-OUT,B-2648-OUT;n:type:ShaderForge.SFN_Add,id:4905,x:31730,y:34312,varname:node_4905,prsc:2|A-2590-OUT,B-799-OUT,C-8526-OUT;n:type:ShaderForge.SFN_Clamp01,id:1536,x:31909,y:34311,cmnt:Full triplanar,varname:node_1536,prsc:2|IN-4905-OUT;n:type:ShaderForge.SFN_Append,id:4389,x:30786,y:34513,varname:node_4389,prsc:2|A-7736-RGB,B-7736-A;n:type:ShaderForge.SFN_Append,id:5860,x:30786,y:34353,varname:node_5860,prsc:2|A-9143-RGB,B-9143-A;n:type:ShaderForge.SFN_Append,id:2488,x:30786,y:34676,varname:node_2488,prsc:2|A-8866-RGB,B-8866-A;n:type:ShaderForge.SFN_Multiply,id:9235,x:32332,y:34959,varname:node_9235,prsc:2|A-9549-OUT,B-7737-OUT,C-9089-OUT;n:type:ShaderForge.SFN_Power,id:7737,x:30766,y:35558,varname:node_7737,prsc:2|VAL-5983-OUT,EXP-6333-OUT;n:type:ShaderForge.SFN_Vector1,id:6333,x:30766,y:35702,varname:node_6333,prsc:2,v1:3;n:type:ShaderForge.SFN_Vector1,id:1897,x:30399,y:35696,varname:node_1897,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Add,id:1895,x:30399,y:35558,varname:node_1895,prsc:2|A-162-OUT,B-1897-OUT;n:type:ShaderForge.SFN_Clamp01,id:5983,x:30577,y:35558,varname:node_5983,prsc:2|IN-1895-OUT;n:type:ShaderForge.SFN_Slider,id:9180,x:31884,y:34872,ptovrint:False,ptlb:CurvaturePower,ptin:_CurvaturePower,varname:node_9180,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:1,max:8;n:type:ShaderForge.SFN_Clamp01,id:4503,x:31401,y:34961,varname:node_4503,prsc:2|IN-4424-OUT;n:type:ShaderForge.SFN_Power,id:9549,x:31962,y:34959,varname:node_9549,prsc:2|VAL-9659-OUT,EXP-9180-OUT;n:type:ShaderForge.SFN_Relay,id:162,x:30986,y:35460,cmnt:AO,varname:node_162,prsc:2|IN-2992-A;n:type:ShaderForge.SFN_Relay,id:1070,x:30986,y:35211,cmnt:Curvature,varname:node_1070,prsc:2|IN-2992-R;n:type:ShaderForge.SFN_Relay,id:9089,x:30986,y:35302,cmnt:Cavity,varname:node_9089,prsc:2|IN-2992-G;n:type:ShaderForge.SFN_Relay,id:8590,x:30986,y:35382,cmnt:Color,varname:node_8590,prsc:2|IN-2992-B;n:type:ShaderForge.SFN_ComponentMask,id:9119,x:32191,y:34494,cmnt:Triplanar noise,varname:node_9119,prsc:2,cc1:3,cc2:-1,cc3:-1,cc4:-1|IN-1536-OUT;n:type:ShaderForge.SFN_Relay,id:8889,x:30986,y:35558,cmnt: AO based mask,varname:node_8889,prsc:2|IN-7737-OUT;n:type:ShaderForge.SFN_Slider,id:928,x:33290,y:35364,ptovrint:False,ptlb:MainMetalness,ptin:_MainMetalness,varname:node_928,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:4363,x:33290,y:35598,ptovrint:False,ptlb:MainSmoothness,ptin:_MainSmoothness,varname:node_4363,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:878,x:33447,y:34562,ptovrint:False,ptlb:MainAlbedo,ptin:_MainAlbedo,varname:node_878,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:7313,x:33290,y:35473,ptovrint:False,ptlb:EdgeMetalness,ptin:_EdgeMetalness,varname:_MainMetalness_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:6199,x:33290,y:35717,ptovrint:False,ptlb:EdgeSmoothness,ptin:_EdgeSmoothness,varname:_MainSmoothness_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8,max:1;n:type:ShaderForge.SFN_Color,id:1149,x:33447,y:34743,ptovrint:False,ptlb:EdgeAlbedo,ptin:_EdgeAlbedo,varname:_MainAlbedo_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.25,c3:0.25,c4:1;n:type:ShaderForge.SFN_Multiply,id:2568,x:33852,y:34562,cmnt:Blended albedo,varname:node_2568,prsc:2|A-878-RGB,B-4274-OUT;n:type:ShaderForge.SFN_Lerp,id:8518,x:33852,y:34722,varname:node_8518,prsc:2|A-2568-OUT,B-1149-RGB,T-8902-OUT;n:type:ShaderForge.SFN_Relay,id:8902,x:33506,y:34916,cmnt:Edge factor,varname:node_8902,prsc:2|IN-3351-OUT;n:type:ShaderForge.SFN_Lerp,id:2191,x:33851,y:35352,cmnt:Blended metalness,varname:node_2191,prsc:2|A-928-OUT,B-7313-OUT,T-8902-OUT;n:type:ShaderForge.SFN_Lerp,id:4832,x:33851,y:35539,cmnt:Blended smoothness,varname:node_4832,prsc:2|A-4363-OUT,B-6199-OUT,T-8902-OUT;n:type:ShaderForge.SFN_Relay,id:9818,x:33506,y:35840,cmnt:Occlusion,varname:node_9818,prsc:2|IN-162-OUT;n:type:ShaderForge.SFN_Relay,id:2935,x:33506,y:35942,cmnt:Emission,varname:node_2935,prsc:2|IN-4194-OUT;n:type:ShaderForge.SFN_Tex2d,id:2840,x:30578,y:35812,ptovrint:False,ptlb:SurfaceTexColors,ptin:_SurfaceTexColors,varname:node_2840,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Relay,id:4274,x:33506,y:35158,cmnt: Colormap,varname:node_4274,prsc:2|IN-2840-RGB;n:type:ShaderForge.SFN_Color,id:4343,x:33447,y:36048,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:_EdgeAlbedo_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.25,c3:0.25,c4:1;n:type:ShaderForge.SFN_Multiply,id:33,x:33848,y:35939,cmnt:Blended emission,varname:node_33,prsc:2|A-2935-OUT,B-4343-RGB,C-9089-OUT;n:type:ShaderForge.SFN_RemapRange,id:2859,x:33447,y:34169,cmnt:Add,varname:node_2859,prsc:2,frmn:0.5,frmx:1,tomn:0,tomx:1|IN-5273-OUT;n:type:ShaderForge.SFN_RemapRange,id:6967,x:33447,y:34355,cmnt:Multiply,varname:node_6967,prsc:2,frmn:0,frmx:0.5,tomn:0,tomx:1|IN-5273-OUT;n:type:ShaderForge.SFN_Multiply,id:3206,x:34355,y:34569,varname:node_3206,prsc:2|A-6967-OUT,B-5864-OUT;n:type:ShaderForge.SFN_Add,id:2388,x:34355,y:34422,cmnt:Overlay blend,varname:node_2388,prsc:2|A-2859-OUT,B-3206-OUT;n:type:ShaderForge.SFN_Clamp01,id:6598,x:34547,y:34422,varname:node_6598,prsc:2|IN-2388-OUT;n:type:ShaderForge.SFN_Slider,id:8625,x:33290,y:35257,ptovrint:False,ptlb:CurvatureAlbedoBoost,ptin:_CurvatureAlbedoBoost,varname:node_8625,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:9852,x:34073,y:34722,varname:node_9852,prsc:2|A-8518-OUT,B-1796-OUT;n:type:ShaderForge.SFN_Multiply,id:1796,x:33852,y:34878,varname:node_1796,prsc:2|A-9697-OUT,B-8625-OUT;n:type:ShaderForge.SFN_Clamp01,id:5864,x:34355,y:34721,varname:node_5864,prsc:2|IN-9852-OUT;n:type:ShaderForge.SFN_Relay,id:9697,x:33506,y:34999,cmnt:Curvature,varname:node_9697,prsc:2|IN-517-OUT;n:type:ShaderForge.SFN_Add,id:4424,x:31234,y:34961,varname:node_4424,prsc:2|A-1070-OUT,B-7517-OUT;n:type:ShaderForge.SFN_Slider,id:9822,x:31156,y:34738,ptovrint:False,ptlb:CurvatureBoostSelf,ptin:_CurvatureBoostSelf,varname:node_9822,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Relay,id:517,x:31599,y:35092,varname:node_517,prsc:2|IN-4503-OUT;n:type:ShaderForge.SFN_Multiply,id:7517,x:31234,y:34837,varname:node_7517,prsc:2|A-9822-OUT,B-1070-OUT;n:type:ShaderForge.SFN_Relay,id:4194,x:31854,y:35887,cmnt:Emissionsurface mask,varname:node_4194,prsc:2|IN-2840-A;n:type:ShaderForge.SFN_Vector3,id:3889,x:33183,y:34720,varname:node_3889,prsc:2,v1:0.5,v2:0.5,v3:0.5;n:type:ShaderForge.SFN_Lerp,id:5273,x:33183,y:34571,cmnt:Masking A,varname:node_5273,prsc:2|A-5715-OUT,B-3889-OUT,T-4194-OUT;n:type:ShaderForge.SFN_Clamp01,id:9659,x:31766,y:34961,varname:node_9659,prsc:2|IN-8915-OUT;n:type:ShaderForge.SFN_Add,id:8915,x:31599,y:34961,varname:node_8915,prsc:2|A-4503-OUT,B-9050-OUT;n:type:ShaderForge.SFN_Slider,id:9050,x:31521,y:34869,ptovrint:False,ptlb:CurvatureBoostUniform,ptin:_CurvatureBoostUniform,varname:_CurvatureBoostSelf_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Relay,id:7807,x:33506,y:35080,cmnt:Cavity,varname:node_7807,prsc:2|IN-9089-OUT;n:type:ShaderForge.SFN_Multiply,id:8512,x:34882,y:34763,varname:node_8512,prsc:2|A-6598-OUT,B-7807-OUT;n:type:ShaderForge.SFN_RemapRange,id:7572,x:33447,y:36229,cmnt:Add,varname:node_7572,prsc:2,frmn:0.5,frmx:1,tomn:0,tomx:0.1|IN-9119-OUT;n:type:ShaderForge.SFN_RemapRange,id:8137,x:33447,y:36415,cmnt:Multiply,varname:node_8137,prsc:2,frmn:0,frmx:0.5,tomn:0.9,tomx:1|IN-9119-OUT;n:type:ShaderForge.SFN_Add,id:2286,x:35289,y:34905,varname:node_2286,prsc:2|A-7572-OUT,B-9615-OUT;n:type:ShaderForge.SFN_Multiply,id:9615,x:35289,y:34765,cmnt:Occluded dirt,varname:node_9615,prsc:2|A-8512-OUT,B-8137-OUT;n:type:ShaderForge.SFN_Clamp01,id:9603,x:35474,y:34905,varname:node_9603,prsc:2|IN-2286-OUT;n:type:ShaderForge.SFN_Desaturate,id:2521,x:35672,y:34905,varname:node_2521,prsc:2|COL-9603-OUT,DES-1001-OUT;n:type:ShaderForge.SFN_Vector1,id:1001,x:35672,y:34834,varname:node_1001,prsc:2,v1:0.75;n:type:ShaderForge.SFN_Lerp,id:3827,x:35289,y:35076,varname:node_3827,prsc:2|A-2521-OUT,B-8512-OUT,T-3859-OUT;n:type:ShaderForge.SFN_Relay,id:3859,x:33476,y:36609,varname:node_3859,prsc:2|IN-8889-OUT;proporder:2840-2992-1879-4199-4674-9180-878-1149-928-7313-4363-6199-4343-8625-9822-9050;pass:END;sub:END;*/

Shader "Showcase/HardsurfaceTriplanarPacked" {
    Properties {
        _SurfaceTexColors ("SurfaceTexColors", 2D) = "white" {}
        _SurfaceTexAttributes ("SurfaceTexAttributes", 2D) = "black" {}
        _TriplanarAttributes ("TriplanarAttributes", 2D) = "white" {}
        _TriplanarSharpness ("TriplanarSharpness", Range(1, 30)) = 6.265205
        _CurvatureWearFactor ("CurvatureWearFactor", Range(0, 1)) = 1
        _CurvaturePower ("CurvaturePower", Range(1, 8)) = 1
        _MainAlbedo ("MainAlbedo", Color) = (1,1,1,1)
        _EdgeAlbedo ("EdgeAlbedo", Color) = (1,0.25,0.25,1)
        _MainMetalness ("MainMetalness", Range(0, 1)) = 0
        _EdgeMetalness ("EdgeMetalness", Range(0, 1)) = 1
        _MainSmoothness ("MainSmoothness", Range(0, 1)) = 0
        _EdgeSmoothness ("EdgeSmoothness", Range(0, 1)) = 0.8
        _Emission ("Emission", Color) = (1,0.25,0.25,1)
        _CurvatureAlbedoBoost ("CurvatureAlbedoBoost", Range(0, 1)) = 1
        _CurvatureBoostSelf ("CurvatureBoostSelf", Range(0, 1)) = 0
        _CurvatureBoostUniform ("CurvatureBoostUniform", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _SurfaceTexAttributes; uniform float4 _SurfaceTexAttributes_ST;
            uniform float _TriplanarSharpness;
            uniform sampler2D _TriplanarAttributes; uniform float4 _TriplanarAttributes_ST;
            uniform float _CurvatureWearFactor;
            float HeightLerp3( float height , float factor , float mask ){
            return pow (saturate (mask * ((1-height) * 0.5 + height * 2) * factor), 21);
            }
            
            uniform float _CurvaturePower;
            uniform float _MainMetalness;
            uniform float _MainSmoothness;
            uniform float4 _MainAlbedo;
            uniform float _EdgeMetalness;
            uniform float _EdgeSmoothness;
            uniform float4 _EdgeAlbedo;
            uniform sampler2D _SurfaceTexColors; uniform float4 _SurfaceTexColors_ST;
            uniform float4 _Emission;
            uniform float _CurvatureAlbedoBoost;
            uniform float _CurvatureBoostSelf;
            uniform float _CurvatureBoostUniform;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float3 node_3383 = (mul( unity_WorldToObject, float4((i.posWorld.rgb-objPos.rgb),0) ).xyz.rgb/1.0);
                float2 node_5224 = node_3383.gb;
                float4 tex_c_edge = tex2D(_TriplanarAttributes,TRANSFORM_TEX(node_5224, _TriplanarAttributes));
                float3 node_5548 = pow(abs(mul( unity_WorldToObject, float4(i.normalDir,0) ).xyz.rgb),_TriplanarSharpness);
                float node_8130 = (node_5548.r+node_5548.g+node_5548.b);
                float3 divide_mask = (node_5548/float3(node_8130,node_8130,node_8130));
                float2 node_9746 = node_3383.rb;
                float4 tex_b_edge = tex2D(_TriplanarAttributes,TRANSFORM_TEX(node_9746, _TriplanarAttributes));
                float2 node_3378 = node_3383.rg;
                float4 tex_a_edge = tex2D(_TriplanarAttributes,TRANSFORM_TEX(node_3378, _TriplanarAttributes));
                float4 node_1536 = saturate(((float4(tex_c_edge.rgb,tex_c_edge.a)*divide_mask.r)+(float4(tex_b_edge.rgb,tex_b_edge.a)*divide_mask.g)+(float4(tex_a_edge.rgb,tex_a_edge.a)*divide_mask.b))); // Full triplanar
                float node_9119 = node_1536.a; // Triplanar noise
                float4 _SurfaceTexAttributes_var = tex2D(_SurfaceTexAttributes,TRANSFORM_TEX(i.uv0, _SurfaceTexAttributes));
                float node_1070 = _SurfaceTexAttributes_var.r; // Curvature
                float node_4503 = saturate((node_1070+(_CurvatureBoostSelf*node_1070)));
                float node_162 = _SurfaceTexAttributes_var.a; // AO
                float node_7737 = pow(saturate((node_162+0.1)),3.0);
                float node_9089 = _SurfaceTexAttributes_var.g; // Cavity
                float node_8902 = HeightLerp3( node_9119 , _CurvatureWearFactor , (pow(saturate((node_4503+_CurvatureBoostUniform)),_CurvaturePower)*node_7737*node_9089) ); // Edge factor
                float gloss = lerp(_MainSmoothness,_EdgeSmoothness,node_8902);
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                UnityGI gi = UnityGlobalIllumination (d, 1, gloss, normalDirection);
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_9818 = node_162; // Occlusion
                float3 specularAO = node_9818;
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _SurfaceTexColors_var = tex2D(_SurfaceTexColors,TRANSFORM_TEX(i.uv0, _SurfaceTexColors));
                float node_4194 = _SurfaceTexColors_var.a; // Emissionsurface mask
                float3 node_5273 = lerp(node_1536.rgb,float3(0.5,0.5,0.5),node_4194); // Masking A
                float3 node_8512 = (saturate(((node_5273*2.0+-1.0)+((node_5273*2.0+0.0)*saturate((lerp((_MainAlbedo.rgb*_SurfaceTexColors_var.rgb),_EdgeAlbedo.rgb,node_8902)+(node_4503*_CurvatureAlbedoBoost))))))*node_9089);
                float3 diffuseColor = lerp(lerp(saturate(((node_9119*0.2+-0.1)+(node_8512*(node_9119*0.2+0.9)))),dot(saturate(((node_9119*0.2+-0.1)+(node_8512*(node_9119*0.2+0.9)))),float3(0.3,0.59,0.11)),0.75),node_8512,node_7737); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, lerp(_MainMetalness,_EdgeMetalness,node_8902), specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0.0, NdotL * visTerm * normTerm);//(NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 ); //nah, somethin not right with components of second max argument
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular) * specularAO;
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                indirectDiffuse *= node_9818; // Diffuse AO
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (node_4194*_Emission.rgb*node_9089);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _SurfaceTexAttributes; uniform float4 _SurfaceTexAttributes_ST;
            uniform float _TriplanarSharpness;
            uniform sampler2D _TriplanarAttributes; uniform float4 _TriplanarAttributes_ST;
            uniform float _CurvatureWearFactor;
            float HeightLerp3( float height , float factor , float mask ){
            return pow (saturate (mask * ((1-height) * 0.5 + height * 2) * factor), 21);
            }
            
            uniform float _CurvaturePower;
            uniform float _MainMetalness;
            uniform float _MainSmoothness;
            uniform float4 _MainAlbedo;
            uniform float _EdgeMetalness;
            uniform float _EdgeSmoothness;
            uniform float4 _EdgeAlbedo;
            uniform sampler2D _SurfaceTexColors; uniform float4 _SurfaceTexColors_ST;
            uniform float4 _Emission;
            uniform float _CurvatureAlbedoBoost;
            uniform float _CurvatureBoostSelf;
            uniform float _CurvatureBoostUniform;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float3 node_3383 = (mul( unity_WorldToObject, float4((i.posWorld.rgb-objPos.rgb),0) ).xyz.rgb/1.0);
                float2 node_5224 = node_3383.gb;
                float4 tex_c_edge = tex2D(_TriplanarAttributes,TRANSFORM_TEX(node_5224, _TriplanarAttributes));
                float3 node_5548 = pow(abs(mul( unity_WorldToObject, float4(i.normalDir,0) ).xyz.rgb),_TriplanarSharpness);
                float node_8130 = (node_5548.r+node_5548.g+node_5548.b);
                float3 divide_mask = (node_5548/float3(node_8130,node_8130,node_8130));
                float2 node_9746 = node_3383.rb;
                float4 tex_b_edge = tex2D(_TriplanarAttributes,TRANSFORM_TEX(node_9746, _TriplanarAttributes));
                float2 node_3378 = node_3383.rg;
                float4 tex_a_edge = tex2D(_TriplanarAttributes,TRANSFORM_TEX(node_3378, _TriplanarAttributes));
                float4 node_1536 = saturate(((float4(tex_c_edge.rgb,tex_c_edge.a)*divide_mask.r)+(float4(tex_b_edge.rgb,tex_b_edge.a)*divide_mask.g)+(float4(tex_a_edge.rgb,tex_a_edge.a)*divide_mask.b))); // Full triplanar
                float node_9119 = node_1536.a; // Triplanar noise
                float4 _SurfaceTexAttributes_var = tex2D(_SurfaceTexAttributes,TRANSFORM_TEX(i.uv0, _SurfaceTexAttributes));
                float node_1070 = _SurfaceTexAttributes_var.r; // Curvature
                float node_4503 = saturate((node_1070+(_CurvatureBoostSelf*node_1070)));
                float node_162 = _SurfaceTexAttributes_var.a; // AO
                float node_7737 = pow(saturate((node_162+0.1)),3.0);
                float node_9089 = _SurfaceTexAttributes_var.g; // Cavity
                float node_8902 = HeightLerp3( node_9119 , _CurvatureWearFactor , (pow(saturate((node_4503+_CurvatureBoostUniform)),_CurvaturePower)*node_7737*node_9089) ); // Edge factor
                float gloss = lerp(_MainSmoothness,_EdgeSmoothness,node_8902);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _SurfaceTexColors_var = tex2D(_SurfaceTexColors,TRANSFORM_TEX(i.uv0, _SurfaceTexColors));
                float node_4194 = _SurfaceTexColors_var.a; // Emissionsurface mask
                float3 node_5273 = lerp(node_1536.rgb,float3(0.5,0.5,0.5),node_4194); // Masking A
                float3 node_8512 = (saturate(((node_5273*2.0+-1.0)+((node_5273*2.0+0.0)*saturate((lerp((_MainAlbedo.rgb*_SurfaceTexColors_var.rgb),_EdgeAlbedo.rgb,node_8902)+(node_4503*_CurvatureAlbedoBoost))))))*node_9089);
                float3 diffuseColor = lerp(lerp(saturate(((node_9119*0.2+-0.1)+(node_8512*(node_9119*0.2+0.9)))),dot(saturate(((node_9119*0.2+-0.1)+(node_8512*(node_9119*0.2+0.9)))),float3(0.3,0.59,0.11)),0.75),node_8512,node_7737); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, lerp(_MainMetalness,_EdgeMetalness,node_8902), specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, NdotL * visTerm * normTerm); ;//max(0, (NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _SurfaceTexAttributes; uniform float4 _SurfaceTexAttributes_ST;
            uniform float _TriplanarSharpness;
            uniform sampler2D _TriplanarAttributes; uniform float4 _TriplanarAttributes_ST;
            uniform float _CurvatureWearFactor;
            float HeightLerp3( float height , float factor , float mask ){
            return pow (saturate (mask * ((1-height) * 0.5 + height * 2) * factor), 21);
            }
            
            uniform float _CurvaturePower;
            uniform float _MainMetalness;
            uniform float _MainSmoothness;
            uniform float4 _MainAlbedo;
            uniform float _EdgeMetalness;
            uniform float _EdgeSmoothness;
            uniform float4 _EdgeAlbedo;
            uniform sampler2D _SurfaceTexColors; uniform float4 _SurfaceTexColors_ST;
            uniform float4 _Emission;
            uniform float _CurvatureAlbedoBoost;
            uniform float _CurvatureBoostSelf;
            uniform float _CurvatureBoostUniform;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _SurfaceTexColors_var = tex2D(_SurfaceTexColors,TRANSFORM_TEX(i.uv0, _SurfaceTexColors));
                float node_4194 = _SurfaceTexColors_var.a; // Emissionsurface mask
                float4 _SurfaceTexAttributes_var = tex2D(_SurfaceTexAttributes,TRANSFORM_TEX(i.uv0, _SurfaceTexAttributes));
                float node_9089 = _SurfaceTexAttributes_var.g; // Cavity
                o.Emission = (node_4194*_Emission.rgb*node_9089);
                
                float3 node_3383 = (mul( unity_WorldToObject, float4((i.posWorld.rgb-objPos.rgb),0) ).xyz.rgb/1.0);
                float2 node_5224 = node_3383.gb;
                float4 tex_c_edge = tex2D(_TriplanarAttributes,TRANSFORM_TEX(node_5224, _TriplanarAttributes));
                float3 node_5548 = pow(abs(mul( unity_WorldToObject, float4(i.normalDir,0) ).xyz.rgb),_TriplanarSharpness);
                float node_8130 = (node_5548.r+node_5548.g+node_5548.b);
                float3 divide_mask = (node_5548/float3(node_8130,node_8130,node_8130));
                float2 node_9746 = node_3383.rb;
                float4 tex_b_edge = tex2D(_TriplanarAttributes,TRANSFORM_TEX(node_9746, _TriplanarAttributes));
                float2 node_3378 = node_3383.rg;
                float4 tex_a_edge = tex2D(_TriplanarAttributes,TRANSFORM_TEX(node_3378, _TriplanarAttributes));
                float4 node_1536 = saturate(((float4(tex_c_edge.rgb,tex_c_edge.a)*divide_mask.r)+(float4(tex_b_edge.rgb,tex_b_edge.a)*divide_mask.g)+(float4(tex_a_edge.rgb,tex_a_edge.a)*divide_mask.b))); // Full triplanar
                float node_9119 = node_1536.a; // Triplanar noise
                float3 node_5273 = lerp(node_1536.rgb,float3(0.5,0.5,0.5),node_4194); // Masking A
                float node_1070 = _SurfaceTexAttributes_var.r; // Curvature
                float node_4503 = saturate((node_1070+(_CurvatureBoostSelf*node_1070)));
                float node_162 = _SurfaceTexAttributes_var.a; // AO
                float node_7737 = pow(saturate((node_162+0.1)),3.0);
                float node_8902 = HeightLerp3( node_9119 , _CurvatureWearFactor , (pow(saturate((node_4503+_CurvatureBoostUniform)),_CurvaturePower)*node_7737*node_9089) ); // Edge factor
                float3 node_8512 = (saturate(((node_5273*2.0+-1.0)+((node_5273*2.0+0.0)*saturate((lerp((_MainAlbedo.rgb*_SurfaceTexColors_var.rgb),_EdgeAlbedo.rgb,node_8902)+(node_4503*_CurvatureAlbedoBoost))))))*node_9089);
                float3 diffColor = lerp(lerp(saturate(((node_9119*0.2+-0.1)+(node_8512*(node_9119*0.2+0.9)))),dot(saturate(((node_9119*0.2+-0.1)+(node_8512*(node_9119*0.2+0.9)))),float3(0.3,0.59,0.11)),0.75),node_8512,node_7737);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, lerp(_MainMetalness,_EdgeMetalness,node_8902), specColor, specularMonochrome );
                float roughness = 1.0 - lerp(_MainSmoothness,_EdgeSmoothness,node_8902);
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    // CustomEditor "ShaderForgeMaterialInspector"
}
