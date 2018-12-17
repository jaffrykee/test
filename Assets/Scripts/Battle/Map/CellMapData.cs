using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TkmGame.Gtr.Battle {
    public class CellMapData {
        public const int c_rcf001Per = 2;
        public const int c_rcf001MaxHeight = 4;

        public CellMapData() {

        }
        public CellMapData(int x, int y) {
            int count = x * y;
            mapSizeX = x;
            mapSizeY = y;
            cellData = new CellData[count];
            for (int i = 0; i < cellData.Length; i++) {
                cellData[i] = new CellData();
            }
        }
        public void resizeCellMap(int x, int y) {
            int newCount = x * y;
            var newCellDataSetting = new CellData[newCount];
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    if (i < mapSizeX && j < mapSizeY && cellData[i * mapSizeY + j] != null) {
                        newCellDataSetting[i * y + j] = cellData[i * mapSizeY + j];
                    } else {
                        newCellDataSetting[i * y + j] = new CellData();
                    }
                }
            }
            mapSizeX = x;
            mapSizeY = y;
            cellData = newCellDataSetting;
        }
        public void rcf101() {
            for (int i = 0; i <= mapSizeX * mapSizeY - 32; i += 32) {
                var ba = System.Guid.NewGuid().ToByteArray();
                for (int j = 0; j < 16; j++) {
                    short vl = (short)(ba[j] / 16 - 4);
                    short vh = (short)(ba[j] % 16 - 4);
                    if (vl > 4) {
                        vl = 0;
                    }
                    if (vh > 4) {
                        vh = 0;
                    }
                    cellData[i + j * 2].height = vl;
                    cellData[i + j * 2 + 1].height = vh;
                }
            }
        }
        public void rcf001_getPois() {
            int mapSize = mapSizeX * mapSizeY;
            int countPoi = (mapSizeX * mapSizeY) >> c_rcf001Per;
            m_arrRandomPoi = new uint[countPoi];
            if (countPoi < 20000) {
                for (int i = 0; i <= countPoi - 8; i++) {
                    var ba = System.Guid.NewGuid().ToByteArray();
                    for (int j = 0; j < 16; j += 2) {
                        m_arrRandomPoi[i + j / 2] = (ba[j] + ((uint)ba[j + 1] << 8)) % (uint)mapSize;
                    }
                }
            } else {
                for (int i = 0; i <= countPoi - 4; i++) {
                    var ba = System.Guid.NewGuid().ToByteArray();
                    for (int j = 0; j < 16; j += 4) {
                        m_arrRandomPoi[i + j / 4] =
                            (ba[j] + ((uint)ba[j + 1] << 8) + ((uint)ba[j + 2] << 16) + ((uint)ba[j + 3] << 24)) % (uint)mapSize;
                    }
                }
            }
        }
        public void rcf001_terrainFunc(int x, int y) {
            var cell = getCell(x, y);
            var cell0 = getNeighbor0(x, y);
            int nextCount = 0;
            if (cell0 != null) {
                int dh = cell.height - cell0.m_cell.height;
                if (dh < -1) {
                    cell0.m_cell.height = cell.height + 1;
                    nextCount++;
                } else if (dh > 1) {
                    cell0.m_cell.height = cell.height - 1;
                    nextCount++;
                }
            }
            var cell2 = getNeighbor2(x, y);
            if (cell2 != null) {
                int dh = cell.height - cell2.m_cell.height;
                if (dh < -1) {
                    cell2.m_cell.height = cell.height + 1;
                    nextCount++;
                } else if (dh > 1) {
                    cell2.m_cell.height = cell.height - 1;
                    nextCount++;
                }
            }
            var cell4 = getNeighbor4(x, y);
            if (cell4 != null) {
                int dh = cell.height - cell4.m_cell.height;
                if (dh < -1) {
                    cell4.m_cell.height = cell.height + 1;
                    nextCount++;
                } else if (dh > 1) {
                    cell4.m_cell.height = cell.height - 1;
                    nextCount++;
                }
            }
            var cell6 = getNeighbor6(x, y);
            if (cell6 != null) {
                int dh = cell.height - cell6.m_cell.height;
                if (dh < -1) {
                    cell6.m_cell.height = cell.height + 1;
                    nextCount++;
                } else if (dh > 1) {
                    cell6.m_cell.height = cell.height - 1;
                    nextCount++;
                }
            }
            var cell8 = getNeighbor8(x, y);
            if (cell8 != null) {
                int dh = cell.height - cell8.m_cell.height;
                if (dh < -1) {
                    cell8.m_cell.height = cell.height + 1;
                    nextCount++;
                } else if (dh > 1) {
                    cell8.m_cell.height = cell.height - 1;
                    nextCount++;
                }
            }
            var cell10 = getNeighbor10(x, y);
            if (cell10 != null) {
                int dh = cell.height - cell10.m_cell.height;
                if (dh < -1) {
                    cell10.m_cell.height = cell.height + 1;
                    nextCount++;
                } else if (dh > 1) {
                    cell10.m_cell.height = cell.height - 1;
                    nextCount++;
                }
            }
            if (nextCount > 0) {
                if (cell0 != null) {
                    rcf001_terrainFunc(cell0.m_x, cell0.m_y);
                }
                if (cell2 != null) {
                    rcf001_terrainFunc(cell2.m_x, cell2.m_y);
                }
                if (cell4 != null) {
                    rcf001_terrainFunc(cell4.m_x, cell4.m_y);
                }
                if (cell6 != null) {
                    rcf001_terrainFunc(cell6.m_x, cell6.m_y);
                }
                if (cell8 != null) {
                    rcf001_terrainFunc(cell8.m_x, cell8.m_y);
                }
                if (cell10 != null) {
                    rcf001_terrainFunc(cell10.m_x, cell10.m_y);
                }
            }
        }
        public void rcf001_terrain() {
            foreach (var curPoi in m_arrRandomPoi) {
                var cx = getCellX((int)curPoi);
                var cy = getCellY((int)curPoi);
                var cell = getCell(cx, cy);
                if (cell.height != 0) {
                    rcf001_terrainFunc(cx, cy);
                }
            }
        }
        public void test(int x, int y) {
            var curCell = getCell(x, y);
            curCell.height = -4;
            getNeighbor0(x, y).m_cell.height = -2;
            getNeighbor2(x, y).m_cell.height = -1;
            getNeighbor4(x, y).m_cell.height = 0;
            getNeighbor6(x, y).m_cell.height = 1;
            getNeighbor8(x, y).m_cell.height = 2;
            getNeighbor10(x, y).m_cell.height = 3;
        }
        public void rcf001Ex() {
            rcf001_getPois();
            for (int i = 0; i < m_arrRandomPoi.Length; i += 32) {
                var ba = System.Guid.NewGuid().ToByteArray();
                for (int j = 0; j < 16; j++) {
                    short vl = (short)((ba[j] >> 5) - 4);
                    short vh = (short)(ba[j] % 8 - 4);
                    if (vl == 0) {
                        vl = 4;
                    }
                    if (vh == 0) {
                        vh = 4;
                    }
                    if (i + j * 2 < m_arrRandomPoi.Length) {
                        cellData[m_arrRandomPoi[i + j * 2]].height = vl;
                    } else {
                        break;
                    }
                    if (i + j * 2 + 1 < m_arrRandomPoi.Length) {
                        cellData[m_arrRandomPoi[i + j * 2 + 1]].height = vh;
                    } else {
                        break;
                    }
                }
            }
            rcf001_terrain();
        }
        public void rcf001() {
            rcf001_getPois();
            for (int i = 0; i < m_arrRandomPoi.Length; i++) {
                if (i % 2 == 0) {
                    cellData[m_arrRandomPoi[i]].height = c_rcf001MaxHeight;
                } else {
                    cellData[m_arrRandomPoi[i]].height = -c_rcf001MaxHeight;
                }
            }
            rcf001_terrain();
        }
        public int getCellX(int index) {
            return index % mapSizeY;
        }
        public int getCellY(int index) {
            return index / mapSizeX;
        }
        public CellData getCell(int x, int y) {
            return cellData[x + mapSizeX * y];
        }
        public bool isLegal(int x, int y) {
            bool ret = false;
            if (x >= 0 && y >= 0 && x < mapSizeX && y < mapSizeY) {
                ret = true;
            }
            return ret;
        }
