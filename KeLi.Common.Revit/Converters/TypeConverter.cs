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

namespace KeLi.Common.Revit.Converters
{
    /// <summary>
    ///     Type converter.
    /// </summary>
    public static class TypeConverter
    {
        /// <summary>
        ///     Convers the space Curve to the plane Line.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static Line ToPlaneLine(this Curve line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            var p1 = line.GetEndPoint(0).ToPlanePoint();
            var p2 = line.GetEndPoint(1).ToPlanePoint();

            return Line.CreateBound(p1, p2);
        }

        /// <summary>
        ///     Convers the space XYZ to the plane XYZ.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static XYZ ToPlanePoint(this XYZ pt)
        {
            if (pt == null)
                throw new ArgumentNullException(nameof(pt));

            return new XYZ(pt.X, pt.Y, 0);
        }

        /// <summary>
        ///     Converts the Reference set to the ReferenceArray.
        /// </summary>
        /// <param name="refs"></param>
        /// <returns></returns>
        public static ReferenceArray ToReferArray(this IEnumerable<Reference> refs)
        {
            if (refs == null)
                throw new ArgumentNullException(nameof(refs));

            var results = new ReferenceArray();

            foreach (var refer in refs)
                results.Append(refer);

            return results;
        }

        /// <summary>
        ///     Converts the ReferenceArray to the Reference list.
        /// </summary>
        /// <param name="refs"></param>
        /// <returns></returns>
        public static List<Reference> ToReferArray(this ReferenceArray refs)
        {
            if (refs == null)
                throw new ArgumentNullException(nameof(refs));

            var results = new List<Reference>();

            foreach (Reference refer in refs)
                results.Add(refer);

            return results;
        }

        /// <summary>
        ///     Converts the CurveLoop list to the CurveArrArray.
        /// </summary>
        /// <param name="curveLoops"></param>
        /// <returns></returns>
        public static CurveArrArray ToCurveArrArray(this IEnumerable<CurveLoop> curveLoops)
        {
            if (curveLoops == null)
                throw new ArgumentNullException(nameof(curveLoops));

            var results = new CurveArrArray();

            foreach (var curveLoop in curveLoops)
                results.Append(curveLoop.ToCurveArray());

            return results;
        }

        /// <summary>
        ///     Converts the CurveLoop list to the CurveArray list.
        /// </summary>
        /// <param name="curveLoops"></param>
        /// <returns></returns>
        public static List<CurveArray> ToCurveArrayList(this IEnumerable<CurveLoop> curveLoops)
        {
            if (curveLoops == null)
                throw new ArgumentNullException(nameof(curveLoops));

            return curveLoops.Select(s => s.ToCurveArray()).ToList();
        }

        /// <summary>
        ///     Converts the CurveLoop list to the Curve list.
        /// </summary>
        /// <param name="curveLoops"></param>
        /// <returns></returns>
        public static List<Curve> ToCurveList(this IEnumerable<CurveLoop> curveLoops)
        {
            if (curveLoops == null)
                throw new ArgumentNullException(nameof(curveLoops));

            return curveLoops.SelectMany(s => s).ToList();
        }

        /// <summary>
        ///     Converts the CurveArrArray to the CurveLoop list.
        /// </summary>
        /// <param name="curveArrArray"></param>
        /// <returns></returns>
        public static List<CurveLoop> ToCurveLoopList(this CurveArrArray curveArrArray)
        {
            if (curveArrArray == null)
                throw new ArgumentNullException(nameof(curveArrArray));

            var results = new List<CurveLoop>();

            foreach (CurveArray curves in curveArrArray)
                results.Add(curves.ToCurveLoop());

            return results;
        }

        /// <summary>
        ///     Converts the CurveArrArray to the CurveArray list.
        /// </summary>
        /// <param name="curveArrArray"></param>
        /// <returns></returns>
        public static List<CurveArray> ToCurveArrayList(this CurveArrArray curveArrArray)
        {
            if (curveArrArray == null)
                throw new ArgumentNullException(nameof(curveArrArray));

            var results = new List<CurveArray>();

            foreach (CurveArray curves in curveArrArray)
                results.Add(curves);

            return results;
        }

