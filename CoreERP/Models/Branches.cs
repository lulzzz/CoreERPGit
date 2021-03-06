﻿using System;
using System.Collections.Generic;

namespace CoreERP.Models
{
    public partial class Branches
    {
        public string BranchCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankBranch { get; set; }
        public string BankName { get; set; }
        public string CompanyCode { get; set; }
        public string Email { get; set; }
        public string Ext1 { get; set; }
        public string Ext2 { get; set; }
        public string Gstno { get; set; }
        public string Ifsccode { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string PinCode { get; set; }
        public string Place { get; set; }
        public int? State { get; set; }
        public string Active { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime? AddDate { get; set; }

        public virtual Companies CompanyCodeNavigation { get; set; }
    }
}
