using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hosoi
{
[System.Serializable]
public class Column
{
	// 縦ブロックの数
	public static int rowNum = 20;
	public Transform[] row = new Transform[rowNum];
}

public class GridManager : MonoBehaviour
{
	// 横ブロックの数
	public static int columnNum = 10;
	// ブロックを保持しておくためのグリッド
	public Column[] gridColumn = new Column[columnNum];

	// ブロックがグリッドからはみ出ていないかの判定
	public bool IsValidGridPosition(Transform obj)
	{
		foreach (Transform child in obj)
		{
			if (child.gameObject.tag.Equals("Block"))
			{
				Vector2 v = Vector2Extension.roundVec2(child.position);

				if (!InsideBorder(v))
				{
					return false;
				}

				// Block in grid cell (and not part of same group)?
				if (gridColumn[(int)v.x].row[(int)v.y] != null &&
					gridColumn[(int)v.x].row[(int)v.y].parent != obj)
					return false;
			}
		}
		return true;
	}
	public bool InsideBorder(Vector2 pos)
	{
		return ((int)pos.x >= 0 && (int)pos.x < columnNum && (int)pos.y >= 0);
	}
	// グリッドを更新する
	public void UpdateGrid(Transform obj)
	{
		for (int y = 0; y < Column.rowNum; y++)
		{
			for (int x = 0; x < columnNum; x++)
			{
				if (gridColumn[x].row[y] != null)
				{
					if (gridColumn[x].row[y].parent == obj)
						gridColumn[x].row[y] = null;
				}
			}
		}

		foreach (Transform child in obj)
		{
			if (child.gameObject.tag.Equals("Block"))
			{
				Vector2 v = Vector2Extension.roundVec2(child.position);
				gridColumn[(int)v.x].row[(int)v.y] = child;
			}
		}
	}






}
}
