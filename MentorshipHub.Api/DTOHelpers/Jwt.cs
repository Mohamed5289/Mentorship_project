﻿
namespace MentorshipHub.Api.DTOHelpers
{
    public  class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInDay { get; set; }
    }
}
