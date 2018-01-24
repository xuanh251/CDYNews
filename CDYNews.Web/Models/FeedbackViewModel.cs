using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDYNews.Web.Models
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        [MaxLength(250,ErrorMessage ="Tên không được quá 250 ký tự")]
        [Required(ErrorMessage ="Phải nhập tên")]
        public string Name { get; set; }
        [MaxLength(250, ErrorMessage = "Email không được quá 250 ký tự")]
        public string Email { get; set; }
        [MaxLength(250, ErrorMessage = "Nội dung không được quá 1000 ký tự")]
        public string Message { get; set; }
        public string UserInfo { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Phải nhập trạng thái")]
        public bool Status { get; set; }
    }
}