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
    
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            this.HOADONs = new HashSet<HOADON>();
        }
    
        public int Manhanvien { get; set; }
        public string Tennhanvien { get; set; }
        public string Gioitinh { get; set; }
        public Nullable<System.DateTime> Ngaysinh { get; set; }
        public string Diachi { get; set; }
        public string Sodienthoai { get; set; }
        public Nullable<int> Maphongban { get; set; }
        public Nullable<int> Machucvu { get; set; }
    
        public virtual CHUCVU CHUCVU { get; set; }
        public virtual DANGNHAP DANGNHAP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        public virtual PHONGBAN PHONGBAN { get; set; }
    }
}