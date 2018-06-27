using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.TherapistViewModel
{
    public class TherapistViewModel
    {
        public TherapyType TherapyType { get; set; }
        public List<Therapist> Therapists { get; set; }
    }
}
