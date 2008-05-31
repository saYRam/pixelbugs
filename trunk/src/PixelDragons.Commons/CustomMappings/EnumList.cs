using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace PixelDragons.Commons.CustomMappings
{
    public class EnumList<T> : IUserType
    {
        private int _fieldSize = 1000;

        public EnumList()
        {
        }

        public EnumList(int fieldSize)
        {
            _fieldSize = fieldSize;
        }
        
        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            List<T> enums = new List<T>();

            string enumsAsString = NHibernateUtil.String.NullSafeGet(rs, names[0]) as string;

            if (enumsAsString != null)
            {
                string[] split = enumsAsString.Split(',');
                foreach (string e in split)
                {
                    try
                    {
                        enums.Add((T)Enum.Parse(typeof(T), e, true));
                    }
                    catch (Exception)
                    {
                        //Can't find a matching enum enum.
                        //Just ignore this one as we might have removed this enum.
                    }
                }
            }

            return enums;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            List<T> enums = (List<T>)value;

            string enumsAsString = null;
            if (enums != null && enums.Count > 0)
            {
                enumsAsString = "";

                for (int x = 0; x < enums.Count; x++)
                {
                    if (x != 0)
                    {
                        enumsAsString += ",";
                    }
                    enumsAsString += enums[x].ToString();
                }
            }

            NHibernateUtil.String.NullSafeSet(cmd, enumsAsString, index);
        }

        public object DeepCopy(object value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                List<T> enums = ((List<T>)value);

                return new List<T>(enums.ToArray());
            }
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public new bool Equals(object x, object y)
        {
            //Compare all elements
            if (x != null && y != null)
            {
                List<T> list1 = (List<T>)x;
                List<T> list2 = (List<T>)y;

                if (list1.Count == list2.Count)
                {
                    foreach (T e in list1)
                    {
                        if (!list2.Contains(e))
                        {
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return (x == y);
        }

        public bool IsMutable
        {
            get { return false; }
        }
        
        public Type ReturnedType
        {
            get { return typeof(List<T>); }
        }

        public SqlType[] SqlTypes
        {
            get { return new SqlType[] { new SqlType(DbType.AnsiString, _fieldSize) }; }
        }


        public object Assemble(object cached, object owner)
        {
            throw new Exception("The method or operation is not implemented (enumsListType.Assemble).");
        }

        public object Disassemble(object value)
        {
            throw new Exception("The method or operation is not implemented (enumsListType.Disassemble).");
        }

        public object Replace(object original, object target, object owner)
        {
            throw new Exception("The method or operation is not implemented. (enumsListType.Replace)");
        }
    }
}
