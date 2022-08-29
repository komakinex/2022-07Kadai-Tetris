using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hosoi
{
public class GameManager : MonoBehaviour
{
	private bool _isGameActive;

	private float _timeSpent = 0;
	private int _numberOfGames = 0;

	private _StatesBase _currentState;
	public _StatesBase State
	{
		get { return _currentState; }
	}

	void Awake()
	{
		EnablePlay(false);
	}

	// テトリススタート！！
	void Start()
	{
		SetState(typeof(MenuState));
	}
	void Update()
	{
		if (_currentState != null)
		{
			_currentState.OnUpdate();
		}
	}

	//Changes the current game state
	public void SetState(System.Type newStateType)
	{
		if (_currentState != null)
		{
			_currentState.OnDeactivate();
		}

		_currentState = GetComponentInChildren(newStateType) as _StatesBase;
		if (_currentState != null)
		{
			_currentState.OnActivate();
			Debug.Log("setstate");
		}
	}

	public void EnablePlay(bool isActive)
	{
		_isGameActive = isActive;
	}

	public void GameCount()
	{
		_numberOfGames++;
	}
}
}