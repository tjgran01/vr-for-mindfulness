﻿using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

namespace CTS
{
    /// <summary>
    /// Editor script for Complete Terrain Shader (CTS)
    /// </summary>
    [CustomEditor(typeof(CompleteTerrainShader))]
    public class CompleteTerrainShaderEditor : Editor
    {
        private GUIStyle m_boxStyle;
        private GUIStyle m_wrapStyle;
        private GUIStyle m_wrapHelpStyle;
        private GUIStyle m_descWrapStyle;
        private CompleteTerrainShader m_shader;
        private bool m_globalHelp = false;

        #region Menu Commands

        /// <summary>
        /// Set linear deferred lighting
        /// </summary>
        [MenuItem("Component/CTS/Set Linear Deffered", false, 40)]
        public static void SetLinearDeferredLighting()
        {
            PlayerSettings.colorSpace = ColorSpace.Linear;

            var tier1 = EditorGraphicsSettings.GetTierSettings(EditorUserBuildSettings.selectedBuildTargetGroup, GraphicsTier.Tier1);
            tier1.renderingPath = RenderingPath.DeferredShading;
            EditorGraphicsSettings.SetTierSettings(EditorUserBuildSettings.selectedBuildTargetGroup, GraphicsTier.Tier1, tier1);

            var tier2 = EditorGraphicsSettings.GetTierSettings(EditorUserBuildSettings.selectedBuildTargetGroup, GraphicsTier.Tier2);
            tier2.renderingPath = RenderingPath.DeferredShading;
            EditorGraphicsSettings.SetTierSettings(EditorUserBuildSettings.selectedBuildTargetGroup, GraphicsTier.Tier2, tier2);

            var tier3 = EditorGraphicsSettings.GetTierSettings(EditorUserBuildSettings.selectedBuildTargetGroup, GraphicsTier.Tier3);
            tier3.renderingPath = RenderingPath.DeferredShading;
            EditorGraphicsSettings.SetTierSettings(EditorUserBuildSettings.selectedBuildTargetGroup, GraphicsTier.Tier3, tier3);
        }

        /// <summary>
        /// Add terrain shader and create materials
        /// </summary>
        [MenuItem("Component/CTS/Add CTS To Terrain", false, 41)]
        public static void AddCTSToTerrain(MenuCommand menuCommand)
        {
            CTSTerrainManager.Instance.AddCTSToAllTerrains();
        }

        /// <summary>
        /// Add terrain shader and create materials
        /// </summary>
        [MenuItem("Component/CTS/Create And Apply Profile", false, 42)]
        public static void CreateCTSProfile1(MenuCommand menuCommand)
        {
            CTSProfile profile = ScriptableObject.CreateInstance<CTS.CTSProfile>();
            profile.GlobalDetailNormalMap = GetAsset("T_Detail_Normal_3.png", typeof(Texture2D)) as Texture2D;
            profile.GeoAlbedo = GetAsset("T_Geo_00.png", typeof(Texture2D)) as Texture2D;
            profile.SnowAlbedo = GetAsset("T_Ground_Snow_1_A_Sm.tga", typeof(Texture2D)) as Texture2D;
            profile.SnowNormal = GetAsset("T_Ground_Snow_1_N.tga", typeof(Texture2D)) as Texture2D;
            profile.SnowHeight = GetAsset("T_Ground_Snow_1_H.png", typeof(Texture2D)) as Texture2D;
            profile.SnowAmbientOcclusion = GetAsset("T_Ground_Snow_1_AO.tga", typeof(Texture2D)) as Texture2D;
            profile.SnowNoise = GetAsset("T_Ground_Snow_Noise_1.tga", typeof(Texture2D)) as Texture2D;
            profile.m_ctsDirectory = CompleteTerrainShader.GetCTSDirectory();
            Directory.CreateDirectory(profile.m_ctsDirectory + "Profiles/");
            AssetDatabase.CreateAsset(profile, string.Format("{0}Profiles/CTS_Profile_{1:yyMMdd-HHmm}.asset", profile.m_ctsDirectory, DateTime.Now));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            CTSTerrainManager.Instance.BroadcastProfileSelect(profile);
            EditorGUIUtility.PingObject(profile);
        }

