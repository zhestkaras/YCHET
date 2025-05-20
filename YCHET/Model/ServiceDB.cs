using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;

namespace YCHET.Model
{
    internal class ServiceDB
    {
         
              
            DbConnection connection;

        private ServiceDB(DbConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Service service)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `service` Values (0, @equipment_id, @description_works, @employee_id, @setting_date, @date_service);select LAST_INSERT_ID();");

                // путем добавления значений в запрос через параметры мы используем экранирование опасных символов
                cmd.Parameters.Add(new MySqlParameter("equipment_id", service.Equipment_id));
                cmd.Parameters.Add(new MySqlParameter("description_works", service.Description_works));
                cmd.Parameters.Add(new MySqlParameter("employee_id", service.Employee_id));
                cmd.Parameters.Add(new MySqlParameter("setting_date", service.Setting_date));
                cmd.Parameters.Add(new MySqlParameter("date_service", service.Date_service));
                /*
                 * id 
                  equipment_id 
                  description_works 
                  employee_id  
                  setting_date 
                  date_service 
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
                        service.Id = id;
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

        internal List<Service> SelectAll()
        {
            List<Service> service = new List<Service>();
            if (connection == null)
                return service;

            if (connection.OpenConnection())
            {                                                                                                                                                                           //////////// как сделать двойной запрос  `employee_id`                                               
                var command = connection.CreateCommand("select service.id, `equipment_id`, `description_works`, `employee_id`, `setting_date`, `date_service` from `service` JOIN `equipment` AS e WHERE e.id = `equipment_id` ");
                try
                {
                    // выполнение запроса, который возвращает результат-таблицу
                    MySqlDataReader dr = command.ExecuteReader();
                    // в цикле читаем построчно всю таблицу
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int equipment_id = dr.GetInt32(1);
                        string description_works = string.Empty;
                        int employee_id = dr.GetInt32(3);
                        DateTime? setting_date = null;
                        DateTime? date_service = null;
                        if (!dr.IsDBNull(2))
                            description_works = dr.GetString(2);                     
                        if (!dr.IsDBNull(4))
                            setting_date = dr.GetDateTime(4);
                        if (!dr.IsDBNull(5))
                            date_service = dr.GetDateTime(5);

                        Equipment equipment = new Equipment();
                        service.Add(new Service
                        {
                            Id = id,
                            Equipment_id = equipment_id,
                            Description_works = description_works,
                            Employee_id = employee_id,
                            Setting_date = setting_date,
                            Date_service = date_service,
                            Equipment= equipment

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return service;
        }

        internal bool Update(Service edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {                                                             
                var mc = connection.CreateCommand($"update `service` set `equipment_id`=@equipment_id,`description_works`=@description_works,`employee_id`=@employee_id,`setting_date`=@setting_date,`date_service`=@date_service where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("equipment_id", edit.Equipment_id));
                mc.Parameters.Add(new MySqlParameter("description_works", edit.Description_works));
                mc.Parameters.Add(new MySqlParameter("employee_id", edit.Employee_id));
                mc.Parameters.Add(new MySqlParameter("setting_date", edit.Setting_date));
                mc.Parameters.Add(new MySqlParameter("date_service", edit.Date_service));


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


        internal bool Remove(Service remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `service` where `id` = {remove.Id}");
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

        static ServiceDB db;
        public static ServiceDB GetDb()
        {
            if (db == null)
                db = new ServiceDB(DbConnection.GetDbConnection());
            return db;
        }
        internal IEnumerable<Service> SelectBy(string search)
        {
            List<Service> service = new List<Service>();
            if (connection == null)
                return service;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `equipment_id`, `description_works`, `employee_id`, " +
                    "`setting_date`, `date_service` from `service` WHERE `equipment_id` like @search  or `description_works` like @search  or `employee_id` like @search " +
                    "or `setting_date` like @search or `date_service` like @search ");
                try
                {
                    command.Parameters.Add(new MySqlParameter("search", "%" + search + "%"));
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {

                        int id = dr.GetInt32(0);
                        int equipment_id = dr.GetInt32(1);
                        string description_works = string.Empty;
                        int employee_id = dr.GetInt32(3);
                        DateTime? setting_date = null;
                        DateTime? date_service = null;

                        if (!dr.IsDBNull(2))
                            description_works = dr.GetString(2);
                        if (!dr.IsDBNull(4))
                            setting_date = dr.GetDateTime(4);
                        if (!dr.IsDBNull(5))
                            date_service = dr.GetDateTime(5);

                        Workers workers = new Workers();
                        Equipment equipment = new Equipment();

                        if (!dr.IsDBNull(6))
                            workers.Last_name = dr.GetString(6);
                        if (!dr.IsDBNull(7))
                            workers.First_name = dr.GetString(7);

                    
                        service.Add(new Service
                        {
                            Id = id,
                            Equipment_id = equipment_id,
                            Description_works = description_works,
                            Employee_id = employee_id,
                            Setting_date = setting_date,
                            Date_service = date_service,
                            Equipment = equipment,
                            Workers = workers
                        });
                    }
                }
                /*
                 * id 
                  equipment_id 
                  description_works 
                  employee_id  
                  setting_date 
                  date_service 
                 * */
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return service;
        }

        internal List<Service> SelectBy(Equipment selectedEquipment)
        {
            List<Service> service = new List<Service>();
            if (connection == null)
                return service;

            if (connection.OpenConnection())
            {                                                                                                                                                                           //////////// как сделать двойной запрос  `employee_id`                                               
                var command = connection.CreateCommand("select s.id, `equipment_id`, `description_works`, `employee_id`, `setting_date`, `date_service` , w.last_name, w.first_name from `service` AS s  JOIN `equipment` AS e on e.id =  s.`equipment_id`  JOIN `workers` w ON w.id = s.employee_id  where s.`equipment_id` = " + selectedEquipment .Id);
                try
                {
                    // выполнение запроса, который возвращает результат-таблицу
                    MySqlDataReader dr = command.ExecuteReader();
                    // в цикле читаем построчно всю таблицу
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int equipment_id = dr.GetInt32(1);
                        string description_works = string.Empty;
                        int employee_id = dr.GetInt32(3);
                        DateTime? setting_date = null;
                        DateTime? date_service = null;


                        if (!dr.IsDBNull(2))
                            description_works = dr.GetString(2);
                        if (!dr.IsDBNull(4))
                            setting_date = dr.GetDateTime(4);
                        if (!dr.IsDBNull(5))
                            date_service = dr.GetDateTime(5);

                        Workers workers = new Workers();
                        Equipment equipment = new Equipment();

                        if (!dr.IsDBNull(6))
                            workers.Last_name = dr.GetString(6);
                        if (!dr.IsDBNull(7))
                            workers.First_name = dr.GetString(7);

                        service.Add(new Service
                        {
                            Id = id,
                            Equipment_id = equipment_id,
                            Description_works = description_works,
                            Employee_id = employee_id,
                            Setting_date = setting_date,
                            Date_service = date_service,
                            Equipment = equipment,
                            Workers = workers

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return service;
        }
    }
}

