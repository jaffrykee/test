using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct LeagueData {
    public string name;
}
public struct ArmyData {
    public string name;
    //通过type来switch卡片等的初始化流程。
    public int type;
    public string league;
}
/// <summary>
/// 舞台，关卡。
/// </summary>
public class Stage {
    public List<LeagueData> leagues;
    public List<ArmyData> armies;
}
