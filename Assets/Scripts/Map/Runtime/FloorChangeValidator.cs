using Game;

namespace Map.Runtime
{
    public class FloorChangeValidator
    {
        public bool CanChangeFloor(int nextFloor, bool downstairs)
        {
            if (downstairs && PackageController.Instance.HasPackage(nextFloor))
                return true;
            if (!downstairs && nextFloor < 0 && PackageController.Instance.NoPackage())
                return true;

            
            return false;
        }
    }
}