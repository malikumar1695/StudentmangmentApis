using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMangment.Common.DTOs
{
    public class UserAccountDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public byte[] Image { get; set; }
    }
}
