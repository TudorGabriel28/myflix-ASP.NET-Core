﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RepositoryUser : Repository<User>, IRepository<User>
    {
        public RepositoryUser(MyflixContext context) : base(context) { } 
    }
}