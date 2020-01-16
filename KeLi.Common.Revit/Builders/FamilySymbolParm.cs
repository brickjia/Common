﻿
/*
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
     |  |              Creation Time: 01/15/2020 08:05:20 PM |  |  |     |         |      |
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

using Autodesk.Revit.DB;
using System;

namespace KeLi.Common.Revit.Builders
{
    /// <summary>
    /// Family symbol parmater.
    /// </summary>
    public class FamilySymbolParm
    {
        /// <summary>
        /// Family symbol parmater.
        /// </summary>
        /// <param name="templateFileName"></param>
        /// <param name="profile"></param>
        /// <param name="plane"></param>
        /// <param name="end"></param>
        public FamilySymbolParm(string templateFileName, CurveArrArray profile, Plane plane, double end)
        {
            TemplateFileName = templateFileName ?? throw new ArgumentNullException(nameof(templateFileName));
            ExtrusionProfile = profile ?? throw new ArgumentNullException(nameof(profile));
            Plane = plane ?? throw new ArgumentNullException(nameof(plane));
            End = end;
        }

        /// <summary>
        /// Family symbol parmater.
        /// </summary>
        /// <param name="templateFileName"></param>
        /// <param name="profile"></param>
        /// <param name="path"></param>
        /// <param name="index"></param>
        public FamilySymbolParm(string templateFileName, SweepProfile profile, ReferenceArray path, int index)
        {
            TemplateFileName = templateFileName ?? throw new ArgumentNullException(nameof(templateFileName));
            SweepProfile = profile ?? throw new ArgumentNullException(nameof(profile));
            SweepPath = path ?? throw new ArgumentNullException(nameof(path));
            Index = index;
        }

        /// <summary>
        /// The family symbol's template file name including file suffix.
        /// </summary>
        public string TemplateFileName { get; }

        /// <summary>
        /// The extrusion symbol's profile.
        /// </summary>
        public CurveArrArray ExtrusionProfile { get; }

        /// <summary>
        /// The family symbol's reference plane.
        /// </summary>
        public Plane Plane { get; }

        /// <summary>
        /// The family symbol's end length.
        /// </summary>
        public double End { get; }

        /// <summary>
        /// The sweep symbol's profile.
        /// </summary>
        public SweepProfile SweepProfile { get; set; }

        /// <summary>
        /// The sweep symbol's path.
        /// </summary>
        public ReferenceArray SweepPath { get; set; }

        /// <summary>
        /// The sweep symbol's index.
        /// </summary>
        public int Index { get; set; }
    }
}