﻿/*
 * MIT License
 *
 * Copyright(c) 2019 KeLi
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

/*
             ,---------------------------------------------------,              ,---------,
        ,----------------------------------------------------------,          ,"        ,"|
      ,"                                                         ,"|        ,"        ,"  |
     +----------------------------------------------------------+  |      ,"        ,"    |
     |  .----------------------------------------------------.  |  |     +---------+      |
     |  | C:\>FILE -INFO                                     |  |  |     | -==----'|      |
     |  |                                                    |  |  |     |         |      |
     |  |                                                    |  |  |/----|`---=    |      |
     |  |              Author: KeLi                          |  |  |     |         |      |
     |  |              Email: kelistudy@163.com              |  |  |     |         |      |
     |  |              Creation Time: 10/30/2019 07:08:41 PM |  |  |     |         |      |
     |  | C:\>_                                              |  |  |     | -==----'|      |
     |  |                                                    |  |  |   ,/|==== ooo |      ;
     |  |                                                    |  |  |  // |(((( [66]|    ,"
     |  `----------------------------------------------------'  |," .;'| |((((     |  ,"
     +----------------------------------------------------------+  ;;  | |         |,"
        /_)_________________________________________________(_/  //'   | +---------+
           ___________________________/___  `,
          /  oooooooooooooooo  .o.  oooo /,   \,"-----------
         / ==ooooooooooooooo==.o.  ooo= //   ,`\--{)B     ,"
        /_==__==========__==_ooo__ooo=_/'   /___________,"
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace KeLi.Common.Revit.Relations
{
    /// <summary>
    /// About a point and a plane relationship.
    /// </summary>
    public static class PointPlaneRelation
    {
        /// <summary>
        /// Gets the result of whether the point is in the plane direction polygon.
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static bool InPlanePolygon(this XYZ pt, List<Line> polygon)
        {
            if (pt == null)
                throw new ArgumentNullException(nameof(pt));

            if (polygon == null)
                throw new ArgumentNullException(nameof(polygon));

            var x = pt.X;
            var y = pt.Y;
            var xs = new List<double>();
            var ys = new List<double>();

            foreach (var line in polygon)
            {
                xs.Add(line.GetEndPoint(0).X);
                ys.Add(line.GetEndPoint(0).Y);
            }

            var minX = xs.Min();
            var maxX = xs.Max();
            var minY = ys.Min();
            var maxY = ys.Max();

            if (polygon.Count == 0 || x < minX || x > maxX || y < minY || y > maxY)
                return false;

            var result = false;

            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                var dxji = xs[j] - xs[i];
                var dyji = ys[j] - ys[i];

                if (ys[i] > y != ys[j] > y && x < dxji * (y - ys[i]) / dyji + xs[i])
                    result = !result;
            }

            return result;
        }

        /// <summary>
        /// Gets the result of whether the point is in the space direction polygon.
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static bool InSpacePolygon(this XYZ pt, List<Line> polygon)
        {
            if (pt == null)
                throw new ArgumentNullException(nameof(pt));

            if (polygon == null)
                throw new ArgumentNullException(nameof(polygon));

            throw new NotImplementedException();
        }
    }
}