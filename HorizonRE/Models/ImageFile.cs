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
      [StringLength(100)]
      public string ImageName { get; set; }
      [Required(ErrorMessage = "Description of image is required")]
      [StringLength(150)]
      public string ImageDescription { get; set; }
     
      public string Path { get; set; }
    
      public string AltText { get; set; }
      
      [DataType(DataType.DateTime)]
      public DateTime UploadDate { get; set; }
      public bool Approved { get; set; }



  
      public int EmployeeId { get; set; }
      public virtual Employee Employee { get; set; }


      public int? ListingId { get; set; }
      public Listing Listing { get; set; }
   }
}