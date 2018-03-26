using System;

namespace KodeUI
{
    public class KSPAddon : Attribute
    {
        public KSPAddon(Startup when, bool once)
        {
        }

        public enum Startup
        {
            Instantly
        }
    }
}