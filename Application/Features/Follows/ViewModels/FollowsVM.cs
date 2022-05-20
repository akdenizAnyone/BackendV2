using System.Collections.Generic;

namespace Application.Features.Follows.ViewModels{
    public class FollowsVM
    {
        public IEnumerable<int> FollowerIds { get; set; }
        public IEnumerable<int> FollowedIds { get; set; }
    }
}