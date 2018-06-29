using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Tangy.Models.ExternalDataViewModels;

namespace Tangy.Controllers
{
    public class ExternalDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Rights()
        {
            string url = "http://www.molsa.gov.il/Open/Rights/Documents/Rights.xml";
            byte[] data;
            using (WebClient webClient = new WebClient())
                data = webClient.DownloadData(url);

            string str = Encoding.GetEncoding("utf-8").GetString(data);
            XElement doc = XElement.Parse(str);

            IEnumerable<XElement> childList =
                from el in doc.Elements()
                select el;

            XNamespace ns = "http://Molsa.Moss.MisradHarevacha.Rights";

            List<RightsViewModel> rightsViewModels = new List<RightsViewModel>();

            int indexer = 0;

            foreach (XElement e in childList)
            {
                indexer++;
                RightsViewModel rightsViewModel = new RightsViewModel()
                {
                    Id = indexer,
                    Right = e.Element(ns + "Right").Value,
                    Subject = e.Element(ns + "Subject").Value,
                    Documents = e.Element(ns + "Documents").Value,
                    ResponsibleEntity = e.Element(ns + "ResponsibleEntity").Value,
                    Populations = e.Element(ns + "Populations").Value,
                    RightsEligibilityDetails = e.Element(ns + "RightsEligibilityDetails").Value,
                    RightsType = e.Element(ns + "RightsType").Value
                };
                rightsViewModels.Add(rightsViewModel);
            }

            return View(rightsViewModels);
        }

        public IActionResult MentalRetardationHousingData()
        {
            string url = "http://www.molsa.gov.il/Open//MentalRetardationHousing/Documents/MentalRetardationHousing.xml";
            byte[] data;
            using (WebClient webClient = new WebClient())
                data = webClient.DownloadData(url);

            string str = Encoding.GetEncoding("utf-8").GetString(data);
            XElement doc = XElement.Parse(str);

            IEnumerable<XElement> childList =
                from el in doc.Elements()
                select el;

            XNamespace ns = "http://Molsa.Moss.MisradHarevacha.MentalRetardationHousing";

            List<MentalRetardationHousingData> mentalRetardationHousingDatas = new List<MentalRetardationHousingData>();

            int indexer = 0;

            foreach (XElement e in childList)
            {
                indexer++;
                MentalRetardationHousingData mentalRetardationHousingData =new MentalRetardationHousingData()
                {
                    Id = indexer,
                    Address = e.Element(ns + "Address").Value,
                    Age = e.Element(ns + "Age").Value,
                    AnotherContact = e.Element(ns + "AnotherContact").Value,
                    BoardingHomeBranches = e.Element(ns + "BoardingHomeBranches").Value,
                    BoardingHomeType = e.Element(ns + "BoardingHomeType").Value,
                    CityName = e.Element(ns + "CityName").Value,
                    Code = e.Element(ns + "Code").Value,
                    Comments = e.Element(ns + "Comments").Value,
                    District = e.Element(ns + "District").Value,
                    Email = e.Element(ns + "Email").Value,
                    HousingType = e.Element(ns + "HousingType").Value,
                    Link = e.Element(ns + "Link").Value,
                    ManagerName = e.Element(ns + "ManagerName").Value,
                    Mobile = e.Element(ns + "Mobile").Value,
                    Phone = e.Element(ns + "Phone").Value,
                    ResidentsProperties = e.Element(ns + "ResidentsProperties").Value,
                    Sector = e.Element(ns + "Sector").Value,
                    TenantsStandard = e.Element(ns + "TenantsStandard").Value,
                    Zip = e.Element(ns + "Zip").Value
                };
                mentalRetardationHousingDatas.Add(mentalRetardationHousingData);
            }

            return View(mentalRetardationHousingDatas);
        }
    }
}