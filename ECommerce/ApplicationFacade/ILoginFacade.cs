﻿using ECommerce.Models;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ApplicationFacade
{
    public interface ILoginFacade
    {
        JsonResponse<string> Login(User user);

        JsonResponse<List<User> >GetAllEmployees();
    }
}
