using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

public class Goods
{
    public string m_name;
    public Goods(Repertory rep, string name)
    {
        m_name = name;
        rep.addItem(this);
    }
}
