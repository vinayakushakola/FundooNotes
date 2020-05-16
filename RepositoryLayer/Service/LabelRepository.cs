﻿using CommonLayer.Models;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using RepositoryLayer.ApplicationDbContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

        public List<LabelResponseData> GetAllLabels()
        {
            try
            {
                List<LabelResponseData> labels = _context.Labels.
                    Where(label => label.LabelID > 0).
                    Select(label => new LabelResponseData
                    {
                        LabelID = label.LabelID,
                        LabelName = label.LabelName,
                    }).
                    ToList();
                return labels;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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


        public LabelResponseData EditLabel(int userID, int labelID, UpdateLabelRequest updateRequest)
        {
            try
            {

                var labelData = _context.Labels.
                                        Where(label => label.UserID == userID && label.LabelID == labelID).
                                        First<LabelInfo>();
                labelData.LabelName = updateRequest.LabelName;
                _context.SaveChanges();

                LabelResponseData responseData = new LabelResponseData()
                {
                    LabelID = labelData.LabelID,
                    LabelName = labelData.LabelName
                };
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool DeleteLabel(int userID, int labelID)
        {
            try
            {
                var labelData = _context.Labels.
                    Where(label => label.UserID == userID && label.LabelID == labelID).
                    FirstOrDefault<LabelInfo>();
                _context.Remove(labelData);
                return true;
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
