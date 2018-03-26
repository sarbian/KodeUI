namespace KodeUI
{
    public class PartLoader : LoadingSystem
    {
        // It sure loads faster than the real KSP
        public override bool IsReady()
        {
            return true;
        }
    }
}