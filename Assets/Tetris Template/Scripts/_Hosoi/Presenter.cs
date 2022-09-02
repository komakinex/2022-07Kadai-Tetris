using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace hosoi
{
public class Presenter : MonoBehaviour
{
	// View
	[SerializeField] private BlockManager _blockManager;
	[SerializeField] private ButtonManager _buttonManager;
	[SerializeField] private CameraManager _cameraManager;
	[SerializeField] private UIManager _uiManager;
	[SerializeField] private ViewManager _viewManager;

	private float _gamePlayDuration;


	// Model
	[SerializeField] private GameManager _gameManager;
	[SerializeField] private PlayerInputManager _inputManager;
	[SerializeField] private ScoreManager _scoreManager;
	// other
	[SerializeField] private AudioManager _audioManager;


	void Awake ()
	{
		// シーンを切り替えても破棄されないようにしている
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		// いろんなイベントが飛んできた時の対応を書いておく

		// ブロック系
		_blockManager.OnAcceptKeyinput.Subscribe(canInput =>
		{
			_inputManager.EnableKeyInput(canInput);
		}).AddTo(this);
		_blockManager.OnGetScore.Subscribe(_ =>
		{
			_scoreManager.OnScore();
		}).AddTo(this);
		_blockManager.OnGameOver.Subscribe(_ =>
		{
			_gameManager.SetState(State.Gameover);
		}).AddTo(this);


		// ステート変更
		_gameManager.OnStateChangeObservable.Subscribe(state =>
		{
			Debug.Log(state);
			_gameManager.StateAction(state);
			_scoreManager.StateAction(state);

			_audioManager.StateAction(state);
			_blockManager.StateAction(state);
			_cameraManager.StateAction(state);
			_uiManager.StateAction(state);


		// 	switch (state)
		// 	{
		// 		case "menu":
		// 			// もし1つ前がplayだったら
		// 			// if _gameManager.stats.timeSpent += Time.time - _gamePlayDuration;

		// 			_uiManager.MainMenuArrange();
		// 			break;
		// 		case "play":
		// 			_gamePlayDuration = Time.time;

		// 		case "gameover":
		// 			// もし1つ前がplayだったら
		// 			// _gameManager.stats.timeSpent += Time.time - _gamePlayDuration;

		// 			_audioManager.PlayLoseSound();
		// 			break;

		// 	}
		}).AddTo(this);

		// カメラ
		_cameraManager.OnZoomInFinObservable.Subscribe(_ =>
		{
			if(!_gameManager.isGameActive)
			{
				_blockManager.BlockSpawn();
				_gameManager.EnablePlay(true);
			}
		}).AddTo(this);

		// キー入力
		_inputManager.OnKeyInputObservable.Subscribe(key =>
		{
			Debug.Log(key);
			_blockManager.BlockMove(key);
		}).AddTo(this);

		// UI系
		// 押されたボタン：Viewからの通知
		_buttonManager.OnButtonClicked.Subscribe(btn =>
		{
			Debug.Log(btn);
			_audioManager.PlayUIClick();

			_gameManager.ButtonAction(btn);
			_uiManager.ButtonAction(btn);
			_blockManager.ButtonAction(btn);
		}).AddTo(this);

		// スコアの変更
		_scoreManager.OnCurrentScoreObservable.Subscribe(currentScore => 
		{
			Debug.Log("current score: " + currentScore);
			_uiManager.UpdateCurrentScoreUI(currentScore);
		}).AddTo(this);
		_scoreManager.OnHighScoreObservable.Subscribe(highScore => 
		{
			Debug.Log("high score: " + highScore);
			_uiManager.UpdateHighScoreUI(highScore);
		}).AddTo(this);

	}
}
}