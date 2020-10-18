using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using Task.Main;
using System.Threading;

namespace Task.Tests
{
    [TestClass]
    public abstract class BaseTest
    {
        [TestInitialize]
        public void Setup()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(Constants.DefaultCulture);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Constants.DefaultCulture);
        }
    }
}
