using AppointmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApp.Repository
{
    internal interface IRepository
    {
        List<Appointment> GetAllAsync();
        void AddAsync(Appointment appointment);
        Appointment GetAsync(int id);
        bool UpdateAsync(Appointment appointment);
        bool DeleteAsync(int id);
    }
}
