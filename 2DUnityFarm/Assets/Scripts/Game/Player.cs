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
		void Start()
		{

			Global.Days.Register(day =>
			{
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
			GUILayout.Label("ÌìÊý£º"+ Global.Days.Value);
			GUILayout.EndHorizontal();
        }

        private void Update()
        {
			if (Input.GetKeyDown(KeyCode.F)) {
				Global.Days.Value++;
			}

			var cellPosition = Grid.WorldToCell(transform.position);

			var grid = FindObjectOfType<GridController>().ShowGrid;

			var tileWorldPos = Grid.CellToWorld(cellPosition);
			tileWorldPos.x += Grid.cellSize.x * 0.5f;
			tileWorldPos.y += Grid.cellSize.y * 0.5f;

			TileSelectController.Instance.Position(tileWorldPos);


			// 
			if (Input.GetMouseButtonDown(0))
			{
				//explore the filed 
				if (grid[cellPosition.x, cellPosition.y] == null)
				{
					Tilemap.SetTile(cellPosition, FindObjectOfType<GridController>().Pen);
					grid[cellPosition.x, cellPosition.y] = new SoilData();
				}
				//plant the seed
				else if (grid[cellPosition.x, cellPosition.y].HasPlant != true)
				{
					var plantGameObj = ResController.instance.PlantPrefab.Instantiate().Position(tileWorldPos);
					var plant = plantGameObj.GetComponent<Plant>();
					plant.XCell = cellPosition.x;
					plant.YCell = cellPosition.y;
					PlantController.Instance.Plants[cellPosition.x, cellPosition.y] = plant;

					grid[cellPosition.x, cellPosition.y].HasPlant = true;
				}
				// nothing to do
				else { 

				}
			}


			if (Input.GetMouseButtonDown(1))
			{
				if (grid[cellPosition.x, cellPosition.y] != null)
				{
					Tilemap.SetTile(cellPosition, null);
					grid[cellPosition.x, cellPosition.y] = null;
				}
			}

			if (Input.GetKeyDown(KeyCode.E))
			{
				//explore the filed 
				if (grid[cellPosition.x, cellPosition.y] != null)
				{
					if (!grid[cellPosition.x, cellPosition.y].watered) {
						ResController.instance.WaterPrefab.Instantiate().Position(tileWorldPos);

						grid[cellPosition.x, cellPosition.y].watered = true;
					}
				}
			}
		}
    }
}
