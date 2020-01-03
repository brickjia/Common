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

using System.IO;

namespace KeLi.Common.Drive.Excel
{
    /// <summary>
    /// Excel param.
    /// </summary>
    public class ExcelParam
    {
        /// <summary>
        /// This is a excel default sheet name.
        /// </summary>
        private const string SHEET_NAME = "Sheet1";

        /// <summary>
        /// Excel param.
        /// </summary>
        /// <param name="filePath"></param>
        public ExcelParam(FileInfo filePath)
        {
            FilePath = filePath;
            TemplatePath = filePath;
            SheetName = SHEET_NAME;
            RowIndex = 1;
            ColumnIndex = 0;
        }

        /// <summary>
        /// Excel param.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="templatePath"></param>
        public ExcelParam(FileInfo filePath, FileInfo templatePath)
        {
            FilePath = filePath;
            TemplatePath = templatePath;
            SheetName = SHEET_NAME;
            RowIndex = 1;
            ColumnIndex = 0;

            File.Copy(templatePath.FullName, filePath.FullName, true);
        }

        /// <summary>
        /// The template path.
        /// </summary>
        public FileInfo TemplatePath { get; set; }

        /// <summary>
        /// The file path.
        /// </summary>
        public FileInfo FilePath { get; set; }

        /// <summary>
        /// The sheet name.
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// The start row index.
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// The start column index.
        /// </summary>
        public int ColumnIndex { get; set; }
    }
}