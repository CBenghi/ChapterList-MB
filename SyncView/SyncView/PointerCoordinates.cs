﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChapterListMB.SyncView
{
    class PointerCoordinates : ITimedObject
    {
        public PointerCoordinates(int t, int x, int y)
        {
            X = x;
            Y = y;
            TimeStampMilliseconds = t;
        }

        public int TimeStampMilliseconds { get ; set; }

        public int X { get; set; }
        public int Y { get; set; }

        internal static PointerCoordinates FromString(string line)
        {
            //  [008:20.769] 1440, 578
            Regex re = new Regex(@"(^\[[\d:\.]+\] ) *(\d+) *, *(\d+)");
            var m = re.Match(line);
            if (!m.Success)
                return null;

            var t = SyncViewRepository.GetMilli(m.Groups[1].Value);
            int x = Convert.ToInt32(m.Groups[2].Value);
            int y = Convert.ToInt32(m.Groups[3].Value);
            return new PointerCoordinates(t, x, y);
        }
    }
}