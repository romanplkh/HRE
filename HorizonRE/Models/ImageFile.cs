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
      [Required(ErrorMessage = "Please select image first")]
      [StringLength(100)]
      public string ImageName { get; set; }
      [Required(ErrorMessage = "Description of image is required")]
      [StringLength(150)]
      public string ImageDescription { get; set; }
      [Required]
      public string Path { get; set; }
      [Required]
      public string AltText { get; set; }
      [Required]
      [DataType(DataType.DateTime)]
      public DateTime UploadDate { get; set; }
      [Required]
      public bool Approved { get; set; }



  
      public int EmployeeId { get; set; }
      public virtual Employee Employee { get; set; }
   }
}