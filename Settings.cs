using Resort.Types;
using Resort.Types.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resort
{
    static class Settings
    {
        public static UnitValue startMoney = new UnitValue(100_000, Credits.Instance);
        public static UnitValue targetMoney = new UnitValue(1_000_000, Credits.Instance);
        
        public static int StartAmountHotels = 1;
        public static int StartAmountRestaurants = 1;
        public static int StartAmountNoviceTrack = 1;
        public static int StartAmountCommonTrack = 0;
        public static int StartAmountProTrack = 0;
        public static int StartAmountChairlift = 1;
        public static int StartAmountCabinlift = 0;

        public static int RentAmountNovice = 2_000;
        public static int RentAmountPro = 6_000;

        public static int BuildsPerTurn = 2;






    }
}
