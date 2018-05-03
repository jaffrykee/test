using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMapSetting
{
    public int mapSizeX = 1;
    public int mapSizeY = 1;
    public CellData.Config[] cellData = new CellData.Config[1];
    private bool mt_test = false;
    public bool m_test
    {
        get
        {
            return mt_test;
        }
        set
        {
            mt_test = value;
        }
    }
    
    public CellMapSetting()
    {

    }
    public CellMapSetting(int x, int y)
    {
        int count = x * y;
        mapSizeX = x;
        mapSizeY = y;
        cellData = new CellData.Config[count];
        for (int i = 0; i < count; i++)
        {
            cellData[i] = new CellData.Config();
        }
    }
    public void resizeCellMap(int x, int y)
    {
        int newCount = x * y;
        var newCellDataSetting = new CellData.Config[newCount];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                newCellDataSetting[i * y + j] = new CellData.Config();
                if (i < mapSizeX && j < mapSizeY)
                {
                    newCellDataSetting[i * y + j] = cellData[i * mapSizeY + j];
                }
            }
        }
        mapSizeX = x;
        mapSizeY = y;
        cellData = newCellDataSetting;
    }
}
