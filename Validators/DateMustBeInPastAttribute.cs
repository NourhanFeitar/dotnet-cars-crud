using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace API_Lab_1.Validators
{
    public class DateMustBeInPastAttribute:ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is DateTime ProductionDate && ProductionDate < DateTime.Now;
        }
       
        
        //public override bool IsValid(object? value)
        //{
        //    DateTime? ProductionDate=value as DateTime?;
        //    //i couldnt cast it into datetime
        //    if(ProductionDate is null)
        //    {
        //        return false;
        //    }
        //    if(ProductionDate <DateTime.Now)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