        /// <summary>
        /// Add CTS runtime controller to the scene
        /// </summary>
        [MenuItem("Component/CTS/Add Weather Manager", false, 43)]
        public static void AddCTSRuntimeWeatherToScene(MenuCommand menuCommand)
        {
            //Add a weather manager
            GameObject ctsWeatherManager = GameObject.Find("CTS Weather Manager");
            if (ctsWeatherManager == null)
            {
                ctsWeatherManager = new GameObject();
                ctsWeatherManager.name = "CTS Weather Manager";
                ctsWeatherManager.AddComponent<CTSWeatherManager>();
                CompleteTerrainShader.SetDirty(ctsWeatherManager, false, false);
            }
            EditorGUIUtility.PingObject(ctsWeatherManager);

            //And now add weather controllers
            foreach (var terrain in Terrain.activeTerrains)
            {
                CompleteTerrainShader shader = terrain.gameObject.GetComponent<CompleteTerrainShader>();
                if (shader != null)
                {
                    CTSWeatherController controller = terrain.gameObject.GetComponent<CTSWeatherController>();
                    if (controller == null)
                    {
                        controller = terrain.gameObject.AddComponent<CTSWeatherController>();
                        CompleteTerrainShader.SetDirty(terrain, false, false);
                        CompleteTerrainShader.SetDirty(controller, false, false);
                    }
                }
            }
        }

        /// <summary>
        /// Add world API to scene
        /// </summary>
        [MenuItem("Component/CTS/Add World API Integration", false, 44)]
        public static void AddWorldAPIToScene(MenuCommand menuCommand)
        {
            //First - are we even here present
            Type worldAPIType = CompleteTerrainShader.GetType("WAPI.WorldManager");
            if (worldAPIType == null)
            {
                EditorUtility.DisplayDialog("World Manager API", "World Manager is not present in your project. Please go to http://www.procedural-worlds.com/blog/wapi/ to learn about it.", "OK");
                Application.OpenURL("http://www.procedural-worlds.com/blog/wapi/");
                Application.OpenURL("https://github.com/adamgoodrich/WorldManager");
                return;
            }

            //First add a weather manager
            GameObject ctsWeatherManager = GameObject.Find("CTS Weather Manager");
            if (ctsWeatherManager == null)
            {
                ctsWeatherManager = new GameObject();
                ctsWeatherManager.name = "CTS Weather Manager";
                ctsWeatherManager.AddComponent<CTSWeatherManager>();
                CompleteTerrainShader.SetDirty(ctsWeatherManager, false, false);
            }
            EditorGUIUtility.PingObject(ctsWeatherManager);

            //And now add weather controllers
            foreach (var terrain in Terrain.activeTerrains)
            {
                CompleteTerrainShader shader = terrain.gameObject.GetComponent<CompleteTerrainShader>();
                if (shader != null)
                {
                    CTSWeatherController controller = terrain.gameObject.GetComponent<CTSWeatherController>();
                    if (controller == null)
                    {
                        controller = terrain.gameObject.AddComponent<CTSWeatherController>();
                        CompleteTerrainShader.SetDirty(terrain, false, false);
                        CompleteTerrainShader.SetDirty(controller, false, false);
                    }
                }
            }

            //And now add world API integration component to weather manager
            #if WORLDAPI_PRESENT
            var worldAPIIntegration = ctsWeatherManager.GetComponent<CTSWorldAPIIntegration>();
            if (worldAPIIntegration == null)
            {
                worldAPIIntegration = ctsWeatherManager.AddComponent<CTSWorldAPIIntegration>();
            }
            #endif
        }

