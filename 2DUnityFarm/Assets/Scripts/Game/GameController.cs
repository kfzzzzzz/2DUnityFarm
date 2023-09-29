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
            Global.OnChallengeFinish.Register(challenge =>
            {
                Debug.Log("KFZTEST:" + challenge.GetType().Name + "Íê³É");
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
