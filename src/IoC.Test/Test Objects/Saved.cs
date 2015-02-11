using IoC.Test.Test_Interfaces;

namespace IoC.Test.Test_Objects
{
    public class Saved : ISaved
    {
        public bool IsSaved()
        {
            return true;
        }
    }
}
