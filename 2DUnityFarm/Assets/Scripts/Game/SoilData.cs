using UnityEditor;
using UnityEngine;

namespace ProjectindieFarm
{
    public class SoilData 
    {
        public bool HasPlant { get; set; } = false;

        public bool watered { get; set; } = false;

        public PlantStates PlantState { get; set; } = PlantStates.Seed;
    }
}