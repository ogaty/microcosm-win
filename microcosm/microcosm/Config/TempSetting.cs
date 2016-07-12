using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.Config
{
    public class TempSetting
    {
        // 一時的な設定
        // 保存しない
        public enum BandKind
        {
            NATAL = 0,
            PROGRESS = 1,
            TRANSIT = 2,
            SOLAR_RETURN = 10,
            LUNA_RETURN = 11,
            MERCURY_RETURN = 12,
            VENUS_RETURN = 13,
            MARS_RETURN = 14,
            JUPITER_RETURN = 15,
            SATURN_RETURN = 16,
            URANUS_RETURN = 17,
            NEPTUNE_RETURN = 18,
            PLUTO_RETURN = 19
        }
        public enum HouseDivide
        {
            USER1 = 0,
            EVENT1 = 1,
            PROGRESS = 2
        }

        public int bands = 1;
        public BandKind firstBand;
        public BandKind secondBand;
        public BandKind thirdBand;
        public BandKind fourthBand;
        public BandKind fifthBand;
        public BandKind sixthBand;
        public BandKind seventhBand;
        public HouseDivide firstHouseDiv;
        public HouseDivide secondHouseDiv;
        public HouseDivide thirdHouseDiv;
        public HouseDivide fourthHouseDiv;
        public HouseDivide fifthHouseDiv;
        public HouseDivide sixthHouseDiv;
        public HouseDivide seventhHouseDiv;
        public double zodiacCenter;

        public TempSetting(ConfigData config)
        {
            if (config.defaultBands < 1 || config.defaultBands > 7)
            {
                bands = 1;
            } else
            {
                bands = config.defaultBands;
            }
            firstBand = BandKind.NATAL;
            secondBand = BandKind.PROGRESS;
            thirdBand = BandKind.TRANSIT;
            fourthBand = BandKind.TRANSIT;
            fifthBand = BandKind.TRANSIT;
            sixthBand = BandKind.TRANSIT;
            seventhBand = BandKind.TRANSIT;
            firstHouseDiv = HouseDivide.USER1;
            secondHouseDiv = HouseDivide.PROGRESS;
            thirdHouseDiv = HouseDivide.EVENT1;
            fourthHouseDiv = HouseDivide.EVENT1;
            fifthHouseDiv = HouseDivide.EVENT1;
            sixthHouseDiv = HouseDivide.EVENT1;
            seventhHouseDiv = HouseDivide.EVENT1;
        }
    }
}
