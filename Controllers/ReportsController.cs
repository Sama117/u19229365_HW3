using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using HW3.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Xml.Linq;
using GemBox.Document;
using Newtonsoft.Json;



namespace HW3.Controllers
{
    public class ReportsController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        public ActionResult Reporting()
        {
            DateTime startDate = new DateTime(2015, 10, 1);
            DateTime endDate = new DateTime(2015, 11, 30);

            List<Points> borrowData = Dataset.GetBorrowCountForBooks(startDate, endDate);

            ViewBag.BorrowData = borrowData;

            return View();
        }




        public ActionResult Archives()
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/DownloadedDoc/"));
            List<FileModel> files = filePaths.Select(filePath => new FileModel { FileName = Path.GetFileName(filePath) }).ToList();
            return View(files);
        }

        public FileResult DownloadFile(string fileName)
        {
            string path = Server.MapPath("~/DownloadedDoc/") + fileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", fileName);
        }

        public ActionResult DeleteFile(string fileName)
        {
            string path = Server.MapPath("~/DownloadedDoc/") + fileName;
            System.IO.File.Delete(path);
            return RedirectToAction("Reporting");
        }

        public FileResult DisplayFile(string fileName)
        {
            string path = Server.MapPath("~/DownloadedDoc/") + fileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult TinyMCE() => View(new FileModel());

        [HttpPost]
        public FileResult SaveReport(FileModel model)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var templateFile = Server.MapPath("~/App_Data/Template.docx");
            var document = DocumentModel.Load(templateFile);
            var bookmark = document.Bookmarks["HtmlBookmark"];
            bookmark.GetContent(true).LoadText(model.Content, GemBox.Document.LoadOptions.HtmlDefault);
            var saveOptions = GetSaveOptions(model.Extension);
            var stream = new MemoryStream();
            document.Save(stream, saveOptions);
            var downloadDirectory = Server.MapPath("~/DownloadedDoc/");
            var downloadFile = $"{model.FileName}{model.Extension}";
            var fullPath = Path.Combine(downloadDirectory, downloadFile);
            System.IO.File.WriteAllBytes(fullPath, stream.ToArray());

            string[] filePaths = Directory.GetFiles(Server.MapPath("~/DownloadedDoc/"));
            List<FileModel> files = filePaths.Select(filePath => new FileModel { FileName = Path.GetFileName(filePath) }).ToList();

            return File(fullPath, saveOptions.ContentType, downloadFile);
        }

        private static GemBox.Document.SaveOptions GetSaveOptions(string extension)
        {
            switch (extension)
            {
                case ".docx": return GemBox.Document.SaveOptions.DocxDefault;
                case ".xps": return GemBox.Document.SaveOptions.XpsDefault;
                case ".mhtml": return new HtmlSaveOptions() { HtmlType = HtmlType.Mhtml };
                case ".pdf": return GemBox.Document.SaveOptions.PdfDefault;
                case ".html": return GemBox.Document.SaveOptions.HtmlDefault;
                case ".rtf": return GemBox.Document.SaveOptions.RtfDefault;
                case ".png": return GemBox.Document.SaveOptions.ImageDefault;
                case ".xml": return GemBox.Document.SaveOptions.XmlDefault;
                case ".jpeg": return new ImageSaveOptions(ImageSaveFormat.Jpeg);
                case ".bmp": return new ImageSaveOptions(ImageSaveFormat.Bmp);
                case ".gif": return new ImageSaveOptions(ImageSaveFormat.Gif);
                case ".tiff": return new ImageSaveOptions(ImageSaveFormat.Tiff);
                case ".wmp": return new ImageSaveOptions(ImageSaveFormat.Wmp);
                default: return GemBox.Document.SaveOptions.TxtDefault;
            }
        }
    }
}