#region Neighbor
        public class CellDataWithPoi {
            public int m_x;
            public int m_y;
            public CellData m_cell;
        }
        public CellDataWithPoi assignNeighbor(int nx, int ny) {
            CellDataWithPoi ret = null;
            if (isLegal(nx, ny) == true) {
                ret = new CellDataWithPoi();
                ret.m_x = nx;
                ret.m_y = ny;
                ret.m_cell = cellData[nx + mapSizeX * ny];
            }
            return ret;
        }
        public CellDataWithPoi getNeighbor0(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x, y + 1);
            } else {
                return assignNeighbor(x, y + 1);
            }
        }
        public CellDataWithPoi getNeighbor2(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x + 1, y + 1);
            } else {
                return assignNeighbor(x + 1, y);
            }
        }
        public CellDataWithPoi getNeighbor4(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x + 1, y);
            } else {
                return assignNeighbor(x + 1, y - 1);
            }
        }
        public CellDataWithPoi getNeighbor6(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x, y - 1);
            } else {
                return assignNeighbor(x, y - 1);
            }
        }
        public CellDataWithPoi getNeighbor8(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x - 1, y);
            } else {
                return assignNeighbor(x - 1, y - 1);
            }
        }
        public CellDataWithPoi getNeighbor10(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x - 1, y + 1);
            } else {
                return assignNeighbor(x - 1, y);
            }
        }
#endregion

        public int mapSizeX = 1;
        public int mapSizeY = 1;
        public CellData[] cellData = new CellData[1];
        public uint[] m_arrRandomPoi = null;
    }
}
