using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.ExternalDataViewModels
{
    public class PrescriptionDrugsPriceListData
    {
        public int ID { get; set; }

        [Display(Name ="קוד")]
        public String Code { get; set; }

        [Display(Name ="שם תכשיר")]
        public String DrugName { get; set; }

        [Display(Name = "גודל אריזה")]
        public String PackageSize { get; set; }

        [Display(Name = "מחיר מרבי לקימעונאי")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public double MaximumRetailprice { get; set; }

        [Display(Name = "מרווח מרבי לקמעונאי")]
        public String MaximumRetailMargin { get; set; }

        [Display(Name = "מחיר מרבי לצרכן")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double MaximumConsumerPrice { get; set; }

        [Display(Name = "מחיר מרבי לצרכן כולל מעמ")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double MaximumConsumerPriceIncludingVAT { get; set; }

        [Display(Name = "קוד ירפא")]
        public String CodeWillHeal { get; set; }

        [Display(Name = "קוד פארמהסופט")]
        public String CodePharmaSoft { get; set; }
    }
}
