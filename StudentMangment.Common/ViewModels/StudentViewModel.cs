using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMangment.Common.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int RollNo { get; set; }
        public DateTime AddmissionDate { get; set; }
    }
}
