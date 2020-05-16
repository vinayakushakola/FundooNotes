using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        List<LabelResponseData> GetAllLabels();

        LabelResponseData CreateLabel(int userID, LabelRequest label);

        LabelResponseData EditLabel(int userID, int labelID, UpdateLabelRequest label);
        
        bool DeleteLabel(int userID, int labelID);
    }
}
