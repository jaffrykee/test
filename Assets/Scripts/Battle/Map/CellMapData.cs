using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TkmGame.Gtr.Battle {
    public class CellMapData {
        public CellMapData() {

        }
        public CellMapData(int x, int y) {
            int count = x * y;
            m_mapSizeX = x;
            m_mapSizeY = y;
            m_cellData = new CellData[count];
            for (int i = 0; i < m_cellData.Length; i++) {
                m_cellData[i] = new CellData();
            }
        }
        public void resizeCellMap(int x, int y) {
            int newCount = x * y;
            var newCellDataSetting = new CellData[newCount];
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    if (i < m_mapSizeX && j < m_mapSizeY && m_cellData[i * m_mapSizeY + j] != null) {
                        newCellDataSetting[i * y + j] = m_cellData[i * m_mapSizeY + j];
                    } else {
                        newCellDataSetting[i * y + j] = new CellData();
                    }
                }
            }
            m_mapSizeX = x;
            m_mapSizeY = y;
            m_cellData = newCellDataSetting;
        }
        public void randomCellFunc001() {
            var ba = System.Guid.NewGuid().ToByteArray();
            int t = 0;
            foreach (var cur in ba) {
                t++;
            }
            Debug.LogWarning(t.ToString());
        }

        public int m_mapSizeX = 1;
        public int m_mapSizeY = 1;
        public CellData[] m_cellData = new CellData[1];
    }
}
