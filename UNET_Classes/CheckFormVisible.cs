﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNET_Classes
{
    public class CheckFormVisible
    {



        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(POINT Point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
    //    public static implicit operator System.Drawing.Point(POINT p)
    //    {
    //        return new System.Drawing.Point(p.X, p.Y);
    //    }

    //    public static implicit operator POINT(System.Drawing.Point p)
    //    {
    //        return new POINT(p.X, p.Y);
    //    }
    //}

    //public static bool IsControlVisibleToUser(this Control control)
    //{
    //    var pos = control.PointToScreen(control.Location);
    //    var pointsToCheck = new POINT[]
    //                            {
    //                                pos,
    //                                new Point(pos.X + control.Width - 1, pos.Y),
    //                                new Point(pos.X, pos.Y + control.Height - 1),
    //                                new Point(pos.X + control.Width - 1, pos.Y + control.Height - 1),
    //                                new Point(pos.X + control.Width/2, pos.Y + control.Height/2)
    //                            };

    //    foreach (var p in pointsToCheck)
    //    {
    //        var hwnd = WindowFromPoint(p);
    //        var other = Control.FromChildHandle(hwnd);
    //        if (other == null)
    //            continue;

    //        if (control == other || control.Contains(other))
    //            return true;
    //    }

    //    return false;
    //}
}
}