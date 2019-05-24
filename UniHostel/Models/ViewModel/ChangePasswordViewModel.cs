using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniHostel.Models.ViewModel
{
    public class ChangePasswordViewModel
    {
        [StringLength(255)]
        public string ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(255)]
        public string ConfirmPassword { get; set; }

        public ChangePasswordViewModel(User user)
        {
            ID = user.ID;
            NewPassword = "";
            ConfirmPassword = "";

        }

        public ChangePasswordViewModel()
        {

        }

    }
}