using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface ILabelBusiness
    {
        List<LabelResponseData> GetAllLabels();

        LabelResponseData CreateLabel(int userID, LabelRequest labelRequest);

        LabelResponseData EditLabel(int userID, int labelID, UpdateLabelRequest updateRequest);

        bool DeleteLabel(int userID, int labelID);
    }
}
