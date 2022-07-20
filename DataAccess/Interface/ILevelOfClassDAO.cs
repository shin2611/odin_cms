using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interface
{
    public interface ILevelOfClassDAO
    {
        ResponseStatus InsertUpdateLevelOfClass(LevelOfClass levelOfClass);
    }
}
