using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.DoctorViewModel
{
    public class DoctorViewModel
    {
        public MedicalField MedicalField { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
