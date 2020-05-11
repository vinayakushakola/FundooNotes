using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        LabelResponseData CreateLabel(int userID, LabelRequest label);
    }
}
