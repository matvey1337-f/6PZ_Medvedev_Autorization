using System.Collections.Generic;
using _2_wpf_medvedev_.Services;

namespace _2_wpf_medvedev_.Models
{
    public class RullEntities
    {
        public static List<User> Users = new List<User>
        {
            new User { UserLogin = "admin", UserPassword = Hash.HashPassword("123"), RoleName = "Клиент" }
        };
    }
}