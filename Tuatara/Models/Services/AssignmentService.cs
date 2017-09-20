using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Tuatara.Data.Repositories;
using Tuatara.Data.Models;

namespace Tuatara.Models.Services
{
    public class AssignmentService : BaseService
    {
        IAssignmentRepository _repository;
        IMapper _mapper;
        
        /// <summary>
        /// Automapper DI by Unity
        /// </summary>
        /// <param name="mapper"></param>
        public AssignmentService(IMapper mapper, IAssignmentRepository repository)
            : base()
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<PlaybookRowDto> GetAllAssignmentsPerWeek(int weekID)
        {
            var result = new List<PlaybookRowDto>();
            var data = _repository.Query(a => a.WhenID == weekID);
            foreach (var item in data)
            {
                var row = new PlaybookRowDto
                {
                    Description = item.Description,
                    Duration = item.Duration,
                    ID = item.ID,
                    Intraweek = item.Intraweek,
                    Priority = item.Priority,
                    ProjectID = item.WhatID,
                    ResourceID = item.ResourceID,
                    Status = item.Status,
                    Project = item.What.ProjectName,
                    Resource = item.Resource.Name,
                    RequestorName = item.Requestor.Name
                };

                result.Add(row);
            }
            return result;
        }

        protected override void DisposeDisposables()
        {
            _repository.Dispose();
        }
    }
}