using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using WebAPIService.App_Start;

namespace WebAPIService.Controllers
{
    public class DataBaseController : ApiController
    {
        public IEnumerable<DataRow> GetAllDtEmployee()
        {
            return InitDB.dtEmployee.AsEnumerable();
        }
    }
}
