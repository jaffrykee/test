using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

public class Goods {
    private Repertory mt_rep;
    public Repertory m_rep {
        get {
            return mt_rep;
        }
    }
    private string mt_name;
    public string m_name {
        get {
            return mt_name;
        }
        set {
            m_rep.abrase(this);
            mt_name = value;
            m_rep.record(this);
        }
    }
    public Goods(Repertory rep, string name) {
        mt_name = name;
        mt_rep = rep;
        rep.addItem(this);
    }
}
