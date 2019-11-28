using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HorizonRE.Models;


namespace HorizonRE.Controllers
{
    [Authorize(Roles = RoleName.EMPLOYEE)]
    public class FilesUploadController : Controller
    {
        private HorizonContext db = new HorizonContext();

        // GET: FilesUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, ImageFile image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        int imgFileLimit = 5242880;

                        if (file.ContentLength <= imgFileLimit)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/TempFiles"), fileName);

                            //image type validation
                            System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);

                            if (ImageFormat.Jpeg.Equals(img.RawFormat) ||
                                ImageFormat.Gif.Equals(img.RawFormat) ||
                                ImageFormat.Bmp.Equals(img.RawFormat) ||
                                ImageFormat.Png.Equals(img.RawFormat) ||
                                ImageFormat.Tiff.Equals(img.RawFormat)

                                )
                            {
                                if (System.IO.File.Exists(filePath))
                                {
                                    Random rnd = new Random();
                                    string extractName = fileName.Substring(0, fileName.IndexOf("."));
                                    string fileExt = fileName.Substring(fileName.IndexOf("."));
                                    fileName = extractName + "_" + rnd.Next(1, 999999) + fileExt;
                                    filePath = Path.Combine(Server.MapPath("~/TempFiles"), fileName);
                                }


                                file.SaveAs(filePath);


                                string imageRelativePath = $@"/Content/Files/{fileName}";


                                string imageDesc = image.ImageDescription;
                                image.ImageName = fileName;
                                image.AltText = imageDesc;
                                image.Path = imageRelativePath;
                                image.UploadDate = DateTime.Today;
                                //@TODO: REMOVE EMPLOYEE ID 
                                //!REMOVE
                                var currentEmployee = db.Employees.Where(e => e.OfficeEmail == User.Identity.Name).FirstOrDefault();

                                if (currentEmployee != null)
                                {
                                    image.EmployeeId = currentEmployee.EmployeeId;
                                }
                                else
                                {
                                    image.EmployeeId = 1;
                                }


                                image.Approved = false;
                                image.ListingId = null;


                                db.Images.Add(image);
                                db.SaveChanges();

                                ViewBag.Msg = "Uploaded file was saved successfully";

                                return View();
                            }
                            else
                            {
                                ViewBag.Msg = "Not a valid type of file";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.Msg = "File size was exceeded";
                            return View();
                        }
                    }

                    return View();
                }
                catch (DbUpdateException ex)
                {
                    ViewBag.Msg = ex.Message + " " + ex.GetType().ToString();
                    return View();
                }
                catch (Exception ex)
                {
                    ViewBag.Msg = ex.Message + " " + ex.GetType().ToString();
                    return View();
                }
            }
            else
            {
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
                string sourceFile = Server.MapPath("/TempFiles/" + fileToApprove);
                string destinationFile = Server.MapPath("/Content/Files/") + fileToApprove;


                if (!System.IO.File.Exists(destinationFile))
                {

                    //1. Get file record from db

                    ImageFile img = db.Images.Where(im => im.ImageName == fileToApprove).SingleOrDefault();


                    if (img != null)
                    {

                        if (img.Employee.OfficeEmail != User.Identity.Name)
                        {
                            //2. Set approved to true 
                            img.Approved = true;

                            db.Entry(img).State = EntityState.Modified;
                            db.SaveChanges();

                            System.IO.File.Move(sourceFile, destinationFile);
                            ViewBag.Msg = "File has been approved";
                        }
                        else
                        {
                            ViewBag.Msg = "You cannot approve your own images";
                        }


                    }






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
                ViewBag.fileList = fileList.Length > 0 ? list : null;
            }
            else
            {
                ViewBag.Msg = "Directory doesn't exist";
            }
        }
    }
}