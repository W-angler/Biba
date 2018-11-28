using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biba.Model
{
    /// <summary>
    /// Biba模型中的权限
    /// 1. The Simple Integrity Property states that a subject at a given level of integrity must not read data at a lower integrity level (read up).
    /// 2. The* (star) Integrity Property states that a subject at a given level of integrity must not write to data at a higher level of integrity(write down).
    /// 3. Invocation Property states that a process from below cannot request higher access; only with subjects at an equal or lower level.
    /// </summary>
    public enum Permission
    {
        READ,//读
        WRITE,//写
        EXE//执行
    }
}
