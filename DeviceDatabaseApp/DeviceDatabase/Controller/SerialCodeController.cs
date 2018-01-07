using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceDatabase.Controller
{
    public class SerialCodeController
    {
        private static Random rnd = new Random();
        private static string charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private static int charPoolLength = charPool.Length;

        public static string GenerateRandomSerialCode()
        {
            StringBuilder rs = new StringBuilder();

            for (int i = 1; i < 15; i++)
            {
                rs.Append(charPool[rnd.Next(0, charPoolLength)]);
            }

            rs.Insert(3, "-");
            rs.Insert(8, "-");
            rs.Insert(13, "-");

            string result = rs.ToString();

            //Check incase the generated serial code happens to be not unique
            //5.567.902.560 possible combinations, but better safe than sorry
            if (!DatabaseController.CheckIfSerialCodeIsUnique(result))
            {
                result = GenerateRandomSerialCode();
            }

            return result;
        }
    }
}
