using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TkmGame.Gtr.Battle;

namespace TkmGame.Gtr.Controller {
    public class MeshTouch : MonoBehaviour {
        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                var curCell = CellMap.getCellByScreenPoisition(Input.mousePosition, m_isHiddenTouch);
                if (curCell != null) {
                    var map = curCell.getParentMap();
                    if (map != null) {
                        map.setCurCell(curCell);
                    }
                }
            }
        }

        bool m_isHiddenTouch = false;
    }
}
