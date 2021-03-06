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
    [Route("api/Reports/[controller]")]
    [ApiController]
    public class StockVerificationReportController : ControllerBase
    {
        [HttpGet("GetStockVerificationReportData")]
        public async Task<IActionResult> GetStockVerificationReportData(string branchCode, string UserID, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var serviceResult = await Task.FromResult(ReportsHelperClass.GetStockVerificationReportDataList(branchCode,UserID,fromDate,toDate));
                dynamic expdoObj = new ExpandoObject();
                expdoObj.StockVerificationList = serviceResult.Item1;
                expdoObj.headerList = serviceResult.Item2;
                expdoObj.footerList = serviceResult.Item3;
                return Ok(new APIResponse { status = APIStatus.PASS.ToString(), response = expdoObj });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }
    }
}
