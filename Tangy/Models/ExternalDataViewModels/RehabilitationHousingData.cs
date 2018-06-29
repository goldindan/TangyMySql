using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.ExternalDataViewModels
{
    public class RehabilitationHousingData
    {
        public int Id { get; set; }
        public String GIshuvName { get; set; }//GIshuvName: שם יישוב.
        public String AgeGroup { get; set; }//AgeGroup: גיל האוכלוסייה (לדוגמא: בוגרים, בני נוער, מזדקנים).
        public String RehHousingSector { get; set; }//RehHousingSector:מגזר(לדוגמא: ממלכתי, מוסלמי, חרדי) .
        public String DisabilityType { get; set; }//DisabilityType: סוג הנכות(לדוגמא: נכות פיזית).
        public String ManagerName { get; set; }//ManagerName: שם מנהל.
        public String District { get; set; }//District: אזור (לדוגמא: חיפה והצפון, ירושלים והסביבה).
        public String Email { get; set; }//Email: דואר אלקטרוני.
        public String ArrivalHelp { get; set; }//ArrivalHelp: הוראות הגעה.
        public String AgeDetail { get; set; }//AgeDetail: פירוט טקסטואלי של גילאי היעד.
        public String Phone { get; set; }//Phone: טלפון.
        public String Mobile { get; set; }//Mobile: טלפון סלולרי.
        public String InstitutionName { get; set; }//InstitutionName: שם המסגרת.
        public String Address { get; set; }//Address: כתובת.
        public String InstitutionType { get; set; }//InstitutionType: סוג המסגרת (לדוגמא: דירה טיפולית).
        public String InstitutionTypeDetails { get; set; }//InstitutionTypeDetails: פרטים על המסגרת, כתוב ב-html
        public String GIshuvNaPurposeme { get; set; }//Purpose: מטרות המגורים במסגרת, טקסט html
        public String MoreInformation { get; set; }//MoreInformation : מידע נוסף, טקסט html.
        public String MaxResidentsNumber { get; set; }//MaxResidentsNumber: מספר דיירים מקסימלי.
        public String DisabilityTypeDetail { get; set; }//DisabilityTypeDetail: פרטים על הנכות.
        public String Code { get; set; }//Code: סמלי המסגרת.
        public String TariffCode { get; set; }//TariffCode: סמלי התעריף.
        public String ServicesDetails { get; set; }//ServicesDetails: פירוט השירותים הניתנים במסגרת.
        public String AcceptanceProcess { get; set; }//AcceptanceProcess: תנאי קבלה למסגרת.
    }
}
