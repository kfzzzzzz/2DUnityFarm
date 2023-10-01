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
            var randomItem = Global.Challenges.GetRandomItem();
            Global.ActiveChallenges.Add(randomItem);

            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant.RipeDay == Global.Days.Value)
                {
                    Global.RipeCountAndHarvestInCurrentDay.Value++;
                }
            }).UnRegisterWhenGameObjectDestroyed(this);

            Global.OnChallengeFinish.Register(challenge =>
            {
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
            bool hasFinishChallenge = false;
            foreach (var challenge in Global.ActiveChallenges) {
                switch (challenge.State) {
                    case Challenge.States.NotStart:
                        challenge.OnStart();
                        challenge.State = Challenge.States.Started;
                        challenge.StartDate = Global.Days.Value;
                        break;
                    case Challenge.States.Started:
                        if (challenge.CheckFinish()) {
                            challenge.OnFinish();
                            challenge.State = Challenge.States.Finished;
                            Global.OnChallengeFinish.Trigger(challenge);
                            Global.FinishedChallenges.Add(challenge);
                            hasFinishChallenge = true;
                        }
                        break;
                    case Challenge.States.Finished:
                        break;
                }
            }
            if (hasFinishChallenge) {
                Global.ActiveChallenges.RemoveAll(challenage => challenage.State == Challenge.States.Finished);
            }
            if (Global.ActiveChallenges.Count == 0 && Global.FinishedChallenges.Count != Global.Challenges.Count) {
                var randomItem = Global.Challenges.Where(c => c.State == Challenge.States.NotStart).ToList().GetRandomItem();
                Global.ActiveChallenges.Add(randomItem);
            }
        }
    }
}
