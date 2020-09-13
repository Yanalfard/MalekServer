using MalekServer3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MalekServer3.Models.ViewModel
{
    public class RegisterViewModle
    {
        public int id { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        public string Name { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(10, ErrorMessage = "تعداد کاراکتر کم است")]
        public string IdentificationNo { get; set; }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(15, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(10, ErrorMessage = "تعداد کاراکتر کم است")]
        [DataType(DataType.PhoneNumber)]
        public string TellNo { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(25, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(5, ErrorMessage = "تعداد کاراکتر کم است")]
        public string Username { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید ")]
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(5, ErrorMessage = "تعداد کاراکتر کم است")]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید ")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(5, ErrorMessage = "تعداد کاراکتر کم است")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید ")]
        [DataType(DataType.MultilineText)]
        [MaxLength]
        [MinLength(5, ErrorMessage = "تعداد کاراکتر کم است")]
        public string Address { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید ")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(5, ErrorMessage = "تعداد کاراکتر کم است")]
        public string Email { get; set; }
        public string Auth { get; set; }
        public bool IsActive { get; set; }
    }
    public class UpdateRegisterViewModle
    {
        public int id { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        public string Name { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(10, ErrorMessage = "تعداد کاراکتر کم است")]
        public string IdentificationNo { get; set; }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(15, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(11, ErrorMessage = "تعداد کاراکتر کم است")]
        [DataType(DataType.PhoneNumber)]
        public string TellNo { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(25, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(5, ErrorMessage = "تعداد کاراکتر کم است")]
        public string Username { get; set; }
        [Display(Name = "کلمه عبور قدیمی")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید ")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Display(Name = "کلمه عبور جدید ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید ")]
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(5, ErrorMessage = "تعداد کاراکتر کم است")]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید ")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(5, ErrorMessage = "تعداد کاراکتر کم است")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید ")]
        [DataType(DataType.MultilineText)]
        [MaxLength]
        [MinLength(5, ErrorMessage = "تعداد کاراکتر کم است")]
        public string Address { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید ")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(5, ErrorMessage = "تعداد کاراکتر کم است")]
        public string Email { get; set; }
        public string Auth { get; set; }
        public bool IsActive { get; set; }
    }
}