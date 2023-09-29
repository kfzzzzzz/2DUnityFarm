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

			GUI.Label(new Rect(960 - 300, 0, 300, 20), "��ս");

			for (var i = 0;i < Global.Challenges.Count; i++) {
				var challenage = Global.Challenges[i];
				if (challenage.State == Challenge.States.Finished){
					GUI.Label(new Rect(960 - 300, 20 + i * 26, 300, 20), "<color=green>" + challenage.Name + "</color>");
				}
				else {
					GUI.Label(new Rect(960 - 300, 20 + i * 26, 300, 20), "<color=red>" + challenage.Name + "</color>");
				}
			}
        }
    }
}
