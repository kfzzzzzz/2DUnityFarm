using UnityEngine;
using QFramework;

namespace ProjectindieFarm
{
	public partial class AudioController : ViewController, ISingleton
	{
		public static AudioController get => MonoSingletonProperty<AudioController>.Instance;
        public void OnSingletonInit()
        {
            
        }

        void Start()
		{
			// Code Here
		}
	}
}
