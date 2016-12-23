using System;

namespace Mvc5Application1.Data.Model
{
    public interface ITrackableEntity
    {
        string CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        string ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}