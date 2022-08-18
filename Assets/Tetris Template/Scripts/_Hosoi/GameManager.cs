using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hosoi
{
public class GameManager : MonoBehaviour
{
	private bool _isGameActive;
	public TetrisShape currentShape;
	public Transform blockHolder;
	public PlayerStats stats;

	private _StatesBase currentState;
	public _StatesBase State
	{
		get { return currentState; }
	}

	void Awake()
	{
		EnablePlay(false);
	}
	void Start()
	{
		SetState(typeof(MenuState));
	}
	void Update()
	{
		if (currentState != null)
		{
			currentState.OnUpdate();
		}
	}

	//Changes the current game state
	public void SetState(System.Type newStateType)
	{
		if (currentState != null)
		{
			currentState.OnDeactivate();
		}

		currentState = GetComponentInChildren(newStateType) as _StatesBase;
		if (currentState != null)
		{
			currentState.OnActivate();
		}
	}

	public void EnablePlay(bool isActive)
	{
		_isGameActive = isActive;
	}
}
}