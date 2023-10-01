using UnityEngine;
using QFramework;

namespace ProjectindieFarm
{
	public partial class ChallengeController : ViewController
	{
		void Start()
		{
			// Code Here
		}
        private void OnGUI()
        {
			IMGUIHelper.SetDesignResolution(960, 540);

			GUI.Label(new Rect(960 - 300, 0, 300, 20), "ÃÙ’Ω");

			for (var i = 0;i < Global.ActiveChallenges.Count; i++) {
				var challenage = Global.ActiveChallenges[i];
				GUI.Label(new Rect(960 - 300, 20 + i * 26, 300, 20), challenage.Name);
			}

			for (var i = 0; i < Global.FinishedChallenges.Count; i++)
			{
				var challenage = Global.FinishedChallenges[i];
				GUI.Label(new Rect(960 - 300, 20 + i * 26 + Global.ActiveChallenges.Count * 26, 300, 20), "<color=green>" + challenage.Name + "</color>");
			}
		}
    }
}
