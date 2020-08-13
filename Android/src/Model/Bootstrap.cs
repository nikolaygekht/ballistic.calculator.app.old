using BallisticCalculator.Serialization;
using Gehtsoft.BallisticCalculator.DataProviders;

/**
 * Bootstrap class need for resource initialization 
 * at application start time
 */
namespace Gehtsoft.BallisticCalculator.Model
{
    class Bootstrap
    {
        public static Bootstrap Instance { get; private set; }

        static Bootstrap()
        {
            Instance = new Bootstrap();
        }

        public void Init()
        {
            SerializerInstance.Init();
            var ballisticdataProvider = BallisticDataProvider.Instance;
            ballisticdataProvider.LoadTraces();
  
        }

        public void Shutdown()
        {
            var ballisticdataProvider = BallisticDataProvider.Instance;
            ballisticdataProvider.LoadTraces();
        }
    } 
}