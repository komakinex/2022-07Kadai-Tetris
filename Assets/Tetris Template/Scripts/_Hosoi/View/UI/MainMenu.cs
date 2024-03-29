﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

namespace hosoi
{
public class MainMenu : MonoBehaviour
{
	public Text tetrisLogoText;
	public GameObject menuButtons;
	public GameObject restartButton;

	public HorizontalLayoutGroup layout;

	void Awake()
	{
		layout = GetComponent<HorizontalLayoutGroup>();
	}

	public void MainMenuStartAnimation()
	{
		menuButtons.GetComponent<RectTransform>().DOAnchorPosY(-850, 1, true);
		tetrisLogoText.GetComponent<RectTransform>().DOAnchorPosY(600, 1, true);
	}

	public void MainMenuEndAnimation()
	{
		menuButtons.GetComponent<RectTransform>().DOAnchorPosY(-1450, 0.3f, true);
		tetrisLogoText.GetComponent<RectTransform>().DOAnchorPosY(1200, 0.3f, true);
	}
}
}