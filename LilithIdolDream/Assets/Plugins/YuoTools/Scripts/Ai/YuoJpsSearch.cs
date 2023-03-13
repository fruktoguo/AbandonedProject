using System.ComponentModel;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;

namespace YuoTools
{
    public class YuoJpsSearch : SerializedMonoBehaviour
    {


        /// <summary>
        /// 计算方向
        /// </summary>
        /// <param name="now"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Dir ComputeDir(int nowX, int nowY, int endX, int endY)
        {
            int x = endX - nowX;
            int y = endY - nowY;
            if (x.RAbs() >= y.RAbs())
            {
                if (x >= 0)
                {
                    return Dir.Right;
                }
                else
                {
                    return Dir.Left;
                }
            }
            else
            {
                if (y >= 0)
                {
                    return Dir.Up;
                }
                else
                {
                    return Dir.Down;
                }
            }
        }

        private int[,] _Map = new int[,] {
            {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,1,1,1,1,0,1,1,1,1,1,1,0,0,0,0,0,1,0,0,0,0,0,0,0},
            {0,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0},
            {0,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
            {0,0,1,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,0,1,0,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,0,1,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,0,1,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,0,1,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,0,1,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,0,0,1,0,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {0,0,1,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
        [TableMatrix(DrawElementMethod = "DrawElement", RowHeight = 30)]
        public YuoGrid[,] Map;

#if UNITY_EDITOR
        private static YuoGrid DrawElement(Rect rect, YuoGrid value)
        {
            if (value == null)
            {
                return value;
            }
            if (!value.CanMove)
            {
                UnityEditor.EditorGUI.DrawRect(rect, new Color(1, 1, 0));
            }
            if (value.CanJump)
            {
                UnityEditor.EditorGUI.DrawRect(rect, new Color(1, 0, 0));
                if (value.jump.ContainsDir(Dir.Up))
                {
                    UnityEditor.EditorGUI.DrawRect(rect.SetHeight(15).SetWidth(7.5f).AddX(12), new Color(0, 0, 0));
                }
                if (value.jump.ContainsDir(Dir.Down))
                {
                    UnityEditor.EditorGUI.DrawRect(rect.SetHeight(15).SetWidth(7.5f).AddY(15).AddX(12), new Color(0, 0, 0));
                }
                if (value.jump.ContainsDir(Dir.Left))
                {
                    UnityEditor.EditorGUI.DrawRect(rect.SetHeight(7.5f).SetWidth(15).AddY(12), new Color(0, 0, 0));
                }
                if (value.jump.ContainsDir(Dir.Right))
                {
                    UnityEditor.EditorGUI.DrawRect(rect.SetHeight(7.5f).SetWidth(15).AddY(12).AddX(15), new Color(0, 0, 0));
                }
                if (value.Tag)
                {
                    UnityEditor.EditorGUI.DrawRect(rect, new Color(1, 1, 1));
                }
            }
            return value;
        }
#endif
        public int MapSizeX;
        public int MapSizeY;
        void Init()
        {
            MapSizeX = _Map.GetLength(0);
            MapSizeY = _Map.GetLength(1);
            Map = new YuoGrid[MapSizeX, MapSizeY];
            for (int x = 0; x < _Map.GetLength(0); x++)
            {
                for (int y = 0; y < _Map.GetLength(1); y++)
                {
                    var grid = new YuoGrid();
                    grid.x = x;
                    grid.y = y;
                    grid.CanMove = _Map[x, y] == 0;
                    Map[x, y] = grid;
                }
            }
            JumpPoints = new List<YuoGrid>();
        }
        private void Start()
        {
            Init();
            foreach (var item in Map)
            {
                ComputeJump(item);
            }
        }
        void ComputeJump(YuoGrid grid)
        {
            //只有墙体附近可以跳跃
            if (grid.CanMove) return;
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    //去除掉自己
                    if (x == 0 && y == 0)
                        continue;
                    //斜方向
                    if (x != 0 && y != 0)
                    {
                        //如果斜方向的两个相邻直线方向可以通过
                        if (CanMove(grid.x + x, grid.y + y) && CanMove(grid.x + x, grid.y) && CanMove(grid.x, grid.y + y))
                        {
                            SetJump(Map[grid.x + x, grid.y + y], x, y);
                        }
                    }
                }
            }
        }
        void SetJump(YuoGrid grid, int x, int y)
        {
            grid.CanJump = true;
            if (!JumpPoints.Contains(grid))
            {
                print("添加了一个跳点");
                JumpPoints.Add(grid);
            }
            grid.jump.AddDir(x == -1 ? Dir.Right : Dir.Left);
            grid.jump.AddDir(y == 1 ? Dir.Up : Dir.Down);
        }
        public List<YuoGrid> JumpPoints;
        public void Connect()
        {
            foreach (var item in JumpPoints)
            {
                foreach (var dir in item.jump.Dirs)
                {
                    while (true)
                    {
                        int num = 1;
                        YuoGrid grid;
                        grid = GetGrid(item.x + dir.x * num, item.y + dir.y * num);
                        //往一个方向一直找
                        while (true)
                        {
                            grid = GetGrid(item.x + dir.x * num, item.y + dir.y * num);
                            num++;
                        }
                    }
                }

            }
        }
        static Vector2Int GetDir(Dir dir)
        {
            switch (dir)
            {
                case Dir.Up:
                    return Vector2Int.up;
                case Dir.Down:
                    return Vector2Int.down;
                case Dir.Left:
                    return Vector2Int.left;
                case Dir.Right:
                    return Vector2Int.right;
                default:
                    return Vector2Int.zero;
            }
        }
        public YuoGrid GetGrid(int x, int y)
        {
            if (x < 0 || x >= MapSizeX || y < 0 || y >= MapSizeY)
                return null;
            return Map[x, y];
        }
        private bool CanMove(int x, int y)
        {
            if (!(x).InRange(0, MapSizeX - 1) || !(y).InRange(0, MapSizeY - 1))
                return false;
            return Map[x, y].CanMove;
        }
        #region 类型
        public enum Dir
        {
            Up = 1,
            Down = 2,
            Left = 4,
            Right = 8,
        }
        public class JumpPoint
        {
            public void AddDir(Dir dir)
            {
                if (!ContainsDir(dir))
                {
                    Dirs.Add(GetDir(dir));
                    dirs += dir.GetHashCode();
                }
            }
            public bool ContainsDir(Dir dir)
            {
                return (dirs & dir.GetHashCode()) != 0;
            }
            public List<YuoGrid> Connects = new List<YuoGrid>();
            public List<Vector2Int> Dirs = new List<Vector2Int>();
            int dirs = 0;
        }
        public class YuoGrid
        {
            public int x;
            public int y;
            /// <summary>
            /// 显示的时候标记用的,可以删
            /// </summary>
            public bool Tag;
            public bool CanMove;
            public bool IsMoved;
            internal bool CanJump;
            public JumpPoint jump = new JumpPoint();
        }

        #endregion
    }
}