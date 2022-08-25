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
	[SerializeField] private ViewManager _viewManager;

	private float _gamePlayDuration;


	// Model
	[SerializeField] private GameManager _gameManager;
	[SerializeField] private PlayerInputManager _inputManager;
	[SerializeField] private ScoreManager _scoreManager;
	[SerializeField] private StateManager _stateManager;
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
		// ステート変更
		_stateManager.OnStateChangeObservable.Subscribe(state =>
		{
			Debug.Log(state);
			switch (state)
			{
				case "menu":
					// もし1つ前がplayだったら
					// if _gameManager.stats.timeSpent += Time.time - _gamePlayDuration;

					_uiManager.ActivateUI (Menus.MAIN);
					_cameraManager.ZoomOut();
					_uiManager.mainMenu.MainMenuStartAnimation();
					_uiManager.MainMenuArrange();
					break;
				case "play":
					_uiManager.panel.SetActive(false);
					_uiManager.ActivateUI(Menus.INGAME);

					_gamePlayDuration = Time.time;
					_cameraManager.ZoomIn();

					// 随時更新
					// if(Managers.Game.currentShape!=null)
					// 	_gameManager.currentShape.movementController.ShapeUpdate();
					break;
				case "gameover":
					// もし1つ前がplayだったら
					_gameManager.stats.timeSpent += Time.time - _gamePlayDuration;

					_gameManager.EnablePlay(false);
					_gameManager.stats.highScore = _scoreManager.currentScore;
					_gameManager.stats.numberOfGames++;
					_uiManager.popUps.ActivateGameOverPopUp();
					_audioManager.PlayLoseSound();
					break;
				default:
					break;
			}
		}).AddTo(this);


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