using Jwell.Framework.Paging;

namespace Jwell.Application.Services.Params
{
    public class SearchAdminUserParam : PageParam
    {
        public string Name { get; set; }

        public string EmployID { get; set; }
    }
}
