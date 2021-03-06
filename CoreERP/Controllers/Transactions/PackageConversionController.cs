﻿using CoreERP.BussinessLogic.masterHlepers;
using CoreERP.BussinessLogic.TransactionsHelpers;
using CoreERP.DataAccess;
using CoreERP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.Controllers.Transactions
{
    [ApiController]
    [Route("api/Transactions/PackageConversion")]
    public class PackageConversionController : ControllerBase
    {

        [HttpGet("GetInputcodeList")]
        public async Task<IActionResult> GetInputcodeList()
        {
            var result = await Task.Run(() =>
            {
                try
                {
                    dynamic expando = new ExpandoObject();
                    expando.InputcodeList = new PackageConversionHelper().GetInputcodeList().Select(x => new { ID = x.ProductCode, TEXT = x.ProductName });
                    return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }
        [HttpPost("RegisterPackageConversion")]
        public IActionResult RegisterPackageConversion([FromBody]TblPackageConversion packageconversion)
        {
            if (packageconversion == null)
                return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = "object can not be null" });

            try
            {
                if (new PackageConversionHelper().GetList(packageconversion.InputproductCode).Count() > 0)
                    return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = $"Inputproduct Code {nameof(packageconversion.InputproductCode)} is already exists ,Please Use Different Code " });

                var result = new PackageConversionHelper().Register(packageconversion);
                APIResponse apiResponse;
                if (result != null)
                {
                    apiResponse = new APIResponse() { status = APIStatus.PASS.ToString(), response = result };
                }
                else
                {
                    apiResponse = new APIResponse() { status = APIStatus.FAIL.ToString(), response = "Registration Failed." };
                }

                return Ok(apiResponse);

            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }



        [HttpGet("GetPackageConversionList")]
        public async Task<IActionResult> GetPackageConversionList()
        {
            var result = await Task.Run(() =>
            {
                try
                {
                    var packageconversionList = new PackageConversionHelper().GetList();
                    if (packageconversionList.Count() > 0)
                    {
                        dynamic expdoObj = new ExpandoObject();
                        expdoObj.packageconversionsList = packageconversionList;
                        return Ok(new APIResponse { status = APIStatus.PASS.ToString(), response = expdoObj });
                    }
                    else
                    {
                        return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = "No Data Found." });
                    }
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }
        [HttpGet("GetproductNames/{productcode}")]
        public async Task<IActionResult> GetproductNames(string productcode)
        {
            var result = await Task.Run(() =>
            {
                try
                {
                    dynamic expando = new ExpandoObject();
                    expando.productNames = new PackageConversionHelper().GetproductNames(productcode).Select(bc => new { Name = bc.ProductName, Id = bc.ProductId });
                    return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }

        [HttpPut("UpdatePackageConversionList")]
        public IActionResult UpdatePackageConversionList([FromBody] TblPackageConversion packagesconvrsn)
        {
            if (packagesconvrsn == null)
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = $"{nameof(packagesconvrsn)} cannot be null" });

            try
            {
                var rs = new PackageConversionHelper().Update(packagesconvrsn);
                APIResponse apiResponse;
                if (rs != null)
                {
                    apiResponse = new APIResponse() { status = APIStatus.PASS.ToString(), response = rs };
                }
                else
                {
                    apiResponse = new APIResponse() { status = APIStatus.FAIL.ToString(), response = "Updation Failed." };
                }
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }


        [HttpDelete("DeletePackageConversion/{code}")]
        public IActionResult DeletePackageConversion(string code)
        {
            try
            {
                if (code == null)
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = "code can not be null" });

                var rs = new PackageConversionHelper().Delete(code);
                APIResponse apiResponse;
                if (rs != null)
                {
                    apiResponse = new APIResponse() { status = APIStatus.PASS.ToString(), response = rs };
                }
                else
                {
                    apiResponse = new APIResponse() { status = APIStatus.FAIL.ToString(), response = "Deletion Failed." };
                }
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }
    }
}