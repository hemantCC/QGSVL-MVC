using System.Linq;
using System.Web.Http;
using QGS.BL;
using QGS.Entities.BusinessEntities;
using QGS.Entities.DataEntities;

namespace QGS.WebAPI.Controllers
{
    public class AccountController : ApiController
    {

        private AccountBL ObjectBL = new AccountBL();

        [System.Web.Http.Route("Register")]
        [System.Web.Http.HttpPost]
        public void PostRegister(UserVM obj)
        {
            ObjectBL.Register(obj);
        }

        [System.Web.Http.Route("Login")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Login(UserVM obj)
        {
            using (var _context = new DbQGSVLEntities())
            {
                var v = _context.tblUsers.Where(a => a.UserEmail.Equals(obj.UserEmail) && a.Password.Equals(obj.Password)).FirstOrDefault();
                if (v != null)
                {
                    return Ok();
                }
                return BadRequest();
            }

        }
        [System.Web.Http.Route("GetRoles")]
        [System.Web.Http.HttpGet]
        public string[] GetRoles(string username)
        {
            using (var _context = new DbQGSVLEntities())
            {
                var result = (from user in _context.tblUsers
                              join role in _context.tblRoleManagements on user.RoleId equals role.RoleId
                              where user.UserEmail == username
                              select role.RoleName).ToArray();
                return result;
            }
        }

        [System.Web.Http.Route("GetAllMails")]
        [System.Web.Http.HttpGet]
        public string[] GetAllMails()
        {

            using (var _context = new DbQGSVLEntities())
            {
                var result = (from user in _context.tblUsers
                              select user.UserEmail).ToArray();


                return result;
            }
        }

    }
}

