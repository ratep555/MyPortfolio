using System;

namespace Core.Dtos
{
    public class UserToReturnDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}