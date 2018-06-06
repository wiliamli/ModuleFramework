using System.Web.Mvc;
using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Framework.Mvc;
using Jwell.Framework.Paging;
using Jwell.Framework.Excel;

namespace Jwell.Sample.Controllers
{
    public class HomeController : BaseController
    {
        private IAdminUserService adminUserService;
        public HomeController(IAdminUserService adminUserService)
        {
            this.adminUserService = adminUserService;
        }

        public ActionResult Index()
        {

            int count = adminUserService.Count();

            ViewBag.Title = "Home Page";

            return View(count);
        }

        public StandardJsonResult<PageResult<AdminUserDto>> List(SearchAdminUserParam request)
        {
            StandardJsonResult<PageResult<AdminUserDto>> result = 
                base.StandardAction<PageResult<AdminUserDto>>(() =>
            {
                PageResult<AdminUserDto> pageResult = adminUserService.GetAdminUserDtos(request);
                return pageResult;
            });

            return result;
        }

        public StandardJsonResult Save(AdminUserDto dto)
        {
            return base.StandardAction(() =>{
                adminUserService.Count();
            });
        }

        public StandardJsonResult GetResult()
        {
            int result = 0;
            StandardJsonResult jsonResult=  base.StandardAction<int>(() =>
            {
                result = adminUserService.Count();
                return result;
            });
            jsonResult.Success = result > 0;
            jsonResult.Message = jsonResult.Success ? "成功" : "失败";
            return jsonResult;
        }

        public void Export(SearchAdminUserParam request)
        {
             DownloadFile("filename",adminUserService.GetAdminUserDtos(request).Value.ToExcelContent("filename"));
        }
    }
}
