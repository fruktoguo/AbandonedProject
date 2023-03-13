# YuoTools
 <a href="https://gitee.com/YuoHira/YuoTools.git" target="_blank">自己用的一些小工具</a>
----
## 扩展方法:
###  	Log: 可以在任何类型后面.Log直接输出
### 	Mathf:
        Clamp: 限定范围 传两个参数为设置最小值和最大值, 一个参数为设置最大值, 最小值为0, 无参设置为不小于0
        InRange: 判断一个数是否在范围内
### 	Transform:
        ResetTrans:设置为默认值,即Inspector面板上的Reset
        SetposX、Y、Z:单独设置xyz三个坐标中的一个
        SetLocalPos:同上
        Vector2Int.InRange:判断这个点是否在指定范围内
### 	GameObject:
        ReShow:隐藏再显示
        Show/Hide:显示/隐藏物体
### 	String:
        一堆网上嫖的, 自己瞅瞅

-----
## 实用工具:
#### 	YuoDelay: *延迟执行代码*
        使用 this.YuoDelay(需要延迟执行的方法-可使用Lambda表达式, 延迟执行的时间);
        该方法会返回一个YuoDelayMod, 可以通过对mod进行操作来改变延迟方法的状态
			重载1:使用带有一个参数的action,这个参数为这个延迟本身的mod,可用于外部移除延迟
		this.YuoDelayRealtime:同上,但是这个延迟不受TimeScale影响
        YuoStop(YuoDelayMod):可停止还未执行的YuoDelay                        
        YuoStopForce(YuoDelayMod):强制停止还未执行的YuoDelay
        YuoDelayMod:
                AddDelay:可以为还未执行的YuoDelay添加额外的等待时间, 不能减少
                SetDelay:可以为还未执行的YuoDelay刚改等待时间, 可以比原来时间小
		SetUpdate(action):让这个延迟从协程切换成Update模式,并每帧调用事件
			Pause:仅在update模式可用,修改这个参数以暂停延迟
#### 	MouseClickManger: *鼠标连点事件管理器*
        封装了一个判断鼠标是否连点的管理器, 如双击三击(可配置), 可以通过 AddCombo (触发的连击次数, 具体事件)
#### 	Temp: *全局临时变量*
        一些全局的静态变量, 可作为临时变量使用
####     Loom: *异步调用方法*
        Loom.RunAsync(需要异步的事件);
####     InputManger: *一个游戏里可能用得上的按键管理器*
        InputManger.Instance.Add(按键的名字-如"左",new InputItem(keycode,按下持续触发的事件,按下时触发,抬起时触发));
        InputItem.SetMax(最大时间,到达最大时间时触发,没有到达最大时间抬起时触发)//开启最大按下时间,一般用于蓄力
####     SaveGObjSate: *保存物体所有子物体的图片颜色透明度、显示隐藏、位置缩放信息，然后用load加载（用于美术制作2d动画时，重置物体的状态）*
        Save();在Awake调用,可以在animator播放前保存信息
        Load():在动画播放前调用,可以重置状态
####     YuoAnimator: *封装的一个简易的FSM状态机*
        挂载到需要使用的物体上面,会自动添加Animator组件
        使用Add("动画片段的名字",事件执行的时间,执行的事件);给动画片段添加一个事件,可添加多个
        使用Play("动画片段的名字");播放动画,如果有事件则在对应时间调用事件
####    YuoTween: *自用的动画*
        Transform.Move(目标点,需要时间,动画结束时调用的时间,延迟启动的时间);//将物体移动到目标点
        Transform.MoveToCurue(目标点,需要时间,控制点,结束回调);//基于贝塞尔曲线的动画
        Transform.MoveToCurue2D(目标点,需要时间,(控制点x,控制点y)),结束回调);//基于贝塞尔曲线的动画,控制点的两个坐标含义为,以物体当前位置为起点,x为离目标点距离的比例,1为目标点的位置,0为自身的位置,y为离两点连线距离的大小
        float.to(目标值,需要时间,赋值方法(例: x=>float=x),结束回调);//基本就是dotween的那个动画
        Vector.to();//基本同上
        Text.PlayTextUpAndFade(上移的距离,需要的时间);//让一个text上移并且逐渐降低透明度,一般用于伤害数字向上逐渐消失
## 常用方法:
####     Bezier(贝塞尔曲线): 根据起始点结束点控制点返回一个平滑的点的数组
####     CopyToClipboard: 传入一个字符串,将这个字符串复制到剪切板