        /// <summary>
        /// Show the forum
        /// </summary>
        [MenuItem("Component/CTS/Show Forum...", false, 60)]
        public static void ShowForum()
        {
            Application.OpenURL(
                "https://forum.unity3d.com/threads/cts-complete-terrain-shader.477615/");
        }

        /// <summary>
        /// Show tutorial
        /// </summary>
        [MenuItem("Component/CTS/Show Tutorials...", false, 61)]
        public static void ShowTutorial()
        {
            Application.OpenURL("http://www.procedural-worlds.com/cts/tutorials/");
        }

        /// <summary>
        /// Show documentation
        /// </summary>
        [MenuItem("Component/CTS/Show Documentation...", false, 62)]
        public static void ShowDocumentation()
        {
            Application.OpenURL("http://www.procedural-worlds.com/cts/documentation/");
        }

        /// <summary>
        /// Show review option
        /// </summary>
        [MenuItem("Component/CTS/Please Review CTS...", false, 63)]
        public static void ShowAssetStore()
        {
            if (UnityEngine.Random.Range(0,2) == 0)
            {
                Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/91938?aid=1011lGkb");
            }
            else
            {
                Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/91938?aid=1101lSqC");
            }
        }

        /// <summary>
        /// Show review option
        /// </summary>
        [MenuItem("Component/CTS/About/Nature Manufacture...", false, 74)]
        public static void ShowNatureManufacture()
        {
            Application.OpenURL("https://www.assetstore.unity3d.com/#!/list/28499-naturemanufacture-assets?aid=1011lGkb");
        }

        /// <summary>
        /// Show review option
        /// </summary>
        [MenuItem("Component/CTS/About/Procedural Worlds...", false, 75)]
        public static void ShowNatureManufacture2()
        {
            Application.OpenURL("https://www.assetstore.unity3d.com/#!/list/2231-gaia-compatible?aid=1101lSqC");
        }

#endregion

        /// <summary>
        /// Called when we select this in the scene
        /// </summary>
        void OnEnable()
        {
            //Check for target
            if (target == null)
            {
                return;
            }

            //Force a shader update
            CTSTerrainManager.Instance.RegisterAllShaders();

            //Setup target
            m_shader = (CompleteTerrainShader) target;
        }

