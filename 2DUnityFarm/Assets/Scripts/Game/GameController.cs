using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;
using System.Linq;

namespace ProjectindieFarm
{
	public partial class GameController : ViewController
	{
		void Start()
		{
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant.RipeDay == Global.Days.Value)
                {
                    Global.RipeCountAndHarvestInCurrentDay.Value++;
                }
            }).UnRegisterWhenGameObjectDestroyed(this);

            Global.OnChallengeFinish.Register(challenge =>
            {
                Debug.Log("KFZTEST:" + challenge.GetType().Name + "Íê³É");

                if (Global.Challenges.All(challenge => challenge.State == Challenge.States.Finished)) {
                    ActionKit.Delay(0.5f, () =>
                    {
                        SceneManager.LoadScene("GamePass");
                    }).Start(this);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

        private void Update()
        {
            foreach (var challenge in Global.Challenges.Where(challenge => challenge.State != Challenge.States.Finished)) {
                switch (challenge.State) {
                    case Challenge.States.NotStart:
                        challenge.OnStart();
                        challenge.State = Challenge.States.Started;
                        break;
                    case Challenge.States.Started:
                        if (challenge.CheckFinish()) {
                            challenge.OnFinish();
                            challenge.State = Challenge.States.Finished;
                            Global.OnChallengeFinish.Trigger(challenge);
                        }
                        break;
                    case Challenge.States.Finished:
                        break;
                }
            }
        }
    }
}
