# WizardTactics
Unity 2D Tower Defense Game  
# 目次  
1. [概要](#anchor1)  
2. [ダウンロード](#anchor2)
3. [プレイ動画](#anchor3)
4. [動作環境](#anchor4)
5. [ソースコード](#anchor5)
6. [工夫した点](#anchor6)
7. [基本操作](#anchor7)
8. [使用ソフト](#anchor8)
9. [謝辞](#anchor9)

<a id="anchor1"></a>
# 概要  
「うりあの魔導書」はタワーディフェンスゲームです。  
![demo](https://raw.githubusercontent.com/uriahome/WizardTactics/master/Assets/Sprite/UriaHead.png)  
![demo](https://raw.githubusercontent.com/uriahome/WizardTactics/master/Assets/GIF/Title.gif)  
タイトル画面ではゲーム開始時の魔導書を選択することや、魔法一覧を確認することができます。
![demo](https://raw.githubusercontent.com/uriahome/WizardTactics/master/Assets/GIF/Battle1.gif)  
戦闘中はマナを消費して5つの魔法から1つをクリックして発動できます。発動した魔法は削除されて新しい魔法と入れ替わります。
また、一番右の顔アイコンはマナを消費せず削除もされません。 
戦闘終了後には、**3種類の魔法から1つ**習得することができます。  
![demo](https://raw.githubusercontent.com/uriahome/WizardTactics/master/Assets/GIF/Battle2.gif)  
魔法の中には2つ合わせて使用する[ペア]魔法が存在します。  
![demo](https://raw.githubusercontent.com/uriahome/WizardTactics/master/Assets/GIF/Battle3.gif)  
[ビルド]魔法は好きな場所に設置して戦闘をサポートしてくれます。  
<a id="anchor2"></a>
# ダウンロード  
[https://github.com/uriahome/WizardTactics/tree/master/Assets/Game]  
zipファイルをダウンロード後、解凍してから[WizardTactics.exe]を起動することで遊ぶことができます。  
また、起動する際に、アスペクト比が**16:9となるように解像度**を選択してください。  
<a id="anchor3"></a>
# プレイ動画  
[https://github.com/uriahome/WizardTactics/blob/master/Assets/Movie/movie.mp4]  
UnityRecorderを使用して録画した3分ほどの動画があります。 
<a id="anchor4"></a>
# 動作環境  
※動作確認済み環境  
PC  
OS:Windows 10 64bit  
<a id="anchor5"></a>
# ソースコード  
使用しているソースコードは[/Assets/Script/]内に全てあります。  
https://github.com/uriahome/WizardTactics/tree/master/Assets/Script  
<a id="anchor6"></a>
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
  1. **デッキの総枚数が5枚以下の場合でもゲームを進行できる。**  
  2. **戦闘中に一度だけ使用できる魔法を追加することができる。**(例:魔法の「カクセイ」や「ツインソード」のような強力なカードはデメリットとして使用するたびに「ミス」という効果のない魔法がNowDeckListに追加されます。NowDeckListの中身は次の戦闘に引き継がれず、一度その中身が空になるとBattleDeckListがコピーされるため「ミス」は一度使用すればその戦闘では再び手札に追加されることはありません。該当魔法を再度使用した場合は「ミス」の汚染は続きますが...)  
  3. **その戦闘中にのみデッキの中身を変化させることができる。**(例:魔法の「スパイダー」のような魔法では、BattleDeckListに魔法の追加をすることができます。BattleDeckListの中身は各戦闘ごとにDeckListに上書きされるため、その戦闘中のみ「スパイダー」が増殖し、次の戦闘に入ったらリセットされている。このような実装をすることができます)  
- 手札のソート機能  
    https://github.com/uriahome/WizardTactics/blob/master/Assets/Script/DeckController.cs  
    123~142行目のSortHands()  
    「ペア」魔法と呼ばれる2つの魔法のイラストが重なる魔法の実装に当たり。手札に該当する魔法がそろった場合にイラストが噛み合っていないと不自然になる機能が追加されました.  
    それに伴い、SortHands()を作成しHandListの中身を名前順に並べる機能を実装しました。  
    この機能は魔法を使用するたびに利用する、頻繁に利用する機能であるため、
    GameObject.Find()のようなシーン全体から探索する重い処理を多用するのを避け、子オブジェクトを取得する方法を用いました.  
<a id="anchor7"></a>
# 基本操作  
　**すべての操作はマウスのクリックで行われます。**  
  1. タイトル画面で左右のどちらかのキャラクターをクリックして初期魔導書を選択します。  
  2. [START]をクリックすることで戦闘画面にシーンが移動します。  
  3. 戦闘シーンでは、5種類の魔法と一番右の通常攻撃をクリックすることで使用することができます。  
  4. 戦闘終了後の報酬画面やイベントもクリックすることでその選択を行うことができます。  
<a id="anchor8"></a>
# 使用ソフト 
* Unity 2018.3.7f1  
* Aseprite  ドット絵の素材を作成するのに使用しました。  
<a id="anchor9"></a>
# 謝辞  
以下の素材を使用させていただきました。  
- BGM・効果音
  * 魔王魂 （https://maoudamashii.jokersounds.com/music_bgm.html）
  * SKIPMORE(http://www.skipmore.com/)
- フォント
  * PixelMplus（ピクセル・エムプラス）(http://itouhiro.hatenablog.com/entry/20130602/font)

# 連絡先
j318244@ns.kogakuin.ac.jp
