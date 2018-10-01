using System.Text;

namespace Toast.Gamebase
{
    public class GamebaseLog
    {
        public static bool IsDebugMode { get; private set; }
        public static string ComponentName { get; set; }

        public static void SetDebugMode(bool isDebugMode)
        {
            IsDebugMode = isDebugMode;
            ComponentName = "[TCGB][Unity]";
        }

        /// <summary>
        /// Generates a log message.
        /// </summary>
        /// <param name="message">required</param>
        /// <param name="classObj">required</param>
        /// <param name="methodName">required</param>
        /// <returns>[TCGB][Unity][ClassName::MethodName]</returns>
        private static string MakeLog(object message, object classObj, string methodName)
        {
            if (string.IsNullOrEmpty(message.ToString().Trim()) || classObj == null || string.IsNullOrEmpty(methodName.Trim()))
                throw new System.NullReferenceException("[GamebaseLog] message, class instance, method name can not be null.");

            StringBuilder log = new StringBuilder();
            log.AppendFormat("{0}", ComponentName);
            log.AppendFormat("[{0}", classObj.GetType().Name);
            log.AppendFormat("::{0}]", methodName);
            log.AppendFormat(" {0}", message);

            return log.ToString();
        }

        /// <summary>
        /// 1. 함수 내부에 표시되는 로그
        /// 2. 함수 시작 및 플로우 표시를 위한 로그
        /// 3. 데이터와 관련된 로그 표시
        /// </summary>
        public static void Debug(object message, object classObj, string methodName)
        {
            if (IsDebugMode == false)
                return;
            
            UnityEngine.Debug.Log(MakeLog(message, classObj, methodName));
        }

        /// <summary>
        /// 게임 흐름에는 영향이 없으나 제한되거나 권장하지 않는 흐름에 대한 로그
        /// </summary>
        public static void Warn(object message, object classObj, string methodName)
        {
            UnityEngine.Debug.LogWarning(MakeLog(message, classObj, methodName));
        }

        /// <summary>
        /// 게임 흐름에 치명적인 영향이 있는 에러
        /// </summary>
        public static void Error(object message, object classObj, string methodName)
        {
            UnityEngine.Debug.LogError(MakeLog(message, classObj, methodName));
        }
    }
}
