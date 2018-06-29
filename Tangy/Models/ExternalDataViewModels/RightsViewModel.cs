using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.ExternalDataViewModels
{
    public class RightsViewModel
    {
        public int Id { get; set; }

        public String Right { get; set; }

        public String Subject { get; set; }

        public String RightsType { get; set; }

        public String Populations { get; set; }

        public String ResponsibleEntity { get; set; }

        public String RightsEligibilityDetails { get; set; }

        public String Documents { get; set; }
        //Right: שם הזכות(לדוגמא: קצבת נכות כללית, מימון אביזרי עזר, שיקום תעסוקתי).
        //Subject: תחום הזכאות(לדוגמא: תחבורה, מגורים).
        //RightsType: אופי הזכאות(הצורה בה מקבלים את הזכאות, לדוגמא: החזר כספי, סבסוד, קצבה).
        //Populations: אוכלוסייה זכאית, או אוכלוסיה שהיא שילוב של מספר אוכלוסיות(לדוגמא: חירשים וכבדי שמיעה, חירשים עיוורים, עיוורים חולי סרטן).
        //ResponsibleEntity: הגורם האחראי למתן הזכאות(לדוגמא: האגודה למלחמה בסרטן, משרד הבינוי, משרד הבריאות).
        //RightsEligibilityDetails: פירוט הזכאות בקוד html.
        //Documents: קישורים למסמכים רלוונטיים בקוד html.
    }
}
