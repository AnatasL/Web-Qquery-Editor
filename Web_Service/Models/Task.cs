using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Service.Models
{   
    //Main class of the application 
    public class Task
    {
        [Key]
        public int id_task { get; set; }
        public string Nume_Task { get; set; }
        public DateTime Data_Task { get; set; }
        public string Nume_Utilizator { get; set; }
        public string Tip { get; set; }
        public string Statut { get; set; }

    }
}
