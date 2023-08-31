using UnityEngine;
using QFramework;

namespace ProjectindieFarm
{
	public partial class ResController : ViewController,ISingleton
	{

		public GameObject SeedPrefab;
		public GameObject WaterPrefab;
		public GameObject SmallPlantPrefab;

		public static ResController instance => MonoSingletonProperty<ResController>.Instance;

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
