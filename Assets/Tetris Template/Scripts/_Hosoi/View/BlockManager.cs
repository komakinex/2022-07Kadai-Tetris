using System.Collections;
using UnityEngine;
using System.Linq;
using UniRx;
using System;

namespace hosoi
{
public class BlockManager : MonoBehaviour
{
	[SerializeField] private ColorView _colorView;
	[SerializeField] private GridManager _gridManager;

	// for Blocks
	[SerializeField] private GameObject[] _shapeTypes;
	private GameObject _tempBlock;
	private Transform _rotationPivot; // ブロックの回転の中心
	[SerializeField] private Transform _blockHolder;
	// 落下速度
	[SerializeField] private float _transitionInterval = 0.8f;
	[SerializeField] private float _fastTransitionInterval = 0.05f;
	private float _interval = 0.8f;
	private float _lastFall;
	private bool _canFall = false;

	// Presenterへの通知
	private Subject<bool> _acceptKeyinput = new Subject<bool>();
	public IObservable<bool> OnAcceptKeyinput { get { return _acceptKeyinput; } }
	private Subject<Unit> _getScore = new Subject<Unit>();
	public IObservable<Unit> OnGetScore { get { return _getScore; } }
	private Subject<Unit> _gameOver = new Subject<Unit>();
	public IObservable<Unit> OnGameOver { get { return _gameOver; } }
	private Subject<String> _onAudio = new Subject<String>();
	public IObservable<String> OnAudio { get { return _onAudio; } }

	void Update()
	{
		if(_tempBlock != null && _canFall)
			FreeFall();
	}
	public void StateAction(State state)
	{
		switch (state)
		{
			case State.Menu:
				_canFall = false;
				break;
			case State.Play:
				_canFall = true;
				break;
			case State.Pause:
				_canFall = false;
				break;
			default:
				break;
		}
	}

	public void BlockSpawn()
	{
		// 落下速度の初期化
		_interval = _transitionInterval;
		// ブロックの形
		int x = UnityEngine.Random.Range(0, _shapeTypes.Length);

		_tempBlock =Instantiate(_shapeTypes[x]);
		_rotationPivot = _tempBlock.transform.Find("Pivot").transform;
		_tempBlock.transform.parent = _blockHolder;
		AssignRandomColor();

		// 生成した位置が正しくなければGameOver
		if (!_gridManager.IsValidGridPosition(_tempBlock.transform))
		{
			_gameOver.OnNext(Unit.Default);
			Destroy(_tempBlock);
		}
		else
		{
			_acceptKeyinput.OnNext(true);
		}
	}
	// ブロックの色指定
	void AssignRandomColor()
	{
		Color col = _colorView.TurnRandomColorFromTheme();
		foreach (SpriteRenderer renderer in _tempBlock.GetComponentsInChildren<SpriteRenderer>().ToList())
			renderer.color = col;
	}

	public void BlockMove(string key)
	{
		switch (key)
		{
			case "up":
				RotateClockWise(false);
				break;
			case "d":
				RotateClockWise(true);
				break;
			case "left":
				MoveHorizontal(Vector2.left);
				break;
			case "right":
				MoveHorizontal(Vector2.right);
				break;
			case "down":
				if (_tempBlock != null)
				{
					_acceptKeyinput.OnNext(false);
					InstantFall();
				}
				break;
			default:
				break;
		}
	}

	// 横移動
	public void MoveHorizontal(Vector2 direction)
	{
		float deltaMovement = (direction.Equals(Vector2.right)) ? 1.0f : -1.0f;
		_tempBlock.transform.position += new Vector3(deltaMovement, 0, 0);

		// グリッドをはみ出るような入力を打ち消す
		if (_gridManager.IsValidGridPosition(_tempBlock.transform))
		{
			_gridManager.UpdateGrid(_tempBlock.transform);
		}
		else
		{
			_tempBlock.transform.position += new Vector3(-deltaMovement, 0, 0);
		}
	}
	// 回転
	public void RotateClockWise(bool isCw)
	{
		float rotationDegree = (isCw) ? 90.0f : -90.0f;
		_tempBlock.transform.RotateAround(_rotationPivot.position, Vector3.forward, rotationDegree);

		if (_gridManager.IsValidGridPosition(_tempBlock.transform))
		{
			_gridManager.UpdateGrid(_tempBlock.transform);
		}
		else
		{
			_tempBlock.transform.RotateAround(_rotationPivot.position, Vector3.forward, -rotationDegree);
		}
	}
	// デフォルトの落下
	public void FreeFall()
	{
		if (Time.time - _lastFall >= _interval)
		{
			_tempBlock.transform.position += Vector3.down;

			_onAudio.OnNext("drop");

			if (_gridManager.IsValidGridPosition(_tempBlock.transform))
			{
				_gridManager.UpdateGrid(_tempBlock.transform);
			}
			else
			{
				_tempBlock.transform.position += new Vector3(0, 1, 0);

				_acceptKeyinput.OnNext(false);
				_tempBlock = null;
				PlaceShape();
			}
			_lastFall = Time.time;
		}
	}
	// 自動で下までいく
	public void InstantFall()
	{
		_interval = _fastTransitionInterval;
	}


	public void PlaceShape()
	{
		int y = 0;
		StartCoroutine(DeleteRows(y));
	}

	IEnumerator DeleteRows(int k)
	{
		for (int y = k; y < Column.rowNum; ++y)
		{
			if (IsRowFull(y))
			{
				DeleteRow(y);
				DecreaseRowsAbove(y + 1);
				--y;
				_onAudio.OnNext("clear");
				yield return new WaitForSeconds(0.8f);
			}
		}

		foreach (Transform t in _blockHolder)
			if (t.childCount <= 1)
			{
				Destroy(t.gameObject);
			}

		BlockSpawn();

		yield break;
	}

	public bool IsRowFull(int y)
	{
		for (int x = 0; x < GridManager.columnNum; ++x)
			if (_gridManager.gridColumn[x].row[y] == null)
				return false;
		return true;
	}

	public void DeleteRow(int y)
	{
		_getScore.OnNext(Unit.Default);
		Debug.Log("delete row");

		for (int x = 0; x < GridManager.columnNum; ++x)
		{
			Destroy(_gridManager.gridColumn[x].row[y].gameObject);
			_gridManager.gridColumn[x].row[y] = null;
		}
	}

	public void DecreaseRowsAbove(int y)
	{
		for (int i = y; i < Column.rowNum; ++i)
			DecreaseRow(i);
	}

	public void DecreaseRow(int y)
	{
		for (int x = 0; x < GridManager.columnNum; ++x)
		{
			if (_gridManager.gridColumn[x].row[y] != null)
			{
				// Move one towards bottom
				_gridManager.gridColumn[x].row[y - 1] = _gridManager.gridColumn[x].row[y];
				_gridManager.gridColumn[x].row[y] = null;

				// Update Block position
				_gridManager.gridColumn[x].row[y - 1].position += new Vector3(0, -1, 0);
			}
		}
	}

	public void ButtonAction(string btn)
	{
		if(btn == "restart" || btn == "home")
		{
			ClearBoard();
		}
	}

	// ブロック全削除
	private void ClearBoard()
	{
		for (int y = 0; y < Column.rowNum; y++)
		{
			for (int x = 0; x < GridManager.columnNum; x++)
			{
				if (_gridManager.gridColumn[x].row[y] != null)
				{
					Destroy(_gridManager.gridColumn[x].row[y].gameObject);
					_gridManager.gridColumn[x].row[y] = null;
				}
			}
		}

		foreach (Transform t in _blockHolder)
			Destroy(t.gameObject);
	}




}
}