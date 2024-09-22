﻿namespace OnlineStationary.DTOs.AuthorDto
{
    public class AuthorUpdateRequestModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}