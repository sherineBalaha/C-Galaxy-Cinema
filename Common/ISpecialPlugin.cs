using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//added statement to reference Business_Objects Library
using Common.Business_Objects;


namespace Common
{
    public interface ISpecialPlugin
    {
       //check
        bool CalculateSpecial(Booking booking, ref string specialName, ref decimal specialPrice);

       

        //NO CODE IS ALLOWED IN INTERFACE
        //{
        //    // If not Tuesday, not applicable.




        //    // Get base unit price.
        //    decimal basePrice;

        //    // Calculate the discounted price that we would offer.




        //    // If this discount is applicable, set specialName and specialPrice to our name and price.

        //}

    }
}
