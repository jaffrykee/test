using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Operation : Goods {

    public enum OptType {

        //真正的操作，比如选中一个unit，让它攻击另一个unit。

        action,

        //因为一个action所产生的一些中间状态，比如需要中立服务提供的随机值之类。但是是伪随机，应该按照操作和历史操作时间戳等信息，算出一个基于这个历史操作的固定的随机值。

        step,

        max,

    }

    public Turn m_turn;

    public Operation(string name, Turn turn) : base(BattleManager.instance(), name) {
        m_turn = turn;
    }
}
