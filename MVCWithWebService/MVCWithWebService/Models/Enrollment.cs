using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWithWebService.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int TrackID { get; set; }
        public int CustomerID { get; set; }
        [DisplayFormat(NullDisplayText = "No grade")]
         
        public virtual Track Track { get; set; }
        public virtual Customer Customer { get; set; }
    }
}