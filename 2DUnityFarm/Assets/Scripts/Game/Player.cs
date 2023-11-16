using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Linq;

namespace ProjectindieFarm
{
	public partial class Player : ViewController
	{
		public Grid Grid;
		public Tilemap Tilemap;

        private void Awake()
        {
			Global.Player = this;
		}
        void Start()
		{
			Global.Days.Register(day =>
			{
				Global.RipeCountAndHarvestInCurrentDay.Value = 0;
				var soilDatas = FindObjectOfType<GridController>().ShowGrid;

				PlantController.Instance.Plants.ForEach((x, y, plant) =>
				{
					if (plant) {
						if (soilDatas[x, y].watered)
						{
							switch (plant.State)
							{
								case PlantStates.Seed:
									plant.SetState(PlantStates.SmallPlant);
									break;
								case PlantStates.SmallPlant:
									plant.SetState(PlantStates.Ripe);
									break;
								case PlantStates.Ripe:
									break;
							}
						}
					}
				});

				soilDatas.ForEach(soilData => {
					if (soilData != null) {
						soilData.watered = false;
					}
				});
				foreach (var water in SceneManager.GetActiveScene().GetRootGameObjects().Where(gamObj => gamObj.name.StartsWith("water"))) {
					water.DestroySelf();
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

        private void OnGUI()
        {
			IMGUIHelper.SetDesignResolution(640, 360);
			GUILayout.Space(10);
			GUILayout.BeginHorizontal();
			GUILayout.Space(10);
			GUILayout.Label("天数："+ Global.Days.Value);
			GUILayout.Space(10);
			GUILayout.Label("果子：" + Global.FruitCount.Value);
			//GUILayout.Space(10);
			//GUILayout.Label($"当前工具：{Constant.DisplayName(Global.CurrentTool)}");
			GUILayout.EndHorizontal();

			//GUI.Label(new Rect(10, 360 - 24, 200, 24), "[1]手 [2]铁锹 [3]种子 [4]花洒");
        }

        private void Update()
        {
			if (Input.GetKeyDown(KeyCode.F)) {
				Global.Days.Value++;
				AudioController.get.SfxNextDay.Play();
			}

			var cellPosition = Grid.WorldToCell(transform.position);

			var grid = FindObjectOfType<GridController>().ShowGrid;

			var tileWorldPos = Grid.CellToWorld(cellPosition);
			tileWorldPos.x += Grid.cellSize.x * 0.5f;
			tileWorldPos.y += Grid.cellSize.y * 0.5f;

			//if (Global.CurrentTool == Constant.TOOL_SHOVEL && grid[cellPosition.x,cellPosition.y] == null) {
			//	TileSelectController.Instance.Show();
			//}
			//else {
			//	TileSelectController.Instance.Hide();
			//}


			//// 
			//if (Input.GetMouseButtonDown(0))
			//{
			//	if (grid[cellPosition.x, cellPosition.y] != null && grid[cellPosition.x, cellPosition.y].HasPlant != true && Global.CurrentTool == Constant.TOOL_SEED)
			//	{
			//		var plantGameObj = ResController.instance.PlantPrefab.Instantiate().Position(tileWorldPos);
			//		var plant = plantGameObj.GetComponent<Plant>();
			//		plant.XCell = cellPosition.x;
			//		plant.YCell = cellPosition.y;
			//		PlantController.Instance.Plants[cellPosition.x, cellPosition.y] = plant;

			//		grid[cellPosition.x, cellPosition.y].HasPlant = true;
			//		AudioController.get.SfxSeed.Play();
			//	}
			//	else if (grid[cellPosition.x, cellPosition.y] != null && !grid[cellPosition.x, cellPosition.y].watered && Global.CurrentTool == Constant.TOOL_WARTING_SCAN) 
			//	{
			//		ResController.instance.WaterPrefab.Instantiate().Position(tileWorldPos);
			//		grid[cellPosition.x, cellPosition.y].watered = true;
			//		AudioController.get.SfxWater.Play();
			//	}
			//	// get the fruit
			//	else if (grid[cellPosition.x, cellPosition.y] != null && grid[cellPosition.x, cellPosition.y].HasPlant && grid[cellPosition.x, cellPosition.y].PlantState == PlantStates.Ripe && Global.CurrentTool == Constant.TOOL_HAND)
			//	{
			//		Global.OnPlantHarvest.Trigger(PlantController.Instance.Plants[cellPosition.x, cellPosition.y]);
			//		Destroy(PlantController.Instance.Plants[cellPosition.x, cellPosition.y].gameObject);
			//		grid[cellPosition.x, cellPosition.y].HasPlant = false;
			//		//PlantController.Instance.Plants[cellPosition.x, cellPosition.y].SetState(PlantStates.Old);
			//		Global.FruitCount.Value++;
			//		AudioController.get.SfxPlantHarvest.Play();
			//	}
			//}


			if (Input.GetMouseButtonDown(1))
			{
				if (grid[cellPosition.x, cellPosition.y] != null)
				{
					Tilemap.SetTile(cellPosition, null);
					grid[cellPosition.x, cellPosition.y] = null;
				}
			}

			if (Input.GetKeyDown(KeyCode.Return)) {
				SceneManager.LoadScene("GamePass");
			}
		}

        private void OnDestroy()
        {
			Global.Player = null;
        }

    }
}
