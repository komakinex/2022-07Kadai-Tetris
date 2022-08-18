using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace hosoi
{
public class Presenter : MonoBehaviour
{
	// View
	[SerializeField] private ButtonManager _buttonManager;
	[SerializeField] private CameraManager _cameraManager;
	// [SerializeField] private ColorManager _colorManager;
	[SerializeField] private GridManager _gridManager;
	// [SerializeField] private SpawnManager _spawnManager;
	[SerializeField] private UIManager _uiManager;

	
	public CameraManager Cam
	{
		get { return _cameraManager; }
	}

	// Model
	[SerializeField] private GameManager _gameManager;
	[SerializeField] private PlayerInputManager _inputManager;
	// [SerializeField] private ScoreManager _scoreManager;
	// // other
	[SerializeField] private AudioManager _audioManager;

	void Awake ()
	{
		// シーンを切り替えても破棄されないようにしている
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		// いろんなイベントが飛んできた時の対応を書いておく
		// キー入力
		_inputManager.OnKeyInputObservable.Subscribe(key =>
		{
			Debug.Log(key);
			switch (key)
			{
				case "up":
					_gameManager.currentShape.movementController.RotateClockWise(false);
					break;
				case "d":
					_gameManager.currentShape.movementController.RotateClockWise(true);
					break;
				case "left":
					_gameManager.currentShape.movementController.MoveHorizontal(Vector2.left);
					break;
				case "right":
					_gameManager.currentShape.movementController.MoveHorizontal(Vector2.right);
					break;
				case "down":
					if (_gameManager.currentShape != null)
					{
						_inputManager.EnableKeyInput(false);
						_gameManager.currentShape.movementController.InstantFall();
					}
					break;
				default:
					break;
			}
		}).AddTo(this);

		// ボタン系
		// 押されたボタン：Viewからの通知
		_buttonManager.OnButtonClicked.Subscribe(btn => 
		{
			Debug.Log(btn);
			_audioManager.PlayUIClick();
			switch (btn)
			{
				case "play":
					_gameManager.SetState(typeof(GamePlayState));
					break;
				case "pause":
					_gameManager.SetState(typeof(MenuState));
					break;
				case "restart":
					_gridManager.ClearBoard();
					_gameManager.EnablePlay(false);
					_gameManager.SetState(typeof(GamePlayState));
					_uiManager.inGameUI.gameOverPopUp.SetActive(false);
					break;
				case "setting":
					_uiManager.popUps.ActivateSettingsPopUp();
					_uiManager.panel.SetActive(true);
					break;
				case "stats":
					_uiManager.popUps.ActivatePlayerStatsPopUp();
					_uiManager.panel.SetActive(true);
					break;
				default:
					break;
			}
		}).AddTo(this);
	}
}
}