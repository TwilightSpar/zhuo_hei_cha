# 捉黑叉

## 目录
- [项目概要](#项目概要)
- [流程](#流程)
- [类](#类)

---

## 项目概要
一个承载着记忆的网页应用。。。

## 流程

### 开始阶段
1. 给玩家编号
2. 发牌
3. 进贡
4. 暗中交易
5. 亮黑叉

### 游戏过程
1. 玩家出牌
2. 判定：下家是否出牌
   - 是：回到步骤1
   - 否：继续步骤2， 直到所有其他玩家都选择不出牌， 然后转到步骤3
3. 判定：是否有玩家手中无牌
   - 是：判定：游戏是否结束（黑叉或平民全部出完）
     - 是：进入[结尾阶段](#结尾阶段)
     - 否：记录出完牌的玩家，转到步骤4
   - 否：回到步骤1
4. 抢车/给车（*！需要讨论！*），然后回到步骤1

### 结尾阶段
1. 统计结果，计算进贡


### 类

#### Card
- 成员变量
    |变量名|数据类型|描述|取值范围|
    |---|---|---|---|
    |ID|`int`|牌的编号|0-53|
    |suit|`enum CardSuit`|牌的花色|[enum CardSuit](#enum-CardSuit)|
    |pip|`int`|牌的点数|0-12 (王牌只能取0,1)|
- 类函数
    |函数名|参数|返回类型|描述|
    |---|---|---|---|
    |identifyHand|hand: `Card[]`|[`enum PokerHand`](#enum-PokerHand)|判定hand的种类|
    |compareHands|hand1: `Card[]`, hand2: `Card[]`|`int`|比较hand1和hand2的大小|

#### Player
- 成员变量
    |变量名|数据类型|描述|取值范围|
    |---|---|---|---|
    |cards|`Card[]`|手牌|n/a|
    |ID|`int`|玩家序号|0-3|

- 成员函数
    |函数名|参数|返回类型|描述|
    |---|---|---|---|
    |playCards|cards: Card[]|`void`(*！需要讨论！*)|玩家出牌|

#### Game
- 成员变量
    |变量名|数据类型|描述|取值范围|
    |---|---|---|---|
    |deck|`Card[]`|牌堆|n/a|
    |lastPlayedCards|`Card[]`|上次出的牌|n/a|
    |players|`Player[]`|场上玩家|n/a|
    |remainingPlayers|`Player[]`|场上剩余玩家|n/a|
    |？|？|进贡、亮黑叉信息|？|

#### enum CardSuit
- 取值：
  - HEARTS (红桃)
  - DIAMONDS (方片)
  - CLUBS (草花)
  - SPADES (黑桃)
  - JOKERS (王)

#### enum PokerHand
- 取值(有待补全):
  - NOTHING (什么都不是。。。)
  - PAIR (对)
  - STRAIGHT (顺)
  - STRAIGHT_FLUSH (同花顺)
  - THREE_OF_A_KIND (轰)
  - FOUR_OF_A_KIND (炸)