using System;
using System.Configuration;
using System.Web.Http;
using SSC.Core.Api.Models.Entity;
using SSC.Core.Api.Models.SSC;
using System.Collections.Generic;
using System.Web;
using SSC.StudyRecords.Common.Util;

namespace SSC.StudyRecords.Common
{
	public sealed class UserApiHelper
	{
		#region User Types
		public static List<UserTypeEntity> GetAllUserTypes()
		{
			return WebApiHelper.GetListCoreObject<UserTypeEntity>("User/GetUserTypeAll", new TheSscRequest());
		}
		public static List<UserTypeEntity> GetUserTypeByUser(Guid userid)
		{
			return WebApiHelper.GetListCoreObject<UserTypeEntity>("User/GetUserTypeByUser", new TheSscRequest(userid));
		}
		public static List<UserTypeEntity> GetUserTypeByUserInRole(Guid userid)
		{
			return WebApiHelper.GetListCoreObject<UserTypeEntity>("User/GetUserTypeByUserInRole", new TheSscRequest(userid));
		}
		public static bool CheckUserIsSysAdmin(Guid userid)
		{
			return WebApiHelper.GetCoreObject<bool>("User/CheckUserIsSysAdmin", new TheSscRequest(userid));
		}
		public static bool CheckUserIsDptAdmin(Guid userid)
		{
			return WebApiHelper.GetCoreObject<bool>("User/CheckUserIsDptAdmin", new TheSscRequest(userid));
		}
		public static bool CheckUserIsDivAdmin(Guid userid)
		{
			return WebApiHelper.GetCoreObject<bool>("User/CheckUserIsDivAdmin", new TheSscRequest(userid));
		}
		public static bool CheckUserIsSchAdmin(Guid userid)
		{
			return WebApiHelper.GetCoreObject<bool>("User/CheckUserIsSchAdmin", new TheSscRequest(userid));
		}
		public static bool CheckUserIsTeacher(Guid userid)
		{
			return WebApiHelper.GetCoreObject<bool>("User/CheckUserIsTeacher", new TheSscRequest(userid));
		}
		public static bool CheckUserIsStudent(Guid userid)
		{
			return WebApiHelper.GetCoreObject<bool>("User/CheckUserIsStudent", new TheSscRequest(userid));
		}
		#endregion

		#region Users
		public static UserEntity GetUserById(Guid id)
		{
			return WebApiHelper.GetCoreObject<UserEntity>("User/GetUserByUserId", new TheSscRequest(id));
		}

		public static UserEntity GetUserByUsername(string username)
		{
			return WebApiHelper.GetCoreObject<UserEntity>("User/GetUserByUsername", new TheSscRequest(username));
		}

		public static UserProfileEntity GetUserProfile(string sscid)
		{
			return WebApiHelper.GetCoreObject<UserProfileEntity>("User/GetUserProfile", new TheSscRequest(sscid));
		}

		public static List<UserEntity> GetAllUsers()
		{
			return WebApiHelper.GetListCoreObject<UserEntity>("User/GetUserAll", new TheSscRequest());
		}

		public static List<UserEntity> GetUserRange(int pageIndex, int pageSize)
		{
			return WebApiHelper.GetListCoreObject<UserEntity>("User/GetUserAll", new TheSscRequest(pageIndex, pageSize));
		}

		public static List<UserEntity> GetAllUserByType(Guid type)
		{
			return WebApiHelper.GetListCoreObject<UserEntity>("User/GetUserByType", new TheSscRequest(type));
		}

		public static List<UserEntity> GetAllUserByType(Guid type, Guid userid)
		{
			return WebApiHelper.GetListCoreObject<UserEntity>("User/GetUserByTypeAndUser", new TheSscRequest(type, userid));
		}

		public static List<UserEntity> GetUserRangeByType(Guid type, int limit)
		{
			return WebApiHelper.GetListCoreObject<UserEntity>("User/GetUserAll", new TheSscRequest(type, limit));
		}

		public static bool Login(string username, string password)
		{
			bool result = false;
			var user = new UserEntity { Username = username, Password = password };
			var session = new UserSessionEntity { Username = username, SessionId = HttpContext.Current.Session.SessionID, IpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() };

			var response = WebApiHelper.SscCoreRequest("User/Login", new TheSscRequest(user, session) { AccessKey = Guid.NewGuid().ToString() });
			if (response.IsNotNull())
			{
				if (response.ResponseCode.Equals(TheSscResultCode.LoginSucceed))
				{
					result = true;
				}
			}
			return result;
		}
		#endregion
	}
}
