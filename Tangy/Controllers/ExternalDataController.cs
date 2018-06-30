using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ExcelDataReader;
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

        public IActionResult RehabilitationHousingData()
        {
            string url = "http://www.molsa.gov.il/Open/RehabilitationHousing/Documents/RehabilitationHousing.xml";
            byte[] data;
            using (WebClient webClient = new WebClient())
                data = webClient.DownloadData(url);

            string str = Encoding.GetEncoding("utf-8").GetString(data);
            XElement doc = XElement.Parse(str);

            IEnumerable<XElement> childList =
                from el in doc.Elements()
                select el;

            XNamespace ns = "http://Molsa.Moss.MisradHarevacha.RehabilitationHousing";

            List<RehabilitationHousingData> rehabilitationHousingDatas = new List<RehabilitationHousingData>();

            int indexer = 0;

            foreach (XElement e in childList)
            {
                indexer++;
                    RehabilitationHousingData rehabilitationHousingData = new RehabilitationHousingData()
                    {
                        Id = indexer,
                        Address = e.Element(ns + "Address").Value,
                        AcceptanceProcess = e.Element(ns + "AcceptanceProcess").Value,
                        AgeDetail = e.Element(ns + "AgeDetail").Value,
                        AgeGroup = e.Element(ns + "AgeGroup").Value,
                        ArrivalHelp = e.Element(ns + "ArrivalHelp").Value,
                        DisabilityType = e.Element(ns + "DisabilityType").Value,
                        Code = e.Element(ns + "Code").Value,
                        DisabilityTypeDetail = e.Element(ns + "DisabilityTypeDetail").Value,
                        District = e.Element(ns + "District").Value,
                        Email = e.Element(ns + "Email").Value,
                        GIshuvName = e.Element(ns + "GIshuvName").Value,
                        ManagerName = e.Element(ns + "ManagerName").Value,
                        Mobile = e.Element(ns + "Mobile").Value,
                        Phone = e.Element(ns + "Phone").Value,
                        InstitutionName = e.Element(ns + "InstitutionName").Value,
                        InstitutionType = e.Element(ns + "InstitutionType").Value,
                        InstitutionTypeDetails = e.Element(ns + "InstitutionTypeDetails").Value,
                        MaxResidentsNumber = e.Element(ns + "MaxResidentsNumber").Value,
                        MoreInformation = e.Element(ns + "MoreInformation").Value,
                        RehHousingSector = e.Element(ns + "RehHousingSector").Value,
                        TariffCode = e.Element(ns + "TariffCode").Value,
                        ServicesDetails = e.Element(ns + "ServicesDetails").Value
                    };
                rehabilitationHousingDatas.Add(rehabilitationHousingData);
            }
            return View(rehabilitationHousingDatas);
        }

        public IActionResult BlindServicesData()
        {
            string url = "http://www.molsa.gov.il/Open/BlindServices/Documents/BlindServices.xml";
            byte[] data;
            using (WebClient webClient = new WebClient())
                data = webClient.DownloadData(url);

            string str = Encoding.GetEncoding("utf-8").GetString(data);
            XElement doc = XElement.Parse(str);

            IEnumerable<XElement> childList =
                from el in doc.Elements()
                select el;

            XNamespace ns = "http://Molsa.Moss.MisradHarevacha.BlindServices";

            List<BlindServicesData> blindServicesDatas = new List<BlindServicesData>();

            int indexer = 0;

            foreach (XElement e in childList)
            {
                indexer++;
                    BlindServicesData blindServicesData = new BlindServicesData()
                    {
                        Id = indexer,
                        Address = e.Element(ns + "Address").Value,
                        BlndFax = e.Element(ns + "BlndFax").Value,
                        BlndManager = e.Element(ns + "BlndManager").Value,
                        BlndPhone = e.Element(ns + "BlndPhone").Value,
                        BlndTrigger = e.Element(ns + "BlndTrigger").Value,
                        CityName = e.Element(ns + "CityName").Value,
                        OpenDays = e.Element(ns + "OpenDays").Value,
                        OpenHours = e.Element(ns + "OpenHours").Value,
                        District = e.Element(ns + "District").Value,
                        TheService = e.Element(ns + "TheService").Value
                    };
                    blindServicesDatas.Add(blindServicesData);
            }
            return View(blindServicesDatas);
        }

        public IActionResult PrescriptionDrugsPriceList()
        {
            //https://data.gov.il/dataset/76a500c1-f6a9-4276-8606-eccf41962a1f/resource/f40d2fa0-4082-43be-b029-4872d81c8251/download/prescriptiondrugspricelist1.xlsx

            var req = WebRequest.Create("https://data.gov.il/dataset/76a500c1-f6a9-4276-8606-eccf41962a1f/resource/f40d2fa0-4082-43be-b029-4872d81c8251/download/prescriptiondrugspricelist1.xlsx");

            //// Create a request for the URL.   
            //WebRequest request = WebRequest.Create(
            //  "https://data.gov.il/dataset/76a500c1-f6a9-4276-8606-eccf41962a1f/resource/f40d2fa0-4082-43be-b029-4872d81c8251/download/prescriptiondrugspricelist1.xlsx");
            //// If required by the server, set the credentials.  
            ////request.Credentials = CredentialCache.DefaultCredentials;
            //// Get the response.  
            //WebResponse response = request.GetResponse();
            //// Display the status.  
            ////Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            //// Get the stream containing content returned by the server.  
            //Stream dataStream = response.GetResponseStream();
            //// Open the stream using a StreamReader for easy access.  
            //StreamReader reader = new StreamReader(dataStream);
            //// Read the content.  
            //string responseFromServer = reader.ReadToEnd();
            //// Display the content.  
            //Console.WriteLine(responseFromServer);
            //// Clean up the streams and the response.  
            //reader.Close();
            //response.Close();




            var filePath = "https://data.gov.il/dataset/76a500c1-f6a9-4276-8606-eccf41962a1f/resource/f40d2fa0-4082-43be-b029-4872d81c8251/download/prescriptiondrugspricelist1.xlsx";

            using (Stream stream = req.GetResponse().GetResponseStream())
            //using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateBinaryReader(stream))
                {

                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                        }
                    } while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    var result = reader.AsDataSet();

                    int a = 0;
                    // The result of each spreadsheet is in result.Tables
                }
            }

            return View();
        }
    }
}