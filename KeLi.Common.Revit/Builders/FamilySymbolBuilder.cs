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
     |  |              Creation Time: 01/15/2020 07:39:20 PM |  |  |     |         |      |
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
using System.IO;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using KeLi.Common.Revit.Relations;
using KeLi.Common.Revit.Widgets;

namespace KeLi.Common.Revit.Builders
{
    /// <summary>
    ///     FamilySymbol builder.
    /// </summary>
    public static class FamilySymbolBuilder
    {
        /// <summary>
        ///     Creates a new extrusion symbol.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="app"></param>
        /// <param name="parm"></param>
        /// <param name="rfaPath"></param>
        /// <returns></returns>
        public static FamilySymbol CreateExtrusionSymbol(this Document doc, Application app, FamilySymbolParameter parm,
            string rfaPath = null)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));

            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (parm == null)
                throw new ArgumentNullException(nameof(parm));

            var templateFilePath = app.GeTemplateFilePath(parm.TemplateFileName);
            var fdoc = app.NewFamilyDocument(templateFilePath);

            fdoc.AutoTransaction(() =>
            {
                var profile = ResetCurveArrArray(parm.Profile);
                var skectchPlane = fdoc.CreateSketchPlane(parm.Plane);

                fdoc.FamilyCreate.NewExtrusion(true, profile, skectchPlane, parm.End);
            });

            return doc.GetFamilySymbol(fdoc, rfaPath);
        }

        /// <summary>
        ///     Creates a new sweep symbol.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="app"></param>
        /// <param name="parm"></param>
        /// <param name="rfaPath"></param>
        /// <returns></returns>
        public static FamilySymbol CreateSweepSymbol(this Document doc, Application app, FamilySymbolParameter parm,
            string rfaPath = null)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));

            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (parm == null)
                throw new ArgumentNullException(nameof(parm));

            var templateFilePath = app.GeTemplateFilePath(parm.TemplateFileName);
            var fdoc = app.NewFamilyDocument(templateFilePath);

            fdoc.AutoTransaction(() =>
            {
                var profileArrArray = ResetCurveArrArray(parm.Profile);
                var profile = app.Create.NewCurveLoopsProfile(profileArrArray);

                fdoc.FamilyCreate.NewSweep(true, parm.SweepPath, profile, parm.Index, parm.Location);
            });

            return doc.GetFamilySymbol(fdoc, rfaPath);
        }

        /// <summary>
        ///     Creates a new family symbol.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="rfaPath"></param>
        /// <returns></returns>
        public static FamilySymbol CreateFamilySymbol(this Document doc, string rfaPath)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));

            if (rfaPath == null)
                throw new ArgumentNullException(nameof(rfaPath));

            doc.LoadFamily(rfaPath, out var family);

            return doc.GetFamilySymbol(family);
        }

        /// <summary>
        ///     Creates a new family symbol.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="app"></param>
        /// <param name="tplFileName"></param>
        /// <param name="rfaPath"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        public static FamilySymbol CreateFamilySymbol(this Document doc, Application app, string tplFileName,
            string rfaPath, Action<Document> act)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));

            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (tplFileName == null)
                throw new ArgumentNullException(nameof(tplFileName));

            if (rfaPath == null)
                throw new ArgumentNullException(nameof(rfaPath));

            if (act == null)
                throw new ArgumentNullException(nameof(act));

            var templateFilePath = app.GeTemplateFilePath(tplFileName);
            var fdoc = app.NewFamilyDocument(templateFilePath);

            fdoc.AutoTransaction(() => act.Invoke(fdoc));

            return doc.GetFamilySymbol(fdoc, rfaPath);
        }

        /// <summary>
        ///     Gets the first family symbol from family document.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="fdoc"></param>
        /// <param name="rfaPath"></param>
        /// <returns></returns>
        public static FamilySymbol GetFamilySymbol(this Document doc, Document fdoc, string rfaPath = null)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));

            if (fdoc == null)
                throw new ArgumentNullException(nameof(fdoc));

            var family = fdoc.LoadFamily(doc);

            if (rfaPath != null)
                fdoc.CloseUnsavedFile(rfaPath);

            return doc.GetFamilySymbol(family);
        }

        /// <summary>
        ///     Gets the first family symbol from family document.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        public static FamilySymbol GetFamilySymbol(this Document doc, Family family)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));

            if (family == null)
                throw new ArgumentNullException(nameof(family));

            var symbolId = family.GetFamilySymbolIds().FirstOrDefault();
            var result = doc.GetElement(symbolId) as FamilySymbol;

            doc.AutoTransaction(() =>
            {
                if (result != null && !result.IsActive)
                    result.Activate();
            });

            return result;
        }

        /// <summary>
        ///     Gets the template file path.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GeTemplateFilePath(this Application app, string fileName)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            return Path.Combine(app.FamilyTemplatePath, fileName);
        }

        /// <summary>
        ///     Resets the family symbol profile's location to zero point.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private static CurveArrArray ResetCurveArrArray(CurveArrArray profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));

            var results = new CurveArrArray();
            var location = GetLocationPoint(profile);

            foreach (CurveArray lines in profile)
            {
                var tmpLines = new CurveArray();

                foreach (var line in lines.Cast<Line>())
                {
                    var pt1 = line.GetEndPoint(0) - location;
                    var pt2 = line.GetEndPoint(1) - location;
                    var newLine = Line.CreateBound(pt1, pt2);

                    tmpLines.Append(newLine);
                }

                results.Append(tmpLines);
            }

            return results;
        }

        /// <summary>
        ///     Gets the family symbol's raw location, it's for moving zero point.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private static XYZ GetLocationPoint(CurveArrArray profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));

            var curves = profile.Cast<CurveArray>().SelectMany(s => s.Cast<Curve>()).ToList();
            var pts = curves.GetDistinctPointList()
                .OrderBy(o => o.Z).ThenBy(o => o.Y).ThenBy(o => o.X).ToList();

            return pts.FirstOrDefault();
        }
    }
}