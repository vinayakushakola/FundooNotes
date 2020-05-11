using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;

namespace BusinessLayer.Interface
{
    public interface ILabelBusiness
    {
        LabelResponseData CreateLabel(int userID, LabelRequest labelRequest);
    }
}
