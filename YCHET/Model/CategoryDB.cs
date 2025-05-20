using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace YCHET.Model
{
    internal class CategoryDB
    {
              
            DbConnection connection;

            private CategoryDB(DbConnection db)
            {
                this.connection = db;
            }

            public bool Insert(Category category)
            {
                bool result = false;
                if (connection == null)
                    return result;

                if (connection.OpenConnection())
                {
                    MySqlCommand cmd = connection.CreateCommand("insert into `category` Values (0, @title);select LAST_INSERT_ID();");

                    // путем добавления значений в запрос через параметры мы используем экранирование опасных символов
                    cmd.Parameters.Add(new MySqlParameter("Title", category.Title));
                   

                try
                    {
                        // выполняем запрос через ExecuteScalar, получаем id вставленной записи
                        // если нам не нужен id, то в запросе убираем часть select LAST_INSERT_ID(); и выполняем команду через ExecuteNonQuery
                        int id = (int)(ulong)cmd.ExecuteScalar();
                        if (id > 0)
                        {
                            MessageBox.Show(id.ToString());
                        // назначаем полученный id обратно в объект для дальнейшей работы
                            category.Id = id;
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

            internal List<Category> SelectAll()
            {
                List<Category> category = new List<Category>();
                if (connection == null)
                    return category;

                if (connection.OpenConnection())
                {
                    var command = connection.CreateCommand("select `id`, `title` from `category` ");
                    try
                    {
                        // выполнение запроса, который возвращает результат-таблицу
                        MySqlDataReader dr = command.ExecuteReader();
                        // в цикле читаем построчно всю таблицу
                        while (dr.Read())
                        {

                        int id = dr.GetInt32(0);
                        string title = string.Empty;

                        if (!dr.IsDBNull(1))
                            title = dr.GetString(1);
                        
                        category.Add(new Category
                        {
                               Id = id,
                               Title = title,
                                
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                connection.CloseConnection();
                return category;
            }

            internal bool Update(Category edit)
            {
                bool result = false;
                if (connection == null)
                    return result;

                if (connection.OpenConnection())
                {                                                             ///T t  
                    var mc = connection.CreateCommand($"update `category` set `title`=@title where `id` = {edit.Id}");
                    mc.Parameters.Add(new MySqlParameter("fname", edit.Title));
                   

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


            internal bool Remove(Category remove)
            {
                bool result = false;
                if (connection == null)
                    return result;

                if (connection.OpenConnection())
                {
                    var mc = connection.CreateCommand($"delete from `category` where `id` = {remove.Id}");
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
        internal IEnumerable<Category> SelectBy(string search)
    {
            List<Category> category = new List<Category>();
            if (connection == null)
                return category;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `title` from `category` WHERE `title` like @search");
                try
                {
                    command.Parameters.Add(new MySqlParameter("search", "%" + search + "%"));
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                      

                        category.Add(new Category
                        {
                            Id = id,
                            Title = title,
                           
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return category;
        }

        static CategoryDB db;
            public static CategoryDB GetDb()
            {
                if (db == null)
                    db = new CategoryDB(DbConnection.GetDbConnection());
                return db;
            }
        }
    }

