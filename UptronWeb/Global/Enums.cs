﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Global
{
    public static class Enums
    {
        public enum UserRole
        {
            User,
            Admin,
            SuperAdmin
        }
        public enum LoginMessage
        {
            Authenticated = 1,
            InvalidCreadential = 2,
            LoginFailed,
            UserDeleted,
            UserInactive,
            UserBlocked,
            NoResponse
        }

        public enum CrudStatus
        {
            Saved = 1,
            NotSaved = 2,
            Updated,
            NotUpdated,
            Deleted,
            NotDeleted,
            DataNotFound,
            DataAlreadyExist
        }
    }
}