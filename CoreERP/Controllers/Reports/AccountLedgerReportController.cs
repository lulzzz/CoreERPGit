﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreERP.BussinessLogic.ReportsHelpers;
using System.Dynamic;
using System.Data;
namespace CoreERP.Controllers.Reports
{
    [Route("api/Reports/AccountLedgerReport")]
    [ApiController]
    public class AccountLedgerReportController : ControllerBase
    {
        [HttpGet("GetAccountLedgerReportData")]
        public async Task<IActionResult> GetAccountLedgerReportData(string UserID,string ledgerCode, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var serviceResult =await Task.FromResult(ReportsHelperClass.GetAccountLedgerReportDataList(UserID,ledgerCode, fromDate,toDate));
                if (serviceResult.Item1 != null && serviceResult.Item1.Count>0)
                {
                    dynamic expdoObj = new ExpandoObject();
                    expdoObj.accountLedgerList = serviceResult.Item1;
                    expdoObj.headerList = serviceResult.Item2;
                    expdoObj.footerList = serviceResult.Item3;
                    return Ok(new APIResponse { status = APIStatus.PASS.ToString(), response = expdoObj });
                }
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = "No Data Found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }       
        [HttpGet("GetAccountLedgersList")]
        public async Task<IActionResult> GetAccountLedgersList()
        {
            try
            {
                var accountLedgerList = await Task.FromResult(ReportsHelperClass.GetAccountLedgers());
                if (accountLedgerList != null && accountLedgerList.Count > 0)
                {
                    dynamic expdoObj = new ExpandoObject();
                    expdoObj.accountLedgerList = accountLedgerList;
                    return Ok(new APIResponse { status = APIStatus.PASS.ToString(), response = expdoObj });
                }
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = "No Data Found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }
    }
}