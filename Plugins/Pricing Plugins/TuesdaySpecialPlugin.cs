using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//added statement to reference Business_Objects Library and Datat Layer and other
using Common.Business_Objects;
using Common;
//using GalaxyCinemas.DataLayer;

namespace GalaxyCinemas
{
    public class TuesdaySpecialPlugin : ISpecialPlugin 
    {
        public bool CalculateSpecial(Booking booking, ref string specialName, ref decimal specialPrice)
        {

            DayOfWeek dayOfBooking = booking.SessionDate.DayOfWeek;
            Console.WriteLine(dayOfBooking.ToString());
            //if not tuesday
            if (!(dayOfBooking == DayOfWeek.Tuesday))
            {
                // If not Tuesday, not applicable.
                return false;

            }

            else
            {
                // Get base unit price.
                decimal basePrice = booking.OriginalPrice / booking.Quantity ;
                decimal discountedPrice = 0;

                // Calculate the discounted price that we would offer.
               
                if (booking.Quantity <= 5 )
                    {
                 
                        discountedPrice = booking.Quantity * 11;

                    }
                
                else
                    {
                        discountedPrice = 5 * 11;
                        int basePriceQuantity = booking.Quantity - 5;
                        decimal nonDiscountedPrice = basePriceQuantity * basePrice;
                        discountedPrice += nonDiscountedPrice;
                    
                    
                    }

                //check if it is the best deal
                 if (discountedPrice < booking.OriginalPrice)
                    {
                        // If this discount is applicable, set specialName and specialPrice to our name and price.
                        specialPrice = discountedPrice;
                        specialName = "Tuesday Special";
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
