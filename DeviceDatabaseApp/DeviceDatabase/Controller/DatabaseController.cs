using DeviceDatabase.Model;
using DeviceDatabase.Model.Logging;
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

        private static XmlController<Logbook> xmlController = new XmlController<Logbook>();
        private static string logbookFilePath = "Logbook.xml";
        public static Logbook Logbook;

        public static void InitialSetup()
        {
            if (!System.IO.File.Exists(logbookFilePath))
            {
                Logbook = new Logbook();
                xmlController.save(logbookFilePath, Logbook);
            } else
            {
                Logbook = xmlController.Load(logbookFilePath);
            }

            

            string databaseFilename = "Database.sqlite";

            if (!System.IO.File.Exists(databaseFilename))
            {
                SQLiteConnection.CreateFile(databaseFilename);

                AddLog(string.Format("Created {0} file", databaseFilename));

                using (DatabaseContext dc = new DatabaseContext())
                {
                    //https://sqlite.org/foreignkeys.html

                    // SQL Command for creating table Device
                    dc.Database.ExecuteSqlCommand(
                        "CREATE TABLE IF NOT EXISTS 'Device' " +
                        "('DeviceId' INTEGER PRIMARY KEY AUTOINCREMENT," +
                        " 'Name' TEXT NOT NULL," +
                        " 'DeviceTypeId' INTEGER," +
                        " 'SerialCode' TEXT NOT NULL," +
                        "  FOREIGN KEY (DeviceId) REFERENCES Calamity ON DELETE CASCADE, " +
                        // https://www.techonthenet.com/sqlite/foreign_keys/foreign_null.php
                        "  FOREIGN KEY (DeviceTypeId) REFERENCES DeviceType ON DELETE SET NULL, " +
                        "  CONSTRAINT name_unique UNIQUE (Name, SerialCode))"
                    );
                    AddLog("Created table Device");

                    // SQL Command for creating table Calamity
                    dc.Database.ExecuteSqlCommand(
                        "CREATE TABLE IF NOT EXISTS 'Calamity' " +
                        "('CalamityId' INTEGER PRIMARY KEY AUTOINCREMENT," +
                        " 'DeviceId' INTEGER," +
                        " 'About' TEXT NOT NULL," +
                        " 'IsSolvedSolution' TEXT," +
                        " 'IsSolvedDate' DATE," +
                        " 'Date' DATE NOT NULL," +
                        " 'IsSolved' INTEGER, " +
                        "  FOREIGN KEY (CalamityId) REFERENCES Device ON DELETE CASCADE" +
                        " ) "
                    );
                    AddLog("Created table Calamity");

                    // SQL Command for creating table DeviceType
                    dc.Database.ExecuteSqlCommand(
                        "CREATE TABLE IF NOT EXISTS 'DeviceType' " +
                        "('DeviceTypeId' INTEGER PRIMARY KEY AUTOINCREMENT," +
                        " 'Name' TEXT NOT NULL," +
                        "  CONSTRAINT name_unique UNIQUE (Name))"
                    );
                    AddLog("Created table DeviceType");
                    dc.SaveChanges();
                }

                // ToDo Should be removed before publishing to client :)
                //createInitialValues();

            }
        }

        private static void CreateInitialValues()
        {
            // Next lines of code are initial test values for the database

            DeviceType dt_Server = new DeviceType("Server");
            DeviceType dt_Display = new DeviceType("Display");
            DeviceType dt_Workstation = new DeviceType("Workstation");

            AddDeviceType(dt_Server);
            AddDeviceType(dt_Display);
            AddDeviceType(dt_Workstation);

            List<DeviceType> DeviceTypes = GetDeviceTypes();

            Device d_Server = new Device("Server_042", DeviceTypes.FirstOrDefault(e => e.Name == "Server").DeviceTypeId);
            Device d_Laptop = new Device("Laptop_001", DeviceTypes.FirstOrDefault(e => e.Name == "Workstation").DeviceTypeId);
            Device d_Beamer = new Device("Beamer_123", DeviceTypes.FirstOrDefault(e => e.Name == "Display").DeviceTypeId);

            AddDevice(d_Server);
            AddDevice(d_Laptop);
            AddDevice(d_Beamer);

            AddCalamity(d_Server.DeviceId, new Calamity("Many smoke", new DateTime(1992, 01, 24), true));
            AddCalamity(d_Server.DeviceId, new Calamity("Many smoke", new DateTime(1992, 01, 27), true));
            AddCalamity(d_Server.DeviceId, new Calamity("Many smoke", new DateTime(1992, 02, 04), true));
            AddCalamity(d_Server.DeviceId, new Calamity("Many smoke", new DateTime(1992, 02, 10), false));

            AddCalamity(d_Server.DeviceId, new Calamity("Many smoke", new DateTime(1993, 01, 24), true));
            AddCalamity(d_Server.DeviceId, new Calamity("Many smoke", new DateTime(1992, 01, 27), true));
            AddCalamity(d_Server.DeviceId, new Calamity("Many smoke", new DateTime(1993, 02, 04), true));
            AddCalamity(d_Server.DeviceId, new Calamity("Many smoke", new DateTime(1992, 02, 10), false));

            AddCalamity(d_Server.DeviceId, new Calamity("Many smoke", new DateTime(1997, 08, 31)));

            AddCalamity(d_Laptop.DeviceId, new Calamity("Many smoke", new DateTime(1993, 07, 22)));
            AddCalamity(d_Laptop.DeviceId, new Calamity("Many smokes, Many fire, Much confusion, Many firemen", new DateTime(1997, 08, 31)));
        }

        private static void AddLog(string _Action)
        {
            Logbook.Add(new Log(_Action));
            xmlController.save(logbookFilePath, Logbook);
        }

        public static void AddDevice(Device _Device)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                dc.Devices.Add(_Device);
                dc.SaveChanges();
            }

            AddLog("Added new Device");
        }

        public static void DeleteDevice(int _DeviceId)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                Device d = dc.Devices.Find(_DeviceId);
                dc.Entry(d).State = EntityState.Deleted;
                dc.SaveChanges();
            }

            AddLog("Deleted Device");
        }

        public static void EditDevice(Device _NewDevice)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                Device d = dc.Devices.Find(_NewDevice.DeviceId);
                d.Name = _NewDevice.Name;
                d.DeviceType = dc.DeviceTypes.Find(_NewDevice.DeviceType.DeviceTypeId);
                dc.Entry(d).State = EntityState.Modified;
                dc.SaveChanges();
            }

            AddLog("Edited Device");
        }

        public static void DeleteDeviceType(int _DeviceTypeId)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                DeviceType d = dc.DeviceTypes.Find(_DeviceTypeId);
                dc.Entry(d).State = EntityState.Deleted;
                dc.SaveChanges();
            }

            AddLog("Deleted DeviceType");
        }

        public static bool CheckIfDeviceIsUnique(string _Name)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                return !dc.Devices.ToList().Exists(e => e.Name.ToLower() == _Name.ToLower());
            }
        }

        public static List<Device> GetDevices()
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                return dc.Devices.Include("DeviceType").Include("CalamityCollection").ToList();
            }
        }

        public static List<Device> SearchDevices(string str)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                return dc.Devices.Include("CalamityCollection").Include("DeviceType").Where(e => e.Name.ToLower().Contains(str.ToLower())).ToList();
            }
        }

        public static List<Calamity> GetCalamities()
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                List<Calamity> d = dc.Calamities.Include("Device").ToList();
                return dc.Calamities.Include("Device").ToList();
            }
        }

        public static void AddCalamity(int _DeviceId, Calamity _Calamity)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                Device d = dc.Devices.Include("CalamityCollection").FirstOrDefault(e => e.DeviceId == _DeviceId);

                if (d != null)
                {
                    d.CalamityCollection.Add(_Calamity);
                    dc.Entry(d).State = EntityState.Modified;
                    dc.SaveChanges();

                    AddLog("Added Calamity");
                }
            }
        }

        public static void DeleteCalamity(int _CalamityId)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                Calamity d = dc.Calamities.Find(_CalamityId);
                dc.Entry(d).State = EntityState.Deleted;
                dc.SaveChanges();
            }

            AddLog("Deleted Calamity");
        }

        public static void EditCalamity(Calamity _EditedCalamity)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                Calamity d = dc.Calamities.Find(_EditedCalamity.CalamityId);
                d.About = _EditedCalamity.About;
                d.Date = _EditedCalamity.Date;
                d.IsSolved = _EditedCalamity.IsSolved;
                d.IsSolvedDate = _EditedCalamity.IsSolvedDate;
                d.IsSolvedSolution = _EditedCalamity.IsSolvedSolution;
                dc.Entry(d).State = EntityState.Modified;
                dc.SaveChanges();
            }

            AddLog("Edited Calamity");
        }

        public static void AddDeviceType(DeviceType _DeviceType)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                dc.DeviceTypes.Add(_DeviceType);
                dc.SaveChanges();
            }

            AddLog("Added DeviceType");
        }

        public static List<DeviceType> GetDeviceTypes()
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                return dc.DeviceTypes.ToList();
            }
        }

        public static void EditDeviceType(DeviceType _EditedDeviceType)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                DeviceType d = dc.DeviceTypes.Find(_EditedDeviceType.DeviceTypeId);
                d.Name = _EditedDeviceType.Name;
                dc.Entry(d).State = EntityState.Modified;
                dc.SaveChanges();
            }

            AddLog("Edited DeviceType");
        }

        public static bool CheckIfDeviceTypeIsUnique(string _Name)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                return !dc.DeviceTypes.ToList().Exists(e => e.Name.ToLower() == _Name.ToLower());
            }
        }

        public static List<DeviceType> SearchDeviceTypes(string str)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                return dc.DeviceTypes.Where(e => e.Name.ToLower().Contains(str.ToLower())).ToList();
            }
        }

        public static List<Calamity> SearchCalamities(string str)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                return dc.Calamities.Include("Device").Where(e => e.Device.Name.ToLower().Contains(str.ToLower())).ToList();
            }
        }

        public static Device SearchDeviceBySerialCode(string _SerialCode)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                return dc.Devices.FirstOrDefault(e => e.SerialCode == _SerialCode);
            }
        }

        public static bool CheckIfSerialCodeIsUnique(string str)
        {
            using (DatabaseContext dc = new DatabaseContext())
            {
                return dc.Devices.FirstOrDefault(e => e.SerialCode == str) == null;
            }
        }
    }
}
