using SSC.Core.Api.Models.Entity;
using SSC.StudyRecords.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TQTCallAPI.ViewModel;

namespace TQTCallAPI.Services.User
{
    interface IUserService
    {
        List<UserEntity> GetListUser();
    }
    public class UserService : IUserService
    {
        public List<UserEntity> GetListUser()
        {
            return UserApiHelper.GetAllUsers();
        }
    }
}