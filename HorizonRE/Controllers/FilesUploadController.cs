using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HorizonRE.Controllers
{
    public class FilesUploadController : Controller
    {
        // GET: FilesUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    int imgFileLimit = 1048576;

                    if (file.ContentLength <= imgFileLimit)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/TempFiles"), fileName);

                        //image type validation
                        System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);

                        if (ImageFormat.Jpeg.Equals(img.RawFormat) ||
                            ImageFormat.Gif.Equals(img.RawFormat) ||
                            ImageFormat.Bmp.Equals(img.RawFormat) ||
                            ImageFormat.Png.Equals(img.RawFormat))
                        {
                            file.SaveAs(filePath);
                            ViewBag.Msg = "Uploaded file was saved successfully";
                            
                        }
                        else
                        {
                            ViewBag.Msg = "Not a valid type of file";
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "File size was exceeded";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Uploaded file wasn't saved. Please try again";
                return View();
            }
        }



        [HttpGet]
        public ActionResult MoveFile()
        {
            getImages();
            return View();
        }

        [HttpPost]
        public ActionResult MoveFile(List<string> fileList)
        {
            ViewBag.fileName = fileList[0];

            //redirect to a diff view
            return View("MoveFileDisplay");
        }


        public ActionResult ApproveFile(string fileToApprove, string approve)
        {
            if (!string.IsNullOrEmpty(fileToApprove) && approve == "Yes")
            {
                string sourceFile = Server.MapPath("~/TempFiles/" + fileToApprove);
                string destinationFile = Server.MapPath("~/Content/Files/") + fileToApprove;
                if (!System.IO.File.Exists(destinationFile))
                {
                    System.IO.File.Move(sourceFile, destinationFile);
                    ViewBag.Msg = "File has been approved";
                }
                else
                {
                    ViewBag.Msg = "File with that name already exists";
                }
            }
            else
            {
                string sourceFile = Server.MapPath("~/TempFiles/" + fileToApprove);
                string destinationFile = Server.MapPath("~/FilesToDelete/") + fileToApprove;

                System.IO.File.Move(sourceFile, destinationFile);
                //if you want tot delete
                //System.IO.File.Delete(sourceFile);

                ViewBag.Msg = "File has been removed";
            }
            return View("MoveFileDisplay");
        }

        private void getImages()
        {
            //get the images from the system
            string folderPath = Server.MapPath("~/TempFiles");

            if (Directory.Exists(folderPath))
            {
                //create an arry of the files found in the directory at the specified path
                string[] fileEntries = Directory.GetFiles(folderPath);

                //process the list of files found in the directory
                string fileName = null;

                string[] fileList = new string[fileEntries.Count()];
                int i = 0;

                foreach (string file in fileEntries)
                {
                    fileName = Path.GetFileName(file);
                    fileList[i++] = fileName;
                }

                //create select list form array
                SelectList list = new SelectList(fileList);
                ViewBag.fileList = list;
            }
            else
            {
                ViewBag.Msg = "Directory doesn't exist";
            }
        }
    }
}