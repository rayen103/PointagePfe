using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CST.LePoint.Tools
{
    public static class SysHelper
    {
        public const decimal TimFisc = 0.5m;

        public static DateTime DATE_INFINIE = new DateTime(2100, 1, 1);

        public static string ToSqlString(this object chaine)
        {
            if (chaine == null)
                chaine = string.Empty;
            string str = chaine.ToString();
            str = str.Replace("'", "''");

            return str;

        }

        /// <summary>
        /// Retour à la ligne 
        /// </summary>
        /// <returns></returns>
        public static string RetourChariot(string chaine)
        {
            return (chaine + "\n");
        }

        public static string ToCrystalReportFormula(this string str)
        {
            if (str == null)
                str = string.Empty;
            str = String.Format("'{0}'", str.Replace("'", "''"));

            return str;
        }

        public static bool EmailIsValid(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }
            Regex regex = new Regex(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})", RegexOptions.Compiled);
            return regex.IsMatch(email);
        }

        public static int ToInt(object val)
        {
            int myInt = 0;
            if ((val == DBNull.Value) || (val == null))
            {
                val = 0;
                return 0;
            }

            if (string.IsNullOrEmpty(val.ToString()))
            {
                val = 0;
                return 0;
            }

            bool bIntValide = Int32.TryParse(val.ToString(), out myInt);
            if (bIntValide)
                return myInt;
            else
                return 0;

            //return Convert.ToInt32(val == DBNull.Value || String.IsNullOrEmpty(val == null ? "" : val.ToString()) ? 0 : val);
        }

        public static decimal ToDecimal(object val)
        {
            return Convert.ToDecimal(val == DBNull.Value || String.IsNullOrEmpty(val == null ? "" : val.ToString()) ? 0 : val);
        }

        public static DateTime ToDateTime(object val)
        {
            DateTime dt;
            DateTime.TryParse(val == null ? "" : val.ToString(), out dt);
            return dt;
        }

        public static string ToSqlDatetime(object p)
        {
            string str = string.Empty;
            DateTime dt;
            if (p != null)
            {
                DateTime.TryParse(p.ToString(), out dt);
                str = " CONVERT(DATETIME, '" + dt.ToString("dd/MM/yyyy HH:mm:ss") + "',103)";
            }
            else
                str = " CONVERT(DATETIME, Null ,103)";

            return str;
        }
        public static string ToSqlDate(object p)
        {
            string str = string.Empty;
            DateTime dt;
            if (p != null)
            {
                DateTime.TryParse(p.ToString(), out dt);
                str = " CONVERT(DATE, '" + dt.ToString("dd/MM/yyyy") + "',103)";
            }
            else
                str = " CONVERT(DATE, Null ,103)";

            return str;
        }

        public static string ToString(object val)
        {
            return (val == null || val == DBNull.Value) ? null : val.ToString();
        }

        public static string FileNameValide(string nomFichier, string replacement = "_")
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = String.Format(@"[{0}]+", invalidChars);
            return Regex.Replace(nomFichier, invalidReStr, replacement);
        }

        public static string XmlLayoutFileName(Form form, int IdUser)
        {
            return "XMLLayout" + form.Name + "_User" + IdUser + ".xml";
        }

        public static string ConvertQteToLetters(decimal nbr, string libUnité = "")
        {
            var strParts = nbr.ToString().Split(',');
            if (strParts.Length == 2)
            {
                string decPart = strParts[1];
                if (decPart.Length <= 3)
                {
                    int len = decPart.Length;
                    for (int i = 0; i < 3 - len; i++)
                        decPart += "0";
                }
                else if (decPart.Length > 3)
                {
                    decPart = decPart.Substring(0, 4);
                    decPart = decPart.Insert(3, ",");
                }
                long decValue = (long)Math.Round(Decimal.Parse(decPart));
                return ConvertNumberToLetters((long)nbr) + libUnité + ConvertNumberToLetters(decValue);
            }
            else
            {
                return ConvertNumberToLetters((long)nbr) + libUnité;
            }
        }

        public static string ConvertNumberToLetters(double nombre)
        {
            var strParts = nombre.ToString().Split(',');
            if (strParts.Length == 2)
            {
                var strZeros = "";
                int nbrZeros = strParts[1].TakeWhile(c => c == '0').Count();
                for (int i = 0; i < nbrZeros; i++)
                    strZeros += "zéro ";

                decimal d = (decimal)nombre;
                long wholePart = (long)d;
                decimal decPartDecimal = d - wholePart;
                long decPart = (long)(decPartDecimal * (decimal)Math.Round(Math.Pow(10, decPartDecimal.ToString().Length - 2)));
                return ConvertNumberToLetters(wholePart) + " virgule " + strZeros + ConvertNumberToLetters(decPart);
            }
            else
                return ConvertNumberToLetters((int)nombre);
        }

        public static string ConvertNumberToLetters(long chiffre)
        {
            long centaine, dizaine, unite, reste, y;
            bool dix = false;
            string lettre = "";
            //strcpy(lettre, "");
            reste = chiffre / 1;
            for (int i = 1000000000; i >= 1; i /= 1000)
            {
                y = reste / i;
                if (y != 0)
                {
                    centaine = y / 100;
                    dizaine = (y - centaine * 100) / 10;
                    unite = y - (centaine * 100) - (dizaine * 10);
                    switch (centaine)
                    {
                        case 0:
                            break;

                        case 1:
                            lettre += "Cent ";
                            break;

                        case 2:
                            if ((dizaine == 0) && (unite == 0)) lettre += "Deux Cents ";
                            else lettre += "Deux Cent ";
                            break;

                        case 3:
                            if ((dizaine == 0) && (unite == 0)) lettre += "Trois Cents ";
                            else lettre += "Trois Cent ";
                            break;

                        case 4:
                            if ((dizaine == 0) && (unite == 0)) lettre += "Quatre Cents ";
                            else lettre += "Quatre Cent ";
                            break;

                        case 5:
                            if ((dizaine == 0) && (unite == 0)) lettre += "Cinq Cents ";
                            else lettre += "Cinq Cent ";
                            break;

                        case 6:
                            if ((dizaine == 0) && (unite == 0)) lettre += "Six Cents ";
                            else lettre += "Six Cent ";
                            break;

                        case 7:
                            if ((dizaine == 0) && (unite == 0)) lettre += "Sept Cents ";
                            else lettre += "Sept Cent ";
                            break;

                        case 8:
                            if ((dizaine == 0) && (unite == 0)) lettre += "Huit Cents ";
                            else lettre += "Huit Cent ";
                            break;

                        case 9:
                            if ((dizaine == 0) && (unite == 0)) lettre += "Neuf Cents ";
                            else lettre += "Neuf Cent ";
                            break;
                    }// endSwitch(centaine)
                    switch (dizaine)
                    {
                        case 0:
                            break;

                        case 1:
                            dix = true;
                            break;

                        case 2:
                            lettre += "Vingt ";
                            break;

                        case 3:
                            lettre += "Trente ";
                            break;

                        case 4:
                            lettre += "Quarante ";
                            break;

                        case 5:
                            lettre += "Cinquante ";
                            break;

                        case 6:
                            lettre += "Soixante ";
                            break;

                        case 7:
                            dix = true;
                            lettre += "Soixante ";
                            break;

                        case 8:
                            lettre += "Quatre-vingt ";
                            break;

                        case 9:
                            dix = true;
                            lettre += "Quatre-vingt ";
                            break;
                    } // endSwitch(dizaine)
                    switch (unite)
                    {
                        case 0:
                            if (dix) lettre += "Dix ";
                            break;

                        case 1:
                            if (dix) lettre += "Onze ";
                            else lettre += "Un ";
                            break;

                        case 2:
                            if (dix) lettre += "Douze ";
                            else lettre += "Deux ";
                            break;

                        case 3:
                            if (dix) lettre += "Treize ";
                            else lettre += "Trois ";
                            break;

                        case 4:
                            if (dix) lettre += "Quatorze ";
                            else lettre += "Quatre ";
                            break;

                        case 5:
                            if (dix) lettre += "Quinze ";
                            else lettre += "Cinq ";
                            break;

                        case 6:
                            if (dix) lettre += "Seize ";
                            else lettre += "Six ";
                            break;

                        case 7:
                            if (dix) lettre += "Dix-sept ";
                            else lettre += "Sept ";
                            break;

                        case 8:
                            if (dix) lettre += "Dix-huit ";
                            else lettre += "Huit ";
                            break;

                        case 9:
                            if (dix) lettre += "Dix-neuf ";
                            else lettre += "Neuf ";
                            break;
                    } // endSwitch(unite)
                    switch (i)
                    {
                        case 1000000000:
                            if (y > 1) lettre += "Milliards ";
                            else lettre += "Milliard ";
                            break;

                        case 1000000:
                            if (y > 1) lettre += "Millions ";
                            else lettre += "Million ";
                            break;

                        case 1000:
                            lettre += "Mille ";
                            break;
                    }
                } // end if(y!=0)
                reste -= y * i;
                dix = false;
            } // end for
            if (lettre.Length == 0) lettre += "Zero ";
            return lettre;
        }

        public static object CloneObject(object toClone)
        {
            object newObject = Activator.CreateInstance(toClone.GetType());
            PropertyInfo[] propertyInfos = toClone.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType.IsGenericType)
                {
                    if (propertyInfo.PropertyType.GetInterface("IList", true) != null)
                    {
                        IList oldList = propertyInfo.GetValue(toClone, null) as IList;
                        if (oldList != null && oldList.Count > 0 &&
                            oldList[0].GetType().GetInterface("ICloneable", true) != null)
                        {
                            IList newList = (IList)propertyInfo.GetValue(newObject, null);
                            foreach (object obj in oldList)
                            {
                                ICloneable clone = (ICloneable)obj;
                                newList.Add(clone.Clone());
                            }
                        }
                        else
                        {
                            propertyInfo.SetValue(newObject, oldList, null);
                        }
                    }
                    if (propertyInfo.PropertyType.GetInterface("IDictionary", true) != null)
                    {
                        IDictionary oldDic =
                            propertyInfo.GetValue(toClone, null) as IDictionary;
                        if (oldDic != null && oldDic.Count > 0 &&
                            oldDic[0].GetType().GetInterface("ICloneable", true) != null)
                        {
                            IDictionary newDic =
                                (IDictionary)propertyInfo.GetValue(newObject, null);
                            foreach (DictionaryEntry entry in oldDic)
                            {
                                ICloneable clone = (ICloneable)entry.Value;
                                newDic[entry.Key] = clone.Clone();
                            }
                        }
                        else
                        {
                            propertyInfo.SetValue(newObject, oldDic, null);
                        }
                    }
                }
                else
                {
                    //Clone IClonable object
                    if (propertyInfo.GetType().GetInterface("ICloneable", true) != null)
                    {
                        ICloneable clone = (ICloneable)propertyInfo.GetValue(toClone, null);
                        propertyInfo.SetValue(newObject, clone.Clone(), null);
                    }
                    else
                    {
                        propertyInfo.SetValue(
                            newObject, propertyInfo.GetValue(toClone, null), null);
                    }
                }
            }
            return newObject;
        }

        public static void RemoveWhere<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (predicate(item))
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable(typeof(T).Name);
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                //Modifié le 21-04-2015 pour détourner l'erreur de type nullable
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
                //table.Columns.Add(prop.Name, prop.PropertyType);
                //***Fin Modification
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static string CalculateSHA1(string text, Encoding enc = null)
        {
            enc = enc ?? Encoding.Default;
            byte[] buffer = enc.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "").ToUpper();
        }

        /// <summary>
        /// Returns the index of an item in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence containing elements.</param>
        /// <param name="item">The item to locate.</param>
        /// <returns>The index of the entry if it was found in the sequence; otherwise, -1.</returns>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource item)
        {
            return IndexOf(source, item, null);
        }

        // This provides a useful extension-like method to find the index of and item from IEnumerable<T>
        // This was based off of the Enumerable.Count<T> extension method.

        /// <summary>
        /// Returns the index of an item in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence containing elements.</param>
        /// <param name="item">The item to locate.</param>
        /// <param name="itemComparer">The item equality comparer to use. Pass null to use the default comparer.</param>
        /// <returns>The index of the entry if it was found in the sequence; otherwise, -1.</returns>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource item, IEqualityComparer<TSource> itemComparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            IList<TSource> listOfT = source as IList<TSource>;
            if (listOfT != null)
            {
                return listOfT.IndexOf(item);
            }

            IList list = source as IList;
            if (list != null)
            {
                return list.IndexOf(item);
            }

            if (itemComparer == null)
            {
                itemComparer = EqualityComparer<TSource>.Default;
            }

            int i = 0;
            foreach (TSource possibleItem in source)
            {
                if (itemComparer.Equals(item, possibleItem))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Recuperer le nom de lma machine et l'adresse MAC
        /// </summary>
        /// <returns></returns>
        public static string NomCompletPC()
        {
            string nomMachine = string.Empty;
            string adresseMac = string.Empty;
            nomMachine = System.Environment.MachineName;
            //TODO: Créer le traitement de receuperation adresseMac de la machine

            return nomMachine + '-' + adresseMac;
        }

        public enum MoisSansAccent
        {
            Janvier = 1,
            Fevrier = 2,
            Mars = 3,
            Avril = 4,
            Mai = 5,
            Juin = 6,
            Juillet = 7,
            Aout = 8,
            Septembre = 9,
            Octobre = 10,
            Novembre = 11,
            Decembre = 12
        };

        public enum MoisAvecAccent
        {
            Janvier = 1,
            Février = 2,
            Mars = 3,
            Avril = 4,
            Mai = 5,
            Juin = 6,
            Juillet = 7,
            Août = 8,
            Septembre = 9,
            Octobre = 10,
            Novembre = 11,
            Décembre = 12
        };

    }
}