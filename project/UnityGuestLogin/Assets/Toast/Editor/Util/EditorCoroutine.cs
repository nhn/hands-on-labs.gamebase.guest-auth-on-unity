using System.Collections;
using UnityEditor;

namespace Toast.Tools.Util
{
    public class EditorCoroutine
    {
        public static EditorCoroutine Start(IEnumerator _routine)
        {
            EditorCoroutine coroutine = new EditorCoroutine(_routine);
            coroutine.Start();
            return coroutine;
        }

        readonly IEnumerator routine;
        EditorCoroutine(IEnumerator _routine)
        {
            routine = _routine;
        }

        void Start()
        {
            EditorApplication.update += Update;
        }
        public void Stop()
        {
            EditorApplication.update -= Update;
        }

        void Update()
        {
            if (!routine.MoveNext())
            {
                Stop();
            }
        }
    }
}