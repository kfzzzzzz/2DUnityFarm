using System;

namespace ProjectindieFarm
{
    public class ChallengeRipeAndHarvestTwoFruitInOneDay : Challenge
    {
        public override bool CheckFinish()
        {
            return Global.RipeCountAndHarvestInCurrentDay >= 2;
        }
    }
}
