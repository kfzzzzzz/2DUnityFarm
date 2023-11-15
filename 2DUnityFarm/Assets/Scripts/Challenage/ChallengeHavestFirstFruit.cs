using UnityEditor;
using UnityEngine;

namespace ProjectindieFarm
{
    public class ChallengeHavestFirstFruit : Challenge
    {
        public override string Name { get; } = "收获第一个果实";

        private int FruitCount = 0;

        public override void OnStart()
        {
            FruitCount = Global.FruitCount.Value;
        }
        public override bool CheckFinish()
        {
            return Global.FruitCount.Value - FruitCount > 0 && Global.Days.Value != StartDate;
        }

        public override void OnFinish()
        {
            base.OnFinish();
        }
    }
}