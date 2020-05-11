﻿using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBusiness : ILabelBusiness
    {
        private readonly ILabelRepository _labelRepository;

        public LabelBusiness(ILabelRepository labelRepository)
        {
            _labelRepository = labelRepository;
        }
        public LabelResponseData CreateLabel(int userID, LabelRequest labelRequest)
        {
            LabelResponseData responseData = _labelRepository.CreateLabel(userID, labelRequest);
            return responseData;
        }
    }
}
