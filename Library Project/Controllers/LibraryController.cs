using Library_Project.Filters;
using Library_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Project.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        BookContext bc = new BookContext();
        // GET: Library
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult  Home()
        {
            return View();
        }
        [CustomFilter]
     
        public ActionResult GetBooks()
        {

            return View(bc.GetBooks());
        }
        [HttpPost]
        public ActionResult GetBookById(Book b)
        {
            return View(bc.GetBookById(b.BookId));
        }
        [HttpPost]
        public ActionResult GetBookByName(Book b)
        {
            return View(bc.GetBookByName(b.BookName));
        }
        public ActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBook(Book b)
        {
            int i = bc.SaveBook(b);
            if (i > 0)
            {
                return RedirectToAction("GetBooks");
            }
            else
            {
                return View();
            }

        }
        public ActionResult UpdateBook(int? BookId)
        {
            Book b = bc.GetBookById(BookId);
            return View(b);
        }
        [HttpPost]
        public ActionResult UpdateBook(Book b)
        {
            int i = bc.SaveBook(b);
            if (i > 0)
            {
                return RedirectToAction("GetBooks");
            }
            else
            {
                return View();
            }

        }
        public ActionResult DeleteBook(int? BookId)
        {
            Book b = bc.GetBookById(BookId);
            return View(b);

        }
        [HttpPost]
        public ActionResult DeleteBook(Book b)
        {
            int i = bc.DeleteBook(b);
            if (i > 0)
            {
                return RedirectToAction("GetBooks");
            }
            else
            {
                return View();
            }
        }
        [CustomFilter]
        public ActionResult UploadBook(Book b)
        {
            //Book b = bc.GetBookById(BookId);
            ViewBag.UploadMsg = "Book is Uploading";
            return View(b);

        }
       [CustomFilter]
        [HttpPost]
        public ActionResult UploadBook1(Book b)
        {
            //string directory = "C:/Users/pavan/OneDrive/Documents/visual studio 2015/Projects/Library Project/Library Project/Book Images/";
            HttpPostedFileBase photo = Request.Files["photo"];
            Stream stream = photo.InputStream;
            ViewBag.UploadMsg = "Upload in Progress";
    
            BinaryReader binaryreader = new BinaryReader(stream);
            byte[] bytes = binaryreader.ReadBytes((int)stream.Length);
            if (photo != null && photo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                //photo.SaveAs(Path.Combine(directory, fileName));
                int i = bc.FileUpload(b.BookId, bytes);
            }
            //return RedirectToAction("GetBooks");
            return View(bc.GetBookById(b.BookId));
        }
       
        public ActionResult DownloadBook(Book b)
        {
           Book b1= bc.GetUploadImage(b.BookId);
            return File(
        b1.BookImage, System.Net.Mime.MediaTypeNames.Application.Octet);
        }

        //[HttpPost]
        //public ActionResult UploadBook1(Book b)
        //{
        //    //string directory = "C:/Users/pavan/OneDrive/Documents/visual studio 2015/Projects/Library Project/Library Project/Book Images/";
        //    HttpPostedFileBase photo = Request.Files["photo"];
        //    Stream stream = photo.InputStream;
        //    BinaryReader binaryreader = new BinaryReader(stream);
        //    byte[] bytes = binaryreader.ReadBytes((int)stream.Length);
        //    if (photo != null && photo.ContentLength > 0)
        //    {
        //        var fileName = Path.GetFileName(photo.FileName);
        //        //photo.SaveAs(Path.Combine(directory, fileName));
        //        int i = bc.FileUpload(b.BookId, bytes);
        //    }
        //    return RedirectToAction("getbooks");
        //}
    }
}