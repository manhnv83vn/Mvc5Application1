using Mvc5Application1.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Mvc5Application1.Areas.Administration.ViewModels
{
    public class ImportNormViewModel
    {
        [Display(ResourceType = typeof(Label), Name = "Discipline")]
        public string DisciplineId { get; set; }

        public List<SelectListItem> Disciplines { get; set; }

        public string Message { get; set; }
    }
}