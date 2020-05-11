using CommonLayer.Models;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using RepositoryLayer.ApplicationDbContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRepository : ILabelRepository
    {
        private readonly AppDbContext _context;

        public LabelRepository(AppDbContext context)
        {
            _context = context;
        }

        public LabelResponseData CreateLabel(int userID, LabelRequest labelRequest)
        {
            try
            {
                LabelInfo labelInfo = new LabelInfo()
                {
                    UserID = userID,
                    LabelName = labelRequest.LabelName,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                _context.Labels.Add(labelInfo);
                _context.SaveChanges();

                LabelResponseData responseData = new LabelResponseData()
                {
                    LabelID = labelInfo.LabelID,
                    LabelName = labelInfo.LabelName
                };
                return responseData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
