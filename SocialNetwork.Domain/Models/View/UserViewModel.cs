using System;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Models.View
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
        }
    }
}
