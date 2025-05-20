using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;
using static System.Reflection.Metadata.BlobBuilder;

namespace YCHET.Model
{
    internal class EquipmentDB

    {
        DbConnection connection;

        private EquipmentDB(DbConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Equipment equipment)
        {
            bool result = false;
            if (connection == null)
                return result;
            /*
id 
code 
factory_number 
id_category 
type_work 
status 
type 
date_last_serv
scheduled_date 
         */
            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `equipment` Values (0, @code, @factory_number, @id_category, @type_work, @status, @type, @date_last_serv, @scheduled_date);select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("Code", equipment.Code));
                cmd.Parameters.Add(new MySqlParameter("Factory_number", equipment.Factory_number));
                cmd.Parameters.Add(new MySqlParameter("Id_category", equipment.Id_category));
                cmd.Parameters.Add(new MySqlParameter("Type_work", equipment.Type_work));
                cmd.Parameters.Add(new MySqlParameter("Status", equipment.Status));
                cmd.Parameters.Add(new MySqlParameter("Type", equipment.Type));
                cmd.Parameters.Add(new MySqlParameter("Date_last_serv", equipment.Date_last_serv));
                cmd.Parameters.Add(new MySqlParameter("Scheduled_date", equipment.Scheduled_date));




                try
                {
                    
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                       
                        equipment.Id = id;
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
      
        internal List<Equipment> SelectAll()
        {
            List<Equipment> equipment = new List<Equipment>();
            if (connection == null)
                return equipment;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select equipment.id, `code`, `factory_number`, `id_category`, `type_work`, `status`, `type`, `date_last_serv`, `scheduled_date`, c.title from `equipment` join `category` as c  where c.id = `id_category` ");
                try
                {
                    
                    MySqlDataReader dr = command.ExecuteReader();
                   
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int id_category = dr.GetInt32(3);
                        string code = string.Empty;
                        string factory_number = string.Empty;
                        string type_work = string.Empty;
                        string status = string.Empty;
                        string type = string.Empty;
                        DateTime date_last_serv = DateTime.Now;
                        DateTime scheduled_date = DateTime.Now;
                        string title = string.Empty;


                        if (!dr.IsDBNull(1))
                            code = dr.GetString(1);
                        if (!dr.IsDBNull(2))
                            factory_number = dr.GetString(2);
                        if (!dr.IsDBNull(4))
                            type_work = dr.GetString(4);
                        if (!dr.IsDBNull(5))
                            status = dr.GetString(5);
                        if (!dr.IsDBNull(6))
                            type = dr.GetString(6);
                        if (!dr.IsDBNull(7))
                            date_last_serv = dr.GetDateTime(7);
                        if (!dr.IsDBNull(8))
                            scheduled_date = dr.GetDateTime(8);
                        if (!dr.IsDBNull(9))
                            title = dr.GetString(9);

                        Category category = new Category();
                        equipment.Add(new Equipment
                        {
                            Id = id,
                            Id_category = id_category,
                            Code = code,
                            Factory_number = factory_number,
                            Type_work = type_work,
                            Status = status,
                            Type = type,
                            Date_last_serv = date_last_serv,
                            Scheduled_date = scheduled_date,
                            Category = category
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return equipment;
        }

        internal bool Update(Equipment edit)
        {
            bool result = false;
            if (connection == null)
                return result;
            /*
  id 
  code 
  factory_number 
  id_category 
  type_work 
  status 
  type 
  date_last_serv
  scheduled_date 
             */
            if (connection.OpenConnection())
            {                                                             
                var mc = connection.CreateCommand($"update `equipment` set `code`=@code, `factory_number`=@factory_number, `id_category`=@id_category, `type_work`=@type_work, `status`=@status, `type`=@type, `date_last_serv`=@date_last_serv, `scheduled_date`=@scheduled_date where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("code", edit.Code));
                mc.Parameters.Add(new MySqlParameter("factory_number", edit.Factory_number));
                mc.Parameters.Add(new MySqlParameter("id_category", edit.Id_category));
                mc.Parameters.Add(new MySqlParameter("type_work", edit.Type_work));
                mc.Parameters.Add(new MySqlParameter("status", edit.Status));
                mc.Parameters.Add(new MySqlParameter("type", edit.Type));
                mc.Parameters.Add(new MySqlParameter("date_last_serv", edit.Date_last_serv));
                mc.Parameters.Add(new MySqlParameter("scheduled_date", edit.Scheduled_date));
               


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


        internal bool Remove(Equipment remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `equipment` where `id` = {remove.Id}");
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

        static EquipmentDB db;
        public static EquipmentDB GetDb()
        {
            if (db == null)
                db = new EquipmentDB(DbConnection.GetDbConnection());
            return db;
        }

        internal IEnumerable<Equipment> SelectBy(string search)
        {
            List<Equipment> equipment = new List<Equipment>();
            if (connection == null)
                return equipment;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `code`, `factory_number`, `id_category`, " +
                    "`type_work`, `status`, `type`, `date_last_serv`, `scheduled_date` from `student` WHERE `code` like @search  or `factory_number` like @search  or `id_category` like @search " +
                    "or `type_work` like @search or `status` like @search or `type` like @search or `date_last_serv` like @search or `scheduled_date` like @search");
                try
                {
                    command.Parameters.Add(new MySqlParameter("search", "%" + search + "%"));
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {

                        int id = dr.GetInt32(0);
                        int id_category = dr.GetInt32(3);
                        string code = string.Empty;
                        string factory_number = string.Empty;
                        string type_work = string.Empty;
                        string status = string.Empty;
                        string type = string.Empty;
                        DateTime date_last_serv = DateTime.Now;
                        DateTime scheduled_date = DateTime.Now;


                        if (!dr.IsDBNull(1))
                            code = dr.GetString(1);
                        if (!dr.IsDBNull(2))
                            factory_number = dr.GetString(2);
                        if (!dr.IsDBNull(4))
                            type_work = dr.GetString(4);
                        if (!dr.IsDBNull(5))
                            status = dr.GetString(5);
                        if (!dr.IsDBNull(6))
                            type = dr.GetString(6);
                        if (!dr.IsDBNull(7))
                            date_last_serv = dr.GetDateTime(7);
                        if (!dr.IsDBNull(8))
                            scheduled_date = dr.GetDateTime(8);

                        equipment.Add(new Equipment
                        {
                            Id = id,
                            Id_category = id_category,
                            Code = code,
                            Factory_number = factory_number,
                            Type_work = type_work,
                            Status = status,
                            Type = type,
                            Date_last_serv = date_last_serv,
                            Scheduled_date = scheduled_date

                        });
                    }
                }
                /*
 id 
 code 
 factory_number 
 id_category 
 type_work 
 status 
 type 
 date_last_serv
 scheduled_date 
            */
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return equipment;
        }
    }
}

