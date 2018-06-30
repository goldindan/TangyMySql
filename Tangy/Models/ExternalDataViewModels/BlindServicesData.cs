using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.ExternalDataViewModels
{
    public class BlindServicesData
    {
        public int Id { get; set; }
        public String BlndTrigger { get; set; }//BlndTrigger: גורם מפעיל.
        public String District { get; set; }//District: אזור (לדוגמא: חיפה והצפון, ירושלים והסביבה).
        public String CityName { get; set; }//CityName:יישוב.
        public String BlndPhone { get; set; }//BlndPhone: טלפון.
        public String BlndManager { get; set; }//BlndManager: טלפון מנהל.
        public String BlndFax { get; set; }//BlndFax: פקס.
        public String OpenDays { get; set; }//OpenDays: ימי פתיחה.
        public String OpenHours { get; set; }//OpenHours: שעות פתיחה.
        public String TheService { get; set; }//TheService: סוג השירות.
        public String Address { get; set; }//Address: כתובת.
    }
}
