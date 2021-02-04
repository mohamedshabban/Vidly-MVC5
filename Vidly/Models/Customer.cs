using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
       
        [Display(Name = "Birth of Date")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }
       

        public MembershipType MembershipType { get; set; }


        //f.k
        [Display(Name = "Membership Type")]
        public byte MemberShipTypeId { get; set; }
    }
}