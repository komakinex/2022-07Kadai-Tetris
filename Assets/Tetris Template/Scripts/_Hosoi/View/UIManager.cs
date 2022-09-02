
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace hosoi
{
public enum Menus
{
	MAIN,
	INGAME,
	GAMEOVER
}

public class UIManager : MonoBehaviour {

	[SerializeField] private MainMenu _mainMenu;
	[SerializeField] private InGameUI _inGameUI;
	[SerializeField] private PopUp _popUps;
	[SerializeField] private GameObject _activePopUp;
	[SerializeField] private GameObject _panel;


	void Update()
	{
		if (_activePopUp != null)
			HideIfClickedOutside(_activePopUp);
	}
	public void StateAction(State state)
	{
		switch (state)
		{
			case State.Menu:
				ActivateUI (Menus.MAIN);
				_mainMenu.MainMenuStartAnimation();
				MainMenuArrange();
				break;
			case State.Play:
				_panel.SetActive(false);
				ActivateUI(Menus.INGAME);
				break;
			case State.Pause:
				ActivateUI (Menus.MAIN);
				_mainMenu.MainMenuStartAnimation();
				MainMenuArrangeInPause();
				break;
			case State.Gameover:
				_popUps.ActivateGameOverPopUp();
				break;
			default:
				break;
		}
	}
	public void ButtonAction(string btn)
	{
		switch (btn)
		{
			case "restart":
				_inGameUI.gameOverPopUp.SetActive(false);
				break;
			case "setting":
				_popUps.ActivateSettingsPopUp();
				_panel.SetActive(true);
				break;
			case "stats":
				_popUps.ActivatePlayerStatsPopUp();
				_panel.SetActive(true);
				break;
			default:
				break;
		}
	}

	public void ActivateUI(Menus menutype)
	{
		if (menutype.Equals (Menus.MAIN))
		{
			StartCoroutine(ActivateMainMenu());
		}
		else if(menutype.Equals(Menus.INGAME))
		{
			StartCoroutine(ActivateInGameUI());
		}
	}

	IEnumerator ActivateMainMenu()
	{
		_inGameUI.InGameUIEndAnimation();
		_inGameUI.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.3f);
		_mainMenu.gameObject.SetActive(true);
		_mainMenu.MainMenuStartAnimation();
	}

	IEnumerator ActivateInGameUI()
	{
		_mainMenu.MainMenuEndAnimation();
		yield return new WaitForSeconds(0.3f);
		_mainMenu.gameObject.SetActive(false);
		_inGameUI.gameObject.SetActive(true);
		_inGameUI.InGameUIStartAnimation();
	}



	public void MainMenuArrangeInPause()
	{
		_mainMenu.layout.spacing = 20;
		_mainMenu.layout.padding.left = _mainMenu.layout.padding.right = 200;
		_mainMenu.restartButton.SetActive(true);
	}
	public void MainMenuArrange()
	{
			_mainMenu.layout.spacing = 50;
			_mainMenu.layout.padding.left = _mainMenu.layout.padding.right = 250;
			_mainMenu.restartButton.SetActive(false);
	}

	private void HideIfClickedOutside(GameObject outsidePanel)
	{
		if (Input.GetMouseButton(0) && outsidePanel.activeSelf &&
			!RectTransformUtility.RectangleContainsScreenPoint(
				outsidePanel.GetComponent<RectTransform>(),
				Input.mousePosition,
				Camera.main))
		{
			outsidePanel.SetActive(false);
			outsidePanel.transform.parent.gameObject.SetActive(false);
			_panel.SetActive(false);
			_activePopUp = null;
		}
	}

	public void UpdateCurrentScoreUI(int score)
	{
		_inGameUI.UpdateCurrentScoreUI(score);
	}
	public void UpdateHighScoreUI(int score)
	{
		_inGameUI.UpdateHighScoreUI(score);
	}

}
}