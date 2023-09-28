using UnityEngine;
using UnityEngine.SceneManagement;
using QFramework;

namespace ProjectindieFarm
{
	public partial class GameController : ViewController
	{
		void Start()
		{
            // Code Here
            Global.RipeCountAndHarvestInCurrentDay.Register(count =>
            {
                if (count >= 2)
                {
                    _ = ActionKit.Delay(1.0f, () =>
                      {
                          SceneManager.LoadScene("GamePass");
                      }).Start(this);
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
        }
	}
}
