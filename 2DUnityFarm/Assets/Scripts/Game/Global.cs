using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace ProjectindieFarm {
    public class Global {

        public static BindableProperty<int> Days = new BindableProperty<int>(1);

        public static BindableProperty<int> FruitCount = new BindableProperty<int>(0);

        public static BindableProperty<string> CurrentTool = new BindableProperty<string>(Constant.TOOL_HAND);

        public static BindableProperty<int> RipeCountAndHarvestInCurrentDay = new BindableProperty<int>(0);

        public static List<Challenge> Challenges = new List<Challenge>() {
            new ChallengeHavestFirstFruit(),
            new ChallengeRipeAndHarvestTwoFruitsInOneDay(),
            new ChallengeRipeAndHarvestFiveFruitsInOneDay()
        };

        public static List<Challenge> ActiveChallenges = new List<Challenge>() {

        };

        public static List<Challenge> FinishedChallenges = new List<Challenge>()
        {

        };



        public static EasyEvent<Plant> OnPlantHarvest = new EasyEvent<Plant>();

        public static EasyEvent<Challenge> OnChallengeFinish = new EasyEvent<Challenge>();

        public static Player Player = null;
    }

    public class Constant 
    {
        public const string TOOL_HAND = "hand";
        public const string TOOL_SHOVEL = "shovel";
        public const string TOOL_SEED = "seed";
        public const string TOOL_WARTING_SCAN = "watering_scan";

        public static string DisplayName(string tool) {
            switch (tool) {
                case TOOL_HAND:
                    return "手";
                case TOOL_SHOVEL:
                    return "铁锹";
                case TOOL_SEED:
                    return "种子";
                case TOOL_WARTING_SCAN:
                    return "花洒";
            }

            return string.Empty;
        }
    }
}
