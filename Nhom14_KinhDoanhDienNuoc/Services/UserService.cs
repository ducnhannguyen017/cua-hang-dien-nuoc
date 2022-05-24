using MySql.Data.MySqlClient;
using Nhom14_KinhDoanhDienNuoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom14_KinhDoanhDienNuoc.Services
{
    public class UserService
    {
        string myConnectionString = "server=localhost;database=nhom14_qlcuahangdiennuoc;uid=root;pwd=;";
        public List<User> getUserByRole(string role)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("select * from users where role = '"+role+"'", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<User> list = new List<User>();
                        while (reader.Read())
                        {
                            list.Add(
                                new User()
                                {
                                    id = Int32.Parse(reader.GetString("id")),
                                    ten = reader.GetString("ten"),
                                    username = reader.GetString("username"),
                                    password = reader.GetString("password"),
                                    sdt = reader.GetString("sdt"),
                                    email = reader.GetString("email"),
                                    diachi = reader.GetString("diachi"),
                                    role = reader.GetString("role"),
                                });
                        }
                        return list;
                    }
                }
                conn.Close();
            }
        }

        public List<User> insertNhanVien(User user)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "insert into users (username, password, ten, sdt, email, diachi, role) values ('" + user.username + "', '" + user.password + "','" + user.ten + "','" + user.sdt + "','" + user.email + "','" + user.diachi + "','ROLE_EMPLOYEE')";
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<User> list = new List<User>();
                        while (reader.Read())
                        {
                            list.Add(
                                new User()
                                {
                                    id = Int32.Parse(reader.GetString("id")),
                                    ten = reader.GetString("ten"),
                                    username = reader.GetString("username"),
                                    password = reader.GetString("password"),
                                    sdt = reader.GetString("sdt"),
                                    email = reader.GetString("email"),
                                    diachi = reader.GetString("diachi"),
                                    role = reader.GetString("role"),
                                });
                        }
                        return list;
                    }
                }
                conn.Close();
            }
        }
        public List<User> updateNhanVien(User user, int id)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "update users set username='"+user.username+"',ten='"+user.ten+"', password='"+user.password+"',sdt='"+user.sdt+"', email='"+user.email+"',diachi='"+user.diachi+"' where id ='"+id+"'";
                Console.WriteLine(str);
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<User> list = new List<User>();
                        while (reader.Read())
                        {
                            list.Add(
                                new User()
                                {
                                    id = Int32.Parse(reader.GetString("id")),
                                    ten = reader.GetString("ten"),
                                    username = reader.GetString("username"),
                                    password = reader.GetString("password"),
                                    sdt = reader.GetString("sdt"),
                                    email = reader.GetString("email"),
                                    diachi = reader.GetString("diachi"),
                                    role = reader.GetString("role"),
                                });
                        }
                        return list;
                    }
                }
                conn.Close();
            }
        }
        public List<User> deleteNhanVien(int id)
        {
            using (var conn = new MySqlConnection(myConnectionString))
            {
                string str = "delete from users where id ='"+id+"'";
                conn.Open();
                using (var cmd = new MySqlCommand(str, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<User> list = new List<User>();
                        while (reader.Read())
                        {
                            list.Add(
                                new User()
                                {
                                    id = Int32.Parse(reader.GetString("id")),
                                    ten = reader.GetString("ten"),
                                    username = reader.GetString("username"),
                                    password = reader.GetString("password"),
                                    sdt = reader.GetString("sdt"),
                                    email = reader.GetString("email"),
                                    diachi = reader.GetString("diachi"),
                                    role = reader.GetString("role"),
                                });
                        }
                        return list;
                    }
                }
                conn.Close();
            }
        }
    }
}
