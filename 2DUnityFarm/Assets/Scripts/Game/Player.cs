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
				var seeds = SceneManager.GetActiveScene().GetRootGameObjects().Where(gamObj => gamObj.name.StartsWith("seed"));
				foreach (var seed in seeds) {

					var tilePos = Grid.WorldToCell(seed.transform.position);

					var tileData = FindObjectOfType<GridController>().ShowGrid[tilePos.x, tilePos.y];

					if (tileData != null && tileData.watered) {
						ResController.instance.SmallPlantPrefab.Instantiate().Position(seed.transform.position);
						seed.DestroySelf();
					}

				};
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
					ResController.instance.SeedPrefab.Instantiate().Position(tileWorldPos);

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
