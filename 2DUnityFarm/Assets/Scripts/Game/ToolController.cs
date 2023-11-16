using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace ProjectindieFarm
{
	public partial class ToolController : ViewController
	{
        private GridController mGridController;
		private Grid mGird;
		private Camera mMainCamera;
		private SpriteRenderer mSprite;
        private EasyGrid<SoilData> mShowGrid;
        private Tilemap mTilemap;

        private void Awake()
        {
			Global.mouse = this;
		}
        void Start()
		{
            mGridController = FindObjectOfType<GridController>();
            mShowGrid = mGridController.ShowGrid;
            mGird = FindObjectOfType<GridController>().GetComponent<Grid>();
            mTilemap = mGridController.Tilemap;
            mMainCamera = Camera.main;
			mSprite = GetComponent<SpriteRenderer>();
			mSprite.enabled = false;
        }

        private void Update()
        {
            var playerCellPos = mGird.WorldToCell(Global.Player.Position());
            var wordMousePos = mMainCamera.ScreenToWorldPoint(Input.mousePosition);

            Icon.Position(wordMousePos.x + 0.5f, wordMousePos.y - 0.5f);
            var cellPos = mGird.WorldToCell(wordMousePos);
            float absX = Mathf.Abs(cellPos.x - playerCellPos.x);
            float absY = Mathf.Abs(cellPos.y - playerCellPos.y);

            var tileWorldPos = mGird.CellToWorld(cellPos);
            tileWorldPos.x += mGird.cellSize.x * 0.5f;
            tileWorldPos.y += mGird.cellSize.y * 0.5f;

            if (absX <= 1 && absY <= 1)
            {
                var gridOriginPos = mGird.CellToWorld(cellPos);
                gridOriginPos += mGird.cellSize * 0.5f;
                transform.Position(gridOriginPos.x, gridOriginPos.y);
                mSprite.enabled = true;

                if (Input.GetMouseButtonDown(0))
                {
                    //explore the filed 
                    if (mShowGrid[cellPos.x, cellPos.y] == null && Global.CurrentTool == Constant.TOOL_SHOVEL)
                    {
                        mTilemap.SetTile(cellPos, FindObjectOfType<GridController>().Pen);
                        mShowGrid[cellPos.x, cellPos.y] = new SoilData();
                        AudioController.get.SfxShovelDig.Play();
                    }
                    else if (mShowGrid[cellPos.x, cellPos.y] != null && mShowGrid[cellPos.x, cellPos.y].HasPlant != true && Global.CurrentTool == Constant.TOOL_SEED)
                    {
                        var plantGameObj = ResController.instance.PlantPrefab.Instantiate().Position(tileWorldPos);
                        var plant = plantGameObj.GetComponent<Plant>();
                        plant.XCell = cellPos.x;
                        plant.YCell = cellPos.y;
                        PlantController.Instance.Plants[cellPos.x, cellPos.y] = plant;

                        mShowGrid[cellPos.x, cellPos.y].HasPlant = true;
                        AudioController.get.SfxSeed.Play();
                    }
                    else if (mShowGrid[cellPos.x, cellPos.y] != null && !mShowGrid[cellPos.x, cellPos.y].watered && Global.CurrentTool == Constant.TOOL_WARTING_SCAN)
                    {
                        ResController.instance.WaterPrefab.Instantiate().Position(tileWorldPos);
                        mShowGrid[cellPos.x, cellPos.y].watered = true;
                        AudioController.get.SfxWater.Play();
                    }
                    // get the fruit
                    else if (mShowGrid[cellPos.x, cellPos.y] != null && mShowGrid[cellPos.x, cellPos.y].HasPlant && mShowGrid[cellPos.x, cellPos.y].PlantState == PlantStates.Ripe && Global.CurrentTool == Constant.TOOL_HAND)
                    {
                        Global.OnPlantHarvest.Trigger(PlantController.Instance.Plants[cellPos.x, cellPos.y]);
                        Destroy(PlantController.Instance.Plants[cellPos.x, cellPos.y].gameObject);
                        mShowGrid[cellPos.x, cellPos.y].HasPlant = false;
                        //PlantController.Instance.Plants[cellPosition.x, cellPosition.y].SetState(PlantStates.Old);
                        Global.FruitCount.Value++;
                        AudioController.get.SfxPlantHarvest.Play();
                    }
                }

            }
            else
            {
                mSprite.enabled = false;
            }
        }

        private void OnDestroy()
        {
            Global.mouse = null;
        }
    }
}
