using Biba.Model;

namespace Biba.Util
{
    public class ConfigUtil
    {
        #region Object
        public static bool ObjectExists(Ini ini, string name)
        {
            return ini.GetSections().Contains(name);
        }
        public static BibaObject ReadObject(Ini ini, string name)
        {
            return new BibaObject()
            {
                Name = name,
                Value = ini.GetValue("value", name),
                Level = int.Parse(ini.GetValue("level", name))
            };
        }
        public static void WriteObject(Ini ini, BibaObject obj)
        {
            ini.WriteValue("value", obj.Value, obj.Name);
            ini.WriteValue("level", obj.Level.ToString(), obj.Name);
        }
        public static void DeleteObject(Ini ini, BibaObject obj)
        {
            ini.Delete(obj.Name);
        }
        #endregion

        #region Subject
        public static bool SubjectExists(Ini ini, string name)
        {
            return ini.GetSections().Contains(name);
        }
        public static BibaSubject ReadSubject(Ini ini, string name)
        {
            return new BibaSubject()
            {
                Name = name,
                Password = ini.GetValue("password", name),
                Level = int.Parse(ini.GetValue("level", name))
            };
        }
        public static void WriteSubject(Ini ini, BibaSubject subject)
        {
            ini.WriteValue("password", subject.Password, subject.Name);
            ini.WriteValue("level", subject.Level.ToString(), subject.Name);
        }
        #endregion
    }
}
