using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User: AuditableBaseEntity
    {
        public int Id{get;set;}
        public string ApplicationUserId{get;set;}
        public string UserName {get;set;}
        public string FullName{ get; set; }
        public string Email { get; set; }
        public string Website{get;set;}
        public string Description{get;set;}

        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public bool IsCertified{get;set;}

        public string Location{get;set;}

        #region Relations

        public Media Picture { get; set; }
        public Guid? PictureId{ get; set; }
        public Media? Banner{ get; set; }
        public Guid? BannerId{get;set;}
        public ICollection<Post> Posts{get;set;} 
        public ICollection<Follow> Followers{get;set;}
        public ICollection<Follow> Followeds{get;set;}
        public ICollection<UserConversation> UserConversations{get;set;} 

        #endregion

    }

}
