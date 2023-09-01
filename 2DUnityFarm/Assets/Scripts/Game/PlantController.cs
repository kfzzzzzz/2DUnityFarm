using UnityEngine;
using QFramework;

namespace ProjectindieFarm
{
	public enum PlantStates 
	{ 
		Seed,
		SmallPlant,
		Ripe,
		Old
	}
	public partial class PlantController : ViewController,ISingleton
	{
		public static PlantController Instance => MonoSingletonProperty<PlantController>.Instance;

		public EasyGrid<Plant> Plants = new EasyGrid<Plant>(10, 10);
        public void OnSingletonInit()
        {
            //throw new System.NotImplementedException();
        }

        void Start()
		{
			// Code Here
		}
	}
}
