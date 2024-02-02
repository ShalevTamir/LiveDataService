using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LiveDataService.Mongo.Models.FramesController
{
    public class GetFramesQueryParams
    {
        [BindRequired]
        public long MinTimeStamp { get; set; }
        [BindRequired]
        public long MaxTimeStamp { get; set; }
        [BindRequired]
        public int MaxSamplesInPage { get; set; }
        [BindRequired]
        public int PageNumber { get; set; }
    }
}
