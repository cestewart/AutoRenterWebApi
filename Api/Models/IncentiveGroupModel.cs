using System;

namespace Api.Models
{
    public class IncentiveGroupModel
    {
        public int IncentiveGroupId { get; set; }

        public int LocationId { get; set; }

        public int Priority { get; set; }

        public string Name { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }
    }
}