using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using QGS.Entities.Filters;

namespace QGS.Entities.BusinessEntities
{

    public class PersonalDetailsVM
    {
        public string LoginUserEmail { get; set; }
        public string Prefix { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? RegistrationNo { get; set; }

        [Required]
        [Display(Name ="Email")]
        public string UserEmail { get; set; }
        public string Contact { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public int? PostCode { get; set; }
        public string City { get; set; }
        public DateTime LivedSince { get; set; }
        public EmployementDetailsVM EmployementDetails { get; set; }
        public RentDetailsVM RentDetails { get; set; }
        public IList<MaritalStatusVM> MaritalStatus { get; set; }
        public IList<NationalityVM> Nationality { get; set; }
        public IList<EmployementStatusVM> EmployementStatus { get; set; }
        public IList<PropertyStatusVM> PropertyStatus { get; set; }
        public IList<ContractTypeVM> contractTypes { get; set; }
        [Required]
        public int MaritalStatusVal { get; set; }
        [Required]
        public int NationalityVal { get; set; }
        public string SalarySlip { get; set; }

        [AllowExtensions(Extensions = "jpg,pdf,png", ErrorMessage = "You can only insert jpg, pdf and png!")]
        [MaxFileSize(4 * 1024 * 1024, ErrorMessage = "Files Exceeds Maximum Size!")]
        [Required(ErrorMessage = "Must Upload File!")]
        public HttpPostedFileBase SlipFile { get; set; }



    }

    
    public class EmployementDetailsVM
    {
        public string EmployeeName { get; set; }
        public int EmployementStatusVal { get; set; }
        public int ContractTypesVal { get; set; }
        public DateTime StartDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int? empPostCode { get; set; }
        
    }

    public class RentDetailsVM
    {
        public Nullable<int> NetMonthlyIncome { get; set; }
        public Nullable<int> RentalIncome { get; set; }
        public Nullable<int> PropertyStatus { get; set; }
        public Nullable<int> CreditCost { get; set; }
        public Nullable<int> MonthyRent { get; set; }

    }

    public class MaritalStatusVM
    {
        public int Id { get; set; }
        public string MaritalStatus { get; set; }
    }

    public class NationalityVM
    {
        public int Id { get; set; }
        public string Nationality { get; set; }
    }

    public class EmployementStatusVM
    {
        public int Id { get; set; }
        public string EmpStatus { get; set; }
    }

    public class PropertyStatusVM
    {
        public int Id { get; set; }

        public string PropertyStatus {get;set;}
    }

    public class ContractTypeVM
    {
        public int Id { get; set; }
        public string ContractType { get; set; }
    }

}

