using System;

namespace SocialNetwork.Domain.Models.View
{
    public class PopularUserViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int SubscriberCount { get; set; }
    }
}