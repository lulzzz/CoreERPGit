﻿using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using CoreERP.BussinessLogic.masterHlepers;
using CoreERP.BussinessLogic.TransactionsHelpers;
using CoreERP.Helpers.SharedModels;
using CoreERP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CoreERP.Controllers.Transactions
{
    [Route("api/Transactions/Stockreceipt")]
    [ApiController]
    public class StockreceiptController : ControllerBase
    {
        [HttpGet("GetBranchesList")]
        public async Task<IActionResult> GetBranchesListforStockreceipt()
        {
            var result = await Task.Run(() =>
            {
                try
                {
                    dynamic expando = new ExpandoObject();
                    expando.BranchesList = new StockreceiptHelpers().GetBranchesListforStockreceipt().Select(x => new { ID = x.BranchCode, TEXT = x.BranchName });
                    return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }

        [HttpGet("GetProductLists/{productCode}/{branchCode}")]
        public async Task<IActionResult> GetOpStockreceiptDetailsection1(string productcode, string branchCode)
        {
            var result = await Task.Run(() =>
            {
                try
                {
                    dynamic expando = new ExpandoObject();
                    expando.productsList = new StockreceiptHelpers().GetOpStockIssuesDetailsection(productcode, branchCode);
                    return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }

        [HttpGet("GetToBranchesList")]
        public async Task<IActionResult> GetToBranchesList()
        {
            var result = await Task.Run(() =>
            {
                try
                {
                    dynamic expando = new ExpandoObject();
                    expando.branch = new StockissuesHelper().GetBranches().Select(x => new { ID = x.BranchCode, TEXT = x.BranchName });
                    return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }

        [HttpGet("GettobranchesList/{branchcode}")]
        public async Task<IActionResult> GetbranchcodeList(string branchcode)
        {
            var result = await Task.Run(() =>
            {
                try
                {
                    dynamic expando = new ExpandoObject();
                    expando.branch = new StockreceiptHelpers().Getbranchcodes(branchcode).Select(x => new { ID = x.BranchCode, TEXT = x.BranchName });
                    return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }

        //GetReceiptNo
        [HttpGet("GetReceiptNo/{branchCode}")]
        public async Task<IActionResult> GetReceiptNo(string branchCode)
        {
            var result = await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(branchCode))
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = "Query string parameter missing." });

                try
                {
                    dynamic expando = new ExpandoObject();
                    expando.ReceiptNo = new StockreceiptHelpers().GetReceiptNo(branchCode);
                    return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }

        [HttpPost("RegisterStockreceipts")]
        public async Task<IActionResult> RegisterStockreceipts([FromBody]JObject objData)
        {
            var result = await Task.Run(() =>
            {
                APIResponse apiResponse = null;
                if (objData == null)
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = "Request is empty" });
                try
                {
                    var _stockreceiptHdr = objData["StackreceiptsHdr"].ToObject<TblOperatorStockReceipt>();
                    var _stockreceiptDtl = objData["StackreceiptsDetail"].ToObject<TblOperatorStockReceiptDetail[]>();

                    var result = new StockreceiptHelpers().RegisterStockreceipts(_stockreceiptHdr, _stockreceiptDtl.ToList());
                    apiResponse = new APIResponse() { status = APIStatus.PASS.ToString(), response = result };
                    return Ok(apiResponse);
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }

        [HttpPost("GetStockreceiptsList/{branchCode}")]
        public async Task<IActionResult> GetStockreceiptsList( [FromBody]VoucherNoSearchCriteria searchCriteria)
        {
            var result = await Task.Run(() =>
            {
                if (searchCriteria == null)
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = "Request is empty" });
                try
                {
                    var stockreceiptMasterList = new StockreceiptHelpers().GetStockissuesMasters(searchCriteria);
                    if (stockreceiptMasterList.Count > 0)
                    {
                        dynamic expando = new ExpandoObject();
                        expando.StockreceiptList = stockreceiptMasterList;
                        return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
                    }

                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = "No StockreceiptsList record found." });
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }


        [HttpGet("GetStockreceiptDeatilList/{issueNo}")]
        public async Task<IActionResult> GetStockreceiptDeatilList(string issueNo)
        {
            var result = await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(issueNo))
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = "Request is empty" });
                try
                {
                    var StockreceiptDeatilList = new StockreceiptHelpers().StockreceiptDeatils(issueNo);
                    if (StockreceiptDeatilList.Count > 0)
                    {
                        dynamic expando = new ExpandoObject();
                        expando.StockreceiptDeatilList = StockreceiptDeatilList;
                        return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
                    }

                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = "No Billing record found." });
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }


    }
}