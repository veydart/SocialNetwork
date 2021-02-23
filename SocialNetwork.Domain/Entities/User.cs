using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SocialNetwork.Domain.Models.Request;

namespace SocialNetwork.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string Name { get; set; }

        public User()
        {
            
        }
        
        public User(UserRequestModel model)
        {
            Name = model.Name;
        }
    }
}