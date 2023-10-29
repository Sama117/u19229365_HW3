using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace HW3.Models
{
    public class Dataset
    {
        public static List<Points> GetBorrowCountForBooks(DateTime startDate, DateTime endDate)
        {
            string connectionString = "Data Source=DESKTOP-JUHLV2N;Initial Catalog=Library;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Points> result = new List<Points>();

                connection.Open();

                string sqlQuery = @"
                    SELECT b.bookid, b.name AS book_name, COUNT(borrows.borrowId) AS borrow_count
                    FROM books b
                    LEFT JOIN borrows ON b.bookid = borrows.bookid
                    WHERE borrows.broughtDate >= borrows.takenDate
                    GROUP BY b.bookid, b.name
                    ORDER BY borrow_count DESC;
                ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.Add(new SqlParameter("@StartDate", startDate));
                    command.Parameters.Add(new SqlParameter("@EndDate", endDate));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bookId = (int)reader["bookid"];
                            string bookName = (string)reader["book_name"];
                            int borrowCount = (int)reader["borrow_count"];
                            result.Add(new Points(borrowCount, bookName));
                        }
                    }
                }

                return result;
            }
        }
    }
}