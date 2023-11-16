using UnityEngine;
using QFramework;

namespace ProjectindieFarm
{
	public partial class MouseController : ViewController
	{
		private Grid mGird;
		private Camera mMainCamera;
		private SpriteRenderer mSprite;
		void Start()
		{
			mGird = FindObjectOfType<GridController>().GetComponent<Grid>();
			mMainCamera = Camera.main;
			mSprite = GetComponent<SpriteRenderer>();
			mSprite.enabled = false;
		}

        private void Update()
        {
			var playerCellPos = mGird.WorldToCell(Global.Player.Position());
			var wordMousePos = mMainCamera.ScreenToWorldPoint(Input.mousePosition);
			var cellPos = mGird.WorldToCell(wordMousePos);
            float absX = Mathf.Abs(cellPos.x - playerCellPos.x);
			float absY = Mathf.Abs(cellPos.y - playerCellPos.y);
			if (absX<= 1 && absY <= 1) {
				var gridOriginPos = mGird.CellToWorld(cellPos);
				gridOriginPos += mGird.cellSize * 0.5f;
				transform.Position(gridOriginPos.x, gridOriginPos.y);
				mSprite.enabled = true;
			}
			else {
				mSprite.enabled = false;
			}
        }
    }
}
