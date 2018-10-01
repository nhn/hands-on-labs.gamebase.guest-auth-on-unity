using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Toast.Tools.Util;

namespace Toast.GamebaseTools.SettingTool
{
    [InitializeOnLoad]
    public class SDKSettingToolWindow : EditorWindow
    {
        public const string version = "v1.3.0";

        enum PlatformType
        {
            UNITY,
            ANDROID,
            IOS
        }

        enum SDK_STATE
        {
            NONE,
            NOT_FILE,
            DOWNLOAD,
            EXTRACT,
        }

        enum UPDATE_STATE
        {
            NONE,
            MANDATORY,
            OPTIONAL,
            ERROR
        }

        private static string downloadPath;        
        private const string downloadURL = "http://static.toastoven.net/toastcloud/sdk_download/gamebase/";
        private const string fileNameXML = "GamebaseSDK.xml";
        private const string versionCheckXMLName = "SupportedSettingToolVersion.xml";
        public const string toastPath = "https://docs.toast.com/ko/Download/#game-gamebase";
        public const string assetStorePath = "http://u3d.as/12WT";
        private const string fileNameIOS = "GamebaseSDK-iOS.zip";
        private const string fileNameAndroid = "GamebaseSDK-Android.zip";
        private const string fileNameUnity = "GamebaseSDK-Unity.zip";
        private const string localSettingInfomationXML = "/TOAST/Editor/GamebaseTools/SettingTool/LocalSettingInfomation.xml";

        private const int offset = 251;
        private const int windowWidth = 191;
        private const int windowHeight = 64;
        private const int spaceTop = 260;
        private const int spaceWidth = 25;
        private const int spaceHeight = 15;

        private static SDKSettingToolWindow window;
        private static FileStream fileStream = null;

        private Rect scrollRect = new Rect(10, 252, 1002, 506);

        private static SDK_STATE sdkState = SDK_STATE.NONE;

        private static string downloadProgress = string.Empty;
        private static string extractProgress = string.Empty;

        private static int tapIndex = 0;

        private static string downloadFileName = string.Empty;
        private static string extractFileName = string.Empty;

        private static List<string> settingsPakageFiles = new List<string>();
        private static Dictionary<string, string> settingsLibsFiles = new Dictionary<string, string>();
        private static List<string> settingsDirectories = new List<string>();
        private static List<string> settingsExternalsDirectories = new List<string>();

        private static List<string> platforms = new List<string>();

        private static SettingToolVO.GamebaseSDKInfo gamebaseSDKInfo;
        private static SettingToolVO.GamebaseSDKInfo gamebaseLocalSettings;
        private static SettingToolVO.GamebaseSDKInfo drawSDKInfo;

        private static EditorCoroutine downloadFileCoroutine = null;
        private EditorCoroutine extractCoroutine = null;

        private static Vector2 scrollPos;

        private static int scrollMaxCountUnity;
        private static int scrollMaxCountIOS;
        private static int scrollMaxCountAndroid;

        private object lockObject = new object();
        private static UPDATE_STATE updateState = UPDATE_STATE.NONE;

        public static void ShowWindow()
        {
            CheckVersion((state) =>
            {
                if (UPDATE_STATE.ERROR == state)
                {
                    return;
                }

                updateState = state;

                if (null != window)
                {
                    window.Close();
                    window = null;
                }

                sdkState = SDK_STATE.NONE;

                LoadSDKSettingFromLocal();

                window = GetWindowWithRect<SDKSettingToolWindow>(new Rect(100, 100, 1024, 778), true, "Gamebase Settings");
            });
        }

        private void OnDestroy()
        {
            if (null != downloadFileCoroutine)
            {
                downloadFileCoroutine.Stop();
            }

            if (null != extractCoroutine)
            {
                extractCoroutine.Stop();
            }

            if (SDK_STATE.DOWNLOAD == sdkState || SDK_STATE.EXTRACT == sdkState)
            {
                if (null != fileStream)
                {
                    fileStream.Close();
                    fileStream = null;
                }

                DeleteDownloadSDK();
            }
        }

        private void CloseEditor()
        {
            lock (lockObject)
            {
                AssetDatabase.Refresh();
                window.Close();
                EditorGUIUtility.ExitGUI();
            }
        }

