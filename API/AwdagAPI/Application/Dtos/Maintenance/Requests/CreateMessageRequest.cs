using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Maintenance.Requests
{
    public class CreateMessageRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(511)]
        public string Description { get; set; }
    }
}