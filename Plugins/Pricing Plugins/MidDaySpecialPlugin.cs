using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//added statement to reference Business_Objects Library and Datat Layer and other
using Common.Business_Objects;
using Common;


namespace GalaxyCinemas
{
    public class MidDaySpecialPlugin : ISpecialPlugin
    {
        public bool CalculateSpecial(Booking booking, ref string specialName, ref decimal specialPrice)
        {
            TimeSpan timeOfDay = booking.SessionDate.TimeOfDay;
            TimeSpan startTimeSpan = new TimeSpan(11, 0, 0);
            TimeSpan endTimeSpan = new TimeSpan(13, 0, 0);


            if (!(timeOfDay.CompareTo(startTimeSpan) == 1) && (timeOfDay.CompareTo(endTimeSpan) == -1))
            {
                // If not mid-day, not applicable.
                // If movie doesn't start between 11am and 1pm
                return false;

            }

            else
            {

                // Calculate the discounted price that we would offer.
                decimal discountPercent = 0.20m;
                decimal discountedPrice = booking.OriginalPrice - (booking.OriginalPrice * discountPercent);

                // If this discount is applicable, set specialName and specialPrice to our name and price.
                if (discountedPrice < booking.OriginalPrice)
                {
                    specialPrice = discountedPrice;
                    specialName = "Mid-day Special";
                    return true;

                }
                else
                {
                    return false;
                }



            }
        }
    }
}
