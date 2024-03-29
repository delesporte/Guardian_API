﻿using System.ComponentModel.DataAnnotations;

namespace Guardian.Domain.DTO.GuardianTask
{
    public class GuardianCreateTaskDTO
    {
        [Required]
        public string TaksName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Category { get; set; }
        [Required]
        public int Priority { get; set; }
        public bool Status { get; set; } = true;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public int IdResponsible { get; set; }

        public bool IsValid(GuardianCreateTaskDTO task)
        {
            return task.TaksName != null && task.Description != null && task.IdResponsible > 0;
        }
    }
}
