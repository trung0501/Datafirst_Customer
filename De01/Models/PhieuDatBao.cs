﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace De01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;
    
    public partial class PhieuDatBao
    {
       [Required(ErrorMessage ="Tên báo không để trống"),Key, DisplayName("Tên báo")]
        public string TenBao { get; set; }
        [Required(ErrorMessage = "Mã KH không để trống"), Key, DisplayName("Mã KH")]
        public int MaKH { get; set; }
        [Required(ErrorMessage = "Không để trống"), DisplayName("Ngày đặt"),DataType(DataType.DateTime)]
        public System.DateTime NgayDat { get; set; }
        [Required(ErrorMessage ="Số lượng phải nhập"), Range(1,10,ErrorMessage ="Số lượng từ 1-10")]
        public int Soluong { get; set; }
        [Required(ErrorMessage ="Đơn giá phải nhập")]
        public long DonGia { get; set; }
    }
}