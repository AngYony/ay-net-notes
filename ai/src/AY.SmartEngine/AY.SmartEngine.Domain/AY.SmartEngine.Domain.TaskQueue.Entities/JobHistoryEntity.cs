using AY.SmartEngine.Domain.TaskQueue.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AY.SmartEngine.Domain.TaskQueue.Entities
{
    public class JobHistoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid JobId { get; set; }
        public JobStatus JobStatus { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
