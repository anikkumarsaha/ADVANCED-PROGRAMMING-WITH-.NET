using System.Web;
using System.Web.Mvc;

namespace Mid_Exam_Scenario_2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
