using Microsoft.Data.SqlClient;

class Test
{
    public Test() { }

    public void test()
    {

        using (SqlConnection conn = new SqlConnection("your_connection_string"))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users", conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["Username"].ToString());

                    }
                }
            }
        }
    }
}

// using (SqlConnection conn = new SqlConnection("your_connection_string"))
// {
//     conn.Open();
//     using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users", conn))
//     {
//         using (SqlDataReader reader = cmd.ExecuteReader())
//         {
//             while (reader.Read())
//             {
//                 Console.WriteLine(reader["Username"].ToString());

//             }
//         }
//     }
// }