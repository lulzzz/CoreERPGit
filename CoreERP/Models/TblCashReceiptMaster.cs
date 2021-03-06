﻿using System;
using System.Collections.Generic;

namespace CoreERP.Models
{
    public partial class TblCashReceiptMaster
    {
        public decimal CashReceiptMasterId { get; set; }
        public decimal? BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string CashReceiptVchNo { get; set; }
        public string VoucherNo { get; set; }
        public DateTime? CashReceiptDate { get; set; }
        public decimal? FromLedgerId { get; set; }
        public string FromLedgerName { get; set; }
        public string FromLedgerCode { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Narration { get; set; }
        public decimal? UserId { get; set; }
        public string UserName { get; set; }
        public decimal? EmployeeId { get; set; }
        public decimal? ShiftId { get; set; }
        public DateTime? ServerDate { get; set; }
    }
}
