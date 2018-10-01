﻿using System;
using System.Collections.Generic;
using Toast.Gamebase.LitJson;

namespace Toast.Gamebase.Mobile
{
    public static class DelegateManager
    {
        public class DelegateData
        {
            public GamebaseCallback.DataDelegate<NativeMessage> eventDelegate;
            public GamebaseCallback.DataDelegate<NativeMessage> pluginEventDelegate;
        }

        private static Dictionary<string, DelegateData> delegateDictionary = new Dictionary<string, DelegateData>();

        public static void AddDelegate(string scheme, GamebaseCallback.DataDelegate<NativeMessage> eventDelegate, GamebaseCallback.DataDelegate<NativeMessage> pluginEventDelegate = null)
        {
            DelegateData delegateData           = new DelegateData();
            delegateData.eventDelegate          = eventDelegate;
            delegateData.pluginEventDelegate    = pluginEventDelegate;

            delegateDictionary.Add(scheme, delegateData);
        }

        public static DelegateData GetDelegate(string scheme)
        {
            return delegateDictionary[scheme];
        }

        public static void SendGamebaseDelegate<T>(NativeMessage message)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<T>>(message.handle);
            if (callback == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(message.jsonData))
            {
                callback(default(T), message.GetGamebaseError());
                return;
            }
            
            if(typeof(string) == typeof(T))
            {
                callback((T)(object)message.jsonData, message.GetGamebaseError());
                return;
            }

            var vo = JsonMapper.ToObject<T>(message.jsonData);

            callback(vo, message.GetGamebaseError());
        }

        public static void SendDataDelegate<T>(NativeMessage message)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.DataDelegate<T>>(message.handle);
            if (callback == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(message.jsonData))
            {
                callback(default(T));
                return;
            }

            if (typeof(string) == typeof(T))
            {
                callback((T)(object)message.jsonData);
                return;
            }

            var vo = JsonMapper.ToObject<T>(message.jsonData);

            callback(vo);
        }

        public static void SendVoidDelegate(NativeMessage message)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.VoidDelegate>(message.handle);
            if (callback == null)
            {
                return;
            }
            callback();
        }

        public static void SendErrorDelegate(NativeMessage message)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.ErrorDelegate>(message.handle);
            if (callback == null)
            {
                return;
            }
            callback(message.GetGamebaseError());
        }
        
        public static void SendGamebaseDelegateOnce<T>(NativeMessage message)
        {
            SendGamebaseDelegate<T>(message);
            GamebaseCallbackHandler.UnregisterCallback(message.handle);
        }

        public static void SendDataDelegateOnce<T>(NativeMessage message)
        {
            SendDataDelegate<T>(message);
            GamebaseCallbackHandler.UnregisterCallback(message.handle);
        }

        public static void SendVoidDelegateOnce(NativeMessage message)
        {
            SendVoidDelegate(message);
            GamebaseCallbackHandler.UnregisterCallback(message.handle);
        }

        public static void SendErrorDelegateOnce(NativeMessage message)
        {
            SendErrorDelegate(message);
            GamebaseCallbackHandler.UnregisterCallback(message.handle);
        }
    }
}
