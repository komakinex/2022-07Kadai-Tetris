# 2022-07Kadai-Tetris
 Unity 2021.3.4f1  
 [元のサンプルプロジェクト](https://assetstore.unity.com/packages/templates/tetris-template-mobile-ready-72717)

## Scripts
Assets/Tetris Template/Scripts/_Hosoi/  
  
Presenter.cs  
・ModelとViewの各マネージャーのやり取りの仲介  
***
**Model**
AudioManager.cs  
・音の再生  
ButtonManage.cs  
・どのボタンが押されたか通知  
GameManager.cs  
・ステートの切り替えを通知  
PlayerInputManager.cs  
・キー入力を通知  
ScoreManager.cs  
・スコアの設定や通知  
State.cs  
・ステートを定義  
***
**View**
BlockManager.cs  
・ブロックの生成・削除、色の指定、ブロックの回転や移動、一列揃ったときのロジックなど  
CameraManager.cs  
・カメラのズームインアウト  
GridManager.cs  
・グリット描画の更新、ブロックの位置判定  
UIManager.cs  
・ポップアップUIの管理、メニューの表示非表示  
ColorView.cs  
・ランダムで割当てられるブロックの色のパレット  
**UI**
PopUp.cs  
・各ポップアップUIの表示非表示  
GameOverPopUp.cs  
・ゲーム終了時のスコア表示  
InGameUI.cs  
・スコア表示とゲーム中UIのアニメーション  
MainMenu.cs  
・メニューUIのアニメーション  
SettingsMenu.cs  
・使ってない  
StatsUI.cs  
・使ってない  