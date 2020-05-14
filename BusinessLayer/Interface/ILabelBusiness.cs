using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;

namespace BusinessLayer.Interface
{
    public interface ILabelBusiness
    {
        LabelResponseData CreateLabel(int userID, LabelRequest labelRequest);

        LabelResponseData EditLabel(int userID, int labelID, UpdateLabelRequest updateRequest);

        bool DeleteLabel(int userID, int labelID);
    }
}
