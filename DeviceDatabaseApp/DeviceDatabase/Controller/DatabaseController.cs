using DeviceDatabase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceDatabase.Controller
{
    public static class DatabaseController
    {
        public static void InitialSetup()
        {
            if (!System.IO.File.Exists("Database.sqlite"))
            {
                SQLiteConnection.CreateFile("Database.sqlite");

                using (DatabaseContext dc = new DatabaseContext())
                {
                    //dc.Database.ExecuteSqlCommand(
                    //    "CREATE TABLE IF NOT EXISTS 'Device' " +
                    //    "('DeviceId' INTEGER PRIMARY KEY AUTOINCREMENT," +
                    //    " 'Name' TEXT NOT NULL," +
                    //    " 'TypeId' INTEGER," +
                    //    " 'SerialCode' TEXT NOT NULL," +
                    //    " 'Status' INTEGER," +
                    //    " CONSTRAINT name_unique UNIQUE (Name, SerialCode))"
                    //);

                    //dc.Database.ExecuteSqlCommand(
                    //    "CREATE TABLE IF NOT EXISTS 'Calamity' " +
                    //    "('CalamityId' INTEGER PRIMARY KEY AUTOINCREMENT," +
                    //    " 'About' TEXT NOT NULL," +
                    //    " 'Date' date," +
                    //    "'DeviceId' REFERENCES Device(DeviceId) " +
                    //    " ) "
                    //);
                    dc.Database.ExecuteSqlCommand(
                        "CREATE TABLE IF NOT EXISTS 'Device' " +
                        "('DeviceId' INTEGER PRIMARY KEY AUTOINCREMENT," +
                        " 'Name' TEXT NOT NULL," +
                        " 'TypeId' INTEGER," +
                        " 'SerialCode' TEXT NOT NULL," +
                        " 'Status' INTEGER," +
                        " CONSTRAINT name_unique UNIQUE (Name, SerialCode))"
                    );

                    dc.Database.ExecuteSqlCommand(
                        "CREATE TABLE IF NOT EXISTS 'Calamity' " +
                        "('CalamityId' INTEGER PRIMARY KEY AUTOINCREMENT," +
                        " 'DeviceId' Integer," +
                        " 'About' TEXT NOT NULL," +
                        " 'Date' date," +
                        "FOREIGN KEY (CalamityId) REFERENCES Device " +
                        " ) "
                    );
                    dc.SaveChanges();
                }
            }
        }

        public static void AddDevice(Device _Device)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                dc.Devices.Add(_Device);
                dc.SaveChanges();
            }
        }

        public static List<Device> GetDevices()
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                return dc.Devices.Include("CalamityCollection").ToList();
            }
        }

        public static List<Calamity> GetCalamities()
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                return dc.Calamities.ToList();
            }
        }

        public static void AddCalamity(int _DeviceId, Calamity _Calamity)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                Device d = dc.Devices.Find(_DeviceId);

                if (d != null)
                {
                    d.CalamityCollection.Add(_Calamity);
                    dc.Entry(d).State = EntityState.Modified;
                    dc.SaveChanges();
                }
            }
        }
    }
}
