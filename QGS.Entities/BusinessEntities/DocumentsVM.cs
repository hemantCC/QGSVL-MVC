using System.ComponentModel.DataAnnotations;
using System.Web;
using QGS.Entities.Filters;

namespace QGS.Entities.BusinessEntities
{
    public class DocumentsVM
    {
        [Required(ErrorMessage = "Must Upload File!")]
        [AllowExtensions(Extensions = "jpg,pdf,png", ErrorMessage = "You can only insert jpg, pdf and png!")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Files Exceeds Maximum Size!")]
        public HttpPostedFileBase File1 { get; set; }

        [AllowExtensions(Extensions = "jpg,pdf,png", ErrorMessage = "You can only insert jpg, pdf and png!")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Files Exceeds Maximum Size!")]
        [Required(ErrorMessage = "Must Upload File!")]
        public HttpPostedFileBase File2 { get; set; }

        [AllowExtensions(Extensions = "jpg,pdf,png", ErrorMessage = "You can only insert jpg, pdf and png!")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Files Exceeds Maximum Size!")]
        [Required(ErrorMessage = "Must Upload File!")]
        public HttpPostedFileBase File3 { get; set; }

    }
}
