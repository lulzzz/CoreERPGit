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
    [Route("api/Reports/EmployeeRegisterReport")]
    [ApiController]
    public class EmployeeRegisterReportController : ControllerBase
    {
        [HttpGet("GetEmployeeRegisterReportData")]
        public async Task<IActionResult> GetEmployeeRegisterReportData(string UserID)
        {
            try
            {
                var employeeRegisterList = await Task.FromResult(ReportsHelperClass.GetEmployeeRegisterReportList(UserID));
                if (employeeRegisterList != null && employeeRegisterList.Count > 0)
                {
                    dynamic expdoObj = new ExpandoObject();
                    expdoObj.employeeRegisterList = employeeRegisterList;
                    return Ok(new APIResponse { status = APIStatus.PASS.ToString(), response = expdoObj });
                }
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = "No Data Found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }
        [HttpGet("EmployeeRegisterExcelReport")]
        public async Task<IActionResult> EmployeeRegisterExcelReport(string UserID)
        {
            try
            {
                var excelReport = await Task.FromResult(ReportsHelperClass.GetEmployeeRegisterReportDataTable(UserID));
                var fileContent = ReportsHelperClass.getExcelFromDatatable(excelReport,"Employee Register Report");
                return File(fileContents: fileContent, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName: "EmployeeRegisterReport.xlsx");
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }
        [HttpGet("EmployeeRegisterCSVReport")]
        public async Task<ActionResult> EmployeeRegisterCSVReport(string UserID)
        {
            try
            {
                var EmployeeRegister = await Task.FromResult(ReportsHelperClass.GetEmployeeRegisterReportDataTable(UserID));
                System.Text.StringBuilder fileContent = new System.Text.StringBuilder();
                IEnumerable<string> columnNames = EmployeeRegister.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                fileContent.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in EmployeeRegister.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                    fileContent.AppendLine(string.Join(",", fields));
                }

                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(fileContent.ToString());
                return File(fileContents: bytes, contentType: "text/csv", fileDownloadName: "EmployeeRegisterReport.csv");
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse { status = APIStatus.FAIL.ToString(), response = ex.Message });
            }
        }
    }
}