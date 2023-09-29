using UnityEngine;
using QFramework;

namespace ProjectindieFarm
{
	public partial class Plant : ViewController
	{
		public int XCell;
		public int YCell;

		private PlantStates mState = PlantStates.Seed;

		public int RipeDay = -1;

		public PlantStates State => mState;


		public void SetState(PlantStates newState) 
		{
			if (newState != mState)
			{
				if (mState == PlantStates.SmallPlant && newState == PlantStates.Ripe) {
					RipeDay = Global.Days.Value;
				}
				mState = newState;

				switch(newState) {
                    case PlantStates.Seed:
						GetComponent<SpriteRenderer>().sprite = ResController.instance.SeedSprite;
						break;
					case PlantStates.SmallPlant:
						GetComponent<SpriteRenderer>().sprite = ResController.instance.SmallPlantSprite;
						break;
					case PlantStates.Ripe:
						GetComponent<SpriteRenderer>().sprite = ResController.instance.RipeSprite;
						break;
					case PlantStates.Old:
						GetComponent<SpriteRenderer>().sprite = ResController.instance.OldSprite;
						break;
					default:
						GetComponent<SpriteRenderer>().sprite = ResController.instance.SeedSprite;
						break;
				}

				//KFZ:TODO
				FindObjectOfType<GridController>().ShowGrid[XCell, YCell].PlantState = newState;
			}
		}
		void Start()
		{
			// Code Here
		}
	}
}
