using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct CellData
{
    public CellData(bool disValue = false)
    {
        disable = disValue;
    }
    public bool disable;
}
