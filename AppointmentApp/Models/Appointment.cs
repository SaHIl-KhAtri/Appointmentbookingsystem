using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppointmentApp.Models
{
    public class Appointment
    {

        public int ?Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(50)]

        public string Meetingwith { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }
        [Required]
        public string vanue { get; set; }
        [Required]
        public bool isVip { get; set; }

    }
}   