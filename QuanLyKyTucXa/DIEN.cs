//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKyTucXa
{
    using System;
    using System.Collections.Generic;
    
    public partial class DIEN
    {
        public int ID { get; set; }
        public Nullable<int> ChiSoDau { get; set; }
        public Nullable<int> ChiSoCuoi { get; set; }
        public Nullable<int> Gia { get; set; }
        public Nullable<System.DateTime> NgayThanhToan { get; set; }
        public Nullable<int> Tien { get; set; }
        public string MaSinhVien { get; set; }
    
        public virtual SINHVIEN SINHVIEN { get; set; }
    }
}
