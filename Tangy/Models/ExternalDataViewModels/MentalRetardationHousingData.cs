using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.ExternalDataViewModels
{
    public class MentalRetardationHousingData
    {
        public int Id { get; set; }
        public String BoardingHomeType { get; set; }//BoardingHomeType: סוג בעלות של המסגרת(לדוגמא: פרטי, ציבורי).
        public String HousingType { get; set; }//HousingType: סוג המסגרת(לדוגמא: מעון פנימייה, דיור תומך).
        public String Code { get; set; }//Code: קוד מסגרת.
        public String ResidentsProperties { get; set; }//ResidentsProperties: מאפייני הדיירים (לדוגמא: לא סיעודיים, משולבים בקהילה, רמת המוגבלות).
        public String District { get; set; }//District: אזור(לדוגמא: חיפה והצפון, ירושלים והסביבה).
        public String CityName { get; set; }//CityName: יישוב.
        public String ManagerName { get; set; }//ManagerName: שם מנהל.
        public String AnotherContact { get; set; }//AnotherContact: פרטי קשר נוספים.
        public String Age { get; set; }//Age: גיל הדיירים (לדוגמא: צעירים, מבוגרים).
        public String Comments { get; set; }//Comments: הערות נוספות.
        public String Mobile { get; set; }//Mobile: טלפון סלולרי.
        public String BoardingHomeBranches { get; set; }//BoardingHomeBranches : סוגי סניפים נוספים (לדוגמא: מעון שמפעיל גם הוסטל).
        public String TenantsStandard { get; set; }//TenantsStandard: מספר הדיירים שהתקן מאפשר.
        public String Email { get; set; }//Email: דואר אלקטרוני.
        public String Phone { get; set; }//Phone: טלפון.
        public String Address { get; set; }//Address: כתובת.
        public String Sector { get; set; }//Sector: מיועד למגזר (לדוגמא: מוסלמי, חרדי) .רוב המסגרות מיועדות לכל המגזרים.
        public String Zip { get; set; }//Zip: מיקוד.
        public String Link { get; set; }//Link: קישור לכרטיס הביקור באתר האינטרנט של משרד העבודה, הרווחה והשירותים החברתיים.
    }
}
