﻿using CoreERP.DataAccess;
using CoreERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreERP.BussinessLogic.TransactionsHelpers
{
    public class StockreceiptHelpers
    {
        public  List<TblOperatorStockReceipt> GetStockreceiptList()
        {
            try
            {
                using (Repository<TblOperatorStockReceipt> repo = new Repository<TblOperatorStockReceipt>())
                {
                    return repo.TblOperatorStockReceipt.ToList();
                }
            }
            catch { throw; }
        }


        public  List<Branches> Getbranchcodes(string natureofAccount)
        {
            try
            {
                using (Repository<Branches> repo = new Repository<Branches>())
                {
                    return repo.Branches
                          .Where(x => x.Active.Equals("Y")
                                  && (x.BranchCode.Equals(natureofAccount))
                                )
                          .ToList();
                }
            }
            catch { throw; }
        }

        public  List<TblOperatorStockReceipt> GetStockReceiptlist()
        {
            try
            {
                using (Repository<TblOperatorStockReceipt> repo = new Repository<TblOperatorStockReceipt>())
                {
                    return repo.TblOperatorStockReceipt.ToList();
                }

            }
            catch { throw; }
        }

        public  string GetReceiptNo(string branchCode)
        {
            try
            {
                var voucherNo = GetStockReceiptlist().Where(b => b.FromBranchCode == branchCode).OrderByDescending(x => x.ReceiptNo).FirstOrDefault();
                if (voucherNo != null)
                {
                    string[] splitString = voucherNo.ReceiptNo.Split('/', '-');
                    var noRange = splitString[1];
                    if (noRange.Length > 0)
                    {
                        noRange = (Convert.ToInt32(noRange) + 1).ToString();
                    }

                    return splitString[0] + "-" + noRange + "-" + splitString[2];
                }

                return "OPSR-1-" + branchCode;
            }
            catch { throw; }
        }
    }
}