# 牌的比较

## 给牌分类

### 准备工作
- 在比较两组牌之前，我们先把它们都转化成一个易于比较并且不会丢失信息的类，然后所有的比较都应该用这个类进行。
- 我们会自定义一个`Hand`类来解决这个问题

### Hand 类
- `Hand`类的设计目标是能够用来表示所有可出的、合理的牌的组合，同时又便于比较。也就是说，每一个`Hand`对象都应该一组合理的牌。
- 每一个`Hand`都有一个pattern。举个例子：一个（单）对的pattern应该是 `xx` ，其中`x`为任何非王的牌。同样地，一个（单）顺的pattern应该是`x1...xn`，其中`x1, ..., xn`是`n`张点数连续的牌
- 我们按照这个pattern的思路来设计`Hand`类。`Hand`类会包含以下的属性：

|属性|备注|
|---|---|
|Group|组别：比如这个牌的组合是`基本组`还是`轰炸组`|
|Value|单张牌的大小，如果是顺或者飞机，就是起始牌的大小|
|Length|pattern(去掉重复点数之后)的长度，比如：一个单对的length就是1，一个三张牌的顺length就是3|
|Frequency/Repetition|这个pattern每个点数都被重复了几遍（每张牌必须被重复相同次，暂时不支持斗地主里的3带1或者3带2这类的）|

- 目前看来，这四个属性大概率可以用来代表所有合理的牌的组合（如果不行看看可不可以怎么改进一下）


### 所有牌的组合（用Hand表示出来）
|组合|pattern|表示方法|取值范围|
|---|---|---|---|
|单顺|`x1...xn`| <ul><li>Group: `Group1`</li><li>Value: `x1`</li><li>Length: `n`</li><li>Repetition: `1`</li></ul> |`n >= 3`，`x1, ..., xn`为点数|
|对|`x1x1...xnxn`| <ul><li>Group: `Group1`</li><li>Value: `x1`</li><li>Length: `n`</li><li>Repetition: `2`</li></ul> |`n >= 1`，`x1, ..., xn`为点数|
|轰/飞机|`x1x1x1...xnxnxn`| <ul><li>Group: `Group2`</li><li>Value: `x1`</li><li>Length: `n`</li><li>Repetition: `3`</li></ul> |`n >= 1`，`x1, ..., xn`为点数|
|炸/轰炸机|`x1x1x1x1...xnxnxnxn`| <ul><li>Group: `Group2`</li><li>Value: `x1`</li><li>Length: `n`</li><li>Repetition: `4`</li></ul> |`n >= 1`，`x1, ..., xn`为点数|
|王炸|`小王大王`| <ul><li>Group: `Group2`</li><li>Value: `最大`</li><li>Length: `N/A`</li><li>Repetition: `N/A`</li></ul> |N/A|

### Group
- 每个牌的组合可以分成两个Group，`Group1`和`Group2`
- `Group1`里任何的比较都要遵循以下原则
    - 只有Length和Repetition相等的时候才能进行比较
    - Value大的牌大

- `Group2`和`Group1`比较的时候，永远是`Group2`里的牌大
- `Group2`组内比较的时候，要遵循以下排序链（排名靠上的大）：
  - 王炸
  - 轰炸机 （这里需要展开讨论一下）
  - 飞机（同上）