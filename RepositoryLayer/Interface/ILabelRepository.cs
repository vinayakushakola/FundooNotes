using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        LabelResponseData CreateLabel(int userID, LabelRequest label);

        LabelResponseData EditLabel(int userID, int labelID, UpdateLabelRequest label);
    }
}
