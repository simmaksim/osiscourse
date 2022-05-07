using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LogQuery = Interop.MSUtil.LogQueryClass;
using RegistryInputFormat = Interop.MSUtil.COMRegistryInputContextClass;
using RegRecordSet = Interop.MSUtil.ILogRecordset;

namespace ProcessesInformation
{
    public class RegisterInformation
    {
        #region Fields
        private List<string> info;
        private string subKey;
        private RegistryKey baseRegistryKey;
        #endregion

        #region Properties
        public RegistryKey BaseRegistryKey
        {
            get { return baseRegistryKey; }
            set { baseRegistryKey = value; }
        }

        public string SubKey
        {
            get { return subKey; }
            set { subKey = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Search the value in a registry and return the path.
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public List<string> GetKeyPath(string searchKey)
        {
            info = new List<string>();
            string path = string.Empty;
            try
            {
                foreach (string keyname in baseRegistryKey.OpenSubKey(subKey).GetSubKeyNames())
                {
                    try
                    {
                        using (RegistryKey key = baseRegistryKey.OpenSubKey(subKey).OpenSubKey(keyname, true))
                        {
                            foreach (var name in key.GetSubKeyNames())
                            {
                                MatchCollection coll = Regex.Matches(name.ToString(), searchKey);

                                if (coll != null)
                                {
                                    foreach (var item in coll)
                                        info.Add(baseRegistryKey.ToString() + "\\" + subKey + "\\" +  keyname + "\\" + name);
                                }
                            }
                        }
                    }
                    catch (System.Security.SecurityException)
                    {
                        continue;
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
           
            return info;
        }

        public List<string> Search(string value)
        {
            info = new List<string>();

            RegRecordSet rs = null;
            try
            {
                LogQuery qry = new LogQuery();
                RegistryInputFormat registryFormat = new RegistryInputFormat();
                string query = "SELECT Path from \\" + baseRegistryKey + "\\" + subKey + " where Value=\'" + value + "\'";
                //string query = @"SELECT Path from \HKLM\SOFTWARE\Microsoft where Value='VisualStudio'";
                rs = qry.Execute(query, registryFormat);
                for (; !rs.atEnd(); rs.moveNext())
                    info.Add(rs.getRecord().toNativeString("\n"));
            }
            catch (System.IO.FileNotFoundException ex)
            {
                throw ex;
            }
            finally
            {
                if (rs != null)
                    rs.close();
            }
            return info;
        }

        /// <summary>
        /// Read value from a registry key.
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public string ReadValue(string keyName)
        {
            RegistryKey rk = baseRegistryKey;
            RegistryKey sk = rk.OpenSubKey(subKey);

            if (sk == null)
                return null;
            else
            {
                try
                {
                    return (string)sk.GetValue(keyName.ToUpper());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Write value into a registry key.
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteValue(string keyName, object value)
        {
            try
            {
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk = rk.CreateSubKey(subKey);
                sk.SetValue(keyName.ToUpper(), value);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete a registry key.
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public bool DeleteKey(string keyName)
        {
            try
            {
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk = rk.CreateSubKey(subKey);

                if (sk == null)
                    return true;
                else
                    sk.DeleteValue(keyName);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete a sub key and any child.
        /// </summary>
        /// <returns></returns>
        public bool DeleteSubKeyTree()
        {
            try
            {
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk = rk.OpenSubKey(subKey);

                if (sk != null)
                    rk.DeleteSubKeyTree(subKey);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retrive the count of subkeys at the current key.
        /// </summary>
        /// <returns></returns>
        public int SubKeyCount()
        {
            try
            {
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk = rk.OpenSubKey(subKey);

                if (sk != null)
                    return sk.SubKeyCount;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retrive the count of values in the key.
        /// </summary>
        /// <returns></returns>
        public int ValueCount()
        {
            try
            {
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk = rk.OpenSubKey(subKey);

                if (sk != null)
                    return sk.ValueCount;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
