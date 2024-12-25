using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM_Project_InAsp.NetCoreWebAPI.Models
{
    public class DocumentModel
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile? AdhaarCard { get; set; }
        public string AdhaarCardUrl { get; set; }

        [NotMapped]
        public IFormFile? PanCard { get; set; }
        public string PanCardUrl { get; set; }

        
    }
}

