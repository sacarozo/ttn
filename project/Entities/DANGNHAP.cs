//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class DANGNHAP
    {
        public int Manhanvien { get; set; }
        public string Quyenhan { get; set; }
        public string Tendangnhap { get; set; }
        public string Matkhau { get; set; }
    
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
