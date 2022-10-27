using System.Web;
using System.Web.Mvc;

namespace Mid_Exam_Scenario_1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
