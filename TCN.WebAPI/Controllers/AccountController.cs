using System;
using System.Web.Http;

namespace TCN.WebAPI.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        #region Constructor

        public AccountController()
        {

        }

        #endregion



        [HttpGet]
        [Route("public")]
        public IHttpActionResult GetPublicData()
        {
            try
            {
                return Ok("Public data...");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("private")]
        public IHttpActionResult GetPrivateData()
        {
            try
            {
                var userId = User.Identity.Name;

                return Ok(userId);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
