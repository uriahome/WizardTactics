# WizardTactics
Unity 2D Tower Defense Game  
「うりあの魔導書」はタワーディフェンスゲームです。  
![demo](https://raw.githubusercontent.com/uriahome/WizardTactics/master/Assets/Sprite/UriaHead.png)  
![demo](https://raw.githubusercontent.com/uriahome/WizardTactics/master/Assets/GIF/Title.gif)  
タイトル画面ではゲーム開始時の魔導書を選択することや、魔法一覧を確認することができます。  
![demo](https://raw.githubusercontent.com/uriahome/WizardTactics/master/Assets/GIF/Battle1.gif)  
戦闘中はマナを消費して5つの魔法から1つをクリックして発動できます。発動した魔法は削除されて新しい魔法と入れ替わります。  
また、一番右の顔アイコンはマナを消費せず削除もされません。  
![demo](https://raw.githubusercontent.com/uriahome/WizardTactics/master/Assets/GIF/Battle2.gif)  
魔法の中には2つ合わせて使用する[ペア]魔法が存在します。  
![demo](https://raw.githubusercontent.com/uriahome/WizardTactics/master/Assets/GIF/Battle3.gif)  
[ビルド]魔法は好きな場所に設置して戦闘をサポートしてくれます。  
ダウンロード[https://github.com/uriahome/WizardTactics/tree/master/Assets/Game]  
zipファイルをダウンロード後、解凍してから[WizardTactics.exe]を起動することで遊ぶことができます。  
プレイ動画[https://github.com/uriahome/WizardTactics/blob/master/Assets/Movie/movie.mp4]  
UnityRecorderを使用して録画した3分ほどの動画があります。  
# 工夫した点  
- デッキリストを4つに分割した点  
  https://github.com/uriahome/WizardTactics/blob/master/Assets/Script/DeckController.cs  
  DeckList,BattleDeckList,NowDeckList,HandListの4種類のリストで戦闘に使用する魔法を循環させるシステムを制作しました。  
  それぞれのリストの役割は以下の通りです。  
  * DeckList -> プレイヤーが最初に選んだ魔法や「報酬」やイベントで追加された魔法はこのリストに追加されたり複製、削除されます。  
  * BattleDeckList -> 各戦闘開始時にDecklistの内容をコピーして製作されます。  
  * NowDeckList -> 戦闘中にこのリストの中身が空になるたびにBattleDeckListの中身をコピーしてシャッフルします。
  * HandList -> プレイヤーが実際に確認する5枚の魔法がこのリストです。5枚以下の場合はNowDeckListの中身を1つ追加し、NowDeckList内のその項目を削除します.  
  このように、デッキリストを分割を行うことで、以下のような仕様をゲームに取り込めるようになりました。  
  1. デッキの総枚数が5枚以下の場合でもゲームを進行できる。  
  2. 戦闘中に一度だけ使用できる魔法を追加することができる。(例:魔法の「カクセイ」や「ツインソード」のような強力なカードはデメリットとして使用するたびに「ミス」という効果のない魔法がNowDeckListに追加されます。NowDeckListの中身は次の戦闘に引き継がれず、一度その中身が空になるとBattleDeckListがコピーされるため「ミス」は一度使用すればその戦闘では再び手札に追加されることはありません。該当魔法を再度使用した場合は「ミス」の汚染は続きますが...)  
  3. その戦闘中にのみデッキの中身を変化させることができる。(例:魔法の「スパイダー」のような魔法では、BattleDeckListに魔法の追加をすることができます。BattleDeckListの中身は各戦闘ごとにDeckListに上書きされるため、その戦闘中のみ「スパイダー」が増殖し、次の戦闘に入ったらリセットされている。このような実装をすることができます)  
- 
