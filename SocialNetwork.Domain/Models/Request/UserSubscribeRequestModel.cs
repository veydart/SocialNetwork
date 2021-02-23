using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Domain.Models.Request
{
    public class UserSubscribeRequestModel
    {
        [Required]
        public Guid SubscriberId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}