        /// <summary>
        /// Editor UX
        /// </summary>
        public override void OnInspectorGUI()
        {
            //Set the target
            m_shader = (CompleteTerrainShader) target;

            if (m_shader == null)
            {
                return;
            }

            #region Setup and introduction

            //Set up the box style
            if (m_boxStyle == null)
            {
                m_boxStyle = new GUIStyle(GUI.skin.box);
                m_boxStyle.normal.textColor = GUI.skin.label.normal.textColor;
                m_boxStyle.fontStyle = FontStyle.Bold;
                m_boxStyle.alignment = TextAnchor.UpperLeft;
            }

            //Setup the wrap style
            if (m_wrapStyle == null)
            {
                m_wrapStyle = new GUIStyle(GUI.skin.label);
                m_wrapStyle.fontStyle = FontStyle.Normal;
                m_wrapStyle.wordWrap = true;
            }

            if (m_wrapHelpStyle == null)
            {
                m_wrapHelpStyle = new GUIStyle(GUI.skin.label);
                m_wrapHelpStyle.richText = true;
                m_wrapHelpStyle.wordWrap = true;
            }

            //Text intro
            GUILayout.BeginVertical(string.Format("CTS ({0}.{1})", CTSConstants.MajorVersion, CTSConstants.MinorVersion), m_boxStyle);
            if (m_globalHelp)
            {
                Rect rect = EditorGUILayout.BeginVertical();
                rect.x = rect.width - 10;
                rect.width = 25;
                rect.height = 20;
                if (GUI.Button(rect, "?-"))
                {
                    m_globalHelp = !m_globalHelp;
                }
                EditorGUILayout.EndVertical();
            }
            else
            {
                Rect rect = EditorGUILayout.BeginVertical();
                //rect.y -= 10f;
                rect.x = rect.width - 10;
                rect.width = 25;
                rect.height = 20;
                if (GUI.Button(rect, "?+"))
                {
                    m_globalHelp = !m_globalHelp;
                }
                EditorGUILayout.EndVertical();
            }
            GUILayout.Space(20);
            EditorGUILayout.LabelField("Welcome to CTS. Click ? for help.", m_wrapStyle);
            DrawHelpSectionLabel("Overview");

            if (m_globalHelp)
            {
                if (GUILayout.Button(GetLabel("View Online Tutorials & Docs")))
                {
                    Application.OpenURL("http://www.procedural-worlds.com/cts/");
                }
            }

            GUILayout.EndVertical();
            #endregion

            //Monitor for changes
            EditorGUI.BeginChangeCheck();

            GUILayout.Space(5);

            GUILayout.BeginVertical(m_boxStyle);

            CTSProfile profile = (CTSProfile)EditorGUILayout.ObjectField(GetLabel("Profile"), m_shader.Profile, typeof(CTSProfile), false);
            DrawHelpLabel("Profile");

            EditorGUILayout.LabelField(GetLabel("Global NormalMap"));
            DrawHelpLabel("Global NormalMap");
            EditorGUI.indentLevel++;

            bool autobakeNormalMap = EditorGUILayout.Toggle(GetLabel("AutoBake"), m_shader.AutoBakeNormalMap);
            DrawHelpLabel("AutoBake");

            Texture2D globalNormal = (Texture2D)EditorGUILayout.ObjectField(GetLabel("Normal Map"), m_shader.NormalMap, typeof(Texture2D), false, GUILayout.Height(16f));
            DrawHelpLabel("NormalMap");

            EditorGUI.indentLevel--;

            EditorGUILayout.LabelField(GetLabel("Global ColorMap"));
            DrawHelpLabel("Global ColorMap");
            EditorGUI.indentLevel++;

            bool autobakeColorMap = EditorGUILayout.Toggle(GetLabel("AutoBake"), m_shader.AutoBakeColorMap);
            DrawHelpLabel("AutoBake");
            bool autoBakeGrassIntoColorMap = m_shader.AutoBakeGrassIntoColorMap;
            float autoBakeGrassMixStrength = m_shader.AutoBakeGrassMixStrength;
            float autoBakeGrassDarkenAmount = m_shader.AutoBakeGrassDarkenAmount;
            autoBakeGrassIntoColorMap = EditorGUILayout.Toggle(GetLabel("Bake Grass"), autoBakeGrassIntoColorMap);
            DrawHelpLabel("Bake Grass");
            if (autoBakeGrassIntoColorMap)
            {
                EditorGUI.indentLevel++;
                autoBakeGrassMixStrength = EditorGUILayout.Slider(GetLabel("Grass Strength"), autoBakeGrassMixStrength, 0f, 1f);
                DrawHelpLabel("Grass Strength");
                autoBakeGrassDarkenAmount = EditorGUILayout.Slider(GetLabel("Darken Strength"), autoBakeGrassDarkenAmount, 0f, 1f);
                DrawHelpLabel("Darken Strength");
                EditorGUI.indentLevel--;
            }

            Texture2D globalColorMap = (Texture2D)EditorGUILayout.ObjectField(GetLabel("Color Map"), m_shader.ColorMap, typeof(Texture2D), false, GUILayout.Height(16f));
            DrawHelpLabel("ColorMap");

            EditorGUI.indentLevel--;

            bool useCutout = EditorGUILayout.Toggle(GetLabel("Use Cutout"), m_shader.UseCutout);
            DrawHelpLabel("Use Cutout");
            float heightCutout = m_shader.CutoutHeight;
            Texture2D globalCutoutMask = m_shader.CutoutMask;
            if (useCutout)
            {
                EditorGUI.indentLevel++;
                heightCutout = EditorGUILayout.FloatField(GetLabel("Cutout Below"), heightCutout);
                DrawHelpLabel("Cutout Below");
                globalCutoutMask = (Texture2D)EditorGUILayout.ObjectField(GetLabel("Cutout Mask"), globalCutoutMask, typeof(Texture2D), false, GUILayout.Height(16f));
                DrawHelpLabel("Cutout Mask");
                EditorGUI.indentLevel--;
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(GetLabel("Bake NormalMap")))
            {
                m_shader.BakeTerrainNormals();
                globalNormal = m_shader.NormalMap;
            }
            if (GUILayout.Button(GetLabel("Bake ColorMap")))
            {
                if (!autoBakeGrassIntoColorMap)
                {
                    m_shader.BakeTerrainBaseMap();
                }
                else
                {
                    m_shader.BakeTerrainBaseMapWithGrass();
                }
                globalColorMap = m_shader.ColorMap;
            }
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.Space(5);

            #region Handle changes

            //Check for changes, make undo record, make changes and let editor know we are dirty
            if (EditorGUI.EndChangeCheck())
            {
                CompleteTerrainShader.SetDirty(m_shader, false, false);
                m_shader.Profile = profile;
                m_shader.NormalMap = globalNormal;
                m_shader.AutoBakeNormalMap = autobakeNormalMap;
                m_shader.ColorMap = globalColorMap;
                m_shader.AutoBakeColorMap = autobakeColorMap;
                m_shader.AutoBakeGrassIntoColorMap = autoBakeGrassIntoColorMap;
                m_shader.AutoBakeGrassMixStrength = autoBakeGrassMixStrength;
                m_shader.AutoBakeGrassDarkenAmount = autoBakeGrassDarkenAmount;
                m_shader.UseCutout = useCutout;
                m_shader.CutoutMask = globalCutoutMask;
                m_shader.CutoutHeight = heightCutout;
                m_shader.UpdateMaterialAndShader();
            }
            #endregion
        }

