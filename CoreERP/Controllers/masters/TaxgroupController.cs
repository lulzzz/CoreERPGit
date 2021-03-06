﻿using CoreERP.BussinessLogic.masterHlepers;
using CoreERP.DataAccess;
using CoreERP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.Controllers.masters
{
    [ApiController]
    [Route("api/masters/Taxgroup")]
    public class TaxgroupController : ControllerBase
    {
        [HttpPost("RegisterTaxgroup")]
        public async Task<IActionResult> RegisterTaxgroup([FromBody]TblTaxGroup taxgroup)
        {
            var result = await Task.Run(() =>
            {
                APIResponse apiResponse = null;
                if (taxgroup == null)
                    return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = "object can not be null" });

                try
                {
                    var taxgrouplist = new TaxgroupHelpers().GetList(taxgroup.TaxGroupCode);
                    if (taxgrouplist.Count() > 0)
                        return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = $"productpacking Code {nameof(taxgrouplist)} is already exists ,Please Use Different Code " });

                    var result = new TaxgroupHelpers().Register(taxgroup);
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
            });
            return result;
        }


        [HttpGet("GetTaxgroupList")]
        public async Task<IActionResult> GetTaxgroupList()
        {
            var result = await Task.Run(() =>
            {
                try
                {
                    dynamic expando = new ExpandoObject();
                    var TaxgroupList = new TaxgroupHelpers().GetList();
                    expando.TaxgroupList = TaxgroupList;
                    return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
                }
                catch (Exception ex)
                {
                    return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
                }
            });
            return result;
        }

        

        [HttpPut("UpdateTaxgroup")]
        public async Task<IActionResult> UpdateTaxgroup([FromBody] TblTaxGroup taxgroup)
        {
            var result = await Task.Run(() =>
            {
                APIResponse apiResponse = null;
                if (taxgroup == null)
                    return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = $"{nameof(taxgroup)} cannot be null" });

                try
                {
                    var rs = new TaxgroupHelpers().Update(taxgroup);
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
            });
            return result;
        }


        [HttpDelete("DeleteTaxgroup/{code}")]
        public async Task<IActionResult> DeleteTaxgroup(string code)
        {
            var result = await Task.Run(() =>
            {
                APIResponse apiResponse = null;
                try
                {
                    if (code == null)
                        return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = "code can not be null" });

                    var rs = new TaxgroupHelpers().Delete(code);
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
            });
            return result;
        }

        [HttpGet("GetProductGroups")]
        [Produces(typeof(List<MaterialGroup>))]
        public async Task<IActionResult> GetProductGroups()
        {
            var result = await Task.Run(() =>
            {
                try
                {
                    dynamic expando = new ExpandoObject();
                    expando.ProductGroupsList = new TaxgroupHelpers().GetProductGroups().Select(pro => new { ID = pro.Code, TEXT = pro.GroupName });
                    return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
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