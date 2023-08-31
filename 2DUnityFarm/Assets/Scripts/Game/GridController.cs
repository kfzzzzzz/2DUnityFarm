using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace ProjectindieFarm
{
	public partial class GridController : ViewController
	{
		private EasyGrid<bool> mShowGrid = new EasyGrid<bool>(10, 10);

		public TileBase Pen;
		void Start()
		{
			mShowGrid[0, 0] = true;
			mShowGrid[2, 2] = true;

			mShowGrid.ForEach((x, y, show) =>
			{
				if (show) 
				{
					Tilemap.SetTile(new Vector3Int(x,y,0),Pen);
				}
			});
		}
	}
}
