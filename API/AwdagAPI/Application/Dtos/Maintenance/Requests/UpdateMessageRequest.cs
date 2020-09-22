using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Maintenance.Requests
{
    public class UpdateMessageRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(511)]
        public string Description { get; set; }
    }
}