        private void OnGUI()
        {
            if (null == window)
            {
                return;
            }

            lock (lockObject)
            {
                GUILayout.Space(10f);

                GUILayout.BeginHorizontal();
                {
                    DrawSettingToolUpdate();
                    DrawDownloadSDK();
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(40f);
                DrawSDKSettings();
            }
        }

        #region DrawGUI
        private void DrawSettingToolUpdate()
        {
            GUILayout.BeginVertical();
            {
                GUIStyle guiStyle = new GUIStyle();
                guiStyle.normal.textColor = Color.white;
                guiStyle.fontSize = 18;

                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(12f);
                    GUILayout.Label("SettingTool", guiStyle, GUILayout.Width(100));

                    GUILayout.Space(10f);
                    guiStyle.alignment = TextAnchor.LowerLeft;
                    guiStyle.fontSize = 12;
                    guiStyle.normal.textColor = Color.white;
                    GUILayout.Label(version, guiStyle, GUILayout.Width(280), GUILayout.Height(18));
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(12f);

                DrawUpdateButton();
            }
            GUILayout.EndVertical();
        }

        private void DrawDownloadSDK()
        {
            GUILayout.BeginVertical();
            {
                GUIStyle guiStyle = new GUIStyle();
                guiStyle.normal.textColor = Color.white;
                guiStyle.fontSize = 18;

                GUILayout.BeginHorizontal();
                {
                    //GUILayout.Space(12f);
                    GUILayout.Label("Download SDK", guiStyle, GUILayout.Width(760));
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(12f);

                GUILayout.BeginHorizontal();
                {
                    //GUILayout.Space(10f);

                    guiStyle.fontSize = 12;
                    guiStyle.normal.textColor = Color.gray;
                    GUILayout.Label("Download Path : ", guiStyle, GUILayout.Width(110));
                    GUILayout.BeginVertical();
                    {
                        guiStyle.normal.textColor = Color.white;
                        GUILayout.Label(string.Format("{0}/{1}", "{PROJECT_PATH}", "GamebaseSDK"), guiStyle, GUILayout.Width(904));
                    }
                    GUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                {
                    //GUILayout.Space(116);

                    switch (updateState)
                    {
                        case UPDATE_STATE.NONE:
                            {
                                GUILayout.BeginVertical();
                                {
                                    DrawDownloadSDKButton();
                                }
                                GUILayout.EndVertical();
                                break;
                            }
                        case UPDATE_STATE.MANDATORY:
                            {
                                break;
                            }
                        case UPDATE_STATE.OPTIONAL:
                            {
                                GUILayout.BeginVertical();
                                {
                                    DrawDownloadSDKButton();
                                }
                                GUILayout.EndVertical();
                                break;
                            }
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }

        private void DrawDownloadSDKButton()
        {
            GUILayout.BeginHorizontal();
            {
                if (true == GUILayout.Button("Download SDK", GUILayout.Width(150), GUILayout.Height(30)))
                {
                    if (false == CheckDownloadPopup())
                    {
                        DownloadSDK(
                        () =>
                        {
                            ExtractSDK(
                                () =>
                                {
                                    LoadSDKSettingFromLocal();
                                    sdkState = SDK_STATE.NONE;
                                });
                        });
                    }
                }

                GUILayout.BeginVertical();
                {
                    GUILayout.Space(10);

                    GUIStyle guiStyle = new GUIStyle();
                    guiStyle.fontSize = 11;
                    guiStyle.normal.textColor = Color.cyan;
                    guiStyle.alignment = TextAnchor.MiddleLeft;
                    switch (sdkState)
                    {
                        case SDK_STATE.NONE:
                            {
                                break;
                            }
                        case SDK_STATE.NOT_FILE:
                            {
                                GUILayout.Label("GamebaseSDK download required!!!", guiStyle, GUILayout.Width(400));
                                break;
                            }
                        case SDK_STATE.DOWNLOAD:
                            {
                                GUILayout.Label(downloadFileName + " Downloading : " + downloadProgress, guiStyle, GUILayout.Width(400));
                                break;
                            }
                        case SDK_STATE.EXTRACT:
                            {
                                GUILayout.Label(extractFileName + " Extracting : " + extractProgress, guiStyle, GUILayout.Width(400));
                                break;
                            }
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }

        private void DrawSDKSettings()
        {
            if (null == drawSDKInfo)
            {
                return;
            }

            if (SDK_STATE.EXTRACT == sdkState || SDK_STATE.DOWNLOAD == sdkState)
            {
                return;
            }

            GUIStyle guiStyle = new GUIStyle();
            guiStyle.normal.textColor = Color.white;
            guiStyle.fontSize = 18;

            GUILayout.Space(2f);

            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(12f);
                GUILayout.Label("SDK Settings", guiStyle);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(10f);

                GUILayout.BeginVertical();
                {
                    GUILayout.Space(20f);

                    GUILayout.Label("Select additional platforms to use.", GUILayout.Width(400));

                    GUILayout.BeginHorizontal();
                    {
                        drawSDKInfo.useAndroidPlatform = GUILayout.Toggle(drawSDKInfo.useAndroidPlatform, "Use Android Platform", GUILayout.Width(180));
                        drawSDKInfo.useIOSPlatform = GUILayout.Toggle(drawSDKInfo.useIOSPlatform, "Use iOS Platform", GUILayout.Width(180));
                    }
                    GUILayout.EndHorizontal();

                }
                GUILayout.EndVertical();


                GUILayout.BeginVertical();
                {
                    GUILayout.Space(16f);

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(240f);
                        GUILayout.Space(148f);

                        if (null != gamebaseSDKInfo)
                        {
                            if (true == GUILayout.Button("Settings", GUILayout.Width(100), GUILayout.Height(42)))
                            {
                                if (false == CheckDownloadPopup())
                                {
                                    SettingSDKPopup();
                                }
                            }
                        }

                        if (null != gamebaseLocalSettings)
                        {
                            if (true == GUILayout.Button("Remove", GUILayout.Width(100), GUILayout.Height(42)))
                            {
                                if (false == CheckDownloadPopup())
                                {
                                    RemoveSDKPopup();
                                }
                            }
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(10);
                int maxCount = platforms.Count;

                platforms.Clear();

                platforms.Add("Unity");

                if (true == drawSDKInfo.useAndroidPlatform)
                {
                    platforms.Add("Android");
                }

                if (true == drawSDKInfo.useIOSPlatform)
                {
                    platforms.Add("iOS");
                }

                if (maxCount != platforms.Count)
                {
                    tapIndex = 0;
                }

                if (0 != platforms.Count)
                {
                    int index = GUILayout.Toolbar(tapIndex, platforms.ToArray(), GUILayout.Width(1002));
                    if (index != tapIndex)
                    {
                        tapIndex = index;
                    }
                }
            }
            GUILayout.EndHorizontal();

            DrawScrollBG();

            if (0 != platforms.Count)
            {
                switch (platforms[tapIndex])
                {
                    case "Unity":
                        {
                            DrawAdapterList(PlatformType.UNITY);
                            break;
                        }
                    case "Android":
                        {
                            DrawAdapterList(PlatformType.ANDROID);
                            break;
                        }
                    case "iOS":
                        {
                            DrawAdapterList(PlatformType.IOS);
                            break;
                        }
                }
            }

            guiStyle.alignment = TextAnchor.MiddleCenter;
            guiStyle.normal.textColor = Color.gray;
            guiStyle.fontSize = 10;
            GUI.Label(new Rect(0, 763, 1024, 10), "Copyright ⓒ NHN Ent. Corp.  All Rights Reserved.", guiStyle);
        }

        private void DrawUpdateButton()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(12);

                GUILayout.BeginVertical();
                {
                    GUIStyle guiStyle = new GUIStyle();
                    guiStyle.fontSize = 12;
                    guiStyle.alignment = TextAnchor.MiddleLeft;

                    switch (updateState)
                    {
                        case UPDATE_STATE.NONE:
                            {
                                guiStyle.normal.textColor = Color.white;
                                GUILayout.Label("There are no updates available.", guiStyle, GUILayout.Width(200));
                                break;
                            }
                        case UPDATE_STATE.MANDATORY:
                            {
                                guiStyle.normal.textColor = Color.red;
                                GUILayout.Label("There are updates available.(Mandatory)", guiStyle, GUILayout.Width(200));
                                break;
                            }
                        case UPDATE_STATE.OPTIONAL:
                            {
                                guiStyle.normal.textColor = Color.green;
                                GUILayout.Label("There are updates available.(Optional)", guiStyle, GUILayout.Width(200));
                                break;
                            }
                    }

                    GUILayout.Space(10);

                    if (UPDATE_STATE.NONE != updateState)
                    {
                        if (true == GUILayout.Button("Download SettingTool", GUILayout.Width(150), GUILayout.Height(30)))
                        {
                            Application.OpenURL(toastPath);
                        }
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }

        private void DrawScrollBG()
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, new Color(0.15f, 0.15f, 0.15f));
            texture.Apply();
            GUI.skin.box.normal.background = texture;
            GUI.Box(scrollRect, GUIContent.none);
        }

        private void DrawAdapterList(PlatformType type)
        {
            int provider = 0;
            int purchase = 0;
            int push = 0;
            int etc = 0;

            scrollPos = GUI.BeginScrollView(
                scrollRect,
                scrollPos,
                new Rect(10, 252, 986, GetScrollHeight(type)));
            {
                BeginWindows();

                for (int i = 0; i < drawSDKInfo.adapters.Count; i++)
                {
                    SettingToolVO.AdapterInfo adapter = drawSDKInfo.adapters[i];

                    SettingToolVO.PlatformInfo platform = GetPlatformInfo(type, adapter);

                    if (null != platform)
                    {
                        string path = string.Empty;

                        switch (type)
                        {
                            case PlatformType.UNITY:
                                {
                                    path = downloadPath + "/" + fileNameUnity.Replace(".zip", "") + "/sdk/" + adapter.unity.directory;
                                    if (string.Empty != path && false == Directory.Exists(path))
                                    {
                                        continue;
                                    }

                                    if (false == drawSDKInfo.useUnityAdapter && true == adapter.isUnityPriority)
                                    {
                                        continue;
                                    }
                                    break;
                                }
                            case PlatformType.ANDROID:
                                {
                                    path = downloadPath + "/" + fileNameAndroid.Replace(".zip", "") + "/sdk/" + adapter.android.directory;
                                    if (string.Empty != path && false == Directory.Exists(path))
                                    {
                                        continue;
                                    }

                                    if (true == drawSDKInfo.useUnityAdapter && true == adapter.isUnityPriority)
                                    {
                                        continue;
                                    }
                                    break;
                                }
                            case PlatformType.IOS:
                                {
                                    path = downloadPath + "/" + fileNameIOS.Replace(".zip", "") + "/sdk/" + adapter.ios.directory;
                                    if (string.Empty != path && false == Directory.Exists(path))
                                    {
                                        continue;
                                    }

                                    if (true == drawSDKInfo.useUnityAdapter && true == adapter.isUnityPriority)
                                    {
                                        continue;
                                    }
                                    break;
                                }
                        }

                        int indexY = 0;
                        int indexX = 0;

                        switch (adapter.category)
                        {
                            case "Authentication":
                                {
                                    indexY = provider;
                                    indexX = 0;
                                    provider++;
                                    break;
                                }
                            case "Purchase":
                                {
                                    indexY = purchase;
                                    indexX = 1;
                                    purchase++;
                                    break;
                                }
                            case "Push":
                                {
                                    indexY = push;
                                    indexX = 2;
                                    push++;
                                    break;
                                }
                            case "ETC":
                                {
                                    indexY = etc;
                                    indexX = 3;
                                    etc++;
                                    break;
                                }
                        }

                        GUILayout.Window(
                            i,
                            new Rect(
                                10 + offset * indexX + spaceWidth / 2,
                                spaceTop + (windowHeight + spaceHeight) * indexY,
                                windowWidth,
                                windowHeight),
                            DrawAdapterCell,
                            adapter.category);
                    }
                }

                EndWindows();
            }
            GUI.EndScrollView();

            List<int> maxCount = new List<int>();
            maxCount.Add(provider);
            maxCount.Add(purchase);
            maxCount.Add(push);
            maxCount.Add(etc);

            switch (type)
            {
                case PlatformType.UNITY:
                    {
                        scrollMaxCountUnity = maxCount.Max();
                        break;
                    }
                case PlatformType.ANDROID:
                    {
                        scrollMaxCountAndroid = maxCount.Max();
                        break;
                    }
                case PlatformType.IOS:
                    {
                        scrollMaxCountIOS = maxCount.Max();
                        break;
                    }
            }
        }

        private void DrawAdapterCell(int index)
        {
            SettingToolVO.AdapterInfo adapter = drawSDKInfo.adapters[index];
            switch (platforms[tapIndex])
            {
                case "Unity":
                    {
                        DrawAdapterCellUnity(adapter);
                        break;
                    }
                case "Android":
                    {
                        DrawAdapterCellAndroid(adapter);
                        break;
                    }
                case "iOS":
                    {
                        DrawAdapterCellIOS(adapter);
                        break;
                    }
            }
        }

        private void DrawAdapterCellUnity(SettingToolVO.AdapterInfo adapter)
        {
            if (null == adapter.unity)
            {
                return;
            }

            bool isCheck = EditorGUILayout.BeginToggleGroup(adapter.name, adapter.unity.useAdapter);
            {
                if (isCheck != adapter.unity.useAdapter)
                {
                    adapter.unity.useAdapter = isCheck;
                    if (true == adapter.unity.useAdapter)
                    {
                        if (true == adapter.unityOrNativeParallelUseDisable)
                        {
                            bool iosVerification = false;
                            if (null != adapter.ios && true == adapter.ios.useAdapter)
                            {
                                iosVerification = true;
                            }

                            bool androidVerification = false;
                            if (null != adapter.android && true == adapter.android.useAdapter)
                            {
                                androidVerification = true;
                            }

                            if (true == iosVerification || true == androidVerification)
                            {
                                if (true == EditorUtility.DisplayDialog(
                                    "Dependencies",
                                    "The adapter is currently selected for iOS or Android platforms.\n\nIf you select Unity Adapter, the selection on iOS or Android platform will be canceled.\n\nDo you want to select the Unity Adapter?",
                                    "OK",
                                    "Cancel"))
                                {
                                    VerificationAdapterToggle(adapter.name, PlatformType.UNITY);

                                    if (false == string.IsNullOrEmpty(adapter.unity.desc))
                                    {
                                        if (true == EditorUtility.DisplayDialog(adapter.unity.title, adapter.unity.desc, adapter.unity.button_ok, adapter.unity.button_close))
                                        {
                                            if (false == string.IsNullOrEmpty(adapter.unity.link))
                                            {
                                                Application.OpenURL(adapter.unity.link);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    adapter.unity.useAdapter = !adapter.unity.useAdapter;
                                }
                            }
                            else
                            {
                                if (false == string.IsNullOrEmpty(adapter.unity.desc))
                                {
                                    if (true == EditorUtility.DisplayDialog(adapter.unity.title, adapter.unity.desc, adapter.unity.button_ok, adapter.unity.button_close))
                                    {
                                        if (false == string.IsNullOrEmpty(adapter.unity.link))
                                        {
                                            Application.OpenURL(adapter.unity.link);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (false == string.IsNullOrEmpty(adapter.unity.desc))
                            {
                                if (true == EditorUtility.DisplayDialog(adapter.unity.title, adapter.unity.desc, adapter.unity.button_ok, adapter.unity.button_close))
                                {
                                    if (false == string.IsNullOrEmpty(adapter.unity.link))
                                    {
                                        Application.OpenURL(adapter.unity.link);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            EditorGUILayout.EndToggleGroup();
        }

        private void DrawAdapterCellAndroid(SettingToolVO.AdapterInfo adapter)
        {
            if (null == adapter.android)
            {
                return;
            }
            adapter.android.useAdapter = EditorGUILayout.BeginToggleGroup(adapter.name, adapter.android.useAdapter);
            {
                if (true == adapter.android.useAdapter)
                {
                    if (true == adapter.groupBySelectOnlyOneForCategory)
                    {
                        for (int i = 0; i < gamebaseSDKInfo.adapters.Count; i++)
                        {
                            SettingToolVO.AdapterInfo info = gamebaseSDKInfo.adapters[i];

                            if (false == info.name.Equals(adapter.name))
                            {
                                if (null != info.android && info.category == adapter.category)
                                {
                                    info.android.useAdapter = false;
                                }
                            }
                        }
                    }

                    if (true == adapter.unityOrNativeParallelUseDisable)
                    {
                        if (null != adapter.unity && true == adapter.unity.useAdapter)
                        {
                            if (true == EditorUtility.DisplayDialog(
                                "Dependencies",
                                "The adapter is currently selected for Unity platforms.\n\nIf you select Android Adapter, the selection on Unity platform will be canceled.\n\nDo you want to select the Android Adapter?",
                                "OK",
                                "Cancel"))
                            {
                                VerificationAdapterToggle(adapter.name, PlatformType.ANDROID);
                            }
                            else
                            {
                                adapter.android.useAdapter = !adapter.android.useAdapter;
                            }
                        }
                    }
                }
            }
            EditorGUILayout.EndToggleGroup();

            string path = downloadPath + "/" + fileNameAndroid.Replace(".zip", "") + "/sdk/" + adapter.android.directory;

            if (true == Directory.Exists(path))
            {
                if (true == GUILayout.Button("Dependencies Detail"))
                {
                    string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                    StringBuilder fileList = new StringBuilder();
                    foreach (string file in files)
                    {
                        if (true == file.ToLower().Contains(".jar"))
                        {
                            string[] fileName = file.Split(Path.DirectorySeparatorChar);
                            fileList.AppendLine(fileName[fileName.Length - 1]);
                        }
                        else if (true == file.ToLower().Contains(".aar"))
                        {
                            string[] fileName = file.Split(Path.DirectorySeparatorChar);
                            fileList.AppendLine(fileName[fileName.Length - 1]);

                        }
                    }

                    if (true == EditorUtility.DisplayDialog("Dependencies", fileList.ToString().Substring(0, fileList.ToString().Length - 2), "OK", "Cancel"))
                    {
                    }
                }

            }
        }

        private void DrawAdapterCellIOS(SettingToolVO.AdapterInfo adapter)
        {
            if (null == adapter.ios)
            {
                return;
            }

            adapter.ios.useAdapter = EditorGUILayout.BeginToggleGroup(adapter.name, adapter.ios.useAdapter);
            {
                if (true == adapter.ios.useAdapter)
                {
                    if (true == adapter.unityOrNativeParallelUseDisable)
                    {
                        if (null != adapter.unity && true == adapter.unity.useAdapter)
                        {
                            if (true == EditorUtility.DisplayDialog(
                                "Dependencies",
                                "The adapter is currently selected for Unity platforms.\n\nIf you select iOS Adapter, the selection on Unity platform will be canceled.\n\nDo you want to select the iOS Adapter?",
                                "OK",
                                "Cancel"))
                            {
                                VerificationAdapterToggle(adapter.name, PlatformType.IOS);
                            }
                            else
                            {
                                adapter.ios.useAdapter = !adapter.ios.useAdapter;
                            }
                        }
                    }
                }
            }
            EditorGUILayout.EndToggleGroup();

            if (null != adapter.ios.externals)
            {
                if (true == GUILayout.Button("Dependencies Detail"))
                {
                    if (true == EditorUtility.DisplayDialog("Dependencies", adapter.ios.externals, "OK", "Cancel"))
                    {
                    }
                }
            }
        }
        #endregion

        private int GetScrollHeight(PlatformType type)
        {
            int scrollHeight = 0;

            switch (type)
            {
                case PlatformType.UNITY:
                    {
                        scrollHeight = (windowHeight + spaceHeight) * scrollMaxCountUnity;
                        break;
                    }
                case PlatformType.ANDROID:
                    {
                        scrollHeight = (windowHeight + spaceHeight) * scrollMaxCountAndroid;
                        break;
                    }
                case PlatformType.IOS:
                    {
                        scrollHeight = (windowHeight + spaceHeight) * scrollMaxCountIOS;
                        break;
                    }
            }

            return scrollHeight;
        }

        private SettingToolVO.PlatformInfo GetPlatformInfo(PlatformType type, SettingToolVO.AdapterInfo adapter)
        {
            SettingToolVO.PlatformInfo platform = null;

            switch (type)
            {
                case PlatformType.UNITY:
                    {
                        platform = adapter.unity;
                        break;
                    }
                case PlatformType.ANDROID:
                    {
                        platform = adapter.android;
                        break;
                    }
                case PlatformType.IOS:
                    {
                        platform = adapter.ios;
                        break;
                    }
            }

            return platform;
        }

        private static void CheckVersion(Action<UPDATE_STATE> callback)
        {
            string downloadPath = Application.dataPath.Replace("Assets", "Version");
            string downloadFile = string.Format("{0}/{1}", downloadPath, versionCheckXMLName);

            CreateDirectory(downloadPath);

            downloadFileCoroutine = EditorCoroutine.Start(FileManager.DownloadFile(
                downloadURL + versionCheckXMLName,
                downloadFile,
                (stateCodeFile, messageFile) =>
                {
                    if (FileManager.StateCode.SUCCESS != stateCodeFile)
                    {
                        CheckFileManagerError(stateCodeFile, messageFile, null);
                        return;
                    }

                    XMLManager.LoadXMLFromFile<SettingToolVO.SupportedSettingToolVersion>(
                        downloadFile,
                        (stateCodeXML, dataXML, messageXML) =>
                        {
                            if (XMLManager.StateCode.SUCCESS != stateCodeXML)
                            {
                                CheckXMLManagerError(
                                    stateCodeXML,
                                    messageXML,
                                     () =>
                                     {
                                         window.CloseEditor();
                                     });
                                return;
                            }

                            SettingToolVO.SupportedSettingToolVersion vo = dataXML;
                            DeleteDirectory(downloadPath);

                            if (null == vo || null == vo.newestVersion || null == vo.compatibleVersion)
                            {
                                callback(UPDATE_STATE.MANDATORY);
                                return;
                            }

                            if (true == vo.newestVersion.Equals(version))
                            {
                                callback(UPDATE_STATE.NONE);
                                return;
                            }

                            if (true == vo.compatibleVersion.Equals(version))
                            {
                                callback(UPDATE_STATE.OPTIONAL);
                                return;
                            }

                            UPDATE_STATE state = UPDATE_STATE.NONE;

                            string[] compatibleVersion = vo.compatibleVersion.Replace("v", "").Split('.');
                            string[] newestVersion = vo.newestVersion.Replace("v", "").Split('.');
                            string[] localVersion = version.Replace("v", "").Split('.');

                            for (int i = 0; i < newestVersion.Length; i++)
                            {
                                int newest = int.Parse(newestVersion[i]);
                                int local = int.Parse(localVersion[i]);

                                if (newest > local)
                                {
                                    state = UPDATE_STATE.OPTIONAL;
                                    break;
                                }
                            }

                            for (int i = 0; i < compatibleVersion.Length; i++)
                            {
                                int compatible = int.Parse(compatibleVersion[i]);
                                int local = int.Parse(localVersion[i]);

                                if (compatible > local)
                                {
                                    state = UPDATE_STATE.MANDATORY;
                                    break;
                                }
                            }

                            callback(state);
                        });
                }, null));
        }

        private void VerificationAdapterToggle(string adapterName, PlatformType type)
        {
            foreach (SettingToolVO.AdapterInfo adapter in drawSDKInfo.adapters)
            {
                if (true == adapter.name.Equals(adapterName))
                {
                    if (PlatformType.UNITY == type)
                    {
                        if (null != adapter.ios)
                        {
                            adapter.ios.useAdapter = false;
                        }

                        if (null != adapter.android)
                        {
                            adapter.android.useAdapter = false;
                        }
                    }
                    else
                    {
                        if (null != adapter.unity)
                        {
                            adapter.unity.useAdapter = false;
                        }
                    }
                    break;
                }
            }
        }

        #region DownloadSDK
        private void DownloadSDK(Action callback)
        {
            if (true == EditorUtility.DisplayDialog("Download SDK", "All previous downloaded SDKs will be deleted.", "OK", "Cancel"))
            {
                sdkState = SDK_STATE.DOWNLOAD;

                DeleteDownloadSDK();

                CreateDirectory(downloadPath);

                downloadFileName = fileNameXML;
                DownloadSDKFile(
                    fileNameXML,
                    () =>
                    {
                        downloadFileName = fileNameUnity;
                        DownloadSDKFile(
                            fileNameUnity,
                            () =>
                            {
                                downloadFileName = fileNameIOS;
                                DownloadSDKFile(
                                    fileNameIOS,
                                    () =>
                                    {
                                        downloadFileName = fileNameAndroid;
                                        DownloadSDKFile(
                                            fileNameAndroid,
                                            () =>
                                            {
                                                callback();
                                            });
                                    });
                            });
                    });
            }
        }

        private void DownloadSDKFile(string fileName, Action callback)
        {
            downloadFileName = fileName;
            downloadProgress = "0";
            Repaint();
            
            string url = string.Format("{0}{1}", downloadURL, fileName);
            string downloadFile = string.Format("{0}/{1}", downloadPath, fileName);

            downloadFileCoroutine = EditorCoroutine.Start(FileManager.DownloadFile(
                url,
                downloadFile,
                (stateCode, message) =>
                {
                    if (FileManager.StateCode.SUCCESS != stateCode)
                    {
                        CheckFileManagerError(
                            stateCode,
                            message,
                            () =>
                            {
                                window.CloseEditor();
                            });
                        return;
                    }

                    callback();
                    downloadProgress = "";
                    Repaint();
                },
                (progress) =>
                {
                    downloadProgress = ((int)(progress * 100f)).ToString() + "%";
                    Repaint();
                }));
        }
        #endregion DownloadSDK

        #region ExtractSDK
        private void ExtractSDK(Action callback)
        {
            sdkState = SDK_STATE.EXTRACT;

            extractFileName = fileNameUnity;
            ExtractSDKFile(
                fileNameUnity,
                (isSuccessUnity) =>
                {
                    if (false == isSuccessUnity)
                    {
                        callback();
                        return;
                    }
                    fileStream = null;
                    extractFileName = fileNameIOS;
                    ExtractSDKFile(
                        fileNameIOS,
                        (isSuccessIOS) =>
                        {
                            if (false == isSuccessIOS)
                            {
                                callback();
                                return;
                            }
                            fileStream = null;
                            extractFileName = fileNameAndroid;
                            ExtractSDKFile(
                                fileNameAndroid,
                                (isSuccessAndroid) =>
                                {
                                    fileStream = null;
                                    callback();
                                    Repaint();
                                },
                                (progress) =>
                                {
                                    extractProgress = ((int)(progress * 100f)).ToString() + "%";
                                    Repaint();
                                });
                        },
                        (progress) =>
                        {
                            extractProgress = ((int)(progress * 100f)).ToString() + "%";
                            Repaint();
                        });
                },
                (progress) =>
                {
                    extractProgress = ((int)(progress * 100f)).ToString() + "%";
                    Repaint();
                });
        }

        private void ExtractSDKFile(string fileName, Action<bool> callback, Action<float> progressCallback)
        {
            string downloadFile = string.Format("{0}/{1}", downloadPath, fileName);

            extractCoroutine = EditorCoroutine.Start(
                ZipManager.Extract(
                    downloadFile,
                    downloadFile.Replace(".zip", ""),
                    (stateCode, message) =>
                    {
                        if (ZipManager.StateCode.SUCCESS != stateCode)
                        {
                            CheckZipManagerError(
                                stateCode,
                                message,
                                () =>
                                {
                                    window.CloseEditor();
                                });
                            callback(false);
                            return;
                        }
                        callback(true);
                    },
                    (_fileStream) => { fileStream = _fileStream; },
                    progressCallback,
                    null,
                    true));
        }
        #endregion

        #region LoadSDKData
        private static void LoadSDKSettingFromLocal()
        {
            tapIndex = 0;

            downloadPath = Application.dataPath.Replace("Assets", "GamebaseSDK");

            string gamebaseSettingInfoPath = Application.dataPath + localSettingInfomationXML;

            gamebaseSDKInfo = null;
            gamebaseLocalSettings = null;

            if (true == File.Exists(gamebaseSettingInfoPath))
            {
                XMLManager.LoadXMLFromFile<SettingToolVO.GamebaseSDKInfo>(
                    gamebaseSettingInfoPath,
                    (stateCodeInfo, dataInfo, messageInfo) =>
                    {
                        gamebaseLocalSettings = dataInfo;
                        LoadSDKSettingFromDownload();
                    });
            }
            else
            {
                LoadSDKSettingFromDownload();
            }
        }

        private static void LoadSDKSettingFromDownload()
        {
            string path = string.Format("{0}/{1}", downloadPath, fileNameXML);
            if (true == File.Exists(path))
            {
                XMLManager.LoadXMLFromFile<SettingToolVO.GamebaseSDKInfo>(
                    path,
                    (stateCodeSDKInfo, dataSDKInfo, messageSDKInfo) =>
                    {
                        if (XMLManager.StateCode.SUCCESS != stateCodeSDKInfo)
                        {
                            CheckXMLManagerError(
                                stateCodeSDKInfo,
                                messageSDKInfo,
                                () =>
                                {
                                    DeleteDownloadSDK();
                                    window.CloseEditor();
                                });
                            //return;
                        }

                        gamebaseSDKInfo = dataSDKInfo;

                        drawSDKInfo = null;

                        if (null != gamebaseSDKInfo)
                        {
                            if (null != gamebaseLocalSettings)
                            {
                                LoadPlatformInformationFromSDK();
                            }

                            drawSDKInfo = gamebaseSDKInfo;
                        }
                        else
                        {
                            if (null != gamebaseLocalSettings)
                            {
                                drawSDKInfo = gamebaseLocalSettings;
                            }
                        }

                        if (null == gamebaseSDKInfo)
                        {
                            sdkState = SDK_STATE.NOT_FILE;
                        }
                    });
            }
            else
            {
                sdkState = SDK_STATE.NOT_FILE;
            }
        }

        private static void LoadPlatformInformationFromSDK()
        {
            gamebaseSDKInfo.useUnityAdapter = gamebaseLocalSettings.useUnityAdapter;
            gamebaseSDKInfo.useAndroidPlatform = gamebaseLocalSettings.useAndroidPlatform;
            gamebaseSDKInfo.useIOSPlatform = gamebaseLocalSettings.useIOSPlatform;

            Dictionary<string, SettingToolVO.AdapterInfo> adapterDictionary = gamebaseLocalSettings.adapters.ToDictionary(adapter => adapter.name);

            foreach (SettingToolVO.AdapterInfo adapter in gamebaseSDKInfo.adapters)
            {
                SettingToolVO.AdapterInfo adapterLocal = adapterDictionary[adapter.name];
                if (null != adapterLocal)
                {
                    if (null != adapter.unity && null != adapterLocal.unity)
                    {
                        adapter.unity.useAdapter = adapterLocal.unity.useAdapter;
                    }
                    if (null != adapter.android && null != adapterLocal.android)
                    {
                        adapter.android.useAdapter = adapterLocal.android.useAdapter;
                    }
                    if (null != adapter.ios && null != adapterLocal.ios)
                    {
                        adapter.ios.useAdapter = adapterLocal.ios.useAdapter;
                    }
                }
            }

            downloadPath = Application.dataPath.Replace("Assets", "GamebaseSDK");
        }
        #endregion

        #region Popup
        private bool CheckDownloadPopup()
        {
            if (null == downloadPath && null == drawSDKInfo)
            {
                if (EditorUtility.DisplayDialog("Notice", "Reopen the settings window.", "OK", "Cancel"))
                {
                    if (null != window)
                    {
                        window.Close();
                        window = null;
                    }
                }
                return true;
            }

            if (SDK_STATE.DOWNLOAD == sdkState || SDK_STATE.EXTRACT == sdkState)
            {
                if (true == EditorUtility.DisplayDialog("Download", "Please wait until the operation is completed", "OK", "Cancel"))
                {
                }
                return true;
            }

            return false;
        }

        private void SettingSDKPopup()
        {
            if (EditorUtility.DisplayDialog("Setting Gamebase SDK", "This operations will settings Gamebase SDK completely.\nAfter settings Gamebase SDK has completed, The Settings Tool window closes automatically.", "Setting!", "Cancel"))
            {
                SettingSDK();

                CloseEditor();
            }
        }

        private void RemoveSDKPopup()
        {
            if (EditorUtility.DisplayDialog("Remove Gamebase SDK", "This operations will remove Gamebase SDK completely.\nAfter removing Gamebase SDK has completed, The Settings Tool window closes automatically.", "Remove!", "Cancel"))
            {
                RemoveSDK();

                CloseEditor();
            }
        }

        private void NotFoundFilePopup(string fileName)
        {
            EditorUtility.DisplayDialog("Not Found File or Directory", "GamebaseSDK file or directory not found.\nThe Settings Tool window closes automatically.\nReopen the Settings Tool window and download GamebaseSDK again.\n", "OK");

            DeleteDownloadSDK();
            RemoveSDK();
        }

        private static void FileIOErrorPopup(string message)
        {
            EditorUtility.DisplayDialog("File IO Error", message, "OK");

            //DeleteDownloadSDK();
            //window.RemoveSDK();
        }
        #endregion

        #region Error
        private static void CheckXMLManagerError(XMLManager.StateCode stateCode, string message, Action callback)
        {
            string title = "XML Parsing Error";
            string msg = string.Empty;

            switch (stateCode)
            {
                case XMLManager.StateCode.FILE_NOT_FOUND_ERROR:
                    {
                        msg = string.Format("File is not found!\nPath : {0}", message);
                        break;
                    }
                case XMLManager.StateCode.DATA_IS_NULL_ERROR:
                    {
                        msg = "Data is null!";
                        break;
                    }
                case XMLManager.StateCode.PATH_IS_NULL_ERROR:
                    {
                        msg = "Path is null or empty!";
                        break;
                    }
                case XMLManager.StateCode.UNKNOWN_ERROR:
                    {
                        msg = string.Format("Unknown error!\nMessage : {0}", message);
                        break;
                    }
            }

            if (EditorUtility.DisplayDialog(title, msg, "OK"))
            {
                if (null != callback)
                {
                    callback();
                }
            }
        }

        private static void CheckFileManagerError(FileManager.StateCode stateCode, string message, Action callback)
        {
            string title = "File Download Error";
            string msg = string.Empty;

            switch (stateCode)
            {
                case FileManager.StateCode.FILE_NOT_FOUND_ERROR:
                    {
                        msg = string.Format("File is not found!\nPath : {0}", message);
                        break;
                    }
                case FileManager.StateCode.WEB_REQUEST_ERROR:
                    {
                        msg = string.Format("Web request error!\nMessage : {0}", message);
                        break;
                    }
                case FileManager.StateCode.UNKNOWN_ERROR:
                    {
                        msg = string.Format("Response code!\nCode : {0}", message);
                        break;
                    }
            }

            if (EditorUtility.DisplayDialog(title, msg, "OK"))
            {
                if (null != callback)
                {
                    callback();
                }
            }
        }

        private static void CheckZipManagerError(ZipManager.StateCode stateCode, string message, Action callback)
        {
            string title = "SDK Extract Error";
            string msg = string.Empty;

            switch (stateCode)
            {
                case ZipManager.StateCode.FILE_NOT_FOUND_ERROR:
                    {
                        msg = string.Format("File is not found!\nPath : {0}", message);
                        break;
                    }
                case ZipManager.StateCode.FILE_PATH_NULL:
                    {
                        msg = string.Format("File path is null or empty!\nPath : {0}", message);
                        break;
                    }
                case ZipManager.StateCode.FOLDER_PATH_NULL:
                    {
                        msg = string.Format("Folder path is null or empty!\nPath : {0}", message);
                        break;
                    }
                case ZipManager.StateCode.UNKNOWN_ERROR:
                    {
                        msg = string.Format("Unknown error!\nMessage : {0}", message);
                        break;
                    }
            }

            if (EditorUtility.DisplayDialog(title, msg, "OK"))
            {
                if (null != callback)
                {
                    callback();
                }
            }
        }
        #endregion

        private void SettingSDK()
        {
            string settingPath;
            string from;
            string to;

            settingsPakageFiles.Clear();
            settingsLibsFiles.Clear();
            settingsDirectories.Clear();

            foreach (string baseDirectory in drawSDKInfo.baseInfo.unity.directories)
            {
                string path = downloadPath + "/" + fileNameUnity.Replace(".zip", "/sdk/") + baseDirectory;
                if (false == Directory.Exists(path))
                {
                    path = downloadPath + "/" + fileNameUnity.Replace(".zip", "/") + fileNameUnity.Replace(".zip", "/sdk/") + baseDirectory;
                    if (false == Directory.Exists(path))
                    {
                        NotFoundFilePopup(path);
                        return;
                    }
                }
                string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    if (true == file.ToLower().Contains(".unitypackage"))
                    {
                        settingsPakageFiles.Add(ReplaceDirectorySeparator(file));
                    }
                }
            }

            foreach (string baseDirectory in drawSDKInfo.baseInfo.android.directories)
            {
                string path = downloadPath + "/" + fileNameAndroid.Replace(".zip", "/sdk/") + baseDirectory;
                if (false == Directory.Exists(path))
                {
                    path = downloadPath + "/" + fileNameAndroid.Replace(".zip", "/") + fileNameAndroid.Replace(".zip", "/sdk/") + baseDirectory;
                    if (false == Directory.Exists(path))
                    {
                        NotFoundFilePopup(path);
                        return;
                    }
                }
                string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    if (true == file.ToLower().Contains(".aar") || true == file.ToLower().Contains(".jar"))
                    {
                        string fileName = GetFileOrDirectoryName(file);
                        if (false == settingsLibsFiles.ContainsKey(fileName))
                        {
                            settingsLibsFiles.Add(fileName, file);
                        }
                    }
                }
            }

            foreach (string baseDirectory in drawSDKInfo.baseInfo.ios.directories)
            {
                string path = downloadPath + "/" + fileNameIOS.Replace(".zip", "/sdk/") + baseDirectory;
                if (false == Directory.Exists(path))
                {
                    path = downloadPath + "/" + fileNameIOS.Replace(".zip", "/") + fileNameIOS.Replace(".zip", "/sdk/") + baseDirectory;
                    if (false == Directory.Exists(path))
                    {
                        NotFoundFilePopup(path);
                        return;
                    }
                }
                settingsDirectories.Add(path);
            }

            foreach (SettingToolVO.AdapterInfo adapter in drawSDKInfo.adapters)
            {
                if (null != adapter.unity && true == adapter.unity.useAdapter)
                {
                    string path = downloadPath + "/" + fileNameUnity.Replace(".zip", "/sdk/") + adapter.unity.directory;
                    if (false == Directory.Exists(path))
                    {
                        path = downloadPath + "/" + fileNameUnity.Replace(".zip", "/") + fileNameUnity.Replace(".zip", "/sdk/") + adapter.unity.directory;
                        if (false == Directory.Exists(path))
                        {
                            NotFoundFilePopup(path);
                            return;
                        }
                    }
                    string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        if (true == file.ToLower().Contains(".unitypackage"))
                        {
                            settingsPakageFiles.Add(ReplaceDirectorySeparator(file));
                        }
                    }
                }

                if (null != adapter.android && true == adapter.android.useAdapter)
                {
                    string path = downloadPath + "/" + fileNameAndroid.Replace(".zip", "/sdk/") + adapter.android.directory;
                    if (false == Directory.Exists(path))
                    {
                        path = downloadPath + "/" + fileNameAndroid.Replace(".zip", "/") + fileNameAndroid.Replace(".zip", "/sdk/") + adapter.android.directory;
                        if (false == Directory.Exists(path))
                        {
                            NotFoundFilePopup(path);
                            return;
                        }
                    }
                    string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        if (true == file.ToLower().Contains(".aar") || true == file.ToLower().Contains(".jar"))
                        {
                            string fileName = GetFileOrDirectoryName(file);
                            if (false == settingsLibsFiles.ContainsKey(fileName))
                            {
                                settingsLibsFiles.Add(fileName, file);
                            }
                        }
                    }
                }

                if (null != adapter.ios && true == adapter.ios.useAdapter)
                {
                    string path = downloadPath + "/" + fileNameIOS.Replace(".zip", "/sdk/") + adapter.ios.directory;
                    if (false == Directory.Exists(path))
                    {
                        path = downloadPath + "/" + fileNameIOS.Replace(".zip", "/") + fileNameIOS.Replace(".zip", "/sdk/") + adapter.ios.directory;
                        if (false == Directory.Exists(path))
                        {
                            NotFoundFilePopup(path);
                            return;
                        }
                    }
                    settingsDirectories.Add(path);
                    if (false == string.IsNullOrEmpty(adapter.ios.externals))
                    {
                        path = downloadPath + "/" + fileNameIOS.Replace(".zip", "/sdk/externals/") + adapter.ios.externals;
                        if (false == Directory.Exists(path))
                        {
                            path = downloadPath + "/" + fileNameIOS.Replace(".zip", "/") + fileNameIOS.Replace(".zip", "/sdk/externals/") + adapter.ios.externals;
                            if (false == Directory.Exists(path))
                            {
                                NotFoundFilePopup(path);
                                return;
                            }
                        }
                        settingsExternalsDirectories.Add(path);
                    }
                }
            }

            #region save xml
            string settingsXML = Application.dataPath + localSettingInfomationXML;

            DeleteFile(settingsXML);

            XMLManager.SaveXMLToFile(
                settingsXML,
                drawSDKInfo,
                (stateCode, message) =>
                {
                    if (XMLManager.StateCode.SUCCESS != stateCode)
                    {
                        CheckXMLManagerError(
                            stateCode,
                            message,
                            () =>
                            {
                            });
                    }
                });
            #endregion

            #region copy android
            settingPath = Application.dataPath + "/Plugins/Android/libs/Gamebase/";
            DeleteDirectory(settingPath);

            if (true == gamebaseSDKInfo.useAndroidPlatform)
            {
                if (0 < settingsLibsFiles.Count)
                {
                    settingPath = Application.dataPath + "/Plugins/Android/libs/Gamebase/";
                    CreateDirectory(settingPath);
                    foreach (string file in settingsLibsFiles.Values)
                    {
                        from = ReplaceDirectorySeparator(file);
                        to = ReplaceDirectorySeparator(settingPath + GetFileOrDirectoryName(file));
                        CopyFile(from, to);
                    }
                }
            }
            #endregion

            #region copy iOS
            settingPath = Application.dataPath + "/Plugins/IOS/Gamebase/";
            DeleteDirectory(settingPath);
            settingPath = Application.dataPath + "/Plugins/IOS/Gamebase/externals/";
            DeleteDirectory(settingPath);

            if (true == gamebaseSDKInfo.useIOSPlatform)
            {
                if (0 < settingsDirectories.Count)
                {
                    settingPath = Application.dataPath + "/Plugins/IOS/Gamebase/";
                    CreateDirectory(settingPath);
                    foreach (string directory in settingsDirectories)
                    {
                        from = ReplaceDirectorySeparator(directory);
                        to = ReplaceDirectorySeparator(settingPath + GetFileOrDirectoryName(directory));

                        CopyDirectory(from, to);
                    }
                }
                if (0 < settingsExternalsDirectories.Count)
                {
                    settingPath = Application.dataPath + "/Plugins/IOS/Gamebase/externals/";
                    CreateDirectory(settingPath);
                    foreach (string directory in settingsExternalsDirectories)
                    {
                        from = ReplaceDirectorySeparator(directory);
                        to = ReplaceDirectorySeparator(settingPath + GetFileOrDirectoryName(directory));

                        CopyDirectory(from, to);
                    }
                }
            }
            #endregion

            #region impore unitypackage
            DeleteDirectory(Application.dataPath + "/Gamebase");

            if (0 < settingsPakageFiles.Count)
            {
                foreach (string pakageFile in settingsPakageFiles)
                {
                    if (false == File.Exists(pakageFile))
                    {
                        NotFoundFilePopup(pakageFile);
                        return;
                    }

                    AssetDatabase.ImportPackage(pakageFile, false);
                }
            }
            #endregion            
        }

        private void RemoveSDK()
        {
            string path = "";

            path = Application.dataPath + localSettingInfomationXML;
            DeleteFile(path);

            path = Application.dataPath + "/Gamebase";
            DeleteDirectory(path);

            path = Application.dataPath + "/Plugins/Android/libs/Gamebase";
            DeleteDirectory(path);

            path = Application.dataPath + "/Plugins/IOS/Gamebase";
            DeleteDirectory(path);

            path = Application.dataPath + "/StreamingAssets/Gamebase";
            DeleteDirectory(path);
        }

        private static void DeleteDownloadSDK()
        {
            DeleteDirectory(downloadPath);
#if UNITY_EDITOR_WIN
            string[] directoryies = ReplaceDirectorySeparator(downloadPath).Split(Path.DirectorySeparatorChar);
            string newPathRoot = null;
            if (null != directoryies && 0 < directoryies.Length)
            {
                newPathRoot = directoryies[0] + Path.DirectorySeparatorChar + "GamebaseSettingsToolTemp";
                DeleteDirectory(newPathRoot);
            }
#elif UNITY_EDITOR_OSX
#endif
        }

        private static void CreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch(Exception e)
            {
                FileIOErrorPopup(e.Message);
            }
        }

        private static void DeleteDirectory(string path)
        {
            try
            {
                if (true == Directory.Exists(path))
                {
                    FileUtil.DeleteFileOrDirectory(path);
                }

                if (true == File.Exists(path + ".meta"))
                {
                    FileUtil.DeleteFileOrDirectory(path + ".meta");
                }
            }
            catch (Exception e)
            {
                FileIOErrorPopup(e.Message);
            }
        }

        private static void DeleteFile(string path)
        {
            try
            {
                if (true == File.Exists(path))
                {
                    FileUtil.DeleteFileOrDirectory(path);
                }

                if (true == File.Exists(path + ".meta"))
                {
                    FileUtil.DeleteFileOrDirectory(path + ".meta");
                }
            }
            catch (Exception e)
            {
                FileIOErrorPopup(e.Message);
            }
        }

        private static void CopyFile(string from, string to)
        {
            try
            {                
                FileUtil.CopyFileOrDirectory(from, to);
            }
            catch (Exception e)
            {
                FileIOErrorPopup(e.Message);
            }
        }

        private static void CopyDirectory(string from, string to)
        {
            try
            {                
                FileUtil.CopyFileOrDirectory(from, to);
            }
            catch (Exception e)
            {
                FileIOErrorPopup(e.Message);
            }
        }

        private string GetFileOrDirectoryName(string name)
        {
            string[] values = ReplaceDirectorySeparator(name).Split(Path.DirectorySeparatorChar);
            return values[values.Length - 1];
        }

        public static string ReplaceDirectorySeparator(string path)
        {
            return path.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
        }
    }
}