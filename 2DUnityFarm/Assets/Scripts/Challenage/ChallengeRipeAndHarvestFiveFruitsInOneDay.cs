using System.Collections.Generic;
using QFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectindieFarm
{
    public class ChallengeRipeAndHarvestFiveFruitsInOneDay : Challenge
    {
        public override string Name { get; } = "收获五个当天成熟的果实";
        public override void OnStart()
        {

        }
        public override bool CheckFinish()
        {
            return Global.RipeCountAndHarvestInCurrentDay >= 5;
        }

        public override void OnFinish()
        {

        }
    }
}