using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration;

namespace ATM_Project_InAsp.NetCoreWebAPI.Models
{
    public class CsvDataModel
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }

    }
}
