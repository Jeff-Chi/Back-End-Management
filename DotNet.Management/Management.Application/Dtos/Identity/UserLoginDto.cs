﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; } = string.Empty;

        //[Required(ErrorMessage = "验证码不能为空")]
        //public string VerCode { get; set; }

        //[Required(ErrorMessage = "Guid不能为空")]
        //public Guid? Guid { get; set; }
    }
}
