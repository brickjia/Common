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
     |  |              Creation Time: 10/30/2019 05:38:41 PM |  |  |     |         |      |
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
using System.Reflection;
using Autodesk.Revit;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.RevitAddIns;
using KeLi.Common.Tool.Other;

namespace KeLi.Common.Revit.Widgets
{
    /// <summary>
    /// Revit context.
    /// </summary>
    public class RevitContext: IDisposable
    {
        /// <summary>
        /// Revit version number.
        /// </summary>
        private static int _versionNum;

        /// <summary>
        /// Revit product.
        /// </summary>
        private static Product _product;

        /// <summary>
        /// Cannot build an instance.
        /// </summary>
        private RevitContext()
        {

        }

        /// <summary>
        /// Gets revit application.
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="versionNum"></param>
        /// <returns></returns>
        public Application GetApplication(string clientName, int versionNum)
        {
            _versionNum = versionNum;

            var context = SingletonFactory<RevitContext>.CreateInstance();

            Init(clientName);

            return context.GetApplication(clientName, versionNum);
        }

        /// <summary>
        /// Initializes revit context.
        /// </summary>
        private static void Init(string clientName)
        {
            SetEnvironmentVariable();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            var clientId = new ClientApplicationId(Guid.NewGuid(), clientName, "ADSK");

            // The string must be this 'I am authorized by Autodesk to use this UI-less functionality.'.
            _product.Init(clientId, "I am authorized by Autodesk to use this UI-less functionality.");

            _product = Product.GetInstalledProduct();
        }

        /// <summary>
        /// Gets revit install path by version number.
        /// </summary>
        /// <returns></returns>
        private static string GetRevitInstallPath()
        {
            var products = RevitProductUtility.GetAllInstalledRevitProducts();
            var product = products.FirstOrDefault(f => f.Name.Contains(_versionNum.ToString()));

            if (product == null)
                throw  new ArgumentException(nameof(_versionNum));

            return product.InstallLocation;
        }

        /// <summary>
        /// Sets environment veriable path.
        /// </summary>
        private static void SetEnvironmentVariable()
        {
            var revitPath = new[] { GetRevitInstallPath()};
            var path = new[] { Environment.GetEnvironmentVariable("PATH") ?? string.Empty };
            var newPath = string.Join(Path.PathSeparator.ToString(), path.Concat(revitPath));

            Environment.SetEnvironmentVariable("PATH", newPath);
        }

        /// <summary>
        /// Loads dependent dlls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name);

            var file = $"{Path.Combine(GetRevitInstallPath(), assemblyName.Name)}.dll";

            return File.Exists(file) ? Assembly.LoadFile(file) : null;
        }

        /// <summary>
        /// Disponses the revit.
        /// </summary>
        public void Dispose()
        {
            _product?.Exit();
        }
    }
}