        /// <summary>
        ///     Converts the CurveArrArray to the Curve list.
        /// </summary>
        /// <param name="curveArrArray"></param>
        /// <returns></returns>
        public static List<Curve> ToCurveList(this CurveArrArray curveArrArray)
        {
            if (curveArrArray == null)
                throw new ArgumentNullException(nameof(curveArrArray));

            var results = new List<Curve>();

            foreach (CurveArray curves in curveArrArray)
                results.AddRange(curves.ToCurveList());

            return results;
        }

        /// <summary>
        ///     Converts the CurveArray list to the CurveArrArray.
        /// </summary>
        /// <param name="curveArrays"></param>
        /// <returns></returns>
        public static CurveArrArray ToCurveArrArray(this IEnumerable<CurveArray> curveArrays)
        {
            if (curveArrays == null)
                throw new ArgumentNullException(nameof(curveArrays));

            var results = new CurveArrArray();

            foreach (var curves in curveArrays)
                results.Append(curves);

            return results;
        }

        /// <summary>
        ///     Converts the CurveLoop to the CurveArray.
        /// </summary>
        /// <param name="curveLoop"></param>
        /// <returns></returns>
        public static CurveArray ToCurveArray(this CurveLoop curveLoop)
        {
            if (curveLoop == null)
                throw new ArgumentNullException(nameof(curveLoop));

            var results = new CurveArray();

            foreach (var curve in curveLoop)
                results.Append(curve);

            return results;
        }

        /// <summary>
        ///     Converts the CurveLoop to the Curve list.
        /// </summary>
        /// <param name="curveLoop"></param>
        /// <returns></returns>
        public static List<Curve> ToCurveList(this CurveLoop curveLoop)
        {
            if (curveLoop == null)
                throw new ArgumentNullException(nameof(curveLoop));

            var results = new List<Curve>();

            foreach (var curve in curveLoop)
                results.Add(curve);

            return results;
        }

        /// <summary>
        ///     Converts the CurveArray to the CurveLoop.
        /// </summary>
        /// <param name="curveArray"></param>
        /// <returns></returns>
        public static CurveLoop ToCurveLoop(this CurveArray curveArray)
        {
            if (curveArray == null)
                throw new ArgumentNullException(nameof(curveArray));

            var results = new CurveLoop();

            foreach (Curve curve in curveArray)
                results.Append(curve);

            return results;
        }

        /// <summary>
        ///     Converts the CurveArray to the Curve list.
        /// </summary>
        /// <param name="curveArray"></param>
        /// <returns></returns>
        public static List<Curve> ToCurveList(this CurveArray curveArray)
        {
            if (curveArray == null)
                throw new ArgumentNullException(nameof(curveArray));

            var results = new List<Curve>();

            foreach (Curve curve in curveArray)
                results.Add(curve);

            return results;
        }

        /// <summary>
        ///     Converts the Curve list to the CurveArray.
        /// </summary>
        /// <param name="curves"></param>
        /// <returns></returns>
        public static CurveArray ToCurveArray(this IEnumerable<Curve> curves)
        {
            if (curves == null)
                throw new ArgumentNullException(nameof(curves));

            var results = new CurveArray();

            foreach (var curve in curves)
                results.Append(curve);

            return results;
        }

        /// <summary>
        ///     Converts the Curve list to the CurveLoop.
        /// </summary>
        /// <param name="curves"></param>
        /// <returns></returns>
        public static CurveLoop ToCurveLoop(this IEnumerable<Curve> curves)
        {
            if (curves == null)
                throw new ArgumentNullException(nameof(curves));

            var results = new CurveLoop();

            foreach (var curve in curves)
                results.Append(curve);

            return results;
        }

        /// <summary>
        ///     Converts the Face list to the FaceArray.
        /// </summary>
        /// <param name="faces"></param>
        /// <returns></returns>
        public static FaceArray ToFaceArray(this IEnumerable<Face> faces)
        {
            if (faces == null)
                throw new ArgumentNullException(nameof(faces));

            var results = new FaceArray();

            foreach (var face in faces)
                results.Append(face);

            return results;
        }

        /// <summary>
        ///     Converts the FaceArray to the Face list.
        /// </summary>
        /// <param name="faces"></param>
        /// <returns></returns>
        public static List<Face> ToFaceList(this FaceArray faces)
        {
            if (faces == null)
                throw new ArgumentNullException(nameof(faces));

            var results = new List<Face>();

            foreach (Face face in faces)
                results.Add(face);

            return results;
        }
    }
}