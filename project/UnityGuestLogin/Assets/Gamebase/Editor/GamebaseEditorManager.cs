using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace NE.Hsp.Editor
{
    public class GamebaseEditorManager
    {
        [MenuItem("Gamebase/Gamebase SDK Documentation")]
        public static void OpenGuidePage()
        {
            if (EditorUtility.DisplayDialog("Gamebase Documentation", "You can see documentation for Gamebase Unity SDK.", "Go to page!"))
            {
                Application.OpenURL("http://docs.cloud.toast.com/ko/Game/Gamebase/ko/unity-started/");
            }
        }

        [MenuItem("Gamebase/Gamebase SDK Release Note")]
        public static void OpenReleaseNotePage()
        {
            if (EditorUtility.DisplayDialog("Gamebase Release Note", "You can see release note for Gamebase Unity SDK.", "Go to page!"))
            {
                Application.OpenURL("http://docs.cloud.toast.com/ko/Game/Gamebase/ko/Release%20Notes/");
            }
        }

        [MenuItem("Gamebase/Gamebase SDK Download")]
        public static void OpenDownloadPage()
        {
            if (EditorUtility.DisplayDialog("Gamebase SDK Download", "You can download of Gamebase SDK.", "Go to page!"))
            {
                Application.OpenURL("http://docs.cloud.toast.com/ko/Download/");
            }
        }
    }
}