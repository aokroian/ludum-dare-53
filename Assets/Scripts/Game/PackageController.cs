using Common;
using UnityEngine;

namespace Game
{
    public class PackageController : SingletonScene<PackageController>
    {
        public int deliveredCount;
        
        public bool HasPackage(int depth)
        {
            // TODO: Implement
            return true;
        }

        public bool NoPackage()
        {
            return true;
        }
    }
}