using UnityEngine;
using QFramework;

namespace ProjectindieFarm
{
	public partial class ResController : ViewController,ISingleton
	{
		public GameObject WaterPrefab;
		public GameObject PlantPrefab;

		public Sprite SeedSprite;
		public Sprite SmallPlantSprite;
		public Sprite RipeSprite;
		public Sprite OldSprite;

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
