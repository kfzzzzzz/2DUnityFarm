using System.Collections.Generic;
using QFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectindieFarm
{
    public class ChallengeRipeAndHarvestTwoFruitsInOneDay : Challenge
    {
        public override string Name { get; } = "收获两个当天成熟的果实";
        public override void OnStart()
        {

        }
        public override bool CheckFinish()
        {
            return Global.RipeCountAndHarvestInCurrentDay >= 2 && Global.Days.Value != StartDate;
        }

        public override void OnFinish()
        {
            base.OnFinish();
        }
    }
}