        /// <summary>
        /// Returns the first asset that matches the file path and name passed. Will try
        /// full path first, then will try just the file name.
        /// </summary>
        /// <param name="fileNameOrPath">File name as standalone or fully pathed</param>
        /// <returns>Object or null if it was not found</returns>
        public static UnityEngine.Object GetAsset(string fileNameOrPath, Type assetType)
        {
            if (!string.IsNullOrEmpty(fileNameOrPath))
            {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(fileNameOrPath, assetType);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    string path = GetAssetPath(Path.GetFileName(fileNameOrPath));
                    if (!string.IsNullOrEmpty(path))
                    {
                        return AssetDatabase.LoadAssetAtPath(path, assetType);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Get the asset path of the first thing that matches the name
        /// </summary>
        /// <param name="fileName">File name to search for</param>
        /// <returns></returns>
        public static string GetAssetPath(string fileName)
        {
            string fName = Path.GetFileNameWithoutExtension(fileName);
            string[] assets = AssetDatabase.FindAssets(fName, null);
            for (int idx = 0; idx < assets.Length; idx++)
            {
                string path = AssetDatabase.GUIDToAssetPath(assets[idx]);
                if (Path.GetFileName(path) == fileName)
                {
                    return path;
                }
            }
            return "";
        }

        /// <summary>
        /// Get a content label - look the tooltip up if possible
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        GUIContent GetLabel(string name)
        {
            string tooltip = "";
            if (m_tooltips.TryGetValue(name, out tooltip))
            {
                return new GUIContent(name, tooltip);
            }
            else
            {
                return new GUIContent(name);
            }
        }

        /// <summary>
        /// Draw some help
        /// </summary>
        /// <param name="title"></param>
        private void DrawHelpSectionLabel(string title)
        {
            if (m_globalHelp)
            {
                string description;
                if (m_tooltips.TryGetValue(title, out description))
                {
                    GUILayout.BeginVertical(m_boxStyle);
                    if (EditorGUIUtility.isProSkin)
                    {
                        EditorGUILayout.LabelField(string.Format("<color=#CBC5C1><b>{0}</b>\n\n{1}\n</color>", title, description), m_wrapHelpStyle);
                    }
                    else
                    {
                        EditorGUILayout.LabelField(string.Format("<color=#3F3D40><b>{0}</b>\n\n{1}\n</color>", title, description), m_wrapHelpStyle);
                    }
                    GUILayout.EndVertical();
                }
            }
        }

        /// <summary>
        /// Draw some help
        /// </summary>
        /// <param name="title"></param>
        private void DrawHelpLabel(string title)
        {
            if (m_globalHelp)
            {
                string description;
                if (m_tooltips.TryGetValue(title, out description))
                {
                    //EditorGUILayout.LabelField(string.Format("<color=lightblue><b>{0}</b>\n{1}</color>", title, description), m_wrapHelpStyle);
                    EditorGUI.indentLevel++;
                    if (EditorGUIUtility.isProSkin)
                    {
                        EditorGUILayout.LabelField(string.Format("<color=#98918F>{0}</color>", description), m_wrapHelpStyle);
                    }
                    else
                    {
                        EditorGUILayout.LabelField(string.Format("<color=#6F6C6F>{0}</color>", description), m_wrapHelpStyle);
                    }
                    EditorGUI.indentLevel--;
                }
            }
        }

        /// <summary>
        /// The tooltips
        /// </summary>
        static Dictionary<string, string> m_tooltips = new Dictionary<string, string>
        {
            { "Overview", "    CTS is a terrain shading system driven by profiles. To use CTS, first add CTS to the terrain by selecting Component-> CTS-> Add CTS To Terrain. Then create and apply a new profile by selecting Component-> CTS-> Create And Apply Profile, or by dragging an existing profile into the profile slot, or by hitting Apply Profile on an existing profile.\n\n    To see the latest documentation and video tutorials please click on the button below."},
            { "Mode", "The mode this terrain is in.\n<b>Design Mode</b> - Changes are made via the currently selected profile.\n<b>Runtime Mode</b> - The profile is disconnected from the terrain to reduce runtime memory overhead."},
            { "Profile", "Drop your CTS profile here. Alternatively select a profile and then click Apply Profile. Or to create and apply a new apply a new profile select Component-> CTS-> Create And Apply Profile."},
            { "AutoBake", "Autobake this map when the Bake Terrains button is clicked on the profile."},
            { "Global NormalMap", "Global Normal Map for this terrain tile. It is used to highlight distant terrain features. Strength of application is controlled via Global Normal Power setting in the profile."},
            { "NormalMap", "Global Normal Map for this terrain tile."},
            { "Global ColorMap", "Global Color Map for this terrain tile. Use this to blend additional color detail into the terrain to add interest. Strength of application is controlled via ColorMap Settings in the profile. NOTE: The alpha channel is used to store the cutout mask if cutout masking has been chosen."},
            { "ColorMap", "Color Map for this terrain tile. NOTE: The alpha channel is used to store the cutout mask if cutout masking has been chosen."},
            { "Bake Grass", "Bake the dominant grass color into the color map that is generated to add additional interest to the colormaps."},
            { "Grass Strength", "How strongly the grass should be baked into the underlying color map."},
            { "Darken Strength", "How strongly the baked grass should darkened when baked into the underlying color map."},
            { "Use Cutout", "Enable or disable the use of cutouts. NOTE: Cutouts are more expensive to render, so use only when needed."},
            { "Cutout Below", "Cut out the drawing of all terrain below this height."},
            { "Cutout Mask", "Mask where the cutout will be drawn. This will be baked into the alpha channel of the Global ColorMap."},
        };
    }
}