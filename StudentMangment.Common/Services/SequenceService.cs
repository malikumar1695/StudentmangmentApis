using Microsoft.EntityFrameworkCore;
using StudentMangment.Common.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StudentMangment.Common.Services
{
    public class SequenceService
    {
        private readonly ApplicationDbContext context;
        public SequenceService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public static readonly string StudentSequence = "StdSeq";
        private int GetNextSequence(string sequenceName)
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

           context.Database.ExecuteSqlRaw($"SELECT @result = (NEXT VALUE FOR dbo.{sequenceName})", result);

            return (int)result.Value;
        }
        public int GetNextStudentSequence()
        {
            return GetNextSequence(StudentSequence);
        }
    }
}
