using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;

namespace YCHET.Model
{
    class VacationDB
    {
        DbConnection connection;

        private VacationDB(DbConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Vacation vacation)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `vacation` Values (0, @employee_id, @start_vacation , @how_vacation, @sick_vacation, @vacation_end_date);select LAST_INSERT_ID();");

                // путем добавления значений в запрос через параметры мы используем экранирование опасных символов
                cmd.Parameters.Add(new MySqlParameter("employee_id", vacation.Employee_id));
                cmd.Parameters.Add(new MySqlParameter("start_vacation ", vacation.Start_vacation));
                cmd.Parameters.Add(new MySqlParameter("how_vacation", vacation.How_vacation));
                cmd.Parameters.Add(new MySqlParameter("sick_vacation", vacation.Sick_vacation));
                cmd.Parameters.Add(new MySqlParameter("vacation_end_date", vacation.Vacation_end_date));


                /*
                id 
                 employee_id 
                 start_vacation 
                 how_vacation 
                 sick_vacation 
                vacation_end_date
                */

                try
                {
                    // выполняем запрос через ExecuteScalar, получаем id вставленной записи
                    // если нам не нужен id, то в запросе убираем часть select LAST_INSERT_ID(); и выполняем команду через ExecuteNonQuery
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        // назначаем полученный id обратно в объект для дальнейшей работы
                        vacation.Id = id;
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

        internal List<Vacation> SelectAll()
        {
            List<Vacation> vacation = new List<Vacation>();
            if (connection == null)
                return vacation;

            /*
            id 
             employee_id 
             start_vacation 
             how_vacation 
             sick_vacation 
            vacation_end_date
            */
            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select vacation.id, `employee_id`, `start_vacation`, `how_vacation`, `sick_vacation`, `vacation_end_date`, w.last_name, w.first_name from `vacation` join `workers` as w where w.id = `employee_id`");
                try
                {
                    // выполнение запроса, который возвращает результат-таблицу
                    MySqlDataReader dr = command.ExecuteReader();
                    // в цикле читаем построчно всю таблицу
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int employee_id = dr.GetInt32(1);
                        DateTime start_vacation = DateTime.Now;
                        string how_vacation = string.Empty;
                        int  sick_vacation = dr.GetInt32(4);
                        DateTime vacation_end_date = DateTime.Now;

                        if (!dr.IsDBNull(1))
                            employee_id = dr.GetInt32(1);                      
                        if (!dr.IsDBNull(2))
                            start_vacation = dr.GetDateTime(2);
                        if (!dr.IsDBNull(3))
                            how_vacation = dr.GetString(3);
                        if (!dr.IsDBNull(4))
                            sick_vacation = dr.GetInt32(4);
                        if (!dr.IsDBNull(5))
                            vacation_end_date = dr.GetDateTime(5);
                     

                        Workers workers = new Workers
                        {
                            First_name = dr.GetString(7),
                            Last_name = dr.GetString(6)
                        };


                        vacation.Add(new Vacation
                        {
                            Id = id,
                            Employee_id = employee_id,
                            Start_vacation = start_vacation,
                            How_vacation = how_vacation,
                            Sick_vacation = sick_vacation,
                            Workers = workers,
                            Vacation_end_date = vacation_end_date

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return vacation;
        }

        internal bool Update(Vacation edit)
        {
            bool result = false;
            if (connection == null)
                return result;
            /*
            id
             employee_id
             start_vacation
             how_vacation
             sick_vacation

            vacation_end_date
            */
            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `vacation` set `employee_id`=@employee_id,`start_vacation`=@start_vacation,`how_vacation`=@how_vacation,`sick_vacation`=@sick_vacation,`vacation_end_date`=@vacation_end_date where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("employee_id", edit.Employee_id));
                mc.Parameters.Add(new MySqlParameter("start_vacation", edit.Start_vacation));
                mc.Parameters.Add(new MySqlParameter("how_vacation", edit.How_vacation));
                mc.Parameters.Add(new MySqlParameter("sick_vacation", edit.Sick_vacation));
                mc.Parameters.Add(new MySqlParameter("vacation_end_date", edit.Vacation_end_date));

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


        internal bool Remove(Vacation remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `vacation` where `id` = {remove.Id}");
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

        static VacationDB db;
        public static VacationDB GetDb()
        {
            if (db == null)
                db = new VacationDB(DbConnection.GetDbConnection());
            return db;
        }
        internal IEnumerable<Vacation> SelectBy(string search)
        {
            List<Vacation> vacation = new List<Vacation>();
            if (connection == null)
                return vacation;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `employee_id`, `start_vacation`, `how_vacation`, " +
                    "`sick_vacation` from `vacation` WHERE `employee_id` like @search  or `start_vacation` like @search  or `how_vacation` like @search " +
                    "or `sick_vacation` like @search");
                try
                {
                    command.Parameters.Add(new MySqlParameter("search", "%" + search + "%"));
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int employee_id = dr.GetInt32(1);
                        DateTime start_vacation = DateTime.Now;
                        string how_vacation = string.Empty;
                        int sick_vacation = dr.GetInt32(4);
                        DateTime vacation_end_date = DateTime.Now;


                        if (!dr.IsDBNull(1))
                            employee_id = dr.GetInt32(1);
                        if (!dr.IsDBNull(2))
                            start_vacation = dr.GetDateTime(2);
                        if (!dr.IsDBNull(3))
                            how_vacation = dr.GetString(3);
                        if (!dr.IsDBNull(4))
                            sick_vacation = dr.GetInt32(4);
                        if (!dr.IsDBNull(5))
                            vacation_end_date = dr.GetDateTime(5);

                        vacation.Add(new Vacation
                        {
                            Id = id,
                            Employee_id = employee_id,
                            Start_vacation = start_vacation,
                            How_vacation = how_vacation,
                            Sick_vacation = sick_vacation,
                           Vacation_end_date = vacation_end_date

                        });
                    }
                }
                /*
 id 
  employee_id 
  start_vacation 
  how_vacation 
  sick_vacation 
 */
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return vacation;
        }
    }
}

