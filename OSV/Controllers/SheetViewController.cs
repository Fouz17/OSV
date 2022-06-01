using OSV.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSV.Controllers
{
    public class SheetViewController : Controller
    {
        private readonly DB db = new DB();
        // GET: SheetView
        public ActionResult Index()
        {
            TempData["PaperCode"] = 0;
            List<string> paperCodes = db.tblPaperMasters.Select(s => s.PaperCode).Distinct().ToList();
            ViewBag.PaperCodes = paperCodes;
            List<string> Centers = db.tblOMRDatas.Select(x => x.CenterCode).Distinct().ToList();
            ViewBag.Centers = Centers;
            return View();
        }

        // GET: SheetView/GetSheet?5
        public JsonResult GetSheet(string paperCode, string centerCode, string RollNo)
        {
            var filename = db.tblOMRDatas
                .Where(x => x.CenterCode == centerCode && x.PaperCode == paperCode && x.RollNo == RollNo)
                .Select(s => new { s.FirstPageFile, s.SecondPageFile }).FirstOrDefault();
            if (filename != null)
            {
                string path1 = $"{paperCode}/{centerCode}/{filename.FirstPageFile}";
                string path2 = $"{paperCode}/{centerCode}/{filename.SecondPageFile}";

                List<string> Files = new List<string>
            {
                GetBase64(path1),
                GetBase64(path2)
            };

                return Json(Files, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<string>(), JsonRequestBehavior.AllowGet);
        }

        private string GetBase64(string Path)
        {
            string SourceFilePath = System.Configuration.ConfigurationManager.AppSettings["SourceFilePath"];//.Replace(@"//", "*");
            Path = System.IO.Path.Combine(SourceFilePath + Path);
            string base64String;

            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                using (MemoryStream m = new MemoryStream(client.DownloadData(Path)))
                {
                    using (Image image = Image.FromStream(m))
                    {
                        try
                        {
                            int imageWidth = 700;
                            int imageHeight = (int)((float)image.Height / (float)image.Width * imageWidth);
                            Image resizedImage = new Bitmap(imageWidth, imageHeight + 15);

                            Graphics gfx = Graphics.FromImage(resizedImage);
                            gfx.DrawImage(image, 0, 15, imageWidth, imageHeight - 15);
                            using (MemoryStream msResized = new MemoryStream())
                            {
                                resizedImage.Save(msResized, System.Drawing.Imaging.ImageFormat.Jpeg);
                                // Convert byte[] to Base64 String
                                base64String = Convert.ToBase64String(msResized.ToArray());
                            }

                            return base64String;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            return e.Message;
                        }
                    }
                }
            }
        }
    }
}
