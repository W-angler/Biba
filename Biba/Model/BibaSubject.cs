using System.Collections.Generic;

namespace Biba.Model
{
    /// <summary>
    /// Biba主体
    /// </summary>
    public class BibaSubject
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        
        public bool CanExecutable(BibaObject obj)
        {
            return Level <= obj.Level;
        }
        public bool CanRead(BibaObject obj)
        {
            return Level <= obj.Level;
        }
        public bool CanWrite(BibaObject obj)
        {
            return obj.Level <= Level;
        }
        public bool CanWrite(int level)
        {
            return level <= Level;
        }
    }
}
