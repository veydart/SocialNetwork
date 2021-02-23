using System;
using System.ComponentModel.DataAnnotations.Schema;
using SocialNetwork.Domain.Models.Request;

namespace SocialNetwork.Domain.Entities
{
    public class UserSubscriber
    {
        public Guid UserId { get; set; }

        public Guid SubscriberId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(SubscriberId))]
        public virtual User Subscriber { get; set; }

        public UserSubscriber()
        {
            
        }

        public UserSubscriber(UserSubscribeRequestModel model)
        {
            UserId = model.UserId;
            SubscriberId = model.SubscriberId;
        }
    }
}
