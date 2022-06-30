using System;
using System.Collections.Generic;
using SMMP.Core.Models.Enums;

namespace SMMP.Core.Models
{
    public class Execution
    {
        public Execution()
        {
        }

        public Execution(string name, DateTime startDate, Execution parent = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Status = ExecutionStatus.InProgress;
            StartDate = startDate;
            ParentId = parent?.Id;
            Parent = parent;
        }

        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public string LogInfo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public ExecutionStatus? Status { get; set; }
        public Execution Parent { get; set; }
        public ICollection<Execution> Children { get; set; }
    }
}
