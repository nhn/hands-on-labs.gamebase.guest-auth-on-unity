using System;
using UnityEditor;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

namespace Toast.Tools.Util
{
    public class FileManager
    {
        public enum StateCode
        {
            SUCCESS,
            FILE_NOT_FOUND_ERROR,
            WEB_REQUEST_ERROR,
            UNKNOWN_ERROR,
        }

        public static IEnumerator DownloadFile(String remoteFilename, String localFilename, Action<StateCode, string> callback, Action<float> callbackProgress = null)
        {
            UnityWebRequest www = UnityWebRequest.Get(remoteFilename);
#if UNITY_2017_2_OR_NEWER
            yield return www.SendWebRequest();
#else
            yield return www.Send();
#endif

            while (true)
            {
                if (true == www.isDone)
                {
                    if (200 == www.responseCode)
                    {
#if UNITY_2017_2_OR_NEWER
                        if (true == www.isNetworkError)
#else
                        if (true == www.isError)
#endif
                        {
                            callback(StateCode.WEB_REQUEST_ERROR, www.error);
                            yield break;
                        }
                        else
                        {
                            try
                            {
                                EditorApplication.update = null;
                                File.WriteAllBytes(localFilename, www.downloadHandler.data);
                                callback(StateCode.SUCCESS, null);
                            }
                            catch (Exception e)
                            {
                                callback(StateCode.UNKNOWN_ERROR, e.Message);
                            }
                            yield break;
                        }
                    }
                    else
                    {
                        switch (www.responseCode)
                        {
                            case 404:
                                {
                                    callback(StateCode.FILE_NOT_FOUND_ERROR, remoteFilename);
                                    break;
                                }
                            default:
                                {
#if UNITY_2017_2_OR_NEWER
                                    if (true == www.isNetworkError)
#else
                                    if (true == www.isError)
#endif
                                    {
                                        callback(StateCode.WEB_REQUEST_ERROR, www.error);
                                    }
                                    else
                                    {
                                        callback(StateCode.UNKNOWN_ERROR, www.responseCode.ToString());
                                    }

                                    break;
                                }                                
                        }
                        yield break;
                    }
                }
                else
                {
                    if (null != callbackProgress)
                    {
                        callbackProgress(www.downloadProgress);
                    }
                }

                yield return null;
            }
        }
    }
}