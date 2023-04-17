using AutoserviceBackCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoserviceBackUnitTests
{
    internal static class PublicContext
    {
        public static PracticedbContext context;
        static PublicContext()
        {
            context = new PracticedbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<PracticedbContext>(), new AutoserviceBackCSharp.Singletone.DbConnection("server=185.182.82.8;user=practice_user;password=xS5GRe99v9;database=practicedb"));
        }
    }
}
