using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCWithWebService.Models
{
    public class Track
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int TrackID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

       
        public int FacilityID { get; set; }

        public virtual Facility Facility { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}