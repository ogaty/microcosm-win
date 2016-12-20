using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using microcosm;
using microcosm.Calc;
using microcosm.Config;
using microcosm.Planet;
using microcosm.Common;

namespace microcosmtest
{
    [TestClass]
    public class AstrocalcTest
    {
        [TestMethod]
        public void TestAscMc()
        {
            ConfigData config = new ConfigData();
            AstroCalc calc = new AstroCalc(config);
            Dictionary<int, PlanetData> pdata = new Dictionary<int, PlanetData>();
            double[] houses = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            SettingData setting = new SettingData(0);
            Dictionary<int, bool> disp = new Dictionary<int, bool>();
            disp[CommonData.ZODIAC_ASC] = true;
            disp[CommonData.ZODIAC_MC] = false;

            setting.dispPlanet = new List<Dictionary<int, bool>>();
            setting.dispPlanet.Add(disp);
            setting.dispAspectPlanet = new List<Dictionary<int, bool>>();
            setting.dispAspectPlanet.Add(disp);

            pdata = calc.setHouse(pdata, houses, setting, 0);

            Assert.AreEqual(1, pdata[CommonData.ZODIAC_ASC].absolute_position);
            Assert.IsTrue(pdata[CommonData.ZODIAC_ASC].isDisp);
            Assert.IsTrue(pdata[CommonData.ZODIAC_ASC].isAspectDisp);
            Assert.AreEqual(10, pdata[CommonData.ZODIAC_MC].absolute_position);
            Assert.IsFalse(pdata[CommonData.ZODIAC_MC].isDisp);
            Assert.IsFalse(pdata[CommonData.ZODIAC_MC].isAspectDisp);
        }
    }
}
