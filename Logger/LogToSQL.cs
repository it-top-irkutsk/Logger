using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace Logger
{
    
    public class LogToSQL
    {
        private const string nameBD = "Data Source=BD.sqlite";
        private SqliteConnection bd;
        
        public LogToSQL()
        {
            try
            {
                bd = new SqliteConnection("nameBD");
                bd.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("e");
                throw new Exception($"{e}");
            }
            //TODO  Написать создание таблицы если её нет
            //var CreateTable = "CREATE TABLE Log (id integer PRIMARY KEY AUTOINCREMENT NOT NULL,time char(50) NOT NULL,type char(20) NOT NULL,message char(100) NOT NULL)";
            //SqlRequest(CreateTable);
        }

        private void SqlRequest(string message)
        {
            using (SqliteCommand command = new SqliteCommand())
            {
                try
                {
                    command.Connection = bd;
                    command.CommandText = "message";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("e");
                    throw new Exception($"{e}");
                }
            }
        }

        public void Log(string type, string message)
        {
            var sqlRequest = $"INSERT INTO Log (time,type,message) VALUES ('{DateTime.Now:G}', '{type}', '{message}');";
            SqlRequest(sqlRequest);
        }

        public void Log(LogType type, string message)
        {
            Log(type.ToString(), message);
        }

        public void Info(string message)
        {
            Log(LogType.Info.ToString(), message);
        }
        
        public void Success(string message)
        {
            Log(LogType.Success.ToString(), message);
        }
        
        public void Warning(string message)
        {
            Log(LogType.Warning.ToString(), message);
        }
        
        public void Error(string message)
        {
            Log(LogType.Error.ToString(), message);
        }
        
    }
}