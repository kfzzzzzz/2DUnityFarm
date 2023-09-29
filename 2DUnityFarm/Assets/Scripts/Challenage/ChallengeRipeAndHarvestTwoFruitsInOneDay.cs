using System.Collections.Generic;
using QFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectindieFarm
{
    public class ChallengeRipeAndHarvestTwoFruitsInOneDay : Challenge,IUnRegisterList
    {
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();

        public override void OnStart()
        {
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant.RipeDay == Global.Days.Value)
                {
                    Global.RipeCountAndHarvestInCurrentDay.Value++;
                }
            }).AddToUnregisterList(this);
        }
        public override bool CheckFinish()
        {
            return Global.RipeCountAndHarvestInCurrentDay >= 2;
        }

        public override void OnFinish()
        {
            //this.UnRegisterAll();
            //ActionKit.Delay(1.0f, () =>
            //{
            //    SceneManager.LoadScene("GamePass");
            //}).StartGlobal();
        }
    }
}