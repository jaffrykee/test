﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using System.IO;

namespace TkmGame.GtrEditor {
    public class CellMapFileItem : TreeViewItem {
        public FileSystemInfo m_fdi;
        public string m_resPath;
    }
}
