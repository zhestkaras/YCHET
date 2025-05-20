using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;

namespace YCHET.Model
{
    internal class WorkersDB
    {
        DbConnection connection;

        private WorkersDB(DbConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Workers workers)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `workers` Values (0, @last_name, @first_name, @patronymic, @post, @email, @phone_number);select LAST_INSERT_ID();");

                // путем добавления значений в запрос через параметры мы используем экранирование опасных символов
                cmd.Parameters.Add(new MySqlParameter("last_name", workers.Last_name));
                cmd.Parameters.Add(new MySqlParameter("first_name", workers.First_name));
                cmd.Parameters.Add(new MySqlParameter("patronymic", workers.Patronymic));
                cmd.Parameters.Add(new MySqlParameter("post", workers.Post));
                cmd.Parameters.Add(new MySqlParameter("email", workers.Email));
                cmd.Parameters.Add(new MySqlParameter("phone_number", workers.Phone_number));
                /*
                   id 
                   last_name
                   first_name
                   patronymic 
                   post 
                   phone_number 
                   email 
                 * */

                try
                {
                    // выполняем запрос через ExecuteScalar, получаем id вставленной записи
                    // если нам не нужен id, то в запросе убираем часть select LAST_INSERT_ID(); и выполняем команду через ExecuteNonQuery
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        // назначаем полученный id обратно в объект для дальнейшей работы
                        workers.Id = id;
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Запись не добавлена");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }

        internal List<Workers> SelectAll()
        {
            List<Workers> workers = new List<Workers>();
            if (connection == null)
                return workers;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `last_name`, `first_name`, `patronymic`, `post`, `email`, `phone_number` from `workers` ");
                try
                {
                    // выполнение запроса, который возвращает результат-таблицу
                    MySqlDataReader dr = command.ExecuteReader();
                    // в цикле читаем построчно всю таблицу
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string last_name = string.Empty;
                        string first_name = string.Empty;
                        string patronymic = string.Empty;
                        string post = string.Empty;                     
                        string email = string.Empty;
                        string phone_number = dr.GetString(6);

                        if (!dr.IsDBNull(1))
                            last_name = dr.GetString(1);
                        if (!dr.IsDBNull(2))
                            first_name = dr.GetString(2);
                        if (!dr.IsDBNull(3))
                            patronymic = dr.GetString(3);
                        if (!dr.IsDBNull(4))
                            post = dr.GetString(4);
                        if (!dr.IsDBNull(5))
                            email = dr.GetString(5);


                        workers.Add(new Workers
                        {
                            Id = id,
                            Last_name = last_name,
                            First_name = first_name,
                            Patronymic = patronymic,
                            Post = post, 
                            Email = email,
                            Phone_number = phone_number
                           

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return workers;
        }

        internal bool Update(Workers edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `workers` set `last_name`=@last_name,`first_name`=@first_name,`patronymic`=@patronymic,`post`=@post, `email`=@email, `phone_number`=@phone_number where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("last_name", edit.Last_name));
                mc.Parameters.Add(new MySqlParameter("first_name", edit.First_name));
                mc.Parameters.Add(new MySqlParameter("patronymic", edit.Patronymic));
                mc.Parameters.Add(new MySqlParameter("post", edit.Post)); 
                mc.Parameters.Add(new MySqlParameter("email", edit.Email));
                mc.Parameters.Add(new MySqlParameter("phone_number", edit.Phone_number));

                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }


        internal bool Remove(Workers remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `workers` where `id` = {remove.Id}");
                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }

        static WorkersDB db;
        public static WorkersDB GetDb()
        {
            if (db == null)
                db = new WorkersDB(DbConnection.GetDbConnection());
            return db;
        }
        internal IEnumerable<Workers> SelectBy(string search)
        {
            List<Workers> workers = new List<Workers>();
            if (connection == null)
                return workers;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `last_name`, `first_name`, `patronymic`, " +
                    "`post`, `phone_number`, `email` from `workers` WHERE `last_name` like @search  or `first_name` like @search  or `patronymic` like @search " +
                    "or `post` like @search or `phone_number` like @search or `email` like @search");
                try
                {
                    command.Parameters.Add(new MySqlParameter("search", "%" + search + "%"));
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {

                        int id = dr.GetInt32(0);
                        string last_name = string.Empty;
                        string first_name = string.Empty;
                        string patronymic = string.Empty;
                        string post = string.Empty;
                        string phone_number = dr.GetString(0);
                        string email = string.Empty;


                        if (!dr.IsDBNull(1))
                            last_name = dr.GetString(1);
                        if (!dr.IsDBNull(2))
                            first_name = dr.GetString(2);
                        if (!dr.IsDBNull(3))
                            patronymic = dr.GetString(3);
                        if (!dr.IsDBNull(4))
                            post = dr.GetString(4);
                        if (!dr.IsDBNull(6))
                            email = dr.GetString(6);

                        workers.Add(new Workers
                        {
                            Id = id,
                            Last_name = last_name,
                            First_name = first_name,
                            Patronymic = patronymic,
                            Post = post,
                            Phone_number = phone_number,
                            Email = email

                        });
                    }
                }
                /*
                    id 
                    last_name
                    first_name
                    patronymic 
                    post 
                    phone_number 
                    email 
                  * */
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return workers;
        }
    }
}

