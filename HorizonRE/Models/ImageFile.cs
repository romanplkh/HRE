using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
   public class ImageFile
   {


      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int ImageId { get; set; }
      public string ImageName { get; set; }
      public string ImageDescription { get; set; }
      public string Path { get; set; }
      public string AltText { get; set; }
      public DateTime UploadDate { get; set; }
      public bool Approved { get; set; }
      public int EmployeeId { get; set; }
   }
}