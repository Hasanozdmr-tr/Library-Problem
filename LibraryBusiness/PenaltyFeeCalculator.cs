using System;
using System.Collections.Generic;
using System.Text;
using LibraryConfigUtilities;

namespace LibraryBusiness
{
    /* Description,
     * settingList member holds configuration parameters stored in the App.config file, 
     * please explore the properties and methods in the Country class to get a better understanding.
     * 
     * Please implement this class accordingly to accomplish requirements.
     * Feel free to add any parameters, methods, class members, etc. if necessary
     */
    public class PenaltyFeeCalculator
    {

        private List<Country> settingList = new LibrarySetting().LibrarySettingList;

        public PenaltyFeeCalculator()
        {

        }
        public String CalculateFeeAmount(int totalDays, int penaltyAppliesAfter, decimal dailyPenaltyFee, string currency)
        {
            if (totalDays > penaltyAppliesAfter)
            {
                decimal totalPenalty = (totalDays - penaltyAppliesAfter) * dailyPenaltyFee;
                return totalPenalty + " " + currency;
            }
            return "0";
        }
        public String Calculate(string countryCode, DateTime startDate, DateTime endDate)
        {

            {
                Country country = settingList.Find(x => x.CountryCode == countryCode);
                if (country != null)
                {
                    DateTime indexDate = startDate;
                    int totalDays = 0;
                    while (indexDate <= endDate)
                    {
                        if (!country.WeekendList.Contains(indexDate.DayOfWeek) && !country.HolidayList.Contains(indexDate))
                        {
                            totalDays++;
                        }
                        indexDate = indexDate.AddDays(1);
                    }

                    return CalculateFeeAmount(totalDays, country.PenaltyAppliesAfter, country.DailyPenaltyFee, country.Currency);
                }
                return "CountryCode bulunamadý";

            }
        }
    }
}
