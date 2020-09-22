using System;

namespace Application.Dtos.Maintenance.Responses
{
    public class GetMessageResponse
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }
    }
}