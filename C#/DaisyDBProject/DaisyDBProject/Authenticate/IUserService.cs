﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaisyDBProject
{
    public interface IUserService
    {
        bool IsValid(LoginRequestDTO req);
    }
}
