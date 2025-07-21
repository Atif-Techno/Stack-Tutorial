using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UmbrellaCorp.Infrastructure.Repositories
{
    [Serializable]
    public class UmbrellaLib
    {
        public enum DocumentTypes
        {
            Purchase_order = 6,
            GRN = 25,
            Purchase_Enquery = 62,
            Supplier_Quotation = 66,
            Supplier_Invoice = 8

        }
        public static string DisplayIndianCurrency(float amount)
        {
            return DisplayIndianCurrency(Convert.ToInt32(amount));
        }
        public static string DisplayIndianCurrency(decimal amount)
        {
            return DisplayIndianCurrency(Convert.ToInt32(amount));
        }
        public static string DisplayIndianCurrency(int amount)
        {
            try
            {
                string value = amount.ToString();
                decimal parsed = decimal.Parse(value, CultureInfo.InvariantCulture);
                CultureInfo hindi = new("hi-IN");
                value = string.Format(hindi, "{0:c}", parsed);
                return value.Split(new Char[] { '.' }, StringSplitOptions.None)[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static string ConvertTerminal(string terminal)
        {
            try
            {
                if (terminal != null)
                {
                    terminal = "T" + terminal.Split(new Char[] { ' ' }, StringSplitOptions.None)[1];
                }
                return terminal;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public static string ConvertDate(string Date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(Date.Split(new Char[] { 'T' }, StringSplitOptions.None)[0]);
                dt = new DateTime(dt.Year, dt.Month, dt.Day);
                //Date = dt.ToString("ddd','MMM' 'dd' 'yyyy");
                Date = dt.ToString("MMM' 'dd");
                return Date;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public static string ConvertCustomDateFormat(DateTime dateTime, string format)
        {
            try
            {
                return dateTime.ToString(format);
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static string ConvertDuration(int Duration)
        {
            try
            {
                double hou, min;
                string duration = "0h 0m";

                hou = (Duration - Duration % 60) / 60;
                min = (Duration - hou * 60);

                duration = hou.ToString() + "h " + min.ToString() + "m";
                return duration;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public static string CovertMinuteToHours(int minutes)
        {
            string hours = null;
            try
            {
                TimeSpan result = TimeSpan.FromMinutes(minutes);
                hours = result.Hours.ToString() + "h " + result.Minutes.ToString() + "m";
            }
            catch (Exception)
            {

                throw;
            }
            return hours;
        }
        public static long NEWGUID()
        {
            //// ex: 8a847645-8cac-422c-962a-fdf3aa220065            
            //Guid g = Guid.NewGuid();
            //long value = BitConverter.ToInt64(g.ToByteArray());
            ////var ho= (6666666666666666666 - 6000000000000000000 - 1)* value + 6000000000000000000;
            // value = Convert.ToInt64("6"+ value.ToString().Substring(0, 18));
            //return value;
            long m_strRow = 0;
            try
            {
                Thread.Sleep(10);
                //m_strRow = long.Parse(DateTime.Now.AddYears(-1).Ticks + "1");
                Random random = new Random();
                m_strRow = 3 * (long)Math.Pow(10, 18) + random.NextInt64(0, (long)Math.Pow(10, 18));
            }
            catch
            {
                m_strRow = 0000000000000000000;
            }
            finally
            {
            }
            return m_strRow;
        }

        public static string GetCurrentDateTime(bool blnWithTime = false)
        {
            string strDay;
            string strMonth;
            string strYear;
            string strTime;
            string strDate = "";
            DateTime dateTime = GetISTCurrentDateTime();

            strDay = dateTime.Day.ToString("00");
            strMonth = dateTime.Month.ToString("00");
            strYear = dateTime.Year.ToString("0000");

            strDate = strYear + "-" + strMonth + "-" + strDay;

            strTime = dateTime.ToString("HH:mm:ss");
            if (blnWithTime)
            {
                strDate = strDate + " " + strTime;
            }

            return strDate;
        }
        public static DateTime GetISTCurrentDateTime()
        {
            try
            {
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                return dateTime;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
        public static string Encrypted(string stringToEncrypt)
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(text))
            //    {
            //        byte[] encData_byte = new byte[text.Length];
            //        encData_byte = System.Text.Encoding.UTF8.GetBytes(text);
            //        string encodedData = Convert.ToBase64String(encData_byte);
            //        return encodedData;
            //    }
            //    return "";
            //}
            //catch (Exception)
            //{
            //    return "";
            //}
            string encryptString = null;
            CryptoStream encryptedStream;

            if (string.IsNullOrEmpty(stringToEncrypt))
                return stringToEncrypt;
            else
            {
                byte[] encryptedData;
                MemoryStream dataStream;

                ICryptoTransform encryptor;

                TripleDESCryptoServiceProvider CryptographyProvider = new TripleDESCryptoServiceProvider();
                ///'Ensure that you create your own key and IV.
                ///'key and IV provided to ensure that sample works
                CryptographyProvider.Key = new byte[] { 111, 222, 121, 82, 172, 21, 185, 152, 228, 182, 72, 132, 123, 123, 131, 12 };
                CryptographyProvider.IV = new byte[] { 172, 223, 13, 42, 252, 102, 81, 211 };
                encryptor = CryptographyProvider.CreateEncryptor();

                dataStream = new MemoryStream();

                try
                {
                    // Create the encrypted stream
                    encryptedStream = new CryptoStream(dataStream, encryptor, CryptoStreamMode.Write);

                    StreamWriter theWriter;

                    // Write the string to memory via the encryption algorithm
                    theWriter = new StreamWriter(encryptedStream);
                    // Write the string to the memory stream
                    theWriter.Write(stringToEncrypt);

                    // End the writing
                    theWriter.Flush();
                    encryptedStream.FlushFinalBlock();

                    // Position back at start
                    dataStream.Position = 0;

                    // Create area for data
                    encryptedData = new byte[System.Convert.ToInt32(dataStream.Length) + 1];

                    // Read data from memory
                    dataStream.Read(encryptedData, 0, System.Convert.ToInt32(dataStream.Length));

                    theWriter.Close();

                    encryptedStream.Close();

                    // Convert to String
                    encryptString = Convert.ToBase64String(encryptedData, 0, encryptedData.Length);
                }
                finally
                {
                    dataStream.Close();
                }
                return encryptString;
            }
        }
        public static string Decrypted(string stringToDecrypt)
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(encryptText))
            //    {
            //        UTF8Encoding encoder = new UTF8Encoding();
            //        Decoder utf8Decode = encoder.GetDecoder();
            //        byte[] todecode_byte = Convert.FromBase64String(encryptText);
            //        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            //        char[] decoded_char = new char[charCount];
            //        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            //        string result = new String(decoded_char);
            //        return result;
            //    }
            //    return null;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}

            if (string.IsNullOrEmpty(stringToDecrypt))
                return stringToDecrypt;
            else
            {
                byte[] encryptedData;
                MemoryStream dataStream;
                CryptoStream encryptedStream;
                int intLength;
                stringToDecrypt = stringToDecrypt.Replace(" ", "+");
                // Get the byte data
                encryptedData = Convert.FromBase64String(stringToDecrypt);

                dataStream = new MemoryStream();

                try
                {

                    // Create decryptor and stream
                    ICryptoTransform decryptor;

                    TripleDESCryptoServiceProvider CryptographyProvider = new TripleDESCryptoServiceProvider();
                    ///'Ensure that you create your own key and IV.
                    ///'key and IV provided to ensure that sample works
                    CryptographyProvider.Key = new byte[] { 111, 222, 121, 82, 172, 21, 185, 152, 228, 182, 72, 132, 123, 123, 131, 12 };
                    CryptographyProvider.IV = new byte[] { 172, 223, 13, 42, 252, 102, 81, 211 };
                    decryptor = CryptographyProvider.CreateDecryptor();
                    encryptedStream = new CryptoStream(dataStream, decryptor, CryptoStreamMode.Write);

                    // Write the decrypted data to the memory stream
                    encryptedStream.Write(encryptedData, 0, encryptedData.Length - 1);
                    encryptedStream.FlushFinalBlock();

                    // Position back at start
                    dataStream.Position = 0;

                    // Determine length of decrypted string
                    intLength = System.Convert.ToInt32(dataStream.Length);

                    // Create area for data
                    encryptedData = new byte[intLength - 1 + 1];

                    // Read decrypted data to byte()
                    dataStream.Read(encryptedData, 0, intLength);

                    // Construct string from byte()
                    string retStr = "";

                    int i;
                    for (i = 0; i <= intLength - 1; i++)
                        retStr += (char)encryptedData[i];

                    encryptedStream.Close();

                    return retStr;
                }
                finally
                {
                    dataStream.Close();
                }
            }
        }
        public static string NewNum()
        {
            try
            {
                return "LC/" + DateTime.Now.ToString("yyyMMddhhmmssff");
            }
            catch (Exception)
            {

                return null;
            }

        }
        public static string DateDiff(DateTime startDate, DateTime endDate)
        {
            try
            {
                double diff2 = (endDate.Date - startDate.Date).TotalDays;
                string dayDiff = null;
                if (diff2 > 0)
                {
                    dayDiff = "+" + diff2.ToString() + " Day";
                }
                else
                {
                    dayDiff = null;
                }
                return dayDiff;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public static string DateDiffToolTrip(DateTime startDate, DateTime endDate)
        {
            try
            {
                double diff2 = (endDate.Date - startDate.Date).TotalDays;
                string dayDiff = null;
                if (diff2 > 0)
                {
                    dayDiff = endDate.ToString("ddd-MMM-dd");
                }
                else
                {
                    dayDiff = null;
                }
                return dayDiff;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public static string GetDateYyyyMmDd(string p_date)
        {
            string m_str_date = null;

            try
            {
                DateTime dt = Convert.ToDateTime(p_date);
                m_str_date = dt.ToString("yyyy-MM-ddTHH:mm:ss");

            }
            catch
            {
                m_str_date = null;
            }
            finally
            {
            }

            return m_str_date;
        }
        public static string getDBDateFormat(string objDate, bool blnTime = false)
        {
            try
            {
                DateTime sDate;
                string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
                                "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "MM/dd/yyyy","dd-MM-yyyy HH:mm:ss","dd-MM-yyyyTHH:mm:ss","MM/dd/yyyy HH:mm:ss",
                                    "yyyy-MM-dd HH:mm:ss","yyyy-MM-ddTHH:mm:ss"};
                DateTime.TryParseExact(objDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out sDate);
                if (blnTime == true)
                {
                    return sDate.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return sDate.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception)
            {
                return "0000000";
            }

        }
        public static string ConvertDOBFormat(string dt)
        {
            try
            {
                if (dt != null)
                {
                    DateTime dtFrm = Convert.ToDateTime(dt);
                    return dtFrm.ToString("dd-MM-yyyy");
                }
                else
                    return null;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static string NewBookingNum()
        {
            try
            {
                return DateTime.Now.ToString("yyyMMddhhmmssff");
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static string GetLocalIPAddress()
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public static string GenerateRandomOTP(int iOTPLength)
        {
            try
            {
                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                string sOTP = String.Empty;

                string sTempChars = String.Empty;

                Random rand = new Random();

                for (int i = 0; i < iOTPLength; i++)
                {
                    int p = rand.Next(0, saAllowedCharacters.Length);

                    sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                    sOTP += sTempChars;

                }

                return sOTP;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static string RandomPassword(int passwordLength)
        {
            try
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*()-+=?><";
                return new string(Enumerable.Repeat(chars, passwordLength).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            catch (Exception)
            {

                return null;
            }

        }
        public static string TextFileRead(string FullPath)
        {
            try
            {
                string text = null;
                if (!string.IsNullOrEmpty(FullPath))
                {
                    text = File.ReadAllText(FullPath);
                }
                return text;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string checkDBNull(string strtext)
        {
            try
            {
                if (strtext != null)
                {
                    return strtext;
                }
                else
                    return "";
            }
            catch (Exception)
            {

                return "";
            }
        }
        public static object checkDBNull(object value, bool? returnNotNull = false)
        {
            try
            {
                if (value == null)
                {
                    if ((bool)returnNotNull)
                    {
                        return "";
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                    return value;
            }
            catch (Exception)
            {
                if ((bool)returnNotNull)
                {
                    return "";
                }
                else
                    return null;
            }
        }
        public static object checkNumeric(object value)
        {
            try
            {
                if (value != null)
                {
                    return value;
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static object checkNumeric(object value, bool? convertToInt = false)
        {
            try
            {
                if (value != null)
                {
                    if ((bool)convertToInt)
                    {
                        return Convert.ToInt32(value);
                    }
                    else
                    {
                        return value;
                    }
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static decimal CheckDecimal(object value)
        {
            try
            {
                if (value != null)
                    return Convert.ToDecimal(value);
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static long CheckInt64(object value)
        {
            try
            {
                if (value != null)
                {
                    return Convert.ToInt64(value);
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static decimal RoundDownToNearest(decimal value, int? nearestValue = 50)
        {
            try
            {
                decimal rnd = Math.Floor(value / nearestValue.Value) * nearestValue.Value;
                return rnd;

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static decimal StringToDecimal(string value)
        {
            try
            {
                if (value != null)
                {
                    value = Regex.Replace(value, "[^0-9.]", "");
                    return Convert.ToDecimal(value);
                }
                else
                {
                    return new decimal();
                }
            }
            catch (Exception)
            {
                return new decimal();
            }
        }
        public enum PaxType
        {
            Adult = 1,
            Child = 2,
            Infant = 3
        }
        public enum JourneyType
        {
            Oneway = 1,
            Return = 2,
            MultiCity = 3
        }
        public enum ContactType
        {
            User = 1,
            Agent = 2,
            Admin = 3
        }
        public enum Status
        {
            BookingFaild = 0,
            Ticketed = 1,
            TicketNotSaved = 2,
            TicketNotCreated = 3,
            TicketNotAllowed = 4,
            TicketInProgress = 5,
            TicketAlreadyCreated = 6,
            Booked = 7,
            TicketCancelInProgress = 8,
            PaymentSuccess = 10,
            PaymentProgress = 11,
            PaymentProcessing = 12,
            PaymentFail = 13,
            FundRefund = 14,
            TicketCancelled = 99
        }
    }
    [Serializable]
    public static class LinqHelper
    {
        public static IEnumerable<TSource> l_DistinctBy<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        public static List<t> GetRandomElements<t>(List<t> list, int elementsCount)
        {
            return list.OrderBy(x => Guid.NewGuid()).Take(elementsCount).ToList();
        }
        public static object GetProperty<T>(this T entity, string propertyName) where T : class, new()
        {
            try
            {
                Type type = entity.GetType();
                PropertyInfo propertyInfo = type.GetProperty(propertyName);
                object value = propertyInfo.GetValue(entity);
                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void DetachEntity<T>(this DbContext dbContext, T entity, string propertyName) where T : class, new()
        {
            try
            {
                var dbEntity = dbContext.Find<T>(entity.GetProperty(propertyName));
                if (dbEntity != null)
                    dbContext.Entry(dbEntity).State = EntityState.Detached;
                dbContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataTable CreateDataTable<T>(List<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public static DataTable CreateDataTable<T>(T model)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }
            object[] values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(model);
            }

            dataTable.Rows.Add(values);

            return dataTable;
        }
        public static String ToCamelCase(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string strCamelCaseValue = null;
                    var list = new List<string>();
                    var arr = value.Split(" ");
                    foreach (var str in arr)
                    {
                        list.Add(char.ToUpper(str[0]) + str.Substring(1));
                    }
                    strCamelCaseValue = string.Join(' ', list);
                    return strCamelCaseValue;
                }
                else
                {
                    return value;
                }
            }
            catch (Exception)
            {
                return value;
            }
        }
        public static String ToSentenceCase(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // start by converting entire string to lower case
                    var lowerCase = value.ToLower();
                    // matches the first sentence of a string, as well as subsequent sentences
                    var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
                    // MatchEvaluator delegate defines replacement of setence starts to uppercase
                    var result = r.Replace(lowerCase, s => s.Value.ToUpper());
                    return result;
                }
                else
                {
                    return value;
                }
            }
            catch (Exception)
            {
                return value;
            }

        }
        public static String GetFirstName(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    return value.Split(' ').FirstOrDefault();
                }
                else
                {
                    return value;
                }
            }
            catch (Exception)
            {
                return value;
            }
        }
    }
}
