using UnityEngine;

namespace KodeUI
{
    public class LoadingSystem : MonoBehaviour
    {
        public virtual bool IsReady()
        {
            return false;
        }

        public virtual string ProgressTitle()
        {
            return "";
        }

        public virtual float ProgressFraction()
        {
            return 0f;
        }

        public virtual void StartLoad()
        {
        }
    }
}