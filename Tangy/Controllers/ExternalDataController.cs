using System;
using System.Collections.Generic;
using System.Data;
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

        private void SaveBytesToFile(string filename, byte[] bytesToWrite)
        {
            if (filename != null && filename.Length > 0 && bytesToWrite != null)
            {
                if (!Directory.Exists(Path.GetDirectoryName(filename)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filename));

                FileStream file = System.IO.File.Create(filename);

                file.Write(bytesToWrite, 0, bytesToWrite.Length);

                file.Close();
            }
        }

        private async Task<bool> DownloadNewFile(string url, string path)
        {
            using (var client = new System.Net.Http.HttpClient())
            {

                using (var result = await client.GetAsync(url))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        byte[] fileBytes = await result.Content.ReadAsByteArrayAsync();

                        SaveBytesToFile(path, fileBytes);
                    }
                }
            }
            return true;
        }
        public async Task<IActionResult> OtcPriceList()
        {
            string path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot\\externalFiles",
                "otcPriceList.xlsx");

            string url = "https://data.gov.il/dataset/25908429-78f1-450c-8507-20bcfb5a8a22/resource/69d10416-680b-4bc9-900a-62dd9e82430c/download/otc-price-list.xlsx";

            List<PrescriptionDrugsPriceListData> prescriptionDrugsPriceListDatas = new List<PrescriptionDrugsPriceListData>();

            if (!System.IO.File.Exists(path) || (System.IO.File.Exists(path) && System.IO.File.GetCreationTime(path) < DateTime.Now.AddDays(-7)))
            {
                await DownloadNewFile(url, path);
            }

            using (var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();

                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        int code = 0;

                        if (Int32.TryParse(row[0].ToString(), out code))
                        {
                            PrescriptionDrugsPriceListData prescriptionDrugsPriceListData = new PrescriptionDrugsPriceListData()
                            {
                                Code = code.ToString(),
                                DrugName = row[1].ToString(),
                                PackageSize = row[2].ToString(),
                                MaximumConsumerPrice = Double.Parse(row[3].ToString() == string.Empty ? "0" : row[3].ToString()),
                                MaximumConsumerPriceIncludingVAT = Double.Parse(row[4].ToString() == string.Empty ? "0" : row[4].ToString()),
                            };
                            prescriptionDrugsPriceListDatas.Add(prescriptionDrugsPriceListData);
                        }
                    }
                }
            }

            return View(prescriptionDrugsPriceListDatas);
        }
        public async Task<IActionResult> PrescriptionDrugsPriceList()
        {

            string path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot\\externalFiles",
                "prescriptiondrugspricelist1.xlsx");

            string url = "https://data.gov.il/dataset/76a500c1-f6a9-4276-8606-eccf41962a1f/resource/f40d2fa0-4082-43be-b029-4872d81c8251/download/prescriptiondrugspricelist1.xlsx";

            List<PrescriptionDrugsPriceListData> prescriptionDrugsPriceListDatas = new List<PrescriptionDrugsPriceListData>();

            if (!System.IO.File.Exists(path) || (System.IO.File.Exists(path) && System.IO.File.GetCreationTime(path) < DateTime.Now.AddDays(-7)))
            {
                await DownloadNewFile(url, path);
            }

            //using (Stream stream = req.GetResponse().GetResponseStream())
            using (var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
            {
                
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    //do
                    //{
                    //    while (reader.Read())
                    //    {
                    //        // reader.GetDouble(0);
                    //    }
                    //} while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    var result = reader.AsDataSet();

                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        int code = 0;

                        if(Int32.TryParse(row[0].ToString(), out code))
                        {
                            PrescriptionDrugsPriceListData prescriptionDrugsPriceListData = new PrescriptionDrugsPriceListData()
                            {
                                Code = code.ToString(),
                                DrugName = row[1].ToString(),
                                PackageSize = row[2].ToString(),
                                MaximumRetailprice = Double.Parse(row[3].ToString()==string.Empty?"0": row[3].ToString()),
                                MaximumRetailMargin = row[4].ToString(),
                                MaximumConsumerPrice = Double.Parse(row[5].ToString() == string.Empty ? "0" : row[5].ToString()),
                                MaximumConsumerPriceIncludingVAT = Double.Parse(row[6].ToString() == string.Empty ? "0" : row[6].ToString()),
                                CodeWillHeal = row[7].ToString(),
                                CodePharmaSoft = row[8].ToString()
                            };
                            prescriptionDrugsPriceListDatas.Add(prescriptionDrugsPriceListData);
                        }
                    }

                    // The result of each spreadsheet is in result.Tables
                }
            }

            return View(prescriptionDrugsPriceListDatas);
        }
    }
}