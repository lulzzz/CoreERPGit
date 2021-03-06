﻿using System;
using System.Collections.Generic;

namespace CoreERP.Models
{
    public partial class ItemMaster
    {
        public string ItemNumber { get; set; }
        public string AccClass { get; set; }
        public string Brand { get; set; }
        public long? ClosingStock { get; set; }
        public string Company { get; set; }
        public string ItemName { get; set; }
        public string Ext1 { get; set; }
        public string Ext2 { get; set; }
        public string Ext3 { get; set; }
        public string Ext4 { get; set; }
        public string Ext5 { get; set; }
        public int? UnitId { get; set; }
        public int? PackingSize { get; set; }
        public string Hsncode { get; set; }
        public string InputTaxCode { get; set; }
        public string InventoryAccount { get; set; }
        public string ItemGroup { get; set; }
        public long? PackingCode { get; set; }
        public long? Mrpprice { get; set; }
        public long? MaxQty { get; set; }
        public long? MinQty { get; set; }
        public string Model { get; set; }
        public string Narration { get; set; }
        public string OutputTaxCode { get; set; }
        public string PurchaseAccount { get; set; }
        public long? ReOrdQty { get; set; }
        public long? RetailPrice { get; set; }
        public long? SalePrice { get; set; }
        public string SalesAccount { get; set; }
        public long? Size { get; set; }
        public long? WholePrice { get; set; }
        public string Active { get; set; }
        public DateTime? AddDate { get; set; }
    }
}
