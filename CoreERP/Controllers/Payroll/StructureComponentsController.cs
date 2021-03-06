﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using CoreERP.BussinessLogic.Payroll;
using CoreERP.DataAccess;
using CoreERP.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreERP.Controllers.Payroll
{
    [ApiController]
    [Route("api/payroll/StructureComponents")]
    public class StructureComponentsController : ControllerBase
    {
        [HttpGet("GetStructuresList")]
        public IActionResult GetStructuresList()
        {
            try
            {
                var structuresList = StructureComponentsHelper.GetListOfStructures();
                if (structuresList.Count > 0)
                {
                    dynamic expdoObj = new ExpandoObject();
                    expdoObj.structuresList = structuresList;
                    return Ok(new APIResponse { status = APIStatus.PASS.ToString(), response = expdoObj });
                }
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = "No Data Found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }

        [HttpGet("GetComponentsList")]
        public IActionResult GetComponentsList()
        {
            try
            {
                dynamic expando = new ExpandoObject();
                expando.ComponentsList = StructureComponentsHelper.GetComponentList().Select(x => new { ID = x.ComponentCode, TEXT = x.ComponentName });
                return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }

        [HttpGet("GetStructureCreationList")]
        public IActionResult GetStructureCreationList()
        {
            try
            {
                dynamic expando = new ExpandoObject();
                expando.ComponentsList = StructureComponentsHelper.GetStructureCreationList().Select(x => new { ID = x.StructureCode, TEXT = x.StructureName });
                return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }

        [HttpGet("GetPFList")]
        public IActionResult GetPFList()
        {
            try
            {
                dynamic expando = new ExpandoObject();
                expando.PFList = StructureComponentsHelper.GetPFList().Select(x => new { ID = x.PftypeName, TEXT = x.PftypeName });
                return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = expando });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }

        [HttpPost("RegisterStructure")]
        public IActionResult RegisterStructure([FromBody]List<StructureComponents> structureComponents)
        {

            if (structureComponents == null)
                return Ok(new APIResponse() { status = APIStatus.FAIL.ToString(), response = "Request can not be null" });

            try
            {
                APIResponse apiResponse = null;
                List<StructureComponents> result = StructureComponentsHelper.Register(structureComponents);
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
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }

        }

        [HttpPut("UpdateStructure")]
        public IActionResult UpdateStructure([FromBody] List<StructureComponents> structureComponents)
        {

            if (structureComponents == null)
                return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = "Request cannot be null" });
            try
            {
                APIResponse apiResponse = null;

                List<StructureComponents> result = StructureComponentsHelper.Update(structureComponents);
                if (result != null)
                {
                    apiResponse = new APIResponse() { status = APIStatus.PASS.ToString(), response = result };
                }
                else
                {
                    apiResponse = new APIResponse() { status = APIStatus.FAIL.ToString(), response = "Updation Failed." };
                }
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }

        [HttpDelete("DeleteStructure/{code}")]
        public IActionResult DeleteStructure(string code)
        {
            if (code == null)
                return Ok(new APIResponse() { status = APIStatus.PASS.ToString(), response = "Request can not be null" });

            try
            {
                var result = StructureComponentsHelper.DeleteStructures(code);
                APIResponse apiResponse;
                if (result != null)
                {
                    apiResponse = new APIResponse() { status = APIStatus.PASS.ToString(), response = result };
                }
                else
                {
                    apiResponse = new APIResponse() { status = APIStatus.FAIL.ToString(), response = "Deletion Failed." };
                }
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }

    }
}