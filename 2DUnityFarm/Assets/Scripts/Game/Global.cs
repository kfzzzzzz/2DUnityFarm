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

        public static List<Challenge> Challenges = new List<Challenge>()
        {

        };

        public static EasyEvent<Plant> onPlantHarvest = new EasyEvent<Plant>();
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
                    return "水洒";
            }

            return string.Empty;
        }
